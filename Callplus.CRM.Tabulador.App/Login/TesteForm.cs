using Callplus.CRM.Tabulador.App.Integracoes;
using System;
using System.Windows.Forms;

namespace Callplus.CRM.Tabulador.App.Login
{
    public partial class TesteForm : Form
    {
        private XCallIntegratorAkiva _instancia;
        public TesteForm()
        {
            InitializeComponent();
            _instancia = new XCallIntegratorAkiva(txtLogin.Text, txtLogin.Text);
            ConfigurarEventos();
        }
        private void ConfigurarEventos()
        {
            _instancia.SucessoAoRegistrarAgente += OnSucessoAoRegistrarAgente;
            _instancia.FimDaChamada += OnFimDaChamada;
            _instancia.OnDisposition += OnDisposition;
            _instancia.MudancaNoStatusDoAgente += OnMudancaNoStatusDoAgente;
            _instancia.FalhaAoResponder += OnFalhaAoResponder;
            _instancia.NovaChamadaRecebida += OnNovaChamadaRecebida;
            _instancia.ErroAoRegistrarAgente += OnErroAoRegistrarAgente;
            _instancia.SucessoAoConectar += OnSucessoAoConectar;
            _instancia.FalhaAoConectar += OnFalhaAoConectar; ;

        }

        private void OnFimDaChamada(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnFalhaAoResponder(object sender, DadosDafalha dadosDafalha)
        {
            throw new NotImplementedException();
        }

        private void OnFalhaAoConectar(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnNovaChamadaRecebida(object sender, DadosDaChamada dadosDaChamada)
        {
            //throw new NotImplementedException();
        }

        private void OnDisposition(object sender, DadosDaChamada dadosDaChamada)
        {
            //throw new NotImplementedException();
        }

        private void OnSucessoAoRegistrarAgente(object sender, EventArgs e)
        {
             _instancia.Login();
        }

        private void OnErroAoRegistrarAgente(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void OnSucessoAoConectar(object sender, DadosDaChamada dadosDaChamada)
        {
            //throw new NotImplementedException();
        }

        private void OnMudancaNoStatusDoAgente(object sender, int idStatusAgente)
        {
           
        }


        private void cmdEntrar_Click(object sender, EventArgs e)
        {
            //XCallIntegratorAkiva xc = new XCallIntegratorAkiva(txtLogin.Text, txtLogin.Text);

            _instancia.DiscarManual("11973949545");
            bool result = _instancia.VerificarSessaoAgente();

            if (result)
            {
                var reorno = _instancia.LogOut();
            }
                

            var ret = _instancia.Registrar();

            //ret = _instancia.SairDaPausa();
        }


    }
}
