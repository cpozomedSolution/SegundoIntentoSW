using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SWReenviarSegundoIntento.DAO;
using SWReenviarSegundoIntento.DTO;

namespace SWReenviarSegundoIntento.BCP
{
    public class ReenviarSegundoIntentoBCP
    {
        ReenviarSegundoIntentoDAO reenviarSegundoIntentoDAO = new ReenviarSegundoIntentoDAO();
        int idUsuario = 3;

        public List<CentrosDTO> ObtenerTodosLosCentros()
        {
            try
            {
                List<CentrosDTO> listacentrosDTO = new List<CentrosDTO>();
                List<SP_OBTENER_TODOS_LOS_CENTROS_Result> listacentrosSP = reenviarSegundoIntentoDAO.ObtenerTodosLosCentros();

                foreach (var item in listacentrosSP)
                {
                    CentrosDTO listacentrosDto = new CentrosDTO();
                    listacentrosDto.CentroId = item.NID_CENTRO.ToString();
                    listacentrosDto.OrganismoId = item.NCODIGO_ORGANISMO.ToString();

                    listacentrosDTO.Add(listacentrosDto);
                }

                return listacentrosDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? ReenviaSegundoIntento(int centroId)
        {
            int? retornoactualizacionconsignacion = 0;
            int? retornoreenvio = 0;
            //seleccionar todos los consumos segundo intento por centro
            List<SP_SW_OBTENER_SEGUNDO_INTENTO_POR_CENTROID_Result> listaSegundoIntentoSP = reenviarSegundoIntentoDAO.ObtenerSegundosIntentosPorCentro(centroId);

            foreach (var item in listaSegundoIntentoSP)
            {
                //ejecutar sp que obtenga consignacion por catalogo, y que esta consignacion actualice la consignacion en el despacho del item
                retornoactualizacionconsignacion = reenviarSegundoIntentoDAO.ActualizaSegundoIntento(item.CatalogoId, item.FichaStockId, item.CentroId, item.DespachoId);
                if (retornoactualizacionconsignacion == 0)
                {
                    GuardarInsumosPaciente(item.FichaId,item.CentroId);
                }

                Task.Delay(TimeSpan.FromSeconds(3));

            }

            //foreach por cada consumo 
            //obtiene el catalogo y busca si esta consignado y rescata consignacion
            //update de consignacion
            //devuelve estado a preliminar en ficha stock
            //ejecutar metodo confirmar

            return retornoreenvio;
        }

        public void GuardarInsumosPaciente(int idFicha, int centroId)
        {
            int? retorno = 0;
            int? retornooc = 0;
            int? retornoDespacho = 0;
            int? retornoReposicion = 0;
            int idOc = 0;
            string idCatalogo = string.Empty;
            string idFichaStock = string.Empty;
            string idConsignacion = string.Empty;
            string comentario = "Reposicion Automatica";
            List<SP_OBTENER_INSUMOS_PACIENTE_POR_FICHA_Result> listaInsumosPacientes = reenviarSegundoIntentoDAO.obtenerInsumosPacientes(idFicha);

            foreach (var item in listaInsumosPacientes)
            {
                if(item.LicitacionId != 0)
                {
                    if (DateTime.Parse(item.FechaActual) <= DateTime.Parse(item.FechaTermino))
                    {
                        if (item.IDESTADO == 1)
                        {
                            retorno = reenviarSegundoIntentoDAO.GuardarInsumosPAciente(Convert.ToInt32(item.ID));

                            if (retorno == 0)
                            {
                                AgregarOCDTO datosIngreso = new AgregarOCDTO();

                                datosIngreso.CodOc = "CodigoTemporal";
                                datosIngreso.DescOc = "";
                                datosIngreso.IdLicitacion = item.LicitacionId.ToString();
                                datosIngreso.IdMoneda = "1";
                                datosIngreso.IdPais = "1";
                                datosIngreso.IdTipoOc = "2";//Tipo Reposicion
                                datosIngreso.NombreOc = "CodigoTemporal";
                                datosIngreso.IdProveedor = item.ProveedorId.ToString();

                                retornooc = reenviarSegundoIntentoDAO.AgregarOrdenCompraAutomatizacion(datosIngreso, centroId);

                                if (retornooc != null)
                                {

                                    //TBLPRO_DESPACHO_INSUMOS/TBLPRO_SEGUIMIENTO_DESPACHO
                                    retornoDespacho = reenviarSegundoIntentoDAO.GenerarDespachoAutomatizacion(centroId, idUsuario);
                                    if (retornoDespacho != null)
                                    {
                                        idOc = retornooc == null ? 0 : retornooc.Value;
                                        idCatalogo = item.CatalogoId.ToString();
                                        idFichaStock = item.ID.ToString();
                                        idConsignacion = item.ConsignacionId.ToString();

                                        retornoReposicion = reenviarSegundoIntentoDAO.AgregarReposicionAutomatizacion(Convert.ToInt32(idCatalogo), Convert.ToInt32(idFichaStock), idUsuario, comentario, centroId, idOc, Convert.ToInt32(idConsignacion), retornoDespacho);
                                    }
                                }
                            }

                            if (retorno == 0 && retornooc != null && retornoDespacho != null && retornoReposicion != null)
                            {
                                EnviarCorreoReposicion(Convert.ToInt32(idFichaStock));
                            }

                        }
                    }
                    else
                    {
                        retorno = reenviarSegundoIntentoDAO.GuardarInsumosPAciente(Convert.ToInt32(item.ID));

                    }
                }
            }

        }


        #region EnvioCorreo
        private void EnviarCorreoReposicion(int idFichaStock)
        {
            string remitente = "";
            string aliasRemitente = "";
            List<string> listaDestinatarios = new List<string>();
            List<string> listaDestinatariosConCopia = new List<string>();
            List<string> listaDestinatariosConCopiaOculta = new List<string>();

            InfoCorreoDTO objInfoCorreo = new InfoCorreoDTO(ObtenerInfoCorreo(idFichaStock));

            foreach (var direccion in objInfoCorreo.Correos)
            {
                switch (direccion.IdRol)
                {
                    case 1:
                        remitente = direccion.Direccion;
                        if (!String.IsNullOrEmpty(direccion.Alias))
                        {
                            aliasRemitente = direccion.Alias;
                        }
                        break;
                    case 2:
                        listaDestinatarios.Add(direccion.Direccion);
                        break;
                    case 3:
                        listaDestinatariosConCopia.Add(direccion.Direccion);
                        break;
                    case 4:
                        listaDestinatariosConCopiaOculta.Add(direccion.Direccion);
                        break;
                    default:
                        break;

                }
            }

            //Variables Técnicas correo
            var Host = "smtp.gmail.com";
            var Puerto = "587";
            var Usuario = "CATHREPORTSPA@CATHREPORT.COM";
            var Pass = "C4THR3P0RT@2019";

            SmtpClient smtp = new SmtpClient
            {
                Host = Host,
                Port = Int32.Parse(Puerto),
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(Usuario, Pass),
                EnableSsl = true,//obligatorio para gmail
                Timeout = 10000
            };

            string Asunto = "Alerta Consumo Licitación " + objInfoCorreo.CodigoLicitacion;
            string Mensaje = ObtenerCuerpoCorreoReposicion(objInfoCorreo);

            MailMessage message = new MailMessage
            {
                Body = Mensaje,
                IsBodyHtml = true,
                Subject = Asunto
            };

            //Manejo Remitente
            if (String.IsNullOrEmpty(aliasRemitente))
            {
                message.From = new MailAddress(remitente);
            }
            else
            {
                message.From = new MailAddress(remitente, aliasRemitente);
            }

            //Manejo Destinatario
            if (listaDestinatarios.Count() > 0)
            {
                message.To.Add(String.Join(", ", listaDestinatarios));
            }

            //Manejo Destinatario "Con Copia"
            if (listaDestinatariosConCopia.Count() > 0)
            {
                message.CC.Add(String.Join(", ", listaDestinatariosConCopia));
            }

            //Manejo Destinatario "Con Copia Oculta"
            if (listaDestinatariosConCopiaOculta.Count() > 0)
            {
                message.Bcc.Add(String.Join(", ", listaDestinatariosConCopiaOculta));
            }


            smtp.Send(message);

        }

        public InfoCorreoDTO ObtenerInfoCorreo(int idFichaStock)
        {



            List<SP_OBTENER_INFO_CORREO_Result> listaInfoCorreo = new List<SP_OBTENER_INFO_CORREO_Result>();
            const int IdCorreoReposicion = 2;

            try
            {

                InfoCorreoDTO InfoCorreo = new InfoCorreoDTO();
                listaInfoCorreo = reenviarSegundoIntentoDAO.ObtenerInfoCorreo(idFichaStock);

                if (listaInfoCorreo != null)
                {
                    foreach (var item in listaInfoCorreo)
                    {


                        InfoCorreo.IdReferencia = item.idReferencia;
                        InfoCorreo.NombrePaciente = item.SNOMBRE_PACIENTE;
                        InfoCorreo.RutPaciente = item.SRUT_PACIENTE;
                        InfoCorreo.NombreCategoria = item.nombreCategoria;
                        InfoCorreo.NombreMedidas = item.Medidas;
                        InfoCorreo.NombreModelo = item.nombreModelo;
                        InfoCorreo.NombreSubCategoria = item.nombreSubCategoria;
                        InfoCorreo.GuiaConsumido = item.GDConsumida;
                        InfoCorreo.FechaProcedimiento = item.fechaProcedimiento.ToString("dd-MM-yyyy");
                        InfoCorreo.NombreProveedor = item.nombreProveedor;
                        InfoCorreo.NombreCentro = item.nombreCentro;
                        InfoCorreo.Lote = item.lote;
                        InfoCorreo.NombreLicitacion = item.nombreLicitacion;
                        InfoCorreo.CodigoLicitacion = item.codigoLicitacion;
                        InfoCorreo.IdProveedor = item.idProveedor;
                        InfoCorreo.Correos = ObtenerDireccionesCorreo(IdCorreoReposicion, InfoCorreo.IdProveedor);
                    }
                }

                return InfoCorreo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DireccionEmailDTO> ObtenerDireccionesCorreo(int IdCorreoReposicion, int IdProveedor)
        {

            List<SP_OBTENER_DIRECCIONES_EMAIL_POR_PROVEEDOR_Result> SPDireccionesCorreos = reenviarSegundoIntentoDAO.ObtenerDireccionesCorreo(IdCorreoReposicion, IdProveedor);
            List<DireccionEmailDTO> listaCorreo = new List<DireccionEmailDTO>();

            foreach (var spDireccionCorreo in SPDireccionesCorreos)
            {
                DireccionEmailDTO direccionCorreoTemporal = new DireccionEmailDTO
                {
                    Direccion = spDireccionCorreo.nombreCorreo,
                    Alias = spDireccionCorreo.alias,
                    IdRol = spDireccionCorreo.idRol
                };
                listaCorreo.Add(direccionCorreoTemporal);
            }

            return listaCorreo;
        }

        public string ObtenerCuerpoCorreoReposicion(InfoCorreoDTO correo)
        {
            string mensaje = "<html><head><title>Alerta de Reposición</title></head><body><table><tr><td><table><tr><td>" +

            "Estimado equipo <b>" + correo.NombreProveedor + "</b>," +
            "<br> Se comunica el consumo de el/los siguiente(s) insumo(s) en el centro <b>" + correo.NombreCentro + "</b> correspondiente " +
            "a la licitación N° <b>" + correo.CodigoLicitacion + "</b> \"<b>" + correo.NombreLicitacion + "</b>\":" +

            "<br><br><table border=\"1\">" +
            "<tr>" +
            "<th> Descripción </th>" +
            "<th> Código </th>" +
            "<th> Lote </th>" +
            "<th> GD Consumida </th>" +
            "<th> Paciente </th>" +
            "<th> Fecha Procedimiento </th>" +
            "</tr>" +
             "<tr>" +
            "<td> " + correo.NombreCategoria + " " + correo.NombreSubCategoria + " " + correo.NombreModelo + " " + correo.NombreMedidas + "</th>" +
            "<td> " + correo.IdReferencia + "</th>" +
            "<td> " + correo.Lote + "</th>" +
            "<td> " + correo.GuiaConsumido + " </th>" +
            "<td> " + correo.NombrePaciente + " </th>" +
            "<td> " + correo.FechaProcedimiento + " </th>" +
            "</tr>" +
            "</table>" +
            "<br>Equipo CathReport.<br>" +
            "<font style=\"font - family:arial,sans - serif; font - size:9px; color:#484848;text-align:center;padding:5px 10px\">Este mail fue generado automáticamente por sistema CathReport, favor no responder." +
            " <br>© CathReport 2019 - Todos los derechos reservados.</font>" +
            "</td></tr></table></td></tr></table></body></html>";

            return mensaje;
        }
        #endregion




    }
}
