
<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router'; // Importa o router


const menuAberto = ref(false);
const router = useRouter(); // Instancia do roteador

  const terminarSessao = () => {
    localStorage.removeItem('refreshToken'); // Remove o token do localStorage
    sessionStorage.removeItem('acessToken'); // Remove o token do sessionStorage
    router.push('/'); // Redireciona para a página de login
  };
  function irPara(path) {
  router.push(path);
  menuAberto.value = false;
}
</script>


<template>
<div class="fixed">
    <router-view />

    <div class="flex h-screen relative">
      <!-- Botão hambúrguer -->
      <button
        @click="menuAberto = !menuAberto"
        :class="[
          'absolute top-0 left-1.5 z-20 text-[23px] cursor-pointer focus:outline-none p-2 rounded',
          menuAberto ? 'text-white bg-orange-500' : 'text-white'
        ]"
      >
        ☰
      </button>

      <!-- Sidebar -->
      <div
        :class="[
          'fixed top-0 left-0 h-full lg:w-1/12 w-2/3 bg-orange-500 text-white p-4 transition-transform duration-300 ease-in-out z-50 rounded-r-md shadow-lg',
          menuAberto ? 'translate-x-0' : '-translate-x-full'
        ]"
        ref="sidebar"
        @click.stop
      >
        <div class="text-[11px] text-blue-200 font-bold px-7 py-12.5 whitespace-nowrap">
          Portal dos agentes
        </div>
        <div>
          <hr class="mt-[-32px] text-gray-300">
        </div>

        <div
          class="p-2.5 mt-3 flex items-center rounded-md px-4 duration-400 cursor-pointer hover:bg-orange-500 text-white"
          @click="irPara('/dashboardAgente')"
        >
          <span class="hover:text-orange-100 flex items-center gap-2 whitespace-nowrap">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="size-6">
              <path fill-rule="evenodd" d="M3 6a3 3 0 0 1 3-3h2.25a3 3 0 0 1 3 3v2.25a3 3 0 0 1-3 3H6a3 3 0 0 1-3-3V6Zm9.75 0a3 3 0 0 1 3-3H18a3 3 0 0 1 3 3v2.25a3 3 0 0 1-3 3h-2.25a3 3 0 0 1-3-3V6ZM3 15.75a3 3 0 0 1 3-3h2.25a3 3 0 0 1 3 3V18a3 3 0 0 1-3 3H6a3 3 0 0 1-3-3v-2.25Zm9.75 0a3 3 0 0 1 3-3H18a3 3 0 0 1 3 3V18a3 3 0 0 1-3 3h-2.25a3 3 0 0 1-3-3v-2.25Z" clip-rule="evenodd" />
            </svg>
            Dashboard
          </span>
        </div>

        <!-- Perfil do agente -->
        <div
          class="p-2.5 mt-3 flex items-center rounded-md px-4 duration-400 cursor-pointer hover:bg-orange-500 text-white" 
          @click="irPara('/verPerfilAgente')"
          >
          <span class="hover:text-orange-100 flex items-center gap-2 whitespace-nowrap">
      <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="size-6 text-gray-100 ">
  <path fill-rule="evenodd" d="M7.5 6a4.5 4.5 0 1 1 9 0 4.5 4.5 0 0 1-9 0ZM3.751 20.105a8.25 8.25 0 0 1 16.498 0 .75.75 0 0 1-.437.695A18.683 18.683 0 0 1 12 22.5c-2.786 0-5.433-.608-7.812-1.7a.75.75 0 0 1-.437-.695Z" clip-rule="evenodd" />
