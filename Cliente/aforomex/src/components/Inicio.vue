<template>
  <div>
    <b-card id="localizacion" sub-title="Selecciona una ciudad, ya sea con un código postal o manualmente seleccionando primero un estado">
      <div id="selects">
        <b-form @submit="buscarCP">
          <b-input-group class="selects">
            <b-form-input placeholder="Ingresa un código postal" v-model="cp" pattern="[0-9]+"></b-form-input>
            <b-input-group-append>
              <b-button variant="outline-warning" type="submit">
                <b-icon icon="search"></b-icon>
              </b-button>
            </b-input-group-append>
          </b-input-group>
        </b-form>
        <b-form-select class="selects" v-model="estadoSeleccionado" v-on:change="escogerEstado">
          <option>Selecciona un estado</option>
          <option v-for="estado in estados" v-bind:key="estado">
            {{ estado }}
          </option>
        </b-form-select>
        <b-form-select class="selects" v-model="municipioSeleccionado" v-on:change="escogerMunicipio">
          <option>Selecciona una ciudad</option>
          <option v-for="municipio in municipios" v-bind:key="municipio">
            {{ municipio }}
          </option>
        </b-form-select>
        <b-form-select class="selects" v-model="coloniaSeleccionada" v-on:change="escogerColonia">
          <option>Selecciona una colonia</option>
          <option v-for="colonia in colonias" v-bind:key="colonia">
            {{ colonia }}
          </option>
        </b-form-select>
      </div>
      <div class="filtros">
        <p>Filtra por categoria:</p>
        <b-form-select class="selects" v-model="categoriaSeleccionada" v-on:change="escogerCategoria">
          <option>Todas</option>
          <option v-for="categoria in categorias" v-bind:key="categoria">
            {{ categoria }}
          </option>
        </b-form-select>
      </div>
    </b-card>

    <ul class="list-unstyled">
      <b-media tag="li" v-for="negocio in negociosParaLista" :key="negocio.idNegocio" class="negocioLista">
        <h5>{{negocio.nombre}}</h5>
        <p>{{negocio.categoria}}</p>
        <div class="contenidoNegocio">
          <p>Cupo disponible: {{negocio.aforo - negocio.cupoOcupado}} personas</p>
          <p>Aforo total: {{negocio.aforo}} personas</p>
          <p>Tel: {{negocio.telefono}}</p>
        </div>
        <div class="contenidoNegocio">
          <b-link v-if="negocio.sitioWeb != '' " :href="negocio.sitioWeb">Sitio web</b-link>
          <b-button v-if="negocio.permitirReservaciones && idUsuario != undefined" pill variant="outline-info" class="botonInformacion"
            :to="{ name: 'HacerReservaciones', params: { id: negocio.idNegocio }}">Hacer una
            reservación</b-button>
          <b-button pill variant="outline-info" class="ml-auto" :to="{ name: 'VerNegocio', params: { id: negocio.idNegocio }}">Mas
            información</b-button>
        </div>
      </b-media>
    </ul>
    <b-pagination v-model="currentPage" :total-rows="rows" :per-page="perPage" aria-controls="negociosList" align="center">
    </b-pagination>

    <b-modal id="alerta" centered ok-only title="Error" header-bg-variant="danger" header-text-variant="light">
      <p class="my-4"> {{ mensajeAlerta }}</p>
    </b-modal>
  </div>
</template>

<script>
import { EventBus } from "./bus.js";
import axios from "axios";
export default {
  name: "Inicio",
  data() {
    return {
      mensajeAlerta: "",
      idUsuario: localStorage["idUsuario"],
      estadoSeleccionado: "Selecciona un estado",
      municipioSeleccionado: "Selecciona una ciudad",
      coloniaSeleccionada: "Selecciona una colonia",
      estados: [],
      municipios: [],
      colonias: [],
      negocios: [],
      cp: "",
      busqueda: "",
      categorias: [],
      categoriaSeleccionada: "Todas",
      perPage: 15,
      currentPage: 1,
      rows: 0,

      get negociosParaLista() {
        return this.negocios.slice(
          (this.currentPage - 1) * this.perPage,
          this.currentPage * this.perPage
        );
      },
    };
  },
  created: function () {
    var vm = this;
    axios
      .get("https://api-sepomex.hckdrk.mx/query/get_estados")
      .then(function (response) {
        vm.estados = response.data.response.estado;
      })
      .catch(function () {
        vm.mensajeAlerta =
          "Error al cargar los estados. Revise su conexión a internet e inténtelo de nuevo";
        vm.$bvModal.show("alerta");
      });

    axios
      .get("https://localhost:5001/AforoMex/Negocios")
      .then(function (response) {
        vm.negocios = response.data;
        vm.rows = vm.negocios.length;
      })
      .catch(function () {
        vm.mensajeAlerta =
          "Error al cargar los negocios. Revise su conexión a internet e inténtelo de nuevo";
        vm.$bvModal.show("alerta");
      });

    axios
      .get("https://localhost:5001/AforoMex/Negocios/categorias")
      .then(function (response) {
        vm.categorias = response.data;
      })
      .catch(function () {
        vm.mensajeAlerta =
          "Error al cargar las categorias. Revise su conexión a internet e inténtelo de nuevo";
        vm.$bvModal.show("alerta");
      });

    EventBus.$on("buscarNegocios", (busqueda) => {
      this.busqueda = busqueda;
      this.buscarNegociosConDatos();
    });
  },
  beforeDestroy() {
    EventBus.$off("buscarNegocios");
  },
  methods: {
    escogerEstado: function () {
      if (this.estadoSeleccionado == "Selecciona un estado") {
        this.municipios = [];
      } else {
        this.cargarMunicipios(this.estadoSeleccionado);
      }
    },

    cargarMunicipios: function (estado) {
      var vm = this;
      axios
        .get(
          "https://api-sepomex.hckdrk.mx/query/get_municipio_por_estado/" +
            estado
        )
        .then(function (response) {
          vm.municipios = response.data.response.municipios.sort();
        })
        .catch(function () {
          vm.mensajeAlerta =
            "Error al cargar las ciudades del estado seleccionado. Revise su conexión a internet e inténtelo de nuevo";
          vm.$bvModal.show("alerta");
        });
    },

    escogerMunicipio: function () {
      if (this.municipioSeleccionado == "Selecciona una ciudad") {
        this.colonias = [];
      } else {
        this.cargarColonias(this.municipioSeleccionado);
        this.buscarNegociosConDatos();
      }
    },

    cargarColonias: function (municipio) {
      var vm = this;
      axios
        .get(
          "https://api-sepomex.hckdrk.mx/query/get_colonia_por_municipio/" +
            municipio
        )
        .then(function (response) {
          vm.colonias = response.data.response.colonia.sort();
        })
        .catch(function () {
          vm.mensajeAlerta =
            "Error al cargar las colonias de la ciudad seleccionada. Revise su conexión a internet e inténtelo de nuevo";
          vm.$bvModal.show("alerta");
        });
    },

    escogerColonia: function () {
      if (this.municipioSeleccionado == "Selecciona una colonia") {
        return;
      } else {
        this.buscarNegociosConDatos();
      }
    },

    buscarCP(event) {
      event.preventDefault();
      if (this.cp == "") {
        return;
      }
      var vm = this;
      axios
        .get("https://api-sepomex.hckdrk.mx/query/info_cp/" + this.cp, {
          params: {
            type: "simplified",
          },
        })
        .then((response) => {
          vm.estadoSeleccionado = response.data.response.estado;
          vm.cargarMunicipios(this.estadoSeleccionado);
          vm.municipioSeleccionado = response.data.response.municipio;
          vm.colonias = response.data.response.asentamiento;
        })
        .catch(function (error) {
          vm.cp = "";
          if (error.response.data.code_error == 105) {
            vm.mensajeAlerta =
              "No existe ese código postal. Corrígelo e inténtalo de nuevo.";
          } else {
            vm.mensajeAlerta =
              "Formato de código postal inválido. Corrígelo e inténtalo de nuevo.";
          }
          vm.$bvModal.show("alerta");
        });
    },

    escogerCategoria: function () {
      if (this.categoriaSeleccionada == "Todas") {
        return;
      } else {
        this.buscarNegociosConDatos();
      }
    },

    buscarNegociosConDatos: function () {
      var vm = this;
      var ciudad = "";
      var colonia = "";
      var categoria = "";
      if (vm.municipioSeleccionado != "Selecciona una ciudad") {
        ciudad = vm.municipioSeleccionado;
      }
      if (vm.coloniaSeleccionada != "Selecciona una colonia") {
        colonia = vm.coloniaSeleccionada;
      }
      if (vm.categoriaSeleccionada != "Todas") {
        categoria = vm.categoriaSeleccionada;
      }
      axios
        .get("https://localhost:5001/AforoMex/Negocios/buscarNegocios", {
          params: {
            nombre: vm.busqueda,
            ciudad: ciudad,
            colonia: colonia,
            categoria: categoria,
          },
        })
        .then((response) => {
          vm.negocios = response.data;
          vm.rows = this.negocios.length;
        })
        .catch(function () {
          vm.mensajeAlerta =
            "Error al cargar los negocios. Revise su conexión a internet e inténtelo de nuevo";
          vm.$bvModal.show("alerta");
        });
    },
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
#localizacion {
  width: 90%;
  margin: auto;
  text-align: left;
  border-color: cyan;
  margin-top: 10px;
}
#selects {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
}
.selects {
  max-width: 250px;
  margin-left: 3%;
  margin-right: 3%;
}
.negocioLista {
  max-width: 800px;
  border-style: solid;
  border-radius: 15px;
  border-width: 1px;
  border-color: rgb(122, 146, 146);
  text-align: left;
  padding: 15px;
  margin-top: 50px;
  margin-left: auto;
  margin-right: auto;
}
.contenidoNegocio {
  display: flex;
  flex-wrap: wrap;
  align-items: center;
}
.contenidoNegocio p {
  margin-right: 10%;
}
.botonInformacion {
  margin-left: auto;
}
.filtros {
  display: flex;
  flex-wrap: wrap;
  justify-content: left;
  margin-top: 20px;
}
</style>