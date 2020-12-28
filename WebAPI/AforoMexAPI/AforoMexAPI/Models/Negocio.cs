using System;
using System.Collections.Generic;

#nullable disable

namespace AforoMexAPI.Models
{
    public partial class Negocio
    {
        public Negocio()
        {
            Direcciones = new HashSet<Direccione>();
            Horarios = new HashSet<Horario>();
            Reservaciones = new HashSet<Reservacione>();
        }

        public int IdNegocio { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public string Correo { get; set; }
        public string SitioWeb { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Descripcion { get; set; }
        public string Facebook { get; set; }
        public string Logo { get; set; }
        public bool Abierto { get; set; }
        public int Aforo { get; set; }
        public int CupoOcupado { get; set; }
        public bool PermitirReservaciones { get; set; }
        public decimal AforoReservaciones { get; set; }
        public int LimiteReservaciones { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<Direccione> Direcciones { get; set; }
        public virtual ICollection<Horario> Horarios { get; set; }
        public virtual ICollection<Reservacione> Reservaciones { get; set; }
    }
}
