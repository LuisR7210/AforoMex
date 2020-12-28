<template>
  <div>
    <h1>{{ negocio.nombre }}</h1>
    <div id="contenedorNegocio">
      <div class="datosNegocio">
        <h5>Datos generales</h5>
        <hr>
        <p><b>Categoría:</b> {{ negocio.categoria }}</p>
        <p><b>Teléfono:</b> {{ negocio.telefono }}</p>
        <p v-if="negocio.celular != '' "><b>Celular:</b> {{ negocio.celular }}</p>
        <p><b>Correo:</b> {{ negocio.correo }}</p>
        <p v-if="negocio.sitioWeb != '' "><b>Sitio web:</b>
          <b-link :href="negocio.sitioWeb">{{ negocio.sitioWeb }}</b-link>
        </p>
        <p v-if="negocio.facebook != '' "><b>Red social:</b> {{ negocio.facebook }}</p>
        <p><b>Descripción:</b> {{ negocio.descripcion }}</p>
      </div>
      <div class="datosNegocio">
        <h5>Horarios</h5>
        <hr>
        <b-table small :items="negocio.horarios" :fields="columnasHorarios"></b-table>
      </div>
      <div class="datosNegocio">
        <h5>Dirección</h5>
        <hr>
        <p> {{ negocio.direcciones[0].calle }} {{ negocio.direcciones[0].numero }},
          <span v-if="negocio.direcciones[0].numeroInterior != '' ">num. Interior {{ negocio.direcciones[0].numeroInterior }},</span>
        </p>
        <p>{{ negocio.direcciones[0].colonia }}</p>
        <p>{{ negocio.direcciones[0].ciudad }}, {{ negocio.direcciones[0].estado }}</p>
        <p>C.P. {{ negocio.direcciones[0].codigoPostal }}</p>
      </div>
      <div class="datosNegocio">
        <h5>Aforo y reservaciones</h5>
        <hr>
        <p>Cupo disponible: <b>{{ cupoDisSinReservacion }}</b></p>
        <p>Aforo (cupo máximo de personas): <b>{{ aforoSinReservacion }}</b></p>
        <p><span v-if="negocio.permitirReservaciones">Permite reservaciones</span><span v-if="!negocio.permitirReservaciones"><b>NO</b> permite
            reservaciones</span></p>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
export default {
  data() {
    return {
      idNegocio: this.$route.params.id,
      negocio: {},
      columnasHorarios: [
        { key: "dia", label: "Día de la semama" },
        { key: "horaInicio", label: "Hora de apertura" },
        { key: "horaFin", label: "Hora de cierre" },
      ],
      cupoDisSinReservacion: "",
      aforoSinReservacion: "",
    };
  },
  created: function () {
    var vm = this;
    axios
      .get("https://localhost:5001/AforoMex/Negocios/" + this.idNegocio)
      .then(function (response) {
        vm.negocio = response.data;
        vm.formatearHoras();
        vm.calcularAforos();
      })
      .catch(function () {
        alert(
          "Error al cargar el negocio. Revise su conexión a internet e inténtelo de nuevo"
        );
      });
  },
  methods: {
    formatearHoras: function () {
      this.negocio.horarios.forEach((element) => {
        element.horaInicio = element.horaInicio.substring(0, 5);
        element.horaFin = element.horaFin.substring(0, 5);
      });
    },
    calcularAforos: function () {
      this.aforoSinReservacion = Math.round(
        this.negocio.aforo * (1 - this.negocio.aforoReservaciones)
      );
      this.cupoDisSinReservacion =
        this.aforoSinReservacion - this.negocio.cupoOcupado;
    },
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
#contenedorNegocio {
  margin-left: auto;
  margin-right: auto;
  width: 90%;
  margin-top: 10px;
  margin-bottom: 100px;
  border-style: double;
  border-radius: 15px;
  border-width: 1px;
  border-color: rgb(122, 146, 146);
  padding: 15px;
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
}
.datosNegocio {
  max-width: 450px;
  text-align: justify;
  margin-left: 5%;
  margin-right: 5%;
  margin-bottom: 30px;
  width: 100%;
}
h1 {
  margin-top: 40px;
}
</style>