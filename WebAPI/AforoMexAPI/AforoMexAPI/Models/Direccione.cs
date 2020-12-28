using System;
using System.Collections.Generic;

#nullable disable

namespace AforoMexAPI.Models
{
    public partial class Direccione
    {
        public int IdDireccion { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public int CodigoPostal { get; set; }
        public int Numero { get; set; }
        public int? NumeroInterior { get; set; }
        public int IdNegocio { get; set; }

        public virtual Negocio IdNegocioNavigation { get; set; }
    }
}
