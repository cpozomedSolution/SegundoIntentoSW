using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWReenviarSegundoIntento.DTO
{
    public class AgregarOCDTO
    {
        private string codOc;
        private string idProveedor;
        private string idLicitacion;
        private string idPais;
        private string idMoneda;
        private string nombreOc;
        private string descOc;
        private string idTipoOc;

        public string CodOc { get => codOc; set => codOc = value; }
        public string IdProveedor { get => idProveedor; set => idProveedor = value; }
        public string IdLicitacion { get => idLicitacion; set => idLicitacion = value; }
        public string IdPais { get => idPais; set => idPais = value; }
        public string IdMoneda { get => idMoneda; set => idMoneda = value; }
        public string NombreOc { get => nombreOc; set => nombreOc = value; }
        public string DescOc { get => descOc; set => descOc = value; }
        public string IdTipoOc { get => idTipoOc; set => idTipoOc = value; }
    }
}
