<template>
  <div class="h-screen w-screen fixed">
    <div class="h-18 w-full space-x-2 p-3 bg-orange-500 flex items-center justify-end">

<!-- Botão de voltar-->
      <div class="absolute left-4  transform  text-white text-xl space-x-2 font-semibold flex items-center justify-center">
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" @click="$router.back()" class="size-4">
<path fill-rule="evenodd" d="M7.72 12.53a.75.75 0 0 1 0-1.06l7.5-7.5a.75.75 0 1 1 1.06 1.06L9.31 12l6.97 6.97a.75.75 0 1 1-1.06 1.06l-7.5-7.5Z" clip-rule="evenodd" />

  </svg>
  Todos Relatórios
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
        <div class="space-y-4 w-full mt-10">
          <h2 class="text-xl font-bold text-orange-600 mb-2 whitespace-nowrap flex items-start justify-start">
            Ver todos relatórios
          </h2>

          <!-- CONTROLES DE FILTRO -->
          <div class="flex flex-row space-x-12">
            <!-- Seleção de mês -->
            <div class="flex items-center gap-2">
              <!-- Seta esquerda -->
              <button @click="mesAnterior">
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5"
                  stroke="currentColor" class="size-4">
                  <path stroke-linecap="round" stroke-linejoin="round" d="M15.75 19.5 8.25 12l7.5-7.5" />
                </svg>
              </button>
              <span class="font-medium">{{ meses[mesAtual] }}</span>
              <!-- Seta direita -->
              <button @click="mesSeguinte">
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5"
                  stroke="currentColor" class="size-4">
                  <path stroke-linecap="round" stroke-linejoin="round" d="m8.25 4.5 7.5 7.5-7.5 7.5" />
                </svg>
              </button>
            </div>

            <!-- Filtro por dia -->
            <select v-model="diaSelecionado" class="text-xs border rounded-md px-2 py-1 outline-none">
              <option value="">Selecionar Dia</option>
              <option v-for="dia in diasNoMes" :key="dia" :value="dia">{{ dia }}</option>
            </select>
          </div>

          <!-- TABELA DE RELATÓRIOS -->
          <div class="h-100  overflow-y-auto overflow-x-auto text-sm whitespace-nowrap">
            <table class="table-auto w-full border-collapse overflow-hidden rounded-lg  bg-white min-w-full ">
              <thead class="text-orange-700 bg-orange-200 uppercase text-xs font-semibold tracking-wide select-none">
                <tr>
                  <th class="text-xs text-left py-3 px-4">Dia</th>
                  <th class="text-xs text-left py-3 px-4">Agente</th>
                  <th class="text-xs text-left py-3 px-4">Valor</th>
                  <th class="text-xs text-left py-3 px-4">Hora</th>
                </tr>
              </thead>
              <tbody class="bg-white">
                <tr v-for="item in relatoriosFiltrados" :key="item.id" class="border-b border-orange-100">
                  <td class="p-3">{{ item.dia }}</td>
                  <td class="p-3">{{ item.username }}</td>
                  <td class="p-3">{{ item.valor }}</td>
                  <td class="p-3">{{ item.hora }}</td>
                </tr>
              </tbody>    
            </table>
          </div>
        </div>
      </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import Menu from '@/components/adm/Menu.vue'
import { GetRelatorioMes } from '../../../Servicos/getRelatoriosMes'

// Estados
const relatorios = ref([])
const mesAtual = ref(new Date().getMonth())
const ano = new Date().getFullYear()
const diaSelecionado = ref('')

// Lista de meses
const meses = [
  'Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho',
  'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'
]

// Dias do mês (1 a 31)
const diasNoMes = Array.from({ length: 31 }, (_, i) => i + 1)

// Carrega relatórios do mês
const getRelatoriosMes = async () => {
  const response = await GetRelatorioMes(ano, mesAtual.value + 1)
  relatorios.value = response
}

// Computed para aplicar o filtro por dia
const relatoriosFiltrados = computed(() => {
  if (!diaSelecionado.value) {
    return relatorios.value
  }
  return relatorios.value.filter(r => r.dia == diaSelecionado.value)
})

// Botão para voltar mês
const mesAnterior = async () => {
  mesAtual.value = (mesAtual.value - 1 + 12) % 12
  await getRelatoriosMes()
}

// Botão para avançar mês
const mesSeguinte = async () => {
  mesAtual.value = (mesAtual.value + 1) % 12
  await getRelatoriosMes()
}

// Ao carregar componente
onMounted(async () => {
  await getRelatoriosMes()
})
</script>