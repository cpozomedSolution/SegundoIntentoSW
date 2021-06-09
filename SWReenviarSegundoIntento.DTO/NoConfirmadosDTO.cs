using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWReenviarSegundoIntento.DTO
{
    public class NoConfirmadosDTO
    {
        private int idFicha;
        private int idStockInsumo;
        private string fecha;
        private string nombrePaciente;
        private string rutPaciente;
        private string nombreMedico;
        private string codigoPaciente;
        private string nombreModelo;
        private int idCentro;
        private int idCatalogo;

        public int IdFicha { get => idFicha; set => idFicha = value; }
        public int IdStockInsumo { get => idStockInsumo; set => idStockInsumo = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public string NombrePaciente { get => nombrePaciente; set => nombrePaciente = value; }
        public string RutPaciente { get => rutPaciente; set => rutPaciente = value; }
        public string NombreMedico { get => nombreMedico; set => nombreMedico = value; }
        public string CodigoPaciente { get => codigoPaciente; set => codigoPaciente = value; }
        public string NombreModelo { get => nombreModelo; set => nombreModelo = value; }
        public int IdCentro { get => idCentro; set => idCentro = value; }
        public int IdCatalogo { get => idCatalogo; set => idCatalogo = value; }
    }
}
