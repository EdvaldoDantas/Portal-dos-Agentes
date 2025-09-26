<script setup>
import { reactive } from 'vue'
import Menu from '@/components/adm/Menu.vue'
import api from '../Requisições/Urlapi'
import { auth } from '../../../Servicos/Auth'
import { useRouter } from 'vue-router'


const router = useRouter()

// Objeto reativo para o formulário
const form = reactive({
  nome: '',
  id: '',
  email: '',
  telefone: '',
  endereco: ''
})

const Add = async () => {
  try {
    const token = sessionStorage.getItem('accessToken')

    const formData = new FormData()
    formData.append('agenteId', form.id)
    formData.append('nome', form.nome)
    formData.append('email', form.email)
    formData.append('telefone', form.telefone)
    formData.append('endereco', form.endereco)

    const response = await api.post('/api/User', formData, {
      headers: {
        Authorization: `Bearer ${token}`,
      }
    })

    console.log(response.data)
    alert('Agente cadastrado com sucesso!')
  } catch (error) {
           if (error.response && error.response.status === 401) {
           if(await auth()){
               console.warn("Tentando cadastrar novamente após renovar o token...");
               return await Add(); // Tenta novamente após renovar o token
           } else {
               console.warn("Falha ao renovar o token de acesso. Redirecionando para a página de login...");
               return false
           }
        }
        console.error("Erro ao obter a lista de usuários:", error);
        throw error; // Lança o erro para ser tratado em outro lugar
  }
}



async function handleSubmit() {
  const camposValidos = Object.values(form).every(campo => campo.trim() !== '')
  if (camposValidos) {
    if(await Add())
      router.push('/')

    // Resetar formulário
    form.nome = ''
    form.id = ''
    form.email = ''
    form.telefone = ''
    form.endereco = ''
  } else {
    alert('Por favor, preencha todos os campos corretamente.')
  }
}
</script>


<template>

  <div class=" h-screen w-screen">
    <!-- Cabeçalho -->
      <!-- Div do header-laranja -->
      <div class="h-18 w-full space-x-2 p-3 bg-orange-500 flex items-center justify-end">

<!-- Botão de voltar-->
      <div class="absolute left-4  transform  text-white text-xl space-x-2 font-semibold flex items-center justify-center">
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" @click="$router.back()" class="size-4">
<path fill-rule="evenodd" d="M7.72 12.53a.75.75 0 0 1 0-1.06l7.5-7.5a.75.75 0 1 1 1.06 1.06L9.31 12l6.97 6.97a.75.75 0 1 1-1.06 1.06l-7.5-7.5Z" clip-rule="evenodd" />
  </svg>
  Adicionar
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
  Adicione um novo agente ao sistema, preenchendo os campos necessários e clicando em cadastrar.
</div>




    <!-- Formulário -->

    <div class="flex items-center justify-center ">

      <div class="space-y-4 w-full p-4 mt-9">
        <h2 class="text-2xl font-bold mb-6 text-center sm:text-left">Cadastrar novo agente</h2>
        <div class="">

        <form class="space-y-4" @submit.prevent="handleSubmit">
          <div>
            <label class="block font-medium mb-1">Nome</label>
            <input v-model="form.nome" type="text" class="w-full p-2 border border-orange-500 rounded-lg" placeholder="Nome completo" required />
          </div>
            
          <div>
            <label class="block font-medium mb-1">ID</label>
            <input v-model="form.id" type="text" class="w-full p-2 border border-orange-500 rounded-lg" placeholder="ID do agente" required />
          </div>
          <div>
            <label class="block font-medium mb-1">Email</label>
            <input v-model="form.email" type="email" class="w-full p-2 border border-orange-500 rounded-lg" placeholder="email@exemplo.com" required />
          </div>

          <div>
            <label class="block font-medium mb-1">Número de Telefone</label>
            <input v-model="form.telefone" type="tel" class="w-full p-2 border border-orange-500 rounded-lg" placeholder="(+244) 999-999-999" required pattern="\+?[0-9\s-]{9,15}" />
          </div>

          <div>
            <label class="block font-medium mb-1">Endereço</label>
            <input v-model="form.endereco" type="text" class="w-full p-2 border border-orange-500 rounded-lg" placeholder="Endereço completo" required />
          </div>

          <div class="pt-4">
            <button type="submit" class="bg-orange-500 text-white px-4 py-2 rounded-lg hover:bg-orange-600 w-full sm:w-auto">Cadastrar</button>
          </div>
        </form>
      </div>
      </div>

  </div>
  </div>


</template>


<style scoped>
/* Adicional: garantir que a altura ocupe a tela toda */
html, body, #app {
  height: 100%;
  margin: 0;
}
</style>
