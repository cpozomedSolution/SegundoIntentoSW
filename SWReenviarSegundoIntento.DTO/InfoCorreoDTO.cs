using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWReenviarSegundoIntento.DTO
{
    public class InfoCorreoDTO
    {
        //CorreoReposicion
        private string rutPaciente;
        private string nombrePaciente;
        private string idReferencia;
        private string nombreCategoria;
        private string nombreSubCategoria;
        private string nombreModelo;
        private string nombreMedidas;
        private string fechaProcedimiento;
        private string guiaConsumido;
        private string nombreProveedor;
        private string nombreCentro;
        private string lote;
        private string nombreLicitacion;
        private string codigoLicitacion;
        private int idProveedor;
        //CorreoReposicion

        //CorreoLicitacion
        private string licitacionId;
        private string codigoExterno;
        private string centroId;
        private string descripcion;
        private string duracion;
        private string estado;
        private string fechaadjudicacion;
        private string nombre;
        private string cantidaditem;
        private string valoritem;
        private string proveedorId;
        private string fechaTermino;
        private string cantidadReal;
        private string proveedor;
        private string centro;

        //CorreoLicitacion


        private List<DireccionEmailDTO> correos;
        public InfoCorreoDTO()
        {
        }

        public InfoCorreoDTO(InfoCorreoDTO obj)
        {
            //CorreoReposicion
            this.rutPaciente = obj.rutPaciente;
            this.nombrePaciente = obj.nombrePaciente;
            this.idReferencia = obj.idReferencia;
            this.nombreCategoria = obj.nombreCategoria;
            this.nombreSubCategoria = obj.nombreSubCategoria;
            this.nombreModelo = obj.nombreModelo;
            this.nombreMedidas = obj.nombreMedidas;
            this.fechaProcedimiento = obj.fechaProcedimiento;
            this.guiaConsumido = obj.guiaConsumido;
            this.nombreProveedor = obj.NombreProveedor;
            this.NombreCentro = obj.NombreCentro;
            this.lote = obj.lote;
            this.nombreLicitacion = obj.nombreLicitacion;
            this.codigoLicitacion = obj.codigoLicitacion;
            this.idProveedor = obj.idProveedor;
            this.correos = obj.correos;
            //CorreoReposicion

            //CorreoLicitacion
            this.licitacionId = obj.licitacionId;
            this.codigoExterno = obj.codigoExterno;
            this.centroId = obj.centroId;
            this.descripcion = obj.descripcion;
            this.duracion = obj.duracion;
            this.estado = obj.estado;
            this.fechaadjudicacion = obj.fechaadjudicacion;
            this.nombre = obj.nombre;
            this.cantidaditem = obj.cantidaditem;
            this.valoritem = obj.valoritem;
            this.proveedorId = obj.proveedorId;
            this.fechaTermino = obj.fechaTermino;
            this.cantidadReal = obj.cantidadReal;
            this.proveedor = obj.proveedor;
            this.centro = obj.centro;
            //CorreoLicitacion

        }

        //CorreoReposicion
        public string RutPaciente { get => rutPaciente; set => rutPaciente = value; }
        public string NombrePaciente { get => nombrePaciente; set => nombrePaciente = value; }
        public string IdReferencia { get => idReferencia; set => idReferencia = value; }
        public string NombreCategoria { get => nombreCategoria; set => nombreCategoria = value; }
        public string NombreSubCategoria { get => nombreSubCategoria; set => nombreSubCategoria = value; }
        public string NombreModelo { get => nombreModelo; set => nombreModelo = value; }
        public string NombreMedidas { get => nombreMedidas; set => nombreMedidas = value; }
        public string FechaProcedimiento { get => fechaProcedimiento; set => fechaProcedimiento = value; }
        public string GuiaConsumido { get => guiaConsumido; set => guiaConsumido = value; }
        public string NombreProveedor { get => nombreProveedor; set => nombreProveedor = value; }
        public string NombreCentro { get => nombreCentro; set => nombreCentro = value; }
        public string Lote { get => lote; set => lote = value; }
        public string NombreLicitacion { get => nombreLicitacion; set => nombreLicitacion = value; }
        public string CodigoLicitacion { get => codigoLicitacion; set => codigoLicitacion = value; }
        public int IdProveedor { get => idProveedor; set => idProveedor = value; }
        //CorreoReposicion

        //CorreoLicitacion
        public string LicitacionId { get => licitacionId; set => licitacionId = value; }
        public string CodigoExterno { get => codigoExterno; set => codigoExterno = value; }
        public string CentroId { get => centroId; set => centroId = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Duracion { get => duracion; set => duracion = value; }
        public string Estado { get => estado; set => estado = value; }
        public string Fechaadjudicacion { get => fechaadjudicacion; set => fechaadjudicacion = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Cantidaditem { get => cantidaditem; set => cantidaditem = value; }
        public string Valoritem { get => valoritem; set => valoritem = value; }
        public string ProveedorId { get => proveedorId; set => proveedorId = value; }
        public string FechaTermino { get => fechaTermino; set => fechaTermino = value; }
        public string CantidadReal { get => cantidadReal; set => cantidadReal = value; }
        //CorreoLicitacion

        public List<DireccionEmailDTO> Correos { get => correos; set => correos = value; }
        public string Proveedor { get => proveedor; set => proveedor = value; }
        public string Centro { get => centro; set => centro = value; }
    }
}
