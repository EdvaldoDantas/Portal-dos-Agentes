<template>
 <div class="h-screen w-screen fixed">

  <div class="h-18 w-full space-x-2 p-3 bg-orange-500 flex items-center justify-end">

      <!-- Botão de voltar-->
         <div class="absolute left-4  transform  text-white text-xl space-x-2 font-semibold flex items-center justify-center">
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" @click="$router.back()" class="size-4">
<path fill-rule="evenodd" d="M7.72 12.53a.75.75 0 0 1 0-1.06l7.5-7.5a.75.75 0 1 1 1.06 1.06L9.31 12l6.97 6.97a.75.75 0 1 1-1.06 1.06l-7.5-7.5Z" clip-rule="evenodd" />

  </svg>
  Agentes
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
        Faça a gestão dos agentes do sistema, visualizando seus perfis e eliminando-os se necessário.
    </div>

<!-- Inicio do Ranking -->
<div class="flex items-center justify-center p-4">
        <div class="space-y-8 w-full mt-10">
           
         
          <!-- Input de busca -->
           <div class="space-y-2">
               <div class="px-4 py-2 rounded-md space-x-2 bg-white flex">
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-4 mt-0.5">
  <path stroke-linecap="round" stroke-linejoin="round" d="m21 21-5.197-5.197m0 0A7.5 7.5 0 1 0 5.196 5.196a7.5 7.5 0 0 0 10.607 10.607Z" />
             </svg>
                  <input 
                  type="text"
                  class="text-sm outline-none"
                  placeholder="Pesquisar agentes..."
                   v-model="filtro"
                  >
               </div>

               <!-- Botão de adicionar -->
               <div class="px-4 py-2 rounded-md  flex items-center justify-center bg-blue-500 text-white">
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-4 text-white mt-0.5">
          <path stroke-linecap="round" stroke-linejoin="round" d="M12 4.5v15m7.5-7.5h-15"/>
              </svg>
                  <input 
                  type="button"
                  class="text-sm outline-none"
                  value="Adicionar"
                   @click="Adicionar()"
                  >
              </div>
           </div>
           <!-- A tabela -->

  <div class="h-100  overflow-y-auto overflow-x-auto text-sm whitespace-nowrap">
  <table class="table-auto w-full border-collapse rounded-lg overflow-hidden bg-white min-w-full">
    <thead class="text-orange-700 bg-orange-200 uppercase text-xs font-semibold tracking-wide select-none ">
      <tr>
        <th class="text-left py-3 px-4 text-xs">Id</th>
        <th class="text-left py-3 px-4 text-xs">Nome</th>
      </tr>
    </thead>
      <tbody>
        <tr v-for="agente in resultados" :key="agente.id" @click="abrirPerfil(agente)" :class="[
        'cursor-pointer hover:opacity-80 border-b border-orange-100',
        
      ]">
       
          <td class="p-3 flex text-sm flex-row items-center ">
              <div class="w-7 h-7 rounded-full bg-orange-400 text-white mr-4 flex items-center justify-center p-3">
                 {{ getIniciais(agente.nome) }}
              </div> 
          {{ agente.id }} 
         </td>
          <td class="p-3 text-sm">{{ agente.nome }}</td>
        </tr>
      
      </tbody>
  </table>

  
 </div> 


      </div>
   </div>

    
   
    <!-- <div class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-20 z-50" v-if="load">
      <processo v-if="load" />
    </div> -->
  </div>
</template>

<script setup>
import { watch } from 'vue';
import processo from '../processo.vue'
import { ref } from 'vue'
import verPerfil from './verPerfilAdm.vue'
import Menu from './Menu.vue'
import { getAllUsers } from '../../../Servicos/GetAllUsers'
import { onMounted } from 'vue'
import router from '@/router';
const aparecerPerfil = ref(false)
const agenteSelecionado = ref(null)
const filtro = ref('');
const resultados = ref([]);
const agentes = ref([])






let load = ref(false)
onMounted(async() => {
  load.value = true
  await getAllUsers()
  agentes.value = await getAllUsers()
  resultados.value = agentes.value; // ← Mostra todos inicialmente
  load.value = false
})

function abrirPerfil(agente) {
  router.push({
    name: 'verPerfilAdm',
    params: {
      agente: JSON.stringify(agente) // Serializa o objeto agente
    }
  })
}

function getIniciais(nome) {
  const partes = nome.trim().split(" ");
  return partes[0][0] + (partes[1]?.[0] || '');
}

// Filtro de agentes
let timeout = null;

watch(filtro, (novoValor) => {
  clearTimeout(timeout);
  timeout = setTimeout(() => {
    const texto = novoValor.toLowerCase();
    resultados.value = agentes.value.filter(agente =>
      agente.nome.toLowerCase().includes(texto) ||
      agente.email.toLowerCase().includes(texto) ||
      agente.id.toString().includes(texto)
    );
  }, 300); // 300ms de atraso
}, { immediate: true });


// Adicionar
function Adicionar() {
  router.push('/adicionarAgentes')
}

</script>


<!-- A tabela -->
<!-- 



          <!-- Input de Pesquisar -->
          <!-- <div  class="text-sm text-gray-800 rounded-xl bg-white px-4 py-2 flex flex-row items-center justify-center space-x-2">
         
            <input 
             type="text" 
             placeholder="Pesquisar usuários..."
             class="focus:outline-none focus:ring-0  w-full "
             >
             
          </div> -->
          
          <!-- Botão adicionar novo usuario -->
           
          <!-- <div class="text-sm text-white rounded-md px-2 py-2 flex flex-row items-center justify-center space-x-2 font-bold">
            
          
            <input 
            type="button" 
             value="Usuário"
            
           >
           
        </div> -->