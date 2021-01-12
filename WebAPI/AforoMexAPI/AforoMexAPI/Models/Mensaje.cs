using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AforoMexAPI.Models
{
    public class Mensaje
    {
        public bool error { get; set; }
        public string mensaje { get; set; }
        public Object objeto { get; set; }
    }
}
