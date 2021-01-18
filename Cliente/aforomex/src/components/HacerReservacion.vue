<template>
  <div>
    <br />
    <h1>Crear una reservación {{ negocio.nombre }}</h1>
    <br />
    <div id="contenedorNegocio">
      <div class="datosNegocio">
        <h5>Horarios</h5>
        <hr />
        <b-table small :items="negocio.horarios" :fields="columnasHorarios"></b-table>
      </div>
      <div class="datosNegocio">
        <h5>Aforo y reservaciones</h5>
        <hr />
        <p>
          Cupo disponible:
          <b>{{ cupoDisSinReservacion }}</b>
        </p>
        <p>
          Aforo (cupo máximo de personas):
          <b>{{ aforoSinReservacion }}</b>
        </p>
        <p>
          <span v-if="negocio.permitirReservaciones">Permite reservaciones</span>
          <span v-if="!negocio.permitirReservaciones">
            <b>NO</b> permite
            reservaciones
          </span>
        </p>
      </div>
    </div>
    <div id="contenedorFormulario">
      <b-form @submit="onSubmit">
        <div class="formulario">
          <b-form-group
            class="add-style"
            id="grupoLugares"
            label="Numero de lugar(es):"
            label-for="input-nombre"
          >
            <b-form-input
              type="number"
              id="input-nombre"
              v-model="form.numero"
              placeholder="Ingresa el numero de lugares para la reservación"
              maxlength="50"
              required
            ></b-form-input>
          </b-form-group>
          <b-form-group
            class="add-style"
            id="grupoFecha"
            label="Fecha y hora de reserva:"
            label-for="input-fechaHora"
          >
            <b-form-input
              id="input-fechaHora"
              v-model="form.fechaHora"
              type="datetime-local"
              placeholder="Selecciona la fecha y la hora de tu reserva"
              required
            ></b-form-input>
          </b-form-group>
          <b-button class="botones" type="submit" variant="primary">Registrar Reservación</b-button>
        </div>
      </b-form>
    </div>

    <b-modal
      id="alerta"
      centered
      ok-only
      title="Error al cargar las reservaciones"
      header-bg-variant="danger"
      header-text-variant="light"
    >
      <p class="my-4">{{ mensajeAlerta }}</p>
    </b-modal>
  </div>
</template>

