
<script>
import Menu from '@/components/adm/Menu.vue';
import VerAgentes from './verAgentes.vue';
export default {
 
 
  data() {
    return {
      form: {
        nome: '',
        id: '',
        senha: '',
        email: '',
        telefone: '',
        endereco: ''
      },
      agenteEditado: { ...this.agente }
    };
    },
    props:['agente'],
  components:{
  Menu
  },
  methods: {
    handleSubmit() {
      const camposValidos = Object.values(this.form).every(campo => campo.trim() !== '');
      if (camposValidos) {
        alert('Agente cadastrado com sucesso!');
        this.form = {
          nome: '',
          id: '',
          senha: '',
          email: '',
          telefone: '',
          endereco: ''
        };
      } else {
        alert('Por favor, preencha todos os campos corretamente.');
      }
    }
  }
};
</script>


<template>
  <div class="bg-gray-100 text-black font-sans ">
    <!-- Cabeçalho -->
      <div class="m-0 py-4 px-12 border  border-gray-300 shadow-sm w-[395px]"> 
            <div class="text-md font-bold ">
                Editar Agente
            </div>
                 <div class="text-sm text-gray-600 whitespace-nowrap ">Altere os dados dos agentes de forma rápida e eficiente.
                 </div>
            </div>
            

    <!-- Formulário -->

    <div class="">
    <main class="flex justify-center items-start sm:items-center mt-10 px-4 py-12 px-8">
        <!-- Menu no canto superior esquerdo -->
        <div class="absolute top-6 left-0 z-10">
            <Menu/>
        </div> 
        <div class="bg-white p-6 sm:p-8 rounded-xl shadow-md w-full max-w-2xl border border-orange-500">
        <h2 class="text-2xl font-bold mb-6 text-center sm:text-left">Editar Agente</h2>

        <form class="space-y-4" @submit.prevent="handleSubmit">
          <div>
            <label class="block font-medium mb-1">Nome</label>
            <input
             v-model="agenteEditado.nome" 
            type="text"
             class="w-full p-2 border border-orange-500 rounded-lg" 
             placeholder="Nome completo" 
            
             required />
          </div>
            
          <div>
            <label class="block font-medium mb-1">ID</label>
            <input 
              v-model="form.id"
             type="text" 
             class="w-full p-2 border border-orange-500 rounded-lg" 
             placeholder="ID do agente"
              required />
          </div>

          <div>
            <label class="block font-medium mb-1">Senha</label>
            <input 
            v-model="form.senha" 
            type="password" 
            class="w-full p-2 border border-orange-500 rounded-lg"
             placeholder="Senha segura"
             required
             minlength="6" />
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
            <button type="submit" class="bg-orange-500 text-white px-4 py-2 rounded-lg hover:bg-orange-600 w-full sm:w-auto">Salvar alterações</button>
          </div>
        </form>
      </div>
       
    </main>
  </div>
  </div>


</template>



