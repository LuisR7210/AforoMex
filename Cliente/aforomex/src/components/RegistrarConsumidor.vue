<template>
  <div>
    <br />
    <h1>Crea una nueva cuenta</h1>
    <br />
    <div id="contenedorFormulario">
      <b-form @submit="onSubmit">
        <div class="formulario">
          <b-form-group class="add-style" id="grupoNombre" label="Nombre(s):" label-for="input-nombre">
            <b-form-input id="input-nombre" v-model="form.nombre" placeholder="Ingresa tu nombre" maxlength="50" required></b-form-input>
          </b-form-group>
          <b-form-group class="add-style" id="grupoApellidos" label="Apellidos:" label-for="input-apellidos">
            <b-form-input id="input-apellidos" v-model="form.apellidos" placeholder="Ingresa tus apellidos" maxlength="50" required></b-form-input>
          </b-form-group>

          <b-form-group class="add-style" id="grupoTelefono" label="Teléfono:" label-for="input-telefono">
            <b-form-input id="input-telefono" v-model="form.telefono" type="tel" placeholder="Ingresa tu número telefónico" maxlength="15" required>
            </b-form-input>
          </b-form-group>

          <b-form-group class="add-style" id="grupoCorreo" label="Correo electrónico:" label-for="input-correo">
            <b-form-input id="input-correo" v-model="form.correo" type="email" placeholder="Ingresa tu correo electrónico" maxlength="80" required>
            </b-form-input>
          </b-form-group>

          <b-form-group class="add-style" id="grupoNacimiento" label="Fecha de Nacimiento:" label-for="input-nacimiento">
            <b-form-input id="input-nacimiento" v-model="form.nacimiento" type="date" placeholder="Ingresa tu fecha de nacimiento" min="1900-01-01"
              v-bind:max="hoy" required></b-form-input>
          </b-form-group>

          <b-form-group class="add-style" id="grupoContrasena" label="Contraseña:" label-for="input-contrasena">
            <b-form-input id="input-contrasena" v-model="form.contrasena" type="password" placeholder="Ingresa una contraseña" maxlength="50"
              minlength="8" required></b-form-input>
          </b-form-group>
          <b-button class="botones" type="submit" variant="primary">Registrarme</b-button>
        </div>
      </b-form>
    </div>

    <b-modal id="alerta" centered ok-only title="Error al cargar las reservaciones" header-bg-variant="danger" header-text-variant="light">
      <p class="my-4">{{ mensajeAlerta }}</p>
    </b-modal>
  </div>
</template>

<script>
import axios from "axios";
import { EventBus } from "./bus.js";
export default {
  name: "RegistrarConsumidor",

  data() {
    return {
      hoy: new Date().toISOString().slice(0, 10),
      mensajeAlerta: "",
      form: {
        nombre: "",
        apellidos: "",
        telefono: "",
        correo: "",
        nacimiento: "",
        contrasena: "",
      },
    };
  },

  methods: {
    onSubmit(event) {
      event.preventDefault();
      var vm = this;
      axios
        .post("https://localhost:5001/AforoMex/Usuarios", {
          Nombre: this.form.nombre,
          Apellidos: this.form.apellidos,
          Telefono: this.form.telefono,
          Correo: this.form.correo,
          FechaNacimiento: this.form.nacimiento,
          Contrasena: this.form.contrasena,
          Rol: "consumidor",
        })
        .then((response) => {
          localStorage.setItem("idUsuario", response.data.objeto.idUsuario);
          localStorage.setItem("usuario", response.data.objeto.nombre);
          localStorage.setItem("rol", response.data.objeto.rol);
          EventBus.$emit("iniciarSesion");
          this.$router.push({ name: "Inicio" });
        })
        .catch((e) => {
          if (e.response.data.error && e.response.status == 409) {
            vm.mensajeAlerta = e.response.data.mensaje;
          } else {
            vm.mensajeAlerta =
              "Error en el servidor de AforoMex. Inténtelo de nuevo más tarde.";
          }
          vm.$bvModal.show("alerta");
        });
    },
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.formulario {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
}
.add-style {
  text-align: left;
  width: 350px;
  margin-left: 10%;
  margin-right: 10%;
  margin-bottom: 10px;
  margin-top: 30px;
}
#contenedorFormulario {
  max-width: 80%;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 100px;
}
.botones {
  margin-left: 10%;
  margin-right: 10%;
  margin-top: 50px;
}
</style>