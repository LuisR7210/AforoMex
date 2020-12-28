using System;
using System.Collections.Generic;

#nullable disable

namespace AforoMexAPI.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Negocios = new HashSet<Negocio>();
            Reservaciones = new HashSet<Reservacione>();
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; }

        public virtual ICollection<Negocio> Negocios { get; set; }
        public virtual ICollection<Reservacione> Reservaciones { get; set; }
    }
}
