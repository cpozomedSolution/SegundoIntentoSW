using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWReenviarSegundoIntento.DTO
{
    public  class CentrosDTO
    {
        private string centroId;
        private string organismoId;

        public string CentroId { get => centroId; set => centroId = value; }
        public string OrganismoId { get => organismoId; set => organismoId = value; }
    }
}
