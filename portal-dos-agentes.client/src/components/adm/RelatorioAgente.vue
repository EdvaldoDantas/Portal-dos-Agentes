<template>
    <div class="h-screen w-screen fixed">
      <div class="h-18 w-full space-x-2 p-3 bg-orange-500 flex items-center justify-end">

<!-- Botão de voltar-->
          <div class="absolute left-4  transform  text-white text-xl space-x-2 font-semibold flex items-center justify-center">
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" @click="$router.back()" class="size-4">
<path fill-rule="evenodd" d="M7.72 12.53a.75.75 0 0 1 0-1.06l7.5-7.5a.75.75 0 1 1 1.06 1.06L9.31 12l6.97 6.97a.75.75 0 1 1-1.06 1.06l-7.5-7.5Z" clip-rule="evenodd" />

  </svg>
  Relatório do Agente
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
Veja os relatórios de todos os agentes do sistema, filtrando por mês e dia para uma análise mais detalhada.
</div>

         <div class="flex items-center justify-center p-4">
            <div class="space-y-4 w-full mt-2">
                <h2 class="text-xl font-bold text-orange-600 mb-2 whitespace-nowrap flex items-start justify-start">Relatório</h2>

              <div class="flex flex-row  rounded-md items-center justify-between">        
                <!-- Select do Agente -->
              <select
                v-model="agenteSelecionado"
                class="text-xs px-2 py-1 border rounded-md outline-none"
                @change="GetUserRelatorio(mesSelecionado, anoSelecionado, agenteSelecionado)"
                >
                <option value="">Selecionar agente</option>
                <option v-for="ag in agentes" :key="ag.id" :value="ag.id">{{ ag.nome }}</option>
              </select>

                 <!-- Select do Ano -->
                 <select v-model="anoSelecionado"  class="text-xs px-2 py-1 border rounded-md outline-none" @change="GetUserRelatorio(mesSelecionado, anoSelecionado, agenteSelecionado)">
                    <option value="">Ano</option>
                    <option v-for="(a, i) in ano" :key="i" :value="a">{{ a }}</option>

                 </select>

                 <!-- Select do Mes -->
                 <select v-model="mesSelecionado" class="text-xs px-2 py-1 border rounded-md outline-none"
                 @change="GetUserRelatorio(mesSelecionado, anoSelecionado, agenteSelecionado)">
                    <option value="">Mês</option>
                    <option v-for="(m, i) in meses" :key="i" :value="i">{{ m }}</option>
                 </select>      
              </div>
                 
              <div class="flex flex-col text-sm ">
                <div class="font-bold">{{ agenteSelecionado }}</div>
                <div class="flex flex-row space-x-2 text-xs">
                <div>{{ meses[mesSelecionado] }}</div>
                <div>{{ anoSelecionado }}</div>
                
            </div> 
            <div class="flex flex-row space-x-2">Mensal: {{ valorTotal }}KZ</div>
              </div>
             

              <!-- Tabela -->

              <div class=" p-2">
                <div class="h-100  overflow-y-auto overflow-x-auto text-sm whitespace-nowrap">
                <table class="table-auto w-full border-collapse rounded-lg overflow-hidden bg-white min-w-full ">
                    
                    <thead class="text-orange-700 bg-orange-200 uppercase text-xs font-semibold tracking-wide select-none ">            
                       <tr>
                        
                           <th class="text-left py-3 px-4 text-xs">Dia</th>
                           <th class="text-left py-3 px-4 text-xs">Valor</th>
                           <th class="text-left py-3 px-4 text-xs">Hora</th>
                      
                 </tr>
             </thead>
            
                <tbody>
<tr
    v-for="relatorio in relatorios" :key="relatorio.dia"
    class="hover:bg-gray-50 border-b border-orange-200" >
         <td class="p-3">{{ relatorio.dia }}</td>
         <td class="p-3">{{ relatorio.valor }}</td>
         <td class="p-3">{{ relatorio.hora }}</td>
 </tr>
               </tbody>
            
          </table>
        </div>
       </div>
      </div>
    </div>

</div>
<processo v-if="processing"/>
</template>

<script setup>
import Menu from '@/components/adm/Menu.vue';
import { getAllUsers } from '../../../Servicos/GetAllUsers';
import { ref, computed, reactive } from 'vue'
import { onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { getRelatorio } from '../../../Servicos/GetRelatorio';
import processo from '../processo.vue';

const router = useRouter()
let agentes = ref([])
let processing = ref()
let valorTotal = ref(0);
let hasData = ref[false]
onMounted(async()=>{
  processing.value = true
  const response = await getAllUsers()
  agentes.value = response
  if(!response)
      router.push("/")
  processing.value = false  
})
const ano = ['2025', '2024', '2023']

const meses = [
  'Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho',
  'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'
]

const agenteSelecionado = ref('')
const mesSelecionado = ref(new Date().getMonth())
const anoSelecionado = ref(new Date().getFullYear())

let UserName = agenteSelecionado

//  Zé simulei os dados para relatorios
const relatorios = ref([])
UserName = agenteSelecionado.value

const GetUserRelatorio = async (mesSelecionado, anoSelecionado, agenteSelecionado) => {
  let todosDias = []
  const response = await getRelatorio(agenteSelecionado, mesSelecionado + 1, anoSelecionado)
  if(response == null){
    hasData = true
    relatorios.value = null
    return;
  }

  // Cria um mapa com os dias que vieram da API
  const diasComDados = new Map()
  response.forEach(item => {
    diasComDados.set(item.dia, item)
  })

  // Descobre quantos dias tem o mês selecionado
  const diasNoMes = new Date(anoSelecionado, mesSelecionado + 1, 0).getDate()

  // Cria o array final, preenchendo os dias ausentes com valor 0

  for (let dia = 1; dia <= diasNoMes; dia++) {
    if (diasComDados.has(dia)) {
      todosDias.push(diasComDados.get(dia))
    } else {
      todosDias.push({
        ano: anoSelecionado,
        mes: meses[mesSelecionado], // função para retornar 'Julho', etc
        dia,
        hora: '00:00:00',
        valor: 0
      })
    }
  }

  // Atualiza os relatórios
  relatorios.value = todosDias

  // Soma o total
  valorTotal = relatorios.value.reduce((total, item) => total + item.valor, 0)

  console.log(valorTotal)
  console.log(todosDias)
}


</script>