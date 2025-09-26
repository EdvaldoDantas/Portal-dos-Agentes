<template>
 
  <div class="flex justify-center items-center min-h-screen p-4 bg-white overflow-auto">

  
  
  <!-- Caixa geral -->
  
  <div class="bg-white  flex divCaixa w-[320px] h-[350px] lg:w-[600px] lg:h-[450px] border border-orange-600 rounded-lg pr-8 justify-center items-center overflow-hidden shadow-sm">
 
    <!-- Bloco esquerdo -->
     <div class="bg-white lg:w-[400px] lg:h-[450px] divEsquerdo lg:bg-[#e64e03] text-white lg:mr-8 flex items-center justify-center text-xl text-center font-bold p-2">
   ->
    </div> 


    <!-- Bloco direito (formulário) -->
    <div class="bg-white branca  bg-white  gap-4 ">
      <div class="Inputes space-y-4">
        <label class="text-gray-500 text-sm"  for="id">ID do agente</label>
      <input
      id="id"
        type="number"
        placeholder="Entre com seu Id"
        class="border border-orange-600 rounded-sm text-sm py-3  px-2 w-full "
         v-model="Id"
      />
        <label class="text-gray-500 text-sm" for="senha">Senha</label>
      <input
      id="senha"
        type="password"
        placeholder="Senha enviada pelo seu Agente"
        class="border border-orange-600 rounded-sm text-sm  py-3 px-2 w-full"
       
        v-model="senha"
      />
      <div class="text-end text-gray-500 text-sm">
      <a href="" class="text-blue-800 hover:underline text-sm ml-24 whitespace-nowrap">Ainda não recebi a senha</a>
      </div>
      <input 
           type="submit"
           value="Entrar"
           class="border rounded-sm bg-[#e64e03] mt-2 p-2 text-sm text-white w-full hover:bg-orange-600 shadow-sm"
           v-on:click="Entrar()"
           
       />
        </div>
    </div>

  </div>
  
</div>

<main v-if="AparecerAlerta">
<alerta :texto="alertaTexto" :cor="alertaCor" />
</main>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import alerta from '@/components/alerta.vue'
import api from './Requisições/Urlapi'
import { getRoleFromJWT } from '../../Servicos/GetRoleFromJwt'


const Id = ref('')
const senha = ref('')
const AparecerAlerta = ref(false)
const router = useRouter()

const alertaTexto = ref('')
const alertaCor = ref('bg-green-500')

async function Entrar() {


// if(this.Id === 123 && this.senha ==='adm'){
//                           this.AparecerAlerta = true;
//                          router.push('/dashboardAdmin')
//               } else if(this.Id === 123 && this.senha ==="agente"){
//                     this.AparecerAlerta = true;
//                          router.push('/dashboardAgente')
//               }else{
//                 alert("Dados Inválidos!")
//               }


try {
  const response = await api.post('api/Auth', {
    agenteId: Id.value,
    senha: senha.value
  })
  const token = response.data
  let role = getRoleFromJWT(token.acessToken)
  sessionStorage.setItem('accessToken', token.acessToken)
  localStorage.setItem('refreshToken', token.refreshToken)
  
 if(response.status == 200) {
alertaTexto.value = 'LOGIN SUCEDIDO!'
alertaCor.value = 'bg-green-500'
AparecerAlerta.value = true

setTimeout(() => {
  if(role == "adm")
    router.push('/dashboardAdmin')
  else
    router.push('/dashboardAgente')
}, 1500) // 1.5 segundos de espera
} else {
  alertaTexto.value = 'Erro ao fazer login!'
  alertaCor.value = 'bg-red-500'
  AparecerAlerta.value = true
}
} 
catch (error) {
  console.error(error)
  alertaTexto.value = 'Dados incorretos!'
  alertaCor.value = 'bg-red-500'
  AparecerAlerta.value = true
  }
}
</script>
