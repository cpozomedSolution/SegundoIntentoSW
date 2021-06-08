using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SWReenviarSegundoIntento.BCP;
using SWReenviarSegundoIntento.DTO;


namespace ReenviarSegundoIntento
{
    public class ReenviarSegundoIntento
    {
        private ReenviarSegundoIntentoBCP reenviarSegundoIntentoBCP = new ReenviarSegundoIntentoBCP();

        #region Log
        /// <summary>
        /// Log
        /// </summary>
        /// <param name="Message"></param>
        public void LogSW(string Message)
        {
            StreamWriter sw = null;
            try
            {
                //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                //string filepath = path + "\\LogFile.txt";
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + Message);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region IniciaProceso

        public void IniciaProcesoenCentros()
        {
            try
            {
                int? retorno = 0;
                //RECOLECTA CENTROS
                List<CentrosDTO> listaCentros = reenviarSegundoIntentoBCP.ObtenerTodosLosCentros();

                foreach (var item in listaCentros)
                {
                    retorno = reenviarSegundoIntentoBCP.ReenviaSegundoIntento(Convert.ToInt32(item.CentroId));
                    if(retorno == 0)
                    {
                        LogSW("Se ejecuto correctamente el reenvio");
                    }
                   

                }

                //FOREACH POR CADA CENTRO INICIA PROCESO


            }
            catch (Exception)
            {

                throw;
            }
        }



        #endregion
    }
}
