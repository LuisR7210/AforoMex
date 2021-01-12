<template>
  <div>
    <h1>Agenda</h1>
    <div id="contenedorNegocio">
      <b-form @submit="buscarReservaciones">
        <div class="busquedas">
          <div class="fechas">
            <label for="input-fechaConsulta">Elige una fecha de consulta:</label>
            <b-form-input id="input-fechaConsulta" v-model="fechaConsulta" type="date" min="1900-01-01" required></b-form-input>
          </div>
          <div class="fechas">
            <label for="select-periodo">Por un periodo según la fecha:</label>
            <b-form-select id="select-periodo" v-model="periodoSeleccionado">
              <b-form-select-option value="fechaFin">Hasta el: (ingrese fecha)</b-form-select-option>
              <b-form-select-option value="dia">Día</b-form-select-option>
              <b-form-select-option value="semana">Semana</b-form-select-option>
              <b-form-select-option value="mes">Mes</b-form-select-option>
              <b-form-select-option value="bimestre">Bimestre</b-form-select-option>
              <b-form-select-option value="trimestre">Trimestre</b-form-select-option>
              <b-form-select-option value="cuatrimestre">Cuatrimestre</b-form-select-option>
              <b-form-select-option value="semestre">Semestre</b-form-select-option>
              <b-form-select-option value="ano">Año</b-form-select-option>
            </b-form-select>
          </div>
          <div class="fechas" v-if="periodoSeleccionado == 'fechaFin'">
            <label for="input-fechaFin">Hasta el:</label>
            <b-form-input id="input-fechaFin" v-model="fechaFin" type="date" :min="fechaConsulta" required></b-form-input>
          </div>
          <b-button class="botonBuscar" type="submit" variant="primary">Buscar</b-button>
        </div>
      </b-form>
      <b-table id="tablaReservaciones" hover :items="reservacionesTabla" responsive :fields="columnas" :per-page="perPage"
        :current-page="currentPage">
        <template #cell(estado)="data">
          <span v-if="data.value == true">En curso</span>
          <span v-if="data.value == false">Cancelada</span>
        </template>
      </b-table>
      <b-pagination v-model="currentPage" :total-rows="rows" :per-page="perPage" aria-controls="tablaReservaciones" align="center">
      </b-pagination>
    </div>

    <b-modal id="alerta" centered ok-only title="Error al cargar las reservaciones" header-bg-variant="danger" header-text-variant="light">
      <p class="my-4">Revise su conexión a internet e inténtelo de nuevo.</p>
    </b-modal>
  </div>
</template>

<script>
//select-mode="single" ref="selectableTable" selectable @row-selected="onRowSelected"
import axios from "axios";
export default {
  data() {
    return {
      columnas: [
        { key: "fecha", label: "Fecha", sortable: true },
        "hora",
        { key: "nombre", label: "A nombre de", sortable: true },
        "estado",
        { key: "numLugares", label: "Lugares reservados" },
        { key: "telefono", label: "Teléfono del cliente" },
      ],
      reservacionesTabla: [],
      reservaciones: [],
      perPage: 30,
      currentPage: 1,
      rows: 0,
      fechaConsulta: this.$luxon(new Date().toISOString(), "yyyy-MM-dd"),
      fechaFin: "",
      periodoSeleccionado: "dia",
    };
  },
  created: function () {
    this.consultarReservaciones();
  },
  methods: {
    consultarReservaciones: function () {
      var vm = this;
      axios
        .get("https://localhost:5001/AforoMex/Negocios/reservaciones/", {
          params: {
            idNegocio: localStorage["idUsuario"],
            fechaConsulta: vm.fechaConsulta,
            periodo: vm.periodoSeleccionado,
            fechaFin: vm.fechaFin,
          },
        })
        .then(function (response) {
          vm.reservaciones = response.data;
          vm.cargarTabla();
        })
        .catch(function (error) {
          vm.$bvModal.show("alerta");
          console.log(error);
        });
    },
    cargarTabla: function () {
      this.reservacionesTabla = [];
      this.reservaciones.forEach((reservacion) => {
        let fecha = new Date(reservacion.fechaReserva);
        this.reservacionesTabla.push({
          idReservacion: reservacion.idReservacion,
          fecha: this.$luxon(fecha.toISOString(), "date_short"),
          hora: this.$luxon(fecha.toISOString(), "HH:mm"),
          nombre:
            reservacion.idUsuarioNavigation.nombre +
            " " +
            reservacion.idUsuarioNavigation.apellidos,
          estado: reservacion.estado,
          numLugares: reservacion.numLugares,
          telefono: reservacion.idUsuarioNavigation.telefono,
        });
      });
      this.rows = this.reservacionesTabla.length;
    },
    buscarReservaciones(event) {
      event.preventDefault();
      this.consultarReservaciones();
    },
  },
};
</script>

<!-- Add "scoped" atb-rowibute to limit CSS to this component only -->
<style scoped>
#contenedorNegocio {
  margin-left: auto;
  margin-right: auto;
  max-width: fit-content;
  margin-top: 10px;
  margin-bottom: 100px;
  border-style: double;
  border-radius: 15px;
  border-width: 1px;
  border-color: rgb(122, 146, 146);
  padding: 15px;
}
h1 {
  margin-top: 40px;
}
.busquedas {
  width: 700px;
  margin-bottom: 30px;
  display: flex;
  flex-wrap: wrap;
  justify-content: left;
  align-items: flex-end;
}
.fechas {
  width: 200px;
  margin-right: 10px;
  text-align: left;
}
</style>