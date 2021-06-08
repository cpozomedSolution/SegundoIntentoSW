using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWReenviarSegundoIntento.DTO
{
    public class DireccionEmailDTO
    {
        private string direccion;
        private string rol;
        private int idRol;
        private string alias;

        public string Direccion { get => direccion; set => direccion = value; }
        public string Rol { get => rol; set => rol = value; }
        public string Alias { get => alias; set => alias = value; }
        public int IdRol { get => idRol; set => idRol = value; }
    }
}