</svg>

            Ver Perfil
          
          </span>
        </div>

        <div
          class="text-left text-sm font-thin mt-2 w-4/5 mx-auto text-gray-100"
        >
        </div>

        <!-- Pedidos -->
        <div
          class="p-2.5 mt-3 flex items-center rounded-md px-4 duration-400 cursor-pointer hover:bg-orange-500 text-white"
          @click="irPara('/pedido')"
        >
          <span class="hover:text-orange-100 flex items-center gap-2">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="size-6">
              <path d="M12 1.5a.75.75 0 0 1 .75.75V7.5h-1.5V2.25A.75.75 0 0 1 12 1.5ZM11.25 7.5v5.69l-1.72-1.72a.75.75 0 0 0-1.06 1.06l3 3a.75.75 0 0 0 1.06 0l3-3a.75.75 0 1 0-1.06-1.06l-1.72 1.72V7.5h3.75a3 3 0 0 1 3 3v9a3 3 0 0 1-3 3h-9a3 3 0 0 1-3-3v-9a3 3 0 0 1 3-3h3.75Z" />
            </svg>
            Pedidos
          </span>
        </div>

        <!-- Relatórios -->
        <div
          class="p-2.5 mt-3 flex items-center rounded-md px-4 duration-400 cursor-pointer hover:bg-orange-500 text-white">
          <span class="hover:text-orange-100 flex items-center gap-2"  @click="irPara('/Relatorio')">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="size-6">
              <path fill-rule="evenodd" d="M7.502 6h7.128A3.375 3.375 0 0 1 18 9.375v9.375a3 3 0 0 0 3-3V6.108c0-1.505-1.125-2.811-2.664-2.94a48.972 48.972 0 0 0-.673-.05A3 3 0 0 0 15 1.5h-1.5a3 3 0 0 0-2.663 1.618c-.225.015-.45.032-.673.05C8.662 3.295 7.554 4.542 7.502 6ZM13.5 3A1.5 1.5 0 0 0 12 4.5h4.5A1.5 1.5 0 0 0 15 3h-1.5Z" clip-rule="evenodd" />
              <path fill-rule="evenodd" d="M3 9.375C3 8.339 3.84 7.5 4.875 7.5h9.75c1.036 0 1.875.84 1.875 1.875v11.25c0 1.035-.84 1.875-1.875 1.875h-9.75A1.875 1.875 0 0 1 3 20.625V9.375ZM6 12a.75.75 0 0 1 .75-.75h.008a.75.75 0 0 1 .75.75v.008a.75.75 0 0 1-.75.75H6.75a.75.75 0 0 1-.75-.75V12Zm2.25 0a.75.75 0 0 1 .75-.75h3.75a.75.75 0 0 1 0 1.5H9a.75.75 0 0 1-.75-.75ZM6 15a.75.75 0 0 1 .75-.75h.008a.75.75 0 0 1 .75.75v.008a.75.75 0 0 1-.75.75H6.75a.75.75 0 0 1-.75-.75V15Zm2.25 0a.75.75 0 0 1 .75-.75h3.75a.75.75 0 0 1 0 1.5H9a.75.75 0 0 1-.75-.75ZM6 18a.75.75 0 0 1 .75-.75h.008a.75.75 0 0 1 .75.75v.008a.75.75 0 0 1-.75.75H6.75a.75.75 0 0 1-.75-.75V18Zm2.25 0a.75.75 0 0 1 .75-.75h3.75a.75.75 0 0 1 0 1.5H9a.75.75 0 0 1-.75-.75Z" clip-rule="evenodd" />
            </svg>
            Relatórios
          </span>
        </div>

       
        <!-- A linha que sepepara Definições, Terminar sessão com os demais -->
        <div class="fixed bottom-0 m-4">
        <hr class="text-gray-300">

        <!-- Definições -->
        <div
          class="p-2.5 mt-3 flex items-center rounded-md px-4 duration-400 cursor-pointer hover:bg-orange-500 text-white"
          @click="irPara('/definicoes')"
        >
          <span class="hover:text-orange-100 flex items-center gap-2">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="size-6">
              <path fill-rule="evenodd" d="M11.078 2.25c-.917 0-1.699.663-1.85 1.567L9.05 4.889c-.02.12-.115.26-.297.348a7.493 7.493 0 0 0-.986.57c-.166.115-.334.126-.45.083L6.3 5.508a1.875 1.875 0 0 0-2.282.819l-.922 1.597a1.875 1.875 0 0 0 .432 2.385l.84.692c.095.078.17.229.154.43a7.598 7.598 0 0 0 0 1.139c.015.2-.059.352-.153.43l-.841.692a1.875 1.875 0 0 0-.432 2.385l.922 1.597a1.875 1.875 0 0 0 2.282.818l1.019-.382c.115-.043.283-.031.45.082.312.214.641.405.985.57.182.088.277.228.297.35l.178 1.071c.151.904.933 1.567 1.85 1.567h1.844c.916 0 1.699-.663 1.85-1.567l.178-1.072c.02-.12.114-.26.297-.349.344-.165.673-.356.985-.57.167-.114.335-.125.45-.082l1.02.382a1.875 1.875 0 0 0 2.28-.819l.923-1.597a1.875 1.875 0 0 0-.432-2.385l-.84-.692c-.095-.078-.17-.229-.154-.43a7.614 7.614 0 0 0 0-1.139c-.016-.2.059-.352.153-.43l.84-.692c.708-.582.891-1.59.433-2.385l-.922-1.597a1.875 1.875 0 0 0-2.282-.818l-1.02.382c-.114.043-.282.031-.449-.083a7.49 7.49 0 0 0-.985-.57c-.183-.087-.277-.227-.297-.348l-.179-1.072a1.875 1.875 0 0 0-1.85-1.567h-1.843ZM12 15.75a3.75 3.75 0 1 0 0-7.5 3.75 3.75 0 0 0 0 7.5Z" clip-rule="evenodd" />
            </svg>
            Definições
          </span>
        </div>

        <!-- Terminar Sessão -->
        <div
          class="p-2.5 mt-3 flex items-center rounded-md px-4 duration-400 cursor-pointer hover:bg-orange-500 text-white"
          @click="terminarSessao"
        >
          <span class="hover:text-orange-100 flex items-center gap-2 whitespace-nowrap">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="size-4 text-gray-100">
              <path fill-rule="evenodd" d="M16.5 3.75a1.5 1.5 0 0 1 1.5 1.5v13.5a1.5 1.5 0 0 1-1.5 1.5h-6a1.5 1.5 0 0 1-1.5-1.5V15a.75.75 0 0 0-1.5 0v3.75a3 3 0 0 0 3 3h6a3 3 0 0 0 3-3V5.25a3 3 0 0 0-3-3h-6a3 3 0 0 0-3 3V9A.75.75 0 1 0 9 9V5.25a1.5 1.5 0 0 1 1.5-1.5h6Zm-5.03 4.72a.75.75 0 0 0 0 1.06l1.72 1.72H2.25a.75.75 0 0 0 0 1.5h10.94l-1.72 1.72a.75.75 0 1 0 1.06 1.06l3-3a.75.75 0 0 0 0-1.06l-3-3a.75.75 0 0 0-1.06 0Z" clip-rule="evenodd" />
            </svg>
            Terminar sessão
          </span>
        </div>
      </div>
      </div>

      <!-- Overlay com blur -->
      <div
        v-if="menuAberto"
        class="fixed inset-0 bg-black/50 z-20"
        @click="menuAberto = false"
      ></div>

      <slot />
    </div>
  </div>
  
</template>
