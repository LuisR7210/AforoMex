using System;
using System.Collections.Generic;

#nullable disable

namespace AforoMexAPI.Models
{
    public partial class Horario
    {
        public int IdHorario { get; set; }
        public string Dia { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int IdNegocio { get; set; }

        public virtual Negocio IdNegocioNavigation { get; set; }
    }
}