<script>
import axios from "axios";
//import { EventBus } from "./bus.js";
//import * as moment from 'moment';
import { format, parseISO } from "date-fns";
//import { th } from 'date-fns/locale';
export default {
  name: "RegistrarConsumidor",

  data() {
    return {
      idNegocio: this.$route.params.id,
      negocio: {},
      hoy: new Date().toISOString().slice(0, 19),
      mensajeAlerta: "",
      form: {
        numero: "",
        fechaHora: ""
      },
      columnasHorarios: [
        { key: "dia", label: "Día de la semama" },
        { key: "horaInicio", label: "Hora de apertura" },
        { key: "horaFin", label: "Hora de cierre" }
      ],
      cupoDisSinReservacion: "",
      aforoSinReservacion: ""
    };
  },
  created: function() {
    var vm = this;
    //this.form.hoy=this.form.hoy-1;
    axios
      .get("https://localhost:5001/AforoMex/Negocios/" + this.idNegocio)
      .then(function(response) {
        vm.negocio = response.data;
        vm.formatearHoras();
        vm.calcularAforos();
      })
      .catch(function() {
        alert(
          "Error al cargar el negocio. Revise su conexión a internet e inténtelo de nuevo"
        );
      });
  },
  methods: {
    onSubmit(event) {
      //document.write("hola, estos son los datos: "+this.form.fechaHora+", "+this.form.numero+", "+"Vigente"+
      //    localStorage["idUsuario"]+this.idNegocio);
      event.preventDefault();
      var vm = this;
      //document.write(variable);
      //var ht= this.form.fechaHora;
      //var isoStr = new Date().toISOString(); $('#input-fechaHora').val(isoStr.substring(0,isoStr.length-1));
      //var conv= ht.toLocaleString();
      /*var vnp=moment(this.form.fechaHora).format('YYYY-MM-DDThh:mm');
      console.log(vnp._i);
      console.log(vnp._f);
      console.log(vnp.format('YYYY-MM-DD HH:mm'));*/
      //var hola="3456";
      var diamassemana = new Date(this.hoy);
      diamassemana.setDate(diamassemana.getDate() + 7);
      var loIsDate = new Date(this.form.fechaHora);
      var horaSel = loIsDate.getHours();
      var minuSel = loIsDate.getMinutes();
      var DiaSemana = loIsDate.getDay();
      const diasopen = [20, 20];
      var vali = false;
      //var horaI=new Date().getHours;
      //var horaF=new Date().getHours;
      var hI = new Date();
      var hF = new Date();
      this.negocio.horarios.forEach(function(horario) {
        switch (horario.dia) {
          case "Lunes": //1
            diasopen.push(1);
            break;
          case "Martes": //2
            diasopen.push(2);
            break;
          case "Miércoles": //3
            diasopen.push(3);
            break;
          case "Jueves": //4
            diasopen.push(4);
            break;
          case "Viernes": //5
            diasopen.push(5);
            break;
          case "Sábado": //6
            diasopen.push(6);
            break;
          case "Domingo": //0
            diasopen.push(0);
            break;
        }
      });
      var maximo = this.negocio.limiteReservaciones;
      //alert(maximo);
      //alert(parseInt(this.form.numero,10));
      if (parseInt(this.form.numero, 10) > maximo) {
        vm.mensajeAlerta =
          "Lo sentimos, pero este negocio no permite reservaciones para mas de " +
          maximo +
          " personas";
        vm.$bvModal.show("alerta");
      } else {
        //var horas=loIsDate.getHours();
        //var minutos = loIsDate.getMinutes();
        if (new Date(this.form.fechaHora) > /*new Date(*/ new Date() /*)*/) {
          //dom=0 lun=1 sab=6
          //alert("hola xd: "+this.negocio.horarios[0].dia+" oo: "+DiaSemana);
          //alert("anti jolaXD: "+(new Date(this.form.fechaHora)<new Date(diamassemana)));
          if (new Date(this.form.fechaHora) < new Date(diamassemana)) {
            //alert("jolaXD");
            ////////////////////////////////////////////////////////////////////////////
            switch (DiaSemana) {
              case 0: //domingo /// vector 0 es lunes
                if (diasopen.includes(0)) {
                  ////////////-------------------validacion de hora :'(-------------------
                  this.negocio.horarios.forEach(function(horario) {
                    switch (horario.dia) {
                      case "Domingo": //0
                        var hola = horario.horaInicio.toString();
                        var dos = hola.split(":", 3);
                        hI.setHours(dos[0], dos[1]);
                        var hola1 = horario.horaFin.toString();
                        var tres = hola1.split(":", 3);
                        hF.setHours(tres[0], tres[1]);
                        var iniHo = hI.getHours();
                        var fiHo = hF.getHours();
                        var iniMi = hI.getMinutes();
                        var fiMi = hF.getMinutes();
                        if (horaSel >= iniHo && horaSel <= fiHo) {
                          if (horaSel == iniHo) {
                            if (minuSel > iniMi) {
                              vali = true;
                            }
                          } else {
                            if (horaSel == fiHo) {
                              if (minuSel < fiMi) {
                                vali = true;
                              }
                            } else {
                              vali = true;
                            }
                          }
                        }
                        if (!vali) {
                          vm.mensajeAlerta =
                            "Revisa la hora de tu reservación, el lugar debe estar abierto el dia que deseas ir.";
                          vm.$bvModal.show("alerta");
                        }
                        //alert("este es el datoxdxd: "+horario.horaFin.toString());
                        //horaI=horario.horaInicio.getHours();
                        //horaF=horario.horaFin.getHours();
                        break;
                    }
                  });
                  /////////////////-----------------------------------------------------------
                } else {
                  vm.mensajeAlerta = "Este negocio no abre los domingos.";
                  vm.$bvModal.show("alerta");
                }
                break;
              case 1: //lunes
                //Declaraciones ejecutadas cuando el resultado de expresión coincide con el valor2
                if (diasopen.includes(1)) {
                  ////////////-------------------validacion de hora :'(-------------------
                  this.negocio.horarios.forEach(function(horario) {
                    switch (horario.dia) {
                      case "Lunes": //0
                        var hola = horario.horaInicio.toString();
                        var dos = hola.split(":", 3);
                        hI.setHours(dos[0], dos[1]);
                        var hola1 = horario.horaFin.toString();
                        var tres = hola1.split(":", 3);
                        hF.setHours(tres[0], tres[1]);
                        var iniHo = hI.getHours();
                        var fiHo = hF.getHours();
                        var iniMi = hI.getMinutes();
                        var fiMi = hF.getMinutes();
                        if (horaSel >= iniHo && horaSel <= fiHo) {
                          if (horaSel == iniHo) {
                            if (minuSel > iniMi) {
                              vali = true;
                            }
                          } else {
                            if (horaSel == fiHo) {
                              if (minuSel < fiMi) {
                                vali = true;
                              }
                            } else {
                              vali = true;
                            }
                          }
                        }
                        if (!vali) {
                          vm.mensajeAlerta =
                            "Revisa la hora de tu reservación, el lugar debe estar abierto el dia que deseas ir.";
                          vm.$bvModal.show("alerta");
                        }
                        //alert("este es el datoxdxd: "+horario.horaFin.toString());
                        //horaI=horario.horaInicio.getHours();
                        //horaF=horario.horaFin.getHours();
                        break;
                    }
                  });
                  /////////////////-----------------------------------------------------------
                } else {
                  vm.mensajeAlerta = "Este negocio no abre los lunes.";
                  vm.$bvModal.show("alerta");
                }
                break;
              case 2: //martes
                //Declaraciones ejecutadas cuando el resultado de expresión coincide con valorN
                if (diasopen.includes(2)) {
                  ////////////-------------------validacion de hora :'(-------------------
                  this.negocio.horarios.forEach(function(horario) {
                    switch (horario.dia) {
                      case "Martes": //0
                        var hola = horario.horaInicio.toString();
                        var dos = hola.split(":", 3);
                        hI.setHours(dos[0], dos[1]);
                        var hola1 = horario.horaFin.toString();
                        var tres = hola1.split(":", 3);
                        hF.setHours(tres[0], tres[1]);
                        var iniHo = hI.getHours();
                        var fiHo = hF.getHours();
                        var iniMi = hI.getMinutes();
                        var fiMi = hF.getMinutes();
                        if (horaSel >= iniHo && horaSel <= fiHo) {
                          if (horaSel == iniHo) {
                            if (minuSel > iniMi) {
                              vali = true;
                            }
                          } else {
                            if (horaSel == fiHo) {
                              if (minuSel < fiMi) {
                                vali = true;
                              }
                            } else {
                              vali = true;
                            }
                          }
                        }
                        if (!vali) {
                          vm.mensajeAlerta =
                            "Revisa la hora de tu reservación, el lugar debe estar abierto el dia que deseas ir.";
                          vm.$bvModal.show("alerta");
                        }
                        //alert("este es el datoxdxd: "+horario.horaFin.toString());
                        //horaI=horario.horaInicio.getHours();
                        //horaF=horario.horaFin.getHours();
                        break;
                    }
                  });
                  /////////////////-----------------------------------------------------------
                } else {
                  vm.mensajeAlerta = "Este negocio no abre los martes.";
                  vm.$bvModal.show("alerta");
                }
                break;
              case 3: //miercoles
                //Declaraciones ejecutadas cuando el resultado de expresión coincide con valorN
                if (diasopen.includes(3)) {
                  ////////////-------------------validacion de hora :'(-------------------
                  this.negocio.horarios.forEach(function(horario) {
                    switch (horario.dia) {
                      case "Miércoles": //0
                        var hola = horario.horaInicio.toString();
                        var dos = hola.split(":", 3);
                        hI.setHours(dos[0], dos[1]);
                        var hola1 = horario.horaFin.toString();
                        var tres = hola1.split(":", 3);
                        hF.setHours(tres[0], tres[1]);
                        var iniHo = hI.getHours();
                        var fiHo = hF.getHours();
                        var iniMi = hI.getMinutes();
                        var fiMi = hF.getMinutes();
                        if (horaSel >= iniHo && horaSel <= fiHo) {
                          if (horaSel == iniHo) {
                            if (minuSel > iniMi) {
                              vali = true;
                            }
                          } else {
                            if (horaSel == fiHo) {
                              if (minuSel < fiMi) {
                                vali = true;
                              }
                            } else {
                              vali = true;
                            }
                          }
                        }
                        if (!vali) {
                          vm.mensajeAlerta =
                            "Revisa la hora de tu reservación, el lugar debe estar abierto el dia que deseas ir.";
                          vm.$bvModal.show("alerta");
                        }
                        //alert("este es el datoxdxd: "+horario.horaFin.toString());
                        //horaI=horario.horaInicio.getHours();
                        //horaF=horario.horaFin.getHours();
                        break;
                    }
                  });
                  /////////////////-----------------------------------------------------------
                } else {
                  vm.mensajeAlerta = "Este negocio no abre los miercoles.";
                  vm.$bvModal.show("alerta");
                }
                break;
              case 4: //jueves
                //Declaraciones ejecutadas cuando el resultado de expresión coincide con valorN
                if (diasopen.includes(4)) {
                  ////////////-------------------validacion de hora :'(-------------------
                  this.negocio.horarios.forEach(function(horario) {
                    switch (horario.dia) {
                      case "Jueves": //0
                        var hola = horario.horaInicio.toString();
                        var dos = hola.split(":", 3);
                        hI.setHours(dos[0], dos[1]);
                        var hola1 = horario.horaFin.toString();
                        var tres = hola1.split(":", 3);
                        hF.setHours(tres[0], tres[1]);
                        var iniHo = hI.getHours();
                        var fiHo = hF.getHours();
                        var iniMi = hI.getMinutes();
                        var fiMi = hF.getMinutes();
                        if (horaSel >= iniHo && horaSel <= fiHo) {
                          if (horaSel == iniHo) {
                            if (minuSel > iniMi) {
                              vali = true;
                            }
                          } else {
                            if (horaSel == fiHo) {
                              if (minuSel < fiMi) {
                                vali = true;
                              }
                            } else {
                              vali = true;
                            }
                          }
                        }
                        if (!vali) {
                          vm.mensajeAlerta =
                            "Revisa la hora de tu reservación, el lugar debe estar abierto el dia que deseas ir.";
                          vm.$bvModal.show("alerta");
                        }
                        //alert("este es el datoxdxd: "+horario.horaFin.toString());
                        //horaI=horario.horaInicio.getHours();
                        //horaF=horario.horaFin.getHours();
                        break;
                    }
                  });
                  /////////////////-----------------------------------------------------------
                } else {
                  vm.mensajeAlerta = "Este negocio no abre los jueves.";
                  vm.$bvModal.show("alerta");
                }
                break;
              case 5: //viernes
                //Declaraciones ejecutadas cuando el resultado de expresión coincide con valorN
                if (diasopen.includes(5)) {
                  ////////////-------------------validacion de hora :'(-------------------
                  this.negocio.horarios.forEach(function(horario) {
                    switch (horario.dia) {
                      case "Viernes": //0
                        var hola = horario.horaInicio.toString();
                        var dos = hola.split(":", 3);
                        hI.setHours(dos[0], dos[1]);
                        var hola1 = horario.horaFin.toString();
                        var tres = hola1.split(":", 3);
                        hF.setHours(tres[0], tres[1]);
                        var iniHo = hI.getHours();
                        var fiHo = hF.getHours();
                        var iniMi = hI.getMinutes();
                        var fiMi = hF.getMinutes();
                        if (horaSel >= iniHo && horaSel <= fiHo) {
                          if (horaSel == iniHo) {
                            if (minuSel > iniMi) {
                              vali = true;
                            }
                          } else {
                            if (horaSel == fiHo) {
                              if (minuSel < fiMi) {
                                vali = true;
                              }
                            } else {
                              vali = true;
                            }
                          }
                        }
                        if (!vali) {
                          vm.mensajeAlerta =
                            "Revisa la hora de tu reservación, el lugar debe estar abierto el dia que deseas ir.";
                          vm.$bvModal.show("alerta");
                        }
                        //alert("este es el datoxdxd: "+horario.horaFin.toString());
                        //horaI=horario.horaInicio.getHours();
                        //horaF=horario.horaFin.getHours();
                        break;
                    }
                  });
                  /////////////////-----------------------------------------------------------
                } else {
                  vm.mensajeAlerta = "Este negocio no abre los viernes.";
                  vm.$bvModal.show("alerta");
                }
                break;
              case 6: //sabado
                //Declaraciones ejecutadas cuando el resultado de expresión coincide con valorN
                if (diasopen.includes(6)) {
                  ////////////-------------------validacion de hora :'(-------------------
                  this.negocio.horarios.forEach(function(horario) {
                    switch (horario.dia) {
                      case "Sábado": //0
                        var hola = horario.horaInicio.toString();
                        var dos = hola.split(":", 3);
                        hI.setHours(dos[0], dos[1]);
                        var hola1 = horario.horaFin.toString();
                        var tres = hola1.split(":", 3);
                        hF.setHours(tres[0], tres[1]);
                        var iniHo = hI.getHours();
                        var fiHo = hF.getHours();
                        var iniMi = hI.getMinutes();
                        var fiMi = hF.getMinutes();
                        if (horaSel >= iniHo && horaSel <= fiHo) {
                          if (horaSel == iniHo) {
                            if (minuSel > iniMi) {
                              vali = true;
                            }
                          } else {
                            if (horaSel == fiHo) {
                              if (minuSel < fiMi) {
                                vali = true;
                              }
                            } else {
                              vali = true;
                            }
                          }
                        }
                        if (!vali) {
                          vm.mensajeAlerta =
                            "Revisa la hora de tu reservación, el lugar debe estar abierto el dia que deseas ir.";
                          vm.$bvModal.show("alerta");
                        }
                        //alert("este es el datoxdxd: "+horario.horaFin.toString());
                        //horaI=horario.horaInicio.getHours();
                        //horaF=horario.horaFin.getHours();
                        break;
                    }
                  });
                  /////////////////-----------------------------------------------------------
                } else {
                  vm.mensajeAlerta = "Este negocio no abre los sabados.";
                  vm.$bvModal.show("alerta");
                }
                break;
              default:
                vm.mensajeAlerta =
                  "No se pudo especificar el día seleccionado, por favor seleccionalo otra vez.";
                vm.$bvModal.show("alerta");
                break;
            }
            /////////////////////////////////vali=true;//////////////////////////////////
            if (vali) {
              axios
                .post(
                  "https://localhost:5001/AforoMex/Negocios/reservaciones/post",
                  {
                    FechaReserv: /*parseISO(*/ format(
                      parseISO(this.form.fechaHora),
                      "MM/dd/yyyy"
                    ) /*)*/,
                    NumLugares: this.form.numero,
                    Estado: format(
                      parseISO(this.form.fechaHora),
                      "yyyy-MM-dd HH:mm:ss"
                    ),
                    IdUsuario: localStorage["idUsuario"],
                    IdNegocio: this.idNegocio
                    //  ex: vnp,
                  }
                )
                .then(response => {
                  if(response.data.objeto==null){
            vm.mensajeAlerta =
              "Error en el servidor de AforoMex. Inténtelo de nuevo más tarde.";
              vm.$bvModal.show("alerta");
          }else{
            alert("45");
            vm.mensajeAlerta =
              "Se registró correctamente tu reservación.";
              vm.$bvModal.show("Gracias :)");
          }
                  //EventBus.$emit("iniciarSesion");
                  this.$router.push({ name: "Inicio" });
                })
                .catch(e => {
                  if (e.response.data.error && e.response.status == 409) {
                    vm.mensajeAlerta = e.response.data.mensaje;
                  } else {
                    vm.mensajeAlerta =
                      "Error en el servidor de AforoMex. Inténtelo de nuevo más tarde.";
                  }
                  vm.$bvModal.show("alerta");
                });
            }
          } else {
            vm.mensajeAlerta =
              "Solo se puede establecer una reservación con una semana de antelación.";
            vm.$bvModal.show("alerta");
          }
        } else {
          vm.mensajeAlerta =
            "no puedes crear una reservación de un día que ya pasó.";
          vm.$bvModal.show("alerta");
        }
      }
    },
    formatearHoras: function() {
      this.negocio.horarios.forEach(element => {
        element.horaInicio = element.horaInicio.substring(0, 5);
        element.horaFin = element.horaFin.substring(0, 5);
      });
    },
    calcularAforos: function() {
      this.aforoSinReservacion = Math.round(
        this.negocio.aforo * (1 - this.negocio.aforoReservaciones)
      );
      this.cupoDisSinReservacion =
        this.aforoSinReservacion - this.negocio.cupoOcupado;
    }
  }
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