<template>
  <div>
    <h1>Agenda</h1>
    <div id="contenedorNegocio">
      <h5>{{ mes }}</h5>
      <b-table hover :items="horas" responsive="" :fields="columnas" small>
        <template #cell(hora)="data">
          <span>{{ data.value }}</span>
        </template>
        <template #cell()="data">
          <div class="celda" v-for="reservacion in data.value" v-bind:key="reservacion.idReservacion">
            <router-link :to="{ name: 'VerNegocio', params: { id: reservacion.idNegocio }}"><span><b>{{ reservacion.negocio }}</b>
              </span></router-link><br>
            <span>Hora: {{ reservacion.hora }}</span><br>
            <span>Lugares reservados: {{ reservacion.numLugares }}</span>
            <span v-if="reservacion.estado == false"><br>Cancelada</span>
          </div>
        </template>
      </b-table>
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
      dias: [
        "Domingo",
        "Lunes",
        "Martes",
        "Miércoles",
        "Jueves",
        "Viernes",
        "Sábado",
        "Domingo",
      ],
      mes: "",
      columnas: ["hora"],
      horas: [],
      reservaciones: [],
    };
  },
  created: function () {
    this.cargarColumnasTabla();
    var vm = this;
    axios
      .get(
        "https://localhost:5001/AforoMex/Usuarios/reservacionesSemana/" +
          localStorage["idUsuario"]
      )
      .then(function (response) {
        vm.reservaciones = response.data;
        vm.cargarTabla();
      })
      .catch(function (error) {
        vm.$bvModal.show("alerta");
        console.log(error);
      });
  },
  methods: {
    cargarColumnasTabla: function () {
      var hoy = new Date();
      this.mes = new Intl.DateTimeFormat("es-ES", { month: "long" })
        .format(new Date())
        .toLocaleUpperCase();
      this.mes = this.mes + " " + hoy.getFullYear();

      for (let index = 0; index < 7; index++) {
        this.columnas.push({
          key: hoy.getDay().toString(),
          label: this.dias[hoy.getDay()] + " " + hoy.getDate(),
        });
        hoy.setDate(hoy.getDate() + 1);
      }
    },
    cargarTabla: function () {
      let hoy = new Date();
      hoy.setHours(0, 0, 0);
      for (let index = 0; index < 24; index++) {
        hoy.setHours(index);
        var hora = {
          hora: this.$luxon(hoy.toISOString(), "HH:mm"),
          0: [],
          1: [],
          2: [],
          3: [],
          4: [],
          5: [],
          6: [],
        };
        this.horas.push(hora);
      }
      this.reservaciones.forEach((reservacion) => {
        let fecha = new Date(reservacion.fechaReserva);
        let hora = fecha.getHours();
        let dia = fecha.getDay().toString();
        this.horas[hora][dia].push({
          idReservacion: reservacion.idReservacion,
          negocio: reservacion.idNegocioNavigation.nombre,
          hora: this.$luxon(fecha.toISOString(), "HH:mm"),
          estado: reservacion.estado,
          idNegocio: reservacion.idNegocio,
          numLugares: reservacion.numLugares,
        });
      });
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
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
}
h1 {
  margin-top: 40px;
}
.celda {
  background-color: #e6e6fa;
  width: fit-content;
  margin-left: auto;
  margin-right: auto;
  padding: 5px;
  border-radius: 10px;
  margin-bottom: 3px;
  margin-top: 3px;
}
.celda:hover {
  background-color: lightgray;
}
</style>