<template>
  <div class="h-screen w-screen ">
    <!-- Header fixo -->
    <div class="h-18 w-full space-x-2 p-3 bg-orange-500 flex items-center justify-end">

<!-- Botão de voltar-->
      <div class="absolute left-4  transform  text-white text-xl space-x-2 font-semibold flex items-center justify-center">
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" @click="$router.back()" class="size-4">
<path fill-rule="evenodd" d="M7.72 12.53a.75.75 0 0 1 0-1.06l7.5-7.5a.75.75 0 1 1 1.06 1.06L9.31 12l6.97 6.97a.75.75 0 1 1-1.06 1.06l-7.5-7.5Z" clip-rule="evenodd" />

  </svg>
  Pedidos
</div>    

 <!-- Foto do ADM -->
<div class="w-8 h-8 rounded-full bg-white text-orange-500 text-xs font-bold flex items-center justify-center">
          ADM
</div>

 <!-- Menu -->
<div class="relative mb-12 mr-8">
  <Menu/>
</div>
 
</div>
<div class="h-18 w-full space-x-2 p-4 bg-orange-200 shadow-sm text-xs text-orange-800 font-semibold">
  Faça a gestão dos pedidos do sistema, visualizando seus detalhes e atualizando seu status.
</div>

      <!-- Lista de pedidos -->
       <div class="flex items-center justify-center p-4">
      <div class="space-y-4 w-full">
        <h2 class="text-xl font-bold text-orange-600 mb-2">Lista de Pedidos</h2>
        
       <div class="flex felx-row space-x-4">

        <button
          @click="statusSelecionado = ''"
          :class="statusSelecionado === '' ? 'bg-orange-500 text-white' : 'bg-orange-200'"
          class="text-xs px-2 py-1.5 border border-gray-400 rounded-full text-orange-800">
           Todos
         </button>



          <!--Filtro por Realizados! -->
          <button  @click="statusSelecionado = true"
          :class="statusSelecionado === true ? 'bg-orange-500 text-white' : 'bg-orange-200'"
          class="text-xs px-2 py-1.5 border border-gray-400 rounded-full text-orange-800">
            Realizado
          </button>

          <!-- Filtor por Pendente -->
          <button  @click="statusSelecionado = false"
            :class="statusSelecionado === false ? 'bg-orange-500 text-white' : 'bg-orange-200'"
            class="text-xs px-2 py-1.5 border border-gray-400 rounded-full text-orange-800">
            Pendente
          </button>

        </div>
         <!-- Input de Pesquisar -->
         <div  class="text-sm text-gray-800 rounded-xl bg-white px-4 py-2 flex flex-row items-center justify-center space-x-2">
            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-4 mt-0.5">
  <path stroke-linecap="round" stroke-linejoin="round" d="m21 21-5.197-5.197m0 0A7.5 7.5 0 1 0 5.196 5.196a7.5 7.5 0 0 0 10.607 10.607Z" />
             </svg>
            <input 
             type="text" 
             placeholder="Pesquisar pedidos..."
             v-model="filtro"
             class="focus:outline-none focus:ring-0  w-full "
             >      
          </div>

        <div class="space-y-3 max-h-[550px] overflow-y-auto pr-1">
          <div
            v-for="pedido  in resultados"
            :key="pedido.id"
            class="bg-white shadow-sm rounded-xl p-4 flex justify-between items-center hover:shadow-md transition duration-200 border border-orange-100"
          >
            <div>
              <p class="text-sm text-gray-700 font-semibold">{{ pedido.pedidoId }} - {{ pedido.userName }}</p>
              <p class="text-sm text-gray-500">Valor: {{ pedido.valorPedido }}</p>
              <p class="text-xs text-gray-400">{{ String(pedido.dataPedido).replace(/\.\d+Z$/, "") }}</p>
            </div>

            <button
              @click="Realizar(pedido.pedidoId, pedido.isAceite)"
              :class="pedido.isAceite
                ? 'bg-green-600 hover:bg-green-700'
                : 'bg-red-600 hover:bg-red-700'"
              class="text-white text-sm px-3 py-1 rounded-lg shadow transition"
            >
              {{ pedido.isAceite ? 'Realizado' : 'Pendente' }}
            </button>
          </div>
        </div>
      </div>
      </div>

    <!-- Loader -->
    <div v-if="load" class="fixed inset-0 bg-black bg-opacity-20 z-50 flex items-center justify-center">
      <processo />
    </div>
  </div>
</template>



<script setup>
import { watch } from 'vue';
import Menu from './Menu.vue';
import { ref } from 'vue'
import { get } from '../../../Servicos/GetAllPedidos';
import { onMounted } from 'vue'; 
import processo from '../processo.vue';
import { putPedido } from '../../../Servicos/putPedido';
import { useRouter } from 'vue-router';
const filtro = ref('');
const resultados = ref([]);

const menuAberto = ref(false)
const router = useRouter();
const pedidos = ref([]);
const load = ref(false);
const statusSelecionado = ref(''); // '' = todos, true = realizado, false = pendente


onMounted(async () => {
      load.value = true;
      const response = await get();
      pedidos.value = response;
      resultados.value = pedidos.value; // ← Mostra todos inicialmente

      load.value = false;
});

const Realizar = async (id, isAceite) => {
  if (isAceite) {
    return;
  }
  const response = await putPedido(id);
  if (response) {
    const pedido = pedidos.value.find(p => p.pedidoId === id);
    pedido.isAceite = true;
  } else {
      alert('Erro ao realizar o pedido. Por favor, tente novamente.');
      console.error('Erro ao realizar o pedido:', response);
      router.push('/');
  }
}

// Filtro de Pedidos

let timeout = null;

watch([filtro, statusSelecionado], () => {
  clearTimeout(timeout);
  timeout = setTimeout(() => {
    const texto = filtro.value.toLowerCase();
    resultados.value = pedidos.value.filter(pedido =>
    (pedido.userName.toLowerCase().includes(texto) ||
       pedido.pedidoId.toString().includes(texto)) &&
      (statusSelecionado.value === '' || pedido.isAceite === statusSelecionado.value)
    );
  }, 300);
}, { immediate: true });

</script>