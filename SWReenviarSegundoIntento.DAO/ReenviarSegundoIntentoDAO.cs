using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWReenviarSegundoIntento.DTO;

namespace SWReenviarSegundoIntento.DAO
{
    public class ReenviarSegundoIntentoDAO
    {

        public List<SP_OBTENER_TODOS_LOS_CENTROS_Result> ObtenerTodosLosCentros()
        {
            using (var db = new SWReenviarSegundoIntentoEntities())
            {
                var data = db.SP_OBTENER_TODOS_LOS_CENTROS();

                return data.ToList();
            }

        }

        public List<SP_SW_OBTENER_SEGUNDO_INTENTO_POR_CENTROID_Result> ObtenerSegundosIntentosPorCentro(int centroId)
        {
            using (var db = new SWReenviarSegundoIntentoEntities())
            {
                var data = db.SP_SW_OBTENER_SEGUNDO_INTENTO_POR_CENTROID(centroId);

                return data.ToList();
            }

        }

        public int? ActualizaSegundoIntento(int idCatalogo, int idFichaStock, int idCentro, int idDespacho)
        {
            int? iData = 0;
            using (var db = new SWReenviarSegundoIntentoEntities())
            {
                var data = db.SP_SW_ACTUALIZAR_SEGUNDO_INTENTO(idCatalogo, idFichaStock, idCentro, idDespacho);

                if (data != null)
                    iData = data.FirstOrDefault();

                return iData;
            }
        }

        public List<SP_OBTENER_INSUMOS_PACIENTE_POR_FICHA_Result> obtenerInsumosPacientes(int idFicha, int idCentro)
        {
            using (var db = new SWReenviarSegundoIntentoEntities())
            {
                var data = db.SP_OBTENER_INSUMOS_PACIENTE_POR_FICHA(Convert.ToInt32(idFicha), idCentro);

                return data.ToList();

            }
        }

        public int? GuardarInsumosPAciente(int id)
        {
            int? iData = 0;
            using (var db = new SWReenviarSegundoIntentoEntities())
            {
                var data = db.SP_GUARDAR_STOCK_PACIENTE(id);

                if (data != null)
                    iData = data.FirstOrDefault();

                return iData;
            }
        }

        public int? AgregarOrdenCompraAutomatizacion(AgregarOCDTO objOrdenCompra,
                                                                     int idCentro)
        {
            int? iData = 0;
            using (var db = new SWReenviarSegundoIntentoEntities())
            {
                var data = db.SP_INSERTAR_ORDEN_COMPRA_AUTO(Convert.ToInt32(objOrdenCompra.IdLicitacion),
                                                            objOrdenCompra.CodOc,
                                                            Convert.ToInt32(objOrdenCompra.IdProveedor),
                                                            Convert.ToInt32(objOrdenCompra.IdMoneda),
                                                            Convert.ToInt32(objOrdenCompra.IdPais),
                                                            Convert.ToInt32(objOrdenCompra.IdTipoOc),
                                                            objOrdenCompra.DescOc,
                                                            objOrdenCompra.NombreOc,
                                                            idCentro
                                                             );

                if (data != null)
                    iData = data.FirstOrDefault();

                return iData;
            }
        }

        public int? GenerarDespachoAutomatizacion(int idCentro, int idUsuario)
        {
            int? iData = 0;
            using (var db = new SWReenviarSegundoIntentoEntities())
            {
                var data = db.SP_GENERACION_DE_DESPACHOS_REPOSICION_AUTO(idCentro, idUsuario);

                if (data != null)
                    iData = data.FirstOrDefault();

                return iData;
            }
        }

        public int? AgregarReposicionAutomatizacion(int idCatalogo, int idFichaStock, int idUsuario, string comentario, int idCentro, int idOc, int idConsignacion, int? idDespacho)
        {
            int? iData = 0;
            using (var db = new SWReenviarSegundoIntentoEntities())
            {

                var data = db.SP_INSERTAR_NUEVA_REPOSICION_AUTO(idCatalogo, idFichaStock, idUsuario, comentario, idCentro, idOc, idConsignacion, idDespacho);

                if (data != null)
                    iData = data.FirstOrDefault();

                return iData;
            }
        }

        public List<SP_OBTENER_INFO_CORREO_Result> ObtenerInfoCorreo(int idFichastock)
        {
            using (var db = new SWReenviarSegundoIntentoEntities())
            {

                var data = db.SP_OBTENER_INFO_CORREO(idFichastock);

                return data.ToList();

            }
        }

        public List<SP_OBTENER_DIRECCIONES_EMAIL_POR_PROVEEDOR_Result> ObtenerDireccionesCorreo(int idTipoCorreo, int idProveedor, int idCentro, int idTerritorio)
        {
            using (var db = new SWReenviarSegundoIntentoEntities())
            {

                var data = db.SP_OBTENER_DIRECCIONES_EMAIL_POR_PROVEEDOR(idTipoCorreo, idProveedor, idCentro, idTerritorio);

                return data.ToList();

            }
        }

        public List<SP_SW_OBTENER_INSUMOS_NO_CONFIRMADOS_POR_CENTROID_Result> ObtenerInsumosNoConfirmados(int idCentro)
        {
            using (var db = new SWReenviarSegundoIntentoEntities())
            {

                var data = db.SP_SW_OBTENER_INSUMOS_NO_CONFIRMADOS_POR_CENTROID(idCentro);

                return data.ToList();

            }
        }




    }
}
