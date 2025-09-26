<template>
    <!-- Alert Personalisado -->
<div class="relative" v-if="!fechar">
  <div class="fixed top-100 right-8 h-screen  z-50"> 
 <div class=" flex items-center justify-center ">
 <div :class="`w-[280px] h-[110px] ${cor} text-white grid grid-rows-3 grid-cols-6 border rounded-lg shadow-md`">
 
   <div class="px-3 row-span-3 col-1 py-8">
<svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-10">
  <path stroke-linecap="round" stroke-linejoin="round" d="M9 12.75 11.25 15 15 9.75M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z" />
</svg>
  </div>
   <div class=" px-3 py-3 col-span-4 col-2 text-sm font-bold">{{ texto }}</div>
   <div class="row-span-3 col-6 px-1 py-8 text-sm"> 
   <button> <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-8" @click="fechar = !fechar">
  <path stroke-linecap="round" stroke-linejoin="round" d="M6 18 18 6M6 6l12 12" />
</svg></button>
   </div>
   <div class=" row-2 col-2 row-span-4 col-span-4 px-3  py-2 text-sm ">Seja bem vindo de volta!</div>
   </div>
 </div>
 
 </div>
 <div
   class="fixed inset-0 bg-black/20 z-20"   
  ></div>
</div>
</template>

<script setup>
import { ref, onMounted, watch } from 'vue'

const props = defineProps({
  texto: {
    type: String,
    required: true
  },
  cor: {
    type: String,
    default: 'bg-green-500'
  }
})

let fechar = ref(false)

function iniciarTimeout() {
  setTimeout(() => {
    fechar.value = true
  }, 5000)
}

watch(
  () => [props.texto, props.cor],
  () => {
    fechar.value = false
    iniciarTimeout()
  },
  { immediate: true }
)
</script>