using CALLPLUS_ELO_SES.SERVICOPAUSA.Jobs;
using CALLPLUS_ELO_SES.SERVICOPAUSA.Utils;
using System;
using System.ServiceProcess;
using System.Timers;

namespace CALLPLUS_ELO_SES.SERVICOPAUSA
{
    public partial class Service1 : ServiceBase
    {
        PausaAutomaticaJob _pausaJob;
        private EscreverLogs _escritor;
        Timer timerRodarProcesso = new Timer();
        int tempo = 30;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            IniciarTimer();
        }

        protected override void OnStop()
        {

        }

        private void IniciarTimer()
        {
            timerRodarProcesso.Elapsed += TimerRodarProcesso_Elapsed;
            timerRodarProcesso.Interval = tempo * 1000;
            timerRodarProcesso.Enabled = true;
            timerRodarProcesso.AutoReset = true;
            timerRodarProcesso.Start();
        }

        private void TimerRodarProcesso_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                _escritor = new EscreverLogs();
                var random = new Random();
                var processo = random.Next(0, 15000);
                _escritor.Escrever("Iniciando processo: " + processo);

                _pausaJob = new PausaAutomaticaJob();
                _pausaJob.MudarPausasUsuarios();

                _escritor.Escrever("Processo finalizado: " + processo);
            }
            catch (Exception ex)
            {
                _escritor.Escrever(ex.Message);
            }
        }
    }
}
