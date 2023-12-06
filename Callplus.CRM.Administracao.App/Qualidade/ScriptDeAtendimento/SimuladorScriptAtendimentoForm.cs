using Callplus.CRM.Tabulador.Servico.Servicos;
using NLog;
using System;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Qualidade.ScriptDeAtendimento
{
    public partial class SimuladorScriptAtendimentoForm : Form
    {
        public SimuladorScriptAtendimentoForm()
        {
            _logger = LogManager.GetCurrentClassLogger();
            
            _scriptDeAtendimentoService = new ScriptDeAtendimentoService();

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;
        
        private readonly ScriptDeAtendimentoService _scriptDeAtendimentoService;
        
        private int _idScript;

        #endregion PROPRIEDADES

        public void Iniciar(int idScript)
        {
            _idScript = idScript;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowDialog();
        }

        private void SimuladorScriptAtendimentoForm_Load(object sender, EventArgs e)
        {
            Tabulador.Dominio.Entidades.ScriptAtendimento.ScriptDeAtendimento script = _scriptDeAtendimentoService.RetornarScriptDeAtendimento(_idScript);

            if (script.IdPrimeiraEtapa != null && script.IdPrimeiraEtapa > 0)
            {
                scriptDeOfertaControl.Visible = true;
                scriptDeOfertaControl.Dock = DockStyle.Fill;

                scriptDeOfertaControl.Iniciar(script);
            }
            else
            {
                this.Hide();
                MessageBox.Show("Este script não possui etapa inicial configurada!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }
    }
}
