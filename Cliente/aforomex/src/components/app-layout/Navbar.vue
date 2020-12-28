<template>
  <div>
    <b-navbar id="navbar" toggleable="sm" type="dark" variant="dark">
      <b-button v-b-toggle.barraLateral id="botonLateral" :hidden="!ocultarBtnLogin">
        <b-icon icon="columns-gap" font-scale="1.5"></b-icon>
      </b-button>

      <b-navbar-brand href="#" id="titulo" :to="{ name: 'Inicio' }">
        <img id="logo" src="./../../assets/logoCompleto.png" alt="logo" />
      </b-navbar-brand>

      <b-navbar-toggle target="navbarColapsada">
        <template #default="{ expanded }">
          <b-icon v-if="expanded" icon="chevron-bar-up" font-scale="1.3" class="iconoColapsado"></b-icon>
          <b-icon v-else icon="chevron-bar-down" font-scale="1.3" class="iconoColapsado"></b-icon>
        </template>
      </b-navbar-toggle>

      <b-collapse id="navbarColapsada" is-nav>

        <b-input-group id="barraBusqueda">
          <b-form-input placeholder="Busca un negocio por su nombre"></b-form-input>
          <b-input-group-append>
            <b-button variant="outline-warning">
              <b-icon icon="search"></b-icon>
            </b-button>
          </b-input-group-append>
        </b-input-group>

        <b-navbar-nav id="navbarUsuario" class="ml-auto">
          <b-button variant="outline-info" :to="{ name: 'LoginNegocio' }" :hidden="ocultarBtnLogin" class="botonesNavbar">Mi negocio</b-button>
          <b-button variant="outline-light" :to="{ name: 'Login' }" :hidden="ocultarBtnLogin" class="botonesNavbar">Iniciar sesión / Registrarme
          </b-button>

          <b-nav-item-dropdown :text="usuario" toggle-class="text-light" :hidden="!ocultarBtnLogin" right>
            <b-dropdown-item :hidden="ocultarNegocio">Mi negocio</b-dropdown-item>
            <b-dropdown-item href="#">Mi perfil</b-dropdown-item>
            <b-dropdown-item v-on:click="cerrarSesion">Cerrar sesión</b-dropdown-item>
          </b-nav-item-dropdown>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>

    <b-sidebar id="barraLateral" shadow bg-variant="dark" text-variant="light" backdrop title="Tareas disponibles">
      <template #footer="{ hide }">
        <div class="d-flex align-items-center px-3 py-2">
          <strong class="mr-auto">AforoMex {{ year }} ®</strong>
          <b-button size="sm" @click="hide">Cerrar</b-button>
        </div>
      </template>
      <div>
        <b-img :src="require('./../../assets/logo.png')" fluid id="logoLateral"></b-img>
      </div>

      <b-nav vertical :hidden="!ocultarNegocio">
        <b-nav-item :to="{ name: 'AgendaConsumidor' }" link-classes="text-light">Ver mi agenda</b-nav-item>
      </b-nav>
      <b-nav vertical :hidden="ocultarNegocio">
        <b-nav-item link-classes="text-light">Actualizar cupo</b-nav-item>
        <b-nav-item link-classes="text-light">Ver Agenda del negocio</b-nav-item>
      </b-nav>
    </b-sidebar>
  </div>
</template>

<script>
import { EventBus } from "./../bus.js";
export default {
  name: "Navbar",
  data() {
    return {
      year: new Date().getFullYear(),
      ocultarBtnLogin: false,
      ocultarNegocio: true,
      usuario: "Usuario",
    };
  },
  created: function () {
    EventBus.$on("iniciarSesion", () => {
      this.iniciarSesion();
    });
    this.iniciarSesion();
  },
  methods: {
    iniciarSesion: function () {
      if (
        localStorage["idUsuario"] != null &&
        localStorage["usuario"] != null &&
        localStorage["rol"] != null
      ) {
        this.ocultarBtnLogin = true;
        this.usuario = localStorage["usuario"];
        if (localStorage["rol"] == "negocio") {
          this.ocultarNegocio = false;
        } else {
          this.ocultarNegocio = true;
        }
      } else {
        this.ocultarBtnLogin = false;
      }
    },
    cerrarSesion: function () {
      localStorage.removeItem("correo");
      localStorage.removeItem("idUsuario");
      localStorage.removeItem("rol");
      this.iniciarSesion();
    },
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
#logo {
  height: 30px;
}
#logoLateral {
  width: 100px;
  margin-bottom: 50px;
}
#botonLateral {
  background-color: transparent;
  border: 0px;
  padding: 4px 5px 0px 5px;
  margin-right: 4%;
}
.iconoColapsado {
  color: white;
}
.nav-item:hover {
  background-color: rgba(0, 255, 255, 0.3);
  border-radius: 25px;
}
#barraBusqueda {
  max-width: 600px;
  margin-left: auto;
  margin-right: auto;
  margin-top: 5px;
  margin-bottom: 5px;
}
.botonesNavbar {
  margin-right: 10px;
  margin-top: 5px;
  margin-bottom: 5px;
}
</style>