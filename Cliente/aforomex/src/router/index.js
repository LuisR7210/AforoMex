import Vue from 'vue'
import VueRouter from 'vue-router'
import Inicio from '@/components/Inicio'
import RegConsumidor from '@/components/RegistrarConsumidor'
import RegNegocio from '@/components/RegistrarNegocio'
import Login from '@/components/Login'
import LoginNegocio from '@/components/LoginNegocio'
import VerNegocio from '@/components/VerNegocio'
import AgendaConsumidor from '@/components/AgendaConsumidor'
import AgendaNegocio from '@/components/AgendaNegocio'
import PerfilUsuario from '@/components/PerfilUsuario'
import ActualizarCupo from '@/components/ActualizarCupo'
import EditarNegocio from '@/components/EditarNegocio'
import HacerReservacion from '@/components/HacerReservacion'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Inicio',
    component: Inicio
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/miNegocio',
    name: 'LoginNegocio',
    component: LoginNegocio
  },
  {
    path: '/nuevoConsumidor',
    name: 'RegistrarConsumidor',
    component: RegConsumidor
  },
  {
    path: '/nuevoNegocio',
    name: 'RegistrarNegocio',
    component: RegNegocio
  },
  {
    path: '/negocio/:id',
    name: 'VerNegocio',
    component: VerNegocio
  },
  {
    path: '/agendaConsumidor',
    name: 'AgendaConsumidor',
    component: AgendaConsumidor
  },
  {
    path: '/agendaNegocio',
    name: 'AgendaNegocio',
    component: AgendaNegocio
  },
  {
    path: '/perfil',
    name: 'PerfilUsuario',
    component: PerfilUsuario
  },
  {
    path: '/cupoNegocio',
    name: 'ActualizarCupo',
    component: ActualizarCupo
  },
  {
    path: '/editarNegocio',
    name: 'EditarNegocio',
    component: EditarNegocio
  },
  {
    path: '/HacerReservaciones/:id',
    name: 'HacerReservaciones',
    component: HacerReservacion
  }
]

const router = new VueRouter({
  mode: 'history',//checar implementacion en server
  routes: routes
})

export default router
