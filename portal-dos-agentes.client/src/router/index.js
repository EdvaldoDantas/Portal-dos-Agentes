import { createRouter, createWebHistory } from 'vue-router'
import { getAccessToken } from '../../Servicos/GetAcessToken'
import {getRoleFromJWT} from '../../Servicos/GetRoleFromJwt'
import Login from '@/components/Login.vue'
import dashboardAdmin from '@/components/adm/dashboardAdmin.vue'
import dashboardAgente from '@/components/SubAgentes/dashboardAgente.vue'
import AdicionarAgentes from '@/components/adm/AdicionarAgentes.vue'
import Pedido from '@/components/SubAgentes/Pedido.vue'
import verAgentes from '@/components/adm/verAgentes.vue'
import PedidosAdm from '@/components/adm/PedidosAdm.vue'
import todosRelatorios from '@/components/adm/todosRelatorios.vue'
import RelatorioAgente from '@/components/adm/RelatorioAgente.vue'
import verPerfilAgente from '@/components/SubAgentes/verPerfilAgente.vue'
import verPerfilAdm from '@/components/adm/verPerfilAdm.vue'
import Relatorio from '@/components/SubAgentes/Relatorio.vue'
// Definição das rotas
const routes = [
  {
    path: '/',
    name: 'Login',
    component: Login,
  },
  {
    path: '/dashboardAdmin',
    name: 'dashboardAdmin',
    component: dashboardAdmin,
    meta: { requiresAuth: true },
  },
  {
    path: '/adicionarAgentes',
    name: 'AdicionarAgentes',
    component: AdicionarAgentes,
    meta: { requiresAuth: true },
  },
  {
    path: '/dashboardAgente',
    name: 'dashboardAgente',
    component: dashboardAgente,
    meta: { requiresAuth: true },
  },
  {
    path: '/pedido',
    name: 'Pedido',
    component: Pedido,
    meta: { requiresAuth: true },
  },
  {
    path: '/verAgentes',
    name: 'GerenciarAgentes',
    component: verAgentes,
    meta: { requiresAuth: true },
  },
  {
    path: '/PedidosAdm',
    name: 'PedidosAdm',
    component: PedidosAdm,
    meta: { requiresAuth: true },
  },
  {
    path: '/todosRelatorios',
    name: 'todosRelatorios',
    component: todosRelatorios,
    meta: { requiresAuth: true },

  },
  {
    path: '/RelatorioAgente',
    name: 'RelatorioAgente',
    component: RelatorioAgente,
    meta: { requiresAuth: true },

  },
  {
    path: '/verPerfilAgente',
    name: 'verPerfilAgente',
    component: verPerfilAgente,
    meta: { requiresAuth: true },
  },
  {
    path: '/verPerfilAdm/:agente',
    name: 'verPerfilAdm',
    component: verPerfilAdm,
    props: route => ({
      agente: JSON.parse(route.params.agente)
    }),
    meta: { requiresAuth: true },
  },
  {
    path: '/Relatorio',
    name: 'Relatorio',
    component: Relatorio,
    meta: { requiresAuth: true },

  }
]
// Criação do router
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
})

// Proteção de rotas
// Proteção de rotas
router.beforeEach(async (to, from, next) => {
  console.log('Verificando autenticação para a rota:', to.name)

  const accessToken = sessionStorage.getItem('accessToken')
  const refreshToken = localStorage.getItem('refreshToken')

  if (to.meta.requiresAuth) {
    if (!accessToken) {
      console.warn('Access token não encontrado, buscando o refresh token...')
      const newAccessToken = await getAccessToken(refreshToken)
      console.log('Novo Access Token:', newAccessToken)
      if (!newAccessToken) {
        console.warn('Falha ao obter novo Access Token, redirecionando para a página de login...')
        return next('/')
      }
    }

    // Verifica role se for uma rota de administrador
    const token = sessionStorage.getItem('accessToken')
    const role = getRoleFromJWT(token)

    const rotasAdmin = ['dashboardAdmin', 'AdicionarAgentes', 'GerenciarAgentes']
    if (rotasAdmin.includes(to.name) && role !== 'adm') {
      console.warn('Usuário não tem permissão para acessar esta rota:', to.name)
      return next('/dashboardAgente') // Redireciona para uma rota permitida
    }
  } else {
    if (refreshToken) {
      console.warn('Refresh token encontrado, buscando o token de acesso...')
      try {
        await getAccessToken(refreshToken)
        const newToken = sessionStorage.getItem('accessToken')
        if (newToken) {
          const role = getRoleFromJWT(newToken)
          if (to.path === '/' && newToken) {
            return next(role === 'user' ? '/dashboardAgente' : '/dashboardAdmin')
          }
        }
      } catch (error) {
        console.error('Erro ao renovar token:', error)
        return next('/')
      }
    }
  }

  return next()
})
export default router
