<template>
  <div>
    <br />
    <h1>Datos de tu cuenta de AforoMex</h1>
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

          <b-form-group class="add-style" id="grupoTelefono" label="Teléfono:" label-for="input-telefono" description="Solo números">
            <b-form-input id="input-telefono" v-model="form.telefono" type="tel" placeholder="Ingresa tu número telefónico" 
            maxlength="15" required pattern="[0-9]+">
            </b-form-input>
          </b-form-group>

          <b-form-group class="add-style" id="grupoCorreo" label="Correo electrónico:" label-for="input-correo">
            <b-form-input id="input-correo" v-model="form.correo" type="email" placeholder="Ingresa tu correo electrónico" maxlength="80" required
              readonly>
            </b-form-input>
          </b-form-group>

          <b-form-group class="add-style" id="grupoNacimiento" label="Fecha de Nacimiento:" label-for="input-nacimiento">
            <b-form-input id="input-nacimiento" v-model="form.fechaNacimiento" type="date" placeholder="Ingresa tu fecha de nacimiento"
              min="1900-01-01" v-bind:max="hoy" required></b-form-input>
          </b-form-group>

          <b-form-group class="add-style" id="grupoContrasena" label="Contraseña:" label-for="input-contrasena">
            <b-input-group>
              <b-form-input id="input-contrasena" v-model="form.contrasena" :type="passwordInputType" placeholder="Ingresa una contraseña"
                maxlength="50" minlength="8" required></b-form-input>
              <b-input-group-append>
                <b-button v-on:click="cambiarVisibilidad">
                  <b-icon icon="eye"></b-icon>
                </b-button>
              </b-input-group-append>
            </b-input-group>
          </b-form-group>

          <b-button class="botones" type="submit" variant="primary">Guardar cambios</b-button>
        </div>
      </b-form>
    </div>

    <b-modal id="alertaExito" centered ok-only title="Éxito al guardar los cambios" header-bg-variant="success" header-text-variant="light">
      <p class="my-4">Cambios de los datos de la cuenta guardados con éxito.</p>
    </b-modal>
    <b-modal id="alerta" centered ok-only title="Error" header-bg-variant="danger" header-text-variant="light">
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
        fechaNacimiento: "",
        contrasena: "",
      },
      passwordInputType: "password",
    };
  },

  created: function () {
    var vm = this;
    axios
      .get(
        "https://localhost:5001/AforoMex/Usuarios/" + localStorage["idUsuario"]
      )
      .then(function (response) {
        let perfil = response.data;
        perfil.fechaNacimiento = vm.$luxon(
          perfil.fechaNacimiento,
          "yyyy-MM-dd"
        );
        vm.form = perfil;
      })
      .catch(function () {
        vm.mensajeAlerta =
          "Error con el servidor de AforoMex al cargar el perfil. Inténtelo de nuevo más tarde.";
        vm.$bvModal.show("alerta");
      });
  },

  methods: {
    cambiarVisibilidad: function () {
      this.passwordInputType =
        this.passwordInputType === "password" ? "text" : "password";
    },
    onSubmit(event) {
      event.preventDefault();
      var vm = this;
      axios
        .put(
          "https://localhost:5001/AforoMex/Usuarios/" +
            localStorage["idUsuario"],
          {
            IdUsuario: localStorage["idUsuario"],
            Nombre: this.form.nombre,
            Apellidos: this.form.apellidos,
            Telefono: this.form.telefono,
            Correo: this.form.correo,
            FechaNacimiento: this.form.fechaNacimiento,
            Contrasena: this.form.contrasena,
          }
        )
        .then(() => {
          localStorage.setItem("usuario", vm.form.nombre);
          EventBus.$emit("iniciarSesion");
          vm.$bvModal.show("alertaExito");
        })
        .catch(() => {
          vm.mensajeAlerta =
            "Error al guardar los cambios en el servidor de AforoMex. Inténtelo de nuevo más tarde.";
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