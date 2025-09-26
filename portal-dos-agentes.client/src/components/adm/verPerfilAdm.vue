<template>
 <div class="h-screen w-screen">
             
          <div class="absolute left-4  transform mt-8 text-white text-xl space-x-2 font-semibold flex items-center justify-center">
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" @click="$router.back()" class="size-6">
<path fill-rule="evenodd" d="M7.72 12.53a.75.75 0 0 1 0-1.06l7.5-7.5a.75.75 0 1 1 1.06 1.06L9.31 12l6.97 6.97a.75.75 0 1 1-1.06 1.06l-7.5-7.5Z" clip-rule="evenodd" />

  </svg>
</div> 
        <div class="h-55 w-full space-x-2 p-2 bg-orange-500 flex flex-col items-center justify-center ">


                 <!-- Foto do agente! -->
      <div class="w-23 h-23 rounded-full bg-white flex items-center justify-center">
        <div class="w-22 h-22 rounded-full bg-orange-500 flex items-center justify-center">
          <div class="w-21 h-21 rounded-full bg-white text-orange-500 text-xl font-bold flex items-center justify-center">
           {{ getIniciais(agente.nome) }}
          </div>
        </div>
      </div>
      <!-- O nome do agente -->
      <p class="text-white text-xl font-bold">{{ agente.nome }}</p>
      <!-- Id do agente -->
       <div class="text-white text-sm bg-orange-400 rounded-full p-1.5">
          ID: {{ agente.id }}
       </div>
     
       </div>

      <div class="flex  items-center justify-center p-4">
        <div class="space-y-4 w-full">
        
            <!-- Numero de telefone -->
            <div class="bg-gray-100 h-20 flex rounded-md p-4 ">
               <div class="w-12 h-12 bg-orange-500 rounded-full flex items-center justify-center">
                   <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="size-6 text-white ">
  <path fill-rule="evenodd" d="M1.5 4.5a3 3 0 0 1 3-3h1.372c.86 0 1.61.586 1.819 1.42l1.105 4.423a1.875 1.875 0 0 1-.694 1.955l-1.293.97c-.135.101-.164.249-.126.352a11.285 11.285 0 0 0 6.697 6.697c.103.038.25.009.352-.126l.97-1.293a1.875 1.875 0 0 1 1.955-.694l4.423 1.105c.834.209 1.42.959 1.42 1.82V19.5a3 3 0 0 1-3 3h-2.25C8.552 22.5 1.5 15.448 1.5 6.75V4.5Z" clip-rule="evenodd" />
                 </svg>
               </div>
                    <div class="flex flex-col px-4">
                      <div class="font-semibold text-sm text-gray-600">Telefone</div>
                      <p class="text-md font-semibold">{{agente.telefone}}</p>
                </div>
        </div>

            <!-- Email -->
            <div class="bg-gray-100 h-20 flex rounded-md p-4 ">
                <div class="w-12 h-12 bg-orange-500 rounded-full flex items-center justify-center">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="size-6 text-white">
  <path d="M1.5 8.67v8.58a3 3 0 0 0 3 3h15a3 3 0 0 0 3-3V8.67l-8.928 5.493a3 3 0 0 1-3.144 0L1.5 8.67Z" />
  <path d="M22.5 6.908V6.75a3 3 0 0 0-3-3h-15a3 3 0 0 0-3 3v.158l9.714 5.978a1.5 1.5 0 0 0 1.572 0L22.5 6.908Z" />
                    </svg>
                </div>
                    <div class="flex flex-col px-4">
                         <div class="font-semibold text-sm text-gray-600">E-mail</div>
                         <p class="text-md font-semibold">{{ agente.email }}</p>
                </div>
            </div>
           
            <!-- Endereço -->
            <div class="bg-gray-100 h-20 flex rounded-md p-4 ">
               <div class="w-12 h-12 bg-orange-500 rounded-full flex items-center justify-center">
                     <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="size-6 text-white">
  <path fill-rule="evenodd" d="m11.54 22.351.07.04.028.016a.76.76 0 0 0 .723 0l.028-.015.071-.041a16.975 16.975 0 0 0 1.144-.742 19.58 19.58 0 0 0 2.683-2.282c1.944-1.99 3.963-4.98 3.963-8.827a8.25 8.25 0 0 0-16.5 0c0 3.846 2.02 6.837 3.963 8.827a19.58 19.58 0 0 0 2.682 2.282 16.975 16.975 0 0 0 1.145.742ZM12 13.5a3 3 0 1 0 0-6 3 3 0 0 0 0 6Z" clip-rule="evenodd" />
                     </svg>
               </div>
                <div class="flex flex-col px-4">
                   <div class="font-semibold text-sm text-gray-600">Endereço</div>
                    <p class="text-md font-semibold">{{ agente.endereco }}</p>
             </div>
            </div>
         </div>

                <!-- Botão de editar fixado na parte inferior -->
                <div class="fixed bottom-12 left-0 w-full p-4 ">
                    <button class="bg-orange-500 text-white px-4 py-2 rounded-lg hover:bg-orange-600 w-full sm:w-auto">
                        Editar Perfil
                    </button>
                </div>

                <!-- Botão de elimanar -->
                <div class="fixed bottom-0 left-0 w-full p-4 ">
                    <button class="bg-gray-100 text-red-500 px-4 border border-red-500 py-2 rounded-lg  w-full sm:w-auto" @click="eliminarAgente(agente.id)">            
                        Excluir Agente
                    </button>
                </div>


           <div>
       </div>
      </div>
    </div>
</template>

<script setup>
    import router from '@/router'
    import { deleteAgente } from '../../../Servicos/Delete'
    defineProps(['agente'])
    const emit = defineEmits(['fechar-perfil'])
    const fecharPerfil = () => {
        emit('fechar-perfil')
    }
    const eliminarAgente = async(id) => {
        if (!confirm('Tem certeza que deseja eliminar este agente? Esta ação não pode ser desfeita.'))
        {
            return
        }
        try
        {
            const result = await deleteAgente(id)
            if(!result){
                alert('não autenticado, por favor faça login novamente')
                router.push('/')
            }
            else{
                alert('Agente eliminado com sucesso!')
                router.push('/verAgentes')
            }

        }
        catch (error)
        {
            alert('Erro ao eliminar agente: ' + error.response.data)
            console.error('Erro ao eliminar agente:', error)
        }
    }
    function getIniciais(nome) {
  const partes = nome.trim().split(" ");
  return partes[0][0] + (partes[1]?.[0] || '');
}
</script>









<!-- Nome e ID -->
<!-- 

<span></span>
<p></p> -->

<!-- <p class="text-gray-500"></p> -->

<!-- <p class="mb-[14px] text-gray-500"></p> -->

<!-- <p class="mb-[14px] text-gray-500"></p> -->


<!-- Eliminar -->
            <!-- <button type="submit" class="bg-red-500 text-white px-2 py-1 rounded-md hover:bg-red-400 cursor-pointer " ">Eliminar</button> -->