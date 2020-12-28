<template>
  <div>
    <h1>Agenda</h1>
    <div id="contenedorNegocio">
      <b-container id="timeTable">
        <th></th>
        <th class="titelCell" align="center">00</th>
        <th class="titelCell" align="center">01</th>
        <th class="titelCell" align="center">02</th>
        <th class="titelCell" align="center">03</th>
        <th class="titelCell" align="center">04</th>
        <th class="titelCell" align="center">05</th>
        <th class="titelCell" align="center">06</th>
        <th class="titelCell" align="center">07</th>
        <th class="titelCell" align="center">08</th>
        <th class="titelCell" align="center">09</th>
        <th class="titelCell" align="center">10</th>
        <th class="titelCell" align="center">11</th>
        <th class="titelCell" align="center">12</th>
        <th class="titelCell" align="center">13</th>
        <th class="titelCell" align="center">14</th>
        <th class="titelCell" align="center">15</th>
        <th class="titelCell" align="center">16</th>
        <th class="titelCell" align="center">17</th>
        <th class="titelCell" align="center">18</th>
        <th class="titelCell" align="center">19</th>
        <th class="titelCell" align="center">20</th>
        <th class="titelCell" align="center">21</th>
        <th class="titelCell" align="center">22</th>
        <th class="titelCell" align="center">23</th>

        <b-row>
          <th class="dayCell"> Mon</th>
          <b-col class="ticCell" id="10"></b-col>
          <b-col class="ticCell" id="11"></b-col>
          <b-col class="ticCell" id="12"></b-col>
          <b-col class="ticCell" id="13"></b-col>
          <b-col class="ticCell" id="14"></b-col>
          <b-col class="ticCell" id="15"></b-col>
          <b-col class="ticCell" id="16"></b-col>
          <b-col class="ticCell" id="17"></b-col>
          <b-col class="ticCell" id="18"></b-col>
          <b-col class="ticCell" id="19"></b-col>
          <b-col class="ticCell" id="110"></b-col>
          <b-col class="ticCell" id="111"></b-col>
          <b-col class="ticCell" id="112"></b-col>
          <b-col class="ticCell" id="113"></b-col>
          <b-col class="ticCell" id="114"></b-col>
          <b-col class="ticCell" id="115"></b-col>
          <b-col class="ticCell" id="116"></b-col>
          <b-col class="ticCell" id="117"></b-col>
          <b-col class="ticCell" id="118"></b-col>
          <b-col class="ticCell" id="119"></b-col>
          <b-col class="ticCell" id="120"></b-col>
          <b-col class="ticCell" id="121"></b-col>
          <b-col class="ticCell" id="122"></b-col>
          <b-col class="ticCell" id="123"></b-col>

        </b-row>
        <b-row>
          <th class="dayCell"> Tue</th>
          <b-col class="ticCell" id="20"></b-col>
          <b-col class="ticCell" id="21"></b-col>
          <b-col class="ticCell" id="22"></b-col>
          <b-col class="ticCell" id="23"></b-col>
          <b-col class="ticCell" id="24"></b-col>
          <b-col class="ticCell" id="25"></b-col>
          <b-col class="ticCell" id="26"></b-col>
          <b-col class="ticCell" id="27"></b-col>
          <b-col class="ticCell" id="28"></b-col>
          <b-col class="ticCell" id="29"></b-col>
          <b-col class="ticCell" id="210"></b-col>
          <b-col class="ticCell" id="211"></b-col>
          <b-col class="ticCell" id="212"></b-col>
          <b-col class="ticCell" id="213"></b-col>
          <b-col class="ticCell" id="214"></b-col>
          <b-col class="ticCell" id="215"></b-col>
          <b-col class="ticCell" id="216"></b-col>
          <b-col class="ticCell" id="217"></b-col>
          <b-col class="ticCell" id="218"></b-col>
          <b-col class="ticCell" id="219"></b-col>
          <b-col class="ticCell" id="220"></b-col>
          <b-col class="ticCell" id="221"></b-col>
          <b-col class="ticCell" id="222"></b-col>
          <b-col class="ticCell" id="223"></b-col>
        </b-row>
        <b-row>
          <th class="dayCell"> Wed</th>
          <b-col class="ticCell" id="30"></b-col>
          <b-col class="ticCell" id="31"></b-col>
          <b-col class="ticCell" id="32"></b-col>
          <b-col class="ticCell" id="33"></b-col>
          <b-col class="ticCell" id="34"></b-col>
          <b-col class="ticCell" id="35"></b-col>
          <b-col class="ticCell" id="36"></b-col>
          <b-col class="ticCell" id="37"></b-col>
          <b-col class="ticCell" id="38"></b-col>
          <b-col class="ticCell" id="39"></b-col>
          <b-col class="ticCell" id="310"></b-col>
          <b-col class="ticCell" id="311"></b-col>
          <b-col class="ticCell" id="312"></b-col>
          <b-col class="ticCell" id="313"></b-col>
          <b-col class="ticCell" id="314"></b-col>
          <b-col class="ticCell" id="315"></b-col>
          <b-col class="ticCell" id="316"></b-col>
          <b-col class="ticCell" id="317"></b-col>
          <b-col class="ticCell" id="318"></b-col>
          <b-col class="ticCell" id="319"></b-col>
          <b-col class="ticCell" id="320"></b-col>
          <b-col class="ticCell" id="321"></b-col>
          <b-col class="ticCell" id="322"></b-col>
          <b-col class="ticCell" id="323"></b-col>
        </b-row>

        <b-row>
          <th class="dayCell"> Thu</th>
          <b-col class="ticCell" id="40"></b-col>
          <b-col class="ticCell" id="41"></b-col>
          <b-col class="ticCell" id="42"></b-col>
          <b-col class="ticCell" id="43"></b-col>
          <b-col class="ticCell" id="44"></b-col>
          <b-col class="ticCell" id="45"></b-col>
          <b-col class="ticCell" id="46"></b-col>
          <b-col class="ticCell" id="47"></b-col>
          <b-col class="ticCell" id="48"></b-col>
          <b-col class="ticCell" id="49"></b-col>
          <b-col class="ticCell" id="410"></b-col>
          <b-col class="ticCell" id="411"></b-col>
          <b-col class="ticCell" id="412"></b-col>
          <b-col class="ticCell" id="413"></b-col>
          <b-col class="ticCell" id="414"></b-col>
          <b-col class="ticCell" id="415"></b-col>
          <b-col class="ticCell" id="416"></b-col>
          <b-col class="ticCell" id="417"></b-col>
          <b-col class="ticCell" id="418"></b-col>
          <b-col class="ticCell" id="419"></b-col>
          <b-col class="ticCell" id="420"></b-col>
          <b-col class="ticCell" id="421"></b-col>
          <b-col class="ticCell" id="422"></b-col>
          <b-col class="ticCell" id="423"></b-col>
        </b-row>

        <b-row>
          <th class="dayCell"> Fri</th>
          <b-col class="ticCell" id="50"></b-col>
          <b-col class="ticCell" id="51"></b-col>
          <b-col class="ticCell" id="52"></b-col>
          <b-col class="ticCell" id="53"></b-col>
          <b-col class="ticCell" id="54"></b-col>
          <b-col class="ticCell" id="55"></b-col>
          <b-col class="ticCell" id="56"></b-col>
          <b-col class="ticCell" id="57"></b-col>
          <b-col class="ticCell" id="58"></b-col>
          <b-col class="ticCell" id="59"></b-col>
          <b-col class="ticCell" id="510"></b-col>
          <b-col class="ticCell" id="511"></b-col>
          <b-col class="ticCell" id="512"></b-col>
          <b-col class="ticCell" id="513"></b-col>
          <b-col class="ticCell" id="514"></b-col>
          <b-col class="ticCell" id="515"></b-col>
          <b-col class="ticCell" id="516"></b-col>
          <b-col class="ticCell" id="517"></b-col>
          <b-col class="ticCell" id="518"></b-col>
          <b-col class="ticCell" id="519"></b-col>
          <b-col class="ticCell" id="520"></b-col>
          <b-col class="ticCell" id="521"></b-col>
          <b-col class="ticCell" id="522"></b-col>
          <b-col class="ticCell" id="523"></b-col>
        </b-row>
        <b-row>
          <th class="dayCell"> Sat</th>
          <b-col class="ticCell" id="60"></b-col>
          <b-col class="ticCell" id="61"></b-col>
          <b-col class="ticCell" id="62"></b-col>
          <b-col class="ticCell" id="63"></b-col>
          <b-col class="ticCell" id="64"></b-col>
          <b-col class="ticCell" id="65"></b-col>
          <b-col class="ticCell" id="66"></b-col>
          <b-col class="ticCell" id="67"></b-col>
          <b-col class="ticCell" id="68"></b-col>
          <b-col class="ticCell" id="69"></b-col>
          <b-col class="ticCell" id="610"></b-col>
          <b-col class="ticCell" id="611"></b-col>
          <b-col class="ticCell" id="612"></b-col>
          <b-col class="ticCell" id="613"></b-col>
          <b-col class="ticCell" id="614"></b-col>
          <b-col class="ticCell" id="615"></b-col>
          <b-col class="ticCell" id="616"></b-col>
          <b-col class="ticCell" id="617"></b-col>
          <b-col class="ticCell" id="618"></b-col>
          <b-col class="ticCell" id="619"></b-col>
          <b-col class="ticCell" id="620"></b-col>
          <b-col class="ticCell" id="621"></b-col>
          <b-col class="ticCell" id="622"></b-col>
          <b-col class="ticCell" id="623"></b-col>
        </b-row>
        <b-row>
          <th class="dayCell"> Sun</th>
          <b-col class="ticCell" id="70"></b-col>
          <b-col class="ticCell" id="71"></b-col>
          <b-col class="ticCell" id="72"></b-col>
          <b-col class="ticCell" id="73"></b-col>
          <b-col class="ticCell" id="74"></b-col>
          <b-col class="ticCell" id="75"></b-col>
          <b-col class="ticCell" id="76"></b-col>
          <b-col class="ticCell" id="77"></b-col>
          <b-col class="ticCell" id="78"></b-col>
          <b-col class="ticCell" id="79"></b-col>
          <b-col class="ticCell" id="710"></b-col>
          <b-col class="ticCell" id="711"></b-col>
          <b-col class="ticCell" id="712"></b-col>
          <b-col class="ticCell" id="713"></b-col>
          <b-col class="ticCell" id="714"></b-col>
          <b-col class="ticCell" id="715"></b-col>
          <b-col class="ticCell" id="716"></b-col>
          <b-col class="ticCell" id="717"></b-col>
          <b-col class="ticCell" id="718"></b-col>
          <b-col class="ticCell" id="719"></b-col>
          <b-col class="ticCell" id="720"></b-col>
          <b-col class="ticCell" id="721"></b-col>
          <b-col class="ticCell" id="722"></b-col>
          <b-col class="ticCell" id="723"></b-col>
        </b-row>
      </b-container>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      idNegocio: this.$route.params.id,
      negocio: {},
      columnasHorarios: [
        { key: "hora00", label: "Día de la semama" },
        { key: "hora01", label: "Hora de apertura" },
        { key: "hora02", label: "Hora de cierre" },
      ],
      cupoDisSinReservacion: "",
      aforoSinReservacion: "",
    };
  },
  created: function () {
  },
  methods: {
  },
};
</script>

<!-- Add "scoped" atb-rowibute to limit CSS to this component only -->
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