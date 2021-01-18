<template>
  <div>
    <br />
    <h1>Cupo de tu negocio</h1>
    <br />
    <b-card no-body class="overflow-hidden" border-variant="primary">
      <b-card-header header-bg-variant="info" header-text-variant="white">
        <h3>{{ negocio.nombre }}</h3>
      </b-card-header>
      <p>Manten actualizado el cupo disponible de tu negocio para que tus clientes estén informados.</p>
      <b-form @submit="actualizarCupo">
        <div class="formulario">
          <b-form-group class="add-style" label="Aforo actual:" label-for="input-aforo"
            description="Es el número máximo de personas permitido en tu negocio">
            <b-form-input id="input-aforo" v-model="negocio.aforo" readonly type="number">
            </b-form-input>
          </b-form-group>

          <b-form-group class="add-style" label="Cupo ocupado:" label-for="input-porcentajeR"
            description="Es el número de lugares ocupados de los disponibles en tu aforo">
            <b-form-input id="input-porcentajeR" v-model="negocio.cupoOcupado" :max="negocio.aforo" required type="number" min="0">
            </b-form-input>
          </b-form-group>

          <p class="add-style">Cupo disponible actual: <b>{{ negocio.aforo - negocio.cupoOcupado }}</b></p>

          <b-button class="botones" type="submit" variant="primary">Actualizar cupo</b-button>
        </div>
      </b-form>
    </b-card>

    <b-modal id="alertaExito" centered ok-only title="Éxito al guardar el cupo actual" header-bg-variant="success" header-text-variant="light">
      <p class="my-4">Cambios del cupo del negocio guardados con éxito.</p>
    </b-modal>
    <b-modal id="alerta" centered ok-only title="Error" header-bg-variant="danger" header-text-variant="light">
      <p class="my-4">Error al conectar con el servidor de AforoMex. Inténtelo de nuevo más tarde.</p>
    </b-modal>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "RegistrarConsumidor",

  data() {
    return {
      negocio: {
        nombre: "",
        aforo: "",
        cupoOcupado: "",
        cupoDisponible: "",
      },
    };
  },

  created: function () {
    var vm = this;
    axios
      .get(
        "https://localhost:5001/AforoMex/Negocios/" + localStorage["idNegocio"]
      )
      .then(function (response) {
        vm.negocio.nombre = response.data.nombre;
        vm.negocio.aforo = response.data.aforo;
        vm.negocio.cupoOcupado = response.data.cupoOcupado;
      })
      .catch(function () {
        vm.$bvModal.show("alerta");
      });
  },

  methods: {
    actualizarCupo(event) {
      event.preventDefault();
      var vm = this;
      axios
        .put(
          "https://localhost:5001/AforoMex/Negocios/actualizarCupo/" +
            localStorage["idNegocio"],
          {
            idUsuario: localStorage["idUsuario"],
            idNegocio: localStorage["idNegocio"],
            cupoOcupado: vm.negocio.cupoOcupado,
          }
        )
        .then(() => {
          vm.$bvModal.show("alertaExito");
        })
        .catch(() => {
          vm.$bvModal.show("alerta");
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
  margin-left: auto;
  margin-right: auto;
  margin-top: 30px;
}
.overflow-hidden {
  max-width: 500px;
  margin-top: 15px;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 50px;
}
p,
h3 {
  margin-top: 10px;
}
p {
  max-width: 400px;
  margin-left: auto;
  margin-right: auto;
  text-align: left;
}
.botones {
  margin-top: 40px;
  margin-bottom: 30px;
}
</style>