<template>
  <div>

    <b-card
      no-body
      class="overflow-hidden"
      border-variant="primary"
      
    >
    <b-card-header header-bg-variant="info" header-text-variant="white"><h3>Mi negocio</h3></b-card-header>
    <p>Bienvenido, entra a la cuenta de tu negocio</p>

    <b-form @submit="onSubmit" v-if="show">
      <b-form-group
        class="add-style"
        label="Correo electrónico:"
        label-for="input-correo"
      >
        <b-form-input
          id="input-correo"
          v-model="form.correo"
          type="email"
          placeholder="Ingresa tu correo electrónico"
          maxlength="80"
          required
        ></b-form-input>
      </b-form-group>

      <b-form-group
            class="add-style"
            id="grupoContrasena"
            label="Contraseña:"
            label-for="input-contrasena"
          >
            <b-form-input
              id="input-contrasena"
              v-model="form.contrasena"
              type="password"
              placeholder="Ingresa tu contraseña"
              maxlength="50"
              minlength="8"
              required
            ></b-form-input>
          </b-form-group>

      <b-button id="btnIniciarSesion" type="submit" variant="primary">Iniciar sesión</b-button>
    </b-form>
      <b-card-footer>
        <b-row no-gutters>
          <b-col md="6">
            <p>¿No tienes cuenta?</p>
          </b-col>
          <b-col md="6">
            <b-button variant="primary" :to="{ name: 'RegistrarNegocio' }"
              >Registra tu negocio</b-button
            >
          </b-col>
        </b-row>
      </b-card-footer>
    </b-card>
  </div>
</template>

<script>
import axios from "axios";
import { EventBus } from './bus.js';
export default {
  name: "Login",
  data() {
    return {
      form: {
        correo: "",
        contrasena: "",
      },
      show: true,
    };
  },

  methods: {
    onSubmit(event) {
      event.preventDefault();
      axios
        .post("https://localhost:5001/AforoMex/Usuarios/login", {
          Correo: this.form.correo,
          Contrasena: this.form.contrasena
        })
        .then((response) => {
          if (response.data != null) {
            localStorage.setItem("idUsuario", response.data.idUsuario);
            localStorage.setItem("usuario", response.data.correo);
            localStorage.setItem("rol", response.data.rol);
            EventBus.$emit('iniciarSesion');
            this.$router.push({ name: "Inicio"})
          } else {
            alert("Datos incorrectos. Revíselos e inténtelo de nuevo");
          }
        })
        .catch((e) => {
          console.log(e);
          alert(
            "Error al conectar con el servidor. Revise su conexión a internet e inténtelo de nuevo"
          );
        });
    },
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.add-style {
  text-align: left;
  max-width: 400px;
  margin-left: 50px;
  margin-right: 50px;
  margin-top: 30px;
}
#btnIniciarSesion {
  margin-top: 50px;
  margin-bottom: 50px;
}
.overflow-hidden {
  max-width: 500px;
  margin-top: 50px;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 100px;
}
p, h3 {
  margin-top: 8px;
}
</style>