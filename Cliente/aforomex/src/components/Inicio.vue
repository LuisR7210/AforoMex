<template>
  <div>
    <b-card id="localizacion" sub-title="Selecciona una ciudad">
      <div id="selects">
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
    </b-card>

    <ul class="list-unstyled">
      <b-media tag="li" v-for="negocio in negocios" :key="negocio.idNegocio" class="negocioLista">
        <h5>{{negocio.nombre}}</h5>
        <p>{{negocio.categoria}}</p>
        <div class="contenidoNegocio">
          <p>Cupo disponible: {{negocio.aforo - negocio.cupoOcupado}} personas</p>
          <p>Aforo total: {{negocio.aforo}} personas</p>
          <p>Tel: {{negocio.telefono}}</p>
        </div>
        <div class="contenidoNegocio">
          <b-link v-if="negocio.sitioWeb != '' " :href="negocio.sitioWeb">Sitio web</b-link>
          <b-button v-if="negocio.permitirReservaciones && idUsuario != undefined" pill variant="outline-info" class="botonInformacion" :to="{ name: 'VerNegocio', params: { id: negocio.idNegocio }}">Hacer una
            reservación</b-button>
          <b-button pill variant="outline-info" class="ml-auto" :to="{ name: 'VerNegocio', params: { id: negocio.idNegocio }}">Mas
            información</b-button>
        </div>
      </b-media>
    </ul>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "Inicio",
  data() {
    return {
      idUsuario: localStorage["idUsuario"],
      estadoSeleccionado: "Selecciona un estado",
      municipioSeleccionado: "Selecciona una ciudad",
      coloniaSeleccionada: "Selecciona una colonia",
      estados: [],
      municipios: [],
      colonias: [],
      negocios: [],
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
        alert(
          "Error al cargar los estados. Revise su conexión a internet e inténtelo de nuevo"
        );
      });

    axios
      .get("https://localhost:5001/AforoMex/Negocios")
      .then(function (response) {
        vm.negocios = response.data;
      })
      .catch(function () {
        alert(
          "Error al cargar los estados. Revise su conexión a internet e inténtelo de nuevo"
        );
      });
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
        .catch(function (error) {
          // handle error
          console.log(error);
        })
        .then(function () {
          // always executed
        });
    },

    escogerMunicipio: function () {
      if (this.municipioSeleccionado == "Selecciona un municipio") {
        this.colonias = [];
      } else {
        this.cargarColonias(this.municipioSeleccionado);
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
        .catch(function (error) {
          // handle error
          console.log(error);
        })
        .then(function () {
          // always executed
        });
    },

    escogerColonia: function () {},
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
  max-width: 300px;
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
</style>