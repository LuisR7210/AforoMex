<template>
  <div>
    <br />
    <h1>Registra tu propio negocio</h1>
    <br />
    <!-- Formulario datos personales -->
    <div class="contenedorFormulario" :hidden="ocultarPersonales">
      <h5>Datos personales del administrador del negocio (paso 1 de 5)</h5>
      <b-form @submit="onSubmitPersonales">
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

          <b-form-group class="add-style" id="grupoNacimiento" label="Fecha de Nacimiento:" label-for="input-nacimiento">
            <b-form-input id="input-nacimiento" v-model="form.nacimiento" type="date" placeholder="Ingresa tu fecha de nacimiento" min="1900-01-01"
              v-bind:max="hoy" required></b-form-input>
          </b-form-group>

          <b-form-group class="add-style" id="grupoCorreo" label="Correo electrónico:" label-for="input-correo"
            description="Con este correo deberás iniciar sesión en Mi Negocio">
            <b-form-input id="input-correo" v-model="form.correo" type="email" placeholder="Ingresa tu correo electrónico" maxlength="80" required>
            </b-form-input>
          </b-form-group>

          <b-form-group class="add-style" id="grupoContrasena" label="Contraseña:" label-for="input-contrasena"
            description="Con esta contraseña deberás iniciar sesión en Mi Negocio">
            <b-form-input id="input-contrasena" v-model="form.contrasena" type="password" placeholder="Ingresa una contraseña" maxlength="50"
              minlength="8" required></b-form-input>
          </b-form-group>
          <b-button class="botones" type="submit" variant="primary">Continuar</b-button>
        </div>
      </b-form>
    </div>
    <!-- Formulario datos negocio -->
    <div class="contenedorFormulario" :hidden="ocultarDatosNegocio">
      <h5>Datos de tu negocio (paso 2 de 5)</h5>
      <p>¡Atención: todos estos datos serán mostrados al público!</p>
      <b-form @submit="onSubmitNegocio">
        <div class="formulario">
          <b-form-group class="add-style" label="Nombre:" label-for="input-nombreNegocio">
            <b-form-input id="input-nombreNegocio" v-model="formNegocio.nombre" placeholder="Ingresa el nombre de tu negocio" maxlength="50" required>
            </b-form-input>
          </b-form-group>

          <b-form-group class="add-style" label="Categoría:" label-for="input-categoria">
            <b-form-input id="input-categoria" v-model="formNegocio.categoria" placeholder="Ingresa la categoría de tu negocio" maxlength="50"
              required></b-form-input>
          </b-form-group>

          <b-form-group class="add-style" id="grupoCorreo" label="Correo electrónico de contacto:" label-for="input-correoNegocio">
            <b-form-input id="input-correoNegocio" v-model="formNegocio.correo" type="email" placeholder="Ingresa el correo de tu negocio"
              maxlength="80" required></b-form-input>
          </b-form-group>

          <b-form-group class="add-style" label="Teléfono de contacto:" label-for="input-telefonoNegocio">
            <b-form-input id="input-telefonoNegocio" v-model="formNegocio.telefono" type="tel"
              placeholder="Ingresa el número telefónico de tu negocio" maxlength="15" required></b-form-input>
          </b-form-group>

          <b-form-group class="add-style" label="Celular:" label-for="input-celular" description="Opcional">
            <b-form-input id="input-celular" v-model="formNegocio.celular" type="tel" placeholder="Ingresa el número de celular de tu negocio"
              maxlength="15"></b-form-input>
          </b-form-group>

          <b-form-group class="add-style" label="Sitio web:" label-for="input-sitioWeb" description="Opcional">
            <b-form-input id="input-sitioWeb" v-model="formNegocio.sitioWeb" type="url" placeholder="Ingresa la url del sitio web de tu negocio"
              maxlength="50"></b-form-input>
          </b-form-group>

          <b-form-group class="add-style" label="Descripción:" label-for="textarea-descripcion">
            <b-form-textarea id="textarea-descripcion" placeholder="Describe tu negocio" maxlength="500" v-model="formNegocio.descripcion" required>
            </b-form-textarea>
          </b-form-group>

          <b-form-group class="add-style" label="Red social:" label-for="input-redSocial" description="Opcional">
            <b-form-input id="input-redSocial" v-model="formNegocio.facebook" placeholder="Ingresa la red social de tu negocio" maxlength="50">
            </b-form-input>
          </b-form-group>

          <b-button class="botones" variant="secondary" v-on:click="regresarAdministrador">Regresar</b-button>
          <b-button class="botones" type="submit" variant="primary">Continuar</b-button>
        </div>
      </b-form>
    </div>

    <!-- Formulario dirrección negocio -->
    <div class="contenedorFormulario" :hidden="ocultarDireccionNegocio">
      <h5>Dirección de tu negocio (paso 3 de 5)</h5>
      <p>¡Atención: todos estos datos serán mostrados al público!</p>
      <b-form @submit="onSubmitDireccion">
        <div class="formulario">
          <b-form-group class="add-style" label="Código postal (CP):" label-for="input-codigoPostal"
            description="Al dar click afuera de este campo se buscará el estado, municipio y las colonias correspondientes al CP ingresado">
            <b-form-input id="input-codigoPostal" v-model="formDireccion.codigoPostal" placeholder="Ingresa el código postal de tu negocio"
              maxlength="10" required v-on:focusout="buscarPorCP">
            </b-form-input>
          </b-form-group>

          <b-form-group class="add-style" label="Estado:" label-for="select-estado">
            <b-form-input id="select-estado" v-model="formDireccion.estado" type="text" maxlength="100" required readonly></b-form-input>
          </b-form-group>

          <b-form-group class="add-style" label="Ciudad:" label-for="select-ciudad">
            <b-form-input id="select-ciudad" v-model="formDireccion.ciudad" type="text" maxlength="100" required readonly></b-form-input>
          </b-form-group>

          <b-form-group class="add-style" label="Colonia:" label-for="select-colonia">
            <b-form-select id="select-colonia" v-model="formDireccion.colonia" :options="colonias" required></b-form-select>
          </b-form-group>

          <b-form-group class="add-style" label="Calle:" label-for="input-calle">
            <b-form-input id="input-calle" v-model="formDireccion.calle" placeholder="Ingresa la calle donde está tu negocio" maxlength="100"
              required>
            </b-form-input>
          </b-form-group>

          <div class="numeros">
            <b-form-group class="groupNumeros" label="Número exterior:" label-for="input-numero" description="Requerido">
              <b-form-input id="input-numero" v-model="formDireccion.numero" type="number" max="999999" required min="0">
              </b-form-input>
            </b-form-group>
            <b-form-group class="groupNumeros" label="Número interior:" label-for="input-numeroInt" description="Opcional">
              <b-form-input id="input-numeroInt" v-model="formDireccion.numeroInterior" type="number" max="999999" min="0">
              </b-form-input>
            </b-form-group>
          </div>

          <b-button class="botones" variant="secondary" v-on:click="regresarNegocio">Regresar</b-button>
          <b-button class="botones" type="submit" variant="primary">Continuar</b-button>
        </div>
      </b-form>
    </div>

    <!-- Formulario horarios negocio -->
    <div class="contenedorFormulario" :hidden="ocultarHorariosNegocio">
      <h5>Horarios de atención de tu negocio (paso 4 de 5)</h5>
      <p>¡Atención: todos estos datos serán mostrados al público!</p>
      <div class="formulario">
        <div class="add-style">
          <b-table hover :items="horarios" :fields="columnasHorarios" select-mode="multi" ref="selectableTable" selectable
            @row-selected="onRowSelected"></b-table>
          <b-button variant="secondary" size="sm" class="botonesJuntos" v-on:click="deseleccionarTodos">Deseleccionar todos</b-button>
          <b-button variant="secondary" size="sm" class="botonesJuntos" v-on:click="eliminarDias">Eliminar días</b-button>
        </div>
        <div class="add-style">
          <b-form @submit="onSubmitHorario">
            <b-form-group label="Día de la semana:" label-for="select-dia">
              <b-form-select v-model="formHorario.dia" :options="dias" required></b-form-select>
            </b-form-group>
            <b-form-group label="Hora de apertura (24hrs):" label-for="select-inicio">
              <b-form-input id="select-inicio" v-model="formHorario.horaInicio" type="time" required></b-form-input>
            </b-form-group>
            <b-form-group label="Hora de cierre (24hrs):" label-for="select-fin">
              <b-form-input id="select-fin" v-model="formHorario.horaFin" type="time" required></b-form-input>
            </b-form-group>
            <b-button type="submit" variant="info">Agregar al horario</b-button>
          </b-form>
          <b-button variant="secondary" class="botonesJuntos" v-on:click="regresarDireccion" style="margin-top: 75px">Regresar</b-button>
          <b-button variant="primary" class="botonesJuntos" v-on:click="continuarFinal" style="margin-top: 75px">Continuar</b-button>
        </div>
      </div>
    </div>

    <!-- Formulario aforo negocio -->
    <div class="contenedorFormulario" :hidden="ocultarAforoNegocio">
      <h5>Aforo y reservaciones (paso 5 de 5)</h5>
      <p>¡Atención: todos estos datos serán mostrados al público!</p>
      <b-form @submit="onSubmitAforo">
        <div class="formulario">
          <b-form-group class="add-style" label="Aforo actual:" label-for="input-aforo"
            description="Es el número máximo de personas permitido en tu negocio">
            <b-form-input id="input-aforo" v-model="formNegocio.aforo" placeholder="Ingresa el aforo actual (en no. de personas)" max="999999"
              required type="number" min="0">
            </b-form-input>
          </b-form-group>

          <b-form-checkbox v-model="formNegocio.permitirReservaciones" switch class="add-style">
            ¿Desea permitir hacerse reservaciones a su negocio?
          </b-form-checkbox>

          <b-form-group class="add-style" label="Porcentaje del aforo para reservaciones:" label-for="input-porcentajeR"
            description="Es el porcentaje del aforo reservado para reservaciones exclusivamente">
            <b-form-input id="input-porcentajeR" v-model="formNegocio.porcentajeReservaciones" placeholder="Ingresa el porcentaje (del 1 al 100)"
              max="100" required type="number" min="1" :disabled="!formNegocio.permitirReservaciones">
            </b-form-input>
          </b-form-group>

          <b-form-group class="add-style" label="Limite de personas por reservación:" label-for="input-limiteR"
            description="Es el número límite de personas que pueden asistir en una sola reservación">
            <b-form-input id="input-limiteR" v-model="formNegocio.limitePorReservacion" placeholder="Ingresa el numero de personas"
              :max="formNegocio.aforo" required type="number" min="1" :disabled="!formNegocio.permitirReservaciones">
            </b-form-input>
          </b-form-group>

          <b-button class="botones" variant="secondary" v-on:click="regresarHorarios">Regresar</b-button>
          <b-button class="botones" type="submit" variant="primary">Terminar registro</b-button>
        </div>
      </b-form>
    </div>

    <b-modal id="alerta" centered ok-only title="Error al iniciar sesión" header-bg-variant="danger" header-text-variant="light">
      <p class="my-4"> {{ mensajeAlerta }}</p>
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
      ocultarPersonales: false,
      form: {
        nombre: "",
        apellidos: "",
        telefono: "",
        correo: "",
        nacimiento: "",
        contrasena: "",
      },
      ocultarDatosNegocio: true,
      formNegocio: {
        nombre: "",
        categoria: "",
        telefono: "",
        correo: "",
        sitioWeb: "",
        celular: "",
        descripcion: "",
        facebook: "",
        aforo: 0,
        permitirReservaciones: false,
        porcentajeReservaciones: "",
        limitePorReservacion: "",
      },
      ocultarDireccionNegocio: true,
      formDireccion: {
        codigoPostal: "",
        estado: "",
        ciudad: "",
        colonia: "",
        calle: "",
        numero: "",
        numeroInterior: "",
        pais: "México",
      },
      colonias: [],
      ocultarHorariosNegocio: true,
      formHorario: {
        dia: "Lunes a viernes",
        horaInicio: "",
        horaFin: "",
      },
      dias: [
        "Lunes a viernes",
        "Lunes",
        "Martes",
        "Miércoles",
        "Jueves",
        "Viernes",
        "Sábado",
        "Domingo",
      ],
      columnasHorarios: [
        { key: "dia", label: "Día de la semama" },
        { key: "horaInicio", label: "Hora de apertura" },
        { key: "horaFin", label: "Hora de cierre" },
      ],
      horarios: [
        { dia: "Lunes", horaInicio: "", horaFin: "" },
        { dia: "Martes", horaInicio: "", horaFin: "" },
        { dia: "Miércoles", horaInicio: "", horaFin: "" },
        { dia: "Jueves", horaInicio: "", horaFin: "" },
        { dia: "Viernes", horaInicio: "", horaFin: "" },
        { dia: "Sábado", horaInicio: "", horaFin: "" },
        { dia: "Domingo", horaInicio: "", horaFin: "" },
      ],
      horariosSeleccionados: [],
      ocultarAforoNegocio: true,
    };
  },

  methods: {
    //Funciones datos del administrador
    onSubmitPersonales(event) {
      event.preventDefault();
      var vm = this;
      axios
        .post("https://localhost:5001/AforoMex/Usuarios/verificarCorreo", {
          correo: vm.form.correo,
        })
        .then(() => {
          vm.ocultarDatosNegocio = false;
          vm.ocultarPersonales = true;
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
    //Funciones datos del negocio
    onSubmitNegocio(event) {
      event.preventDefault();
      this.ocultarDatosNegocio = true;
      this.ocultarDireccionNegocio = false;
    },
    regresarAdministrador: function () {
      this.ocultarDatosNegocio = true;
      this.ocultarPersonales = false;
    },
    //Funciones dirección del negocio
    onSubmitDireccion(event) {
      event.preventDefault();
      this.ocultarHorariosNegocio = false;
      this.ocultarDireccionNegocio = true;
    },
    regresarNegocio: function () {
      this.ocultarDatosNegocio = false;
      this.ocultarDireccionNegocio = true;
    },
    buscarPorCP: function () {
      if (this.formDireccion.codigoPostal == "") {
        return;
      }
      var vm = this;
      axios
        .get(
          "https://api-sepomex.hckdrk.mx/query/info_cp/" +
            this.formDireccion.codigoPostal,
          {
            params: {
              type: "simplified",
            },
          }
        )
        .then((response) => {
          vm.formDireccion.estado = response.data.response.estado;
          vm.formDireccion.ciudad = response.data.response.municipio;
          vm.colonias = response.data.response.asentamiento;
        })
        .catch(function (error) {
          vm.formDireccion.codigoPostal = "";
          vm.formDireccion.ciudad = "";
          vm.formDireccion.estado = "";
          vm.colonias = [];
          if (error.response.data.code_error == 105) {
            alert(
              "No existe ese código postal. Corrígelo e inténtalo de nuevo"
            );
          } else {
            alert(
              "Error al conectar con el servidor. Revise su conexión a internet e inténtelo de nuevo"
            );
          }
        });
    },
    //Funciones horario del negocio
    onSubmitHorario(event) {
      event.preventDefault();
      var nuevoDia = Object.assign({}, this.formHorario);
      if (this.formHorario.dia === "Lunes a viernes") {
        for (let index = 0; index < 5; index++) {
          this.horarios[index].horaInicio = nuevoDia.horaInicio;
          this.horarios[index].horaFin = nuevoDia.horaFin;
        }
      } else {
        var indice = this.horarios.findIndex(
          (o) => o.dia === this.formHorario.dia
        );
        this.horarios[indice].horaInicio = nuevoDia.horaInicio;
        this.horarios[indice].horaFin = nuevoDia.horaFin;
      }
    },
    deseleccionarTodos() {
      this.$refs.selectableTable.clearSelected();
      this.horariosSeleccionados = [];
    },
    onRowSelected(items) {
      this.horariosSeleccionados = items;
    },
    eliminarDias: function () {
      this.horariosSeleccionados.forEach((element) => {
        var indice = this.horarios.findIndex((o) => o.dia === element.dia);
        this.horarios[indice].horaInicio = "";
        this.horarios[indice].horaFin = "";
      });
    },
    continuarFinal(event) {
      event.preventDefault();
      if (
        !this.horarios.some(
          (element) => element.horaInicio != "" && element.horaFin != ""
        )
      ) {
        alert("Debes agregar por lo menos un dia de horario de atención");
        return;
      }
      this.ocultarAforoNegocio = false;
      this.ocultarHorariosNegocio = true;
    },
    regresarDireccion: function () {
      this.ocultarHorariosNegocio = true;
      this.ocultarDireccionNegocio = false;
    },
    regresarHorarios: function () {
      this.ocultarHorariosNegocio = false;
      this.ocultarAforoNegocio = true;
    },
    onSubmitAforo(event) {
      event.preventDefault();
      this.registrarNegocio();
    },
    registrarNegocio: function () {
      let horariosVerificados = this.horarios.filter(
        (horario) => horario.horaInicio != "" && horario.horaFin != ""
      );
      if (!this.formNegocio.permitirReservaciones) {
        this.formNegocio.porcentajeReservaciones = 0;
        this.formNegocio.limitePorReservacion = 0;
      }
      if (this.formDireccion.numeroInterior == "") {
        this.formDireccion.numeroInterior = 0;
      }
      this.formDireccion.codigoPostal = parseInt(
        this.formDireccion.codigoPostal
      );
      this.formDireccion.numero = parseInt(this.formDireccion.numero);
      this.formDireccion.numeroInterior = parseInt(
        this.formDireccion.numeroInterior
      );
      var vm = this;
      axios
        .post("https://localhost:5001/AforoMex/Negocios", {
          nombre: this.formNegocio.nombre,
          categoria: this.formNegocio.categoria,
          telefono: this.formNegocio.telefono,
          correo: this.formNegocio.correo,
          sitioWeb: this.formNegocio.sitioWeb,
          celular: this.formNegocio.celular,
          descripcion: this.formNegocio.descripcion,
          facebook: this.formNegocio.facebook,
          aforo: parseInt(this.formNegocio.aforo),
          permitirReservaciones: this.formNegocio.permitirReservaciones,
          aforoReservaciones:
            parseFloat(this.formNegocio.porcentajeReservaciones) / 100,
          limiteReservaciones: parseInt(this.formNegocio.limitePorReservacion),
          idUsuario: parseInt(localStorage["idUsuario"]),
          direcciones: [this.formDireccion],
          horarios: horariosVerificados,
          idUsuarioNavigation: {
            Nombre: this.form.nombre,
            apellidos: this.form.apellidos,
            telefono: this.form.telefono,
            correo: this.form.correo,
            fechaNacimiento: this.form.nacimiento,
            contrasena: this.form.contrasena,
          },
        })
        .then((response) => {
          localStorage.setItem("idUsuario", response.data.idNegocio);
          localStorage.setItem("usuario", response.data.nombre);
          localStorage.setItem("rol", response.data.idUsuarioNavigation.rol);
          EventBus.$emit("iniciarSesion");
          this.$router.push({ name: "Inicio" });
        })
        .catch(() => {
          vm.mensajeAlerta =
            "Error en el servidor de AforoMex. Inténtelo de nuevo más tarde.";
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
  margin-left: 5%;
  margin-right: 5%;
  margin-bottom: 10px;
  margin-top: 30px;
}
.contenedorFormulario {
  max-width: 80%;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 100px;
  border-style: solid;
  border-color: blueviolet;
  border-width: 1px;
  border-radius: 15px;
}
.botones {
  margin-left: 10%;
  margin-right: 10%;
  margin-top: 50px;
  margin-bottom: 50px;
}
h5 {
  text-align: left;
  margin-left: 10%;
  margin-right: 10%;
  margin-top: 30px;
}
.numeros {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  width: 350px;
  margin-left: 10%;
  margin-right: 10%;
  margin-bottom: 10px;
  margin-top: 30px;
  text-align: left;
}
.groupNumeros {
  margin-right: 5px;
  margin-left: 5px;
}
.botonesJuntos {
  margin-left: 5%;
  margin-right: 5%;
}
</style>