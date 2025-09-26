<template>
 <div class="h-screen w-screen fixed">
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
          VM
</div>

 <!-- Menu -->
<div class="relative mb-12 mr-8">
  <MenuAgente/>
</div>
 
</div>
<div class="h-18 w-full space-x-2 p-4 bg-orange-200 shadow-sm text-xs text-orange-800 font-semibold">
  Solicite os teus carregamentos de forma rápida e segura ao seu Agente e poderás analisar os teus pedidos Realizados e Pendentes.
</div>

    <div class="flex items-center justify-center p-4 ">
      <div class="w-full space-y-4">
           <!-- O select do pedido -->
            <div class="space-x-2 mt-12">
                <select name="valor" id="valor" class="border border-orange-600 text-sm text-gray-600 rounded-sm  py-0.5 px-2 outline-none" v-model="valor">
                        <option value="">Selecione o valor</option>
                        <option v-for="valor in [10000, 25000, 50000, 100000]" :key="valor" :value="valor">
                              {{ valor.toLocaleString('pt-PT', { style: 'currency', currency: 'AOA' }) }}
                        </option>
             </select>
                 <input 
                    type="submit"
                    value="Solicitar"
                    class="bg-orange-500 text-white text-sm rounded-sm py-0.5 px-2"
                 @click="FazerPedido"
             >
        </div>
        <!-- Meus pedidos -->
         <div>
            <p class="text-md text-orange-500 font-semibold">Meus Pedidos</p>
         </div>

         <!-- Tabela dos pedidos -->
         <div class="h-100  overflow-y-auto overflow-x-auto text-sm whitespace-nowrap">
         <table class="table-auto w-full border-collapse overflow-hidden rounded-lg  bg-white min-w-full "> 
            <thead class="text-orange-700 bg-orange-200  uppercase text-xs font-semibold tracking-wide select-none ">
                <tr>
                    <th class="text-left py-3 px-4">Valor</th>
                    <th class="text-left py-3 px-4">Data</th>
                    <th class="text-left py-3 px-4">Status</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="pedido in pedidos" :key="pedido.id" class="border-b border-orange-200">
                    <td class="p-3">{{ pedido.valorPedido.toLocaleString('pt-PT', { style: 'currency', currency: 'AOA' }) }}</td>
                    <td class="p-3">{{ new Date(pedido.dataPedido).toISOString().split('.')[0]}}</td>
                    <td class="p-3">{{ pedido.isAceite ? 'Realizado' : 'Pendente' }}</td>
                </tr>
            </tbody>
        </table>
    </div>
      </div>
    </div>

</div>




<div class="py-8 px-8">
   
    <div class="mt-4">
        
    </div>
</div>
 
</template>

<script setup>
import MenuAgente from './MenuAgente.vue';
import { onMounted, reactive, ref } from 'vue'
import { post } from '../../../Servicos/PostPedido';
import { getData } from '../../../Servicos/GetData';
import { useRouter } from 'vue-router';
import { getMyPedidos } from '../../../Servicos/GetMyPedidos';

const router = useRouter();
const pedidos = reactive([]);
//pegando o valor do select
onMounted(async () => {
    await MeusDados();
    const meusPedidos = await getMyPedidos();
    if (meusPedidos) {
        pedidos.push(...meusPedidos);
    } else {
        console.error("Falha ao obter os pedidos do usuário.");
        router.push('/');
    }
});
const valor = ref('')
let UserName = ref('')

const MeusDados = async ()=>
{
    const response = await getData();
    if (response) {
        UserName.value = response.nome;
    } else {
        console.error("Falha ao obter os dados do usuário.");
    }
}


const FazerPedido = async()=>
{
    const response = await post(valor.value);
    if (response) {
        console.log("Pedido realizado com sucesso:", response); 
        pedidos.push(response)   
    } else {
        console.error("Falha ao realizar o pedido, direcionado para o login.");
        router.push('/');
    }
}
</script>