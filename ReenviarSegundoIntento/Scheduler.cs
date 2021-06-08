using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ReenviarSegundoIntento
{
    public partial class Scheduler : ServiceBase
    {
        private Timer timer1 = null;
        private ReenviarSegundoIntento reenviarSegundoIntento = new ReenviarSegundoIntento();
        public Scheduler()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                timer1 = new Timer();
                this.timer1.Interval = 86400000;//39600000;//46800000;//21600000;//64800000;
                this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Tick);
                timer1.Enabled = true;
                reenviarSegundoIntento.LogSW("Servicio Windows Iniciado");

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void timer1_Tick(object sender, ElapsedEventArgs e)
        {

            reenviarSegundoIntento.LogSW("Se ejecuto el Timer");
            //LOGICA OC
            reenviarSegundoIntento.IniciaProcesoenCentros();
        }


        protected override void OnStop()
        {
            timer1.Enabled = false;
            reenviarSegundoIntento.LogSW("Servicio Windows Detenido");
        }

        public void onDebug()
        {
            OnStart(null);

        }
    }
}
