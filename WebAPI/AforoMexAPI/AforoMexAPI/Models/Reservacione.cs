using System;
using System.Collections.Generic;

#nullable disable

namespace AforoMexAPI.Models
{
    public partial class Reservacione
    {
        public int IdReservacion { get; set; }
        public DateTime FechaReserva { get; set; }
        public int NumLugares { get; set; }
        public string Estado { get; set; }
        public int IdUsuario { get; set; }
        public int IdNegocio { get; set; }

        public virtual Negocio IdNegocioNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
