using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using v1Tabulare_z13.ws;

namespace v1Tabulare_z13.operador
{
    public partial class fPlayerAudioOlos : Form
    {
        private string _nomeArquivoReproducao { get; set; }
        private string _loginAgente { get; set; }
        public TimeSpan Duracao { get; private set; }
        public EstadoReproducao EstadoDeReproducao { get; private set; }

        private fPlayerAudioOlos()
        {
            InitializeComponent();
        }


        public fPlayerAudioOlos(string loginAgente, string nomeArquivoAudio)
        {
            InitializeComponent();
            _nomeArquivoReproducao = nomeArquivoAudio;
            _loginAgente = loginAgente;
            Duracao = new TimeSpan(0, 0, 0, 0);
            EstadoReproducao EstadoDeReproducao = EstadoReproducao.Default;
        }

        public void Reproduzir()
        {
            StartPosition = FormStartPosition.CenterScreen;
            timerAtualizarTela.Start();
            btnPlayResume_Click(null, null);
            ShowDialog();
        }

        private async Task<RetornoServico> Play()
        {
            return await Task.Run(() =>
             {
                 var retorno = OlosWsVoiceSupport.VoiceSupportStart(_loginAgente, _nomeArquivoReproducao);

                 if (retorno.Sucesso)
                     EstadoDeReproducao = EstadoReproducao.Reproduzindo;

                 return retorno;
             });
        }

        private async Task<RetornoServico> Pause()
        {
            return await Task.Run(() =>
          {
              var retorno = OlosWsVoiceSupport.VoiceSupportPause(_loginAgente);

              if (retorno.Sucesso)
                  EstadoDeReproducao = EstadoReproducao.Pausado;

              return retorno;
          });
        }

        private async Task<RetornoServico> Resume()
        {
            return await Task.Run(() =>
             {
                 var retorno = OlosWsVoiceSupport.VoiceSupportResume(_loginAgente);

                 if (retorno.Sucesso)
                     EstadoDeReproducao = EstadoReproducao.Reproduzindo;

                 return retorno;
             });
        }

        private async Task<RetornoServico> Stop()
        {
            return await Task.Run(() =>
            {
                var retorno = OlosWsVoiceSupport.VoiceSupportStop(_loginAgente);

                if (retorno.Sucesso)
                    EstadoDeReproducao = EstadoReproducao.Encerrado;

                return retorno;
            });
        }

        private async void btnPlayResume_Click(object sender, EventArgs e)
        {
            RetornoServico resultado;
            ConfigurarBotoes();
            //btnPlayResume.Enabled = false;
            if (EstadoDeReproducao == EstadoReproducao.Pausado)
            {
                resultado = await Resume();
            }
            else
            {
                resultado = await Play();
            }

            if (resultado.Sucesso)
            {
                timerDuracao.Start();
            }
            else
            {
                ExibirMensagemDeErro(resultado.Mensagem);
            }

            ConfigurarBotoes();
        }

        private async void btnPause_Click(object sender, EventArgs e)
        {
            ConfigurarBotoes();
            if (EstadoDeReproducao == EstadoReproducao.Reproduzindo)
            {
                btnPause.Enabled = false;
                var resultado = await Pause();
                if (resultado.Sucesso)
                {
                    timerDuracao.Stop();
                }
                else
                {
                    ExibirMensagemDeErro(resultado.Mensagem);
                }
            }

            ConfigurarBotoes();
        }

        private async void btnStop_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = false;

            var resultado = await Stop();
            if (resultado.Sucesso)
            {
                timerDuracao.Stop();
                Duracao = new TimeSpan(0);
            }
            else
            {
                ExibirMensagemDeErro(resultado.Mensagem);
            }

            ConfigurarBotoes();
        }

        private void timerDuracao_Tick(object sender, EventArgs e)
        {
            var tickSegundos = (Duracao.Ticks / TimeSpan.TicksPerSecond) + 1;
            Duracao = new TimeSpan(TimeSpan.TicksPerSecond * tickSegundos);
        }

        private void AtualizarTituloTela()
        {
            string estadoAtual = RetornarDescricaoEstadoAtual();
            this.Text = $"{Duracao:g} - {estadoAtual}";
        }

        private string RetornarDescricaoEstadoAtual()
        {
            switch (EstadoDeReproducao)
            {
                case EstadoReproducao.Default:
                    return "";
                case EstadoReproducao.Reproduzindo:
                    return "Reproduzindo";
                case EstadoReproducao.Pausado:
                    return "Pausado";
                case EstadoReproducao.Encerrado:
                    return "Encerrado";
                default: return "";
            }
        }

        private void fPlayerAudioOlos_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerDuracao.Stop();
        }

        private void ConfigurarBotoes()
        {
            switch (EstadoDeReproducao)
            {
                case EstadoReproducao.Default:
                    {
                        btnPause.Enabled = false;
                        btnStop.Enabled = false;
                        btnPlayResume.Enabled = true;
                        break;
                    }

                case EstadoReproducao.Reproduzindo:
                    {
                        btnPause.Enabled = true;
                        btnStop.Enabled = true;
                        btnPlayResume.Enabled = false;
                        break;
                    }
                case EstadoReproducao.Pausado:
                    {
                        btnPause.Enabled = false;
                        btnStop.Enabled = true;
                        btnPlayResume.Enabled = true;
                        break;
                    }
                case EstadoReproducao.Encerrado:
                    {
                        btnPause.Enabled = false;
                        btnStop.Enabled = false;
                        btnPlayResume.Enabled = true;
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ExibirMensagemDeErro(string mensagem)
        {
            MessageBox.Show(mensagem, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void timerAtualizarTela_Tick(object sender, EventArgs e)
        {
            AtualizarTituloTela();
        }
    }

    public enum EstadoReproducao : int
    {
        Default = 0, Reproduzindo = 1, Pausado = 2, Encerrado = 3
    }
}
