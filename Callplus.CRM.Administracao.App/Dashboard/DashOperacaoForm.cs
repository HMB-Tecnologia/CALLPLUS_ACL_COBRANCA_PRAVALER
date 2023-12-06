using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using CallplusUtil.Validacoes;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Callplus.CRM.Administracao.App.Dashboard
{
    public partial class DashOperacaoForm : Form
    {
        public DashOperacaoForm()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _usuarioService = new UsuarioService();
            _campanhaService = new CampanhaService();
            _relatorioService = new RelatorioService();

            InitializeComponent();
        }

        #region VARIAVEIS

        private readonly UsuarioService _usuarioService;
        private readonly CampanhaService _campanhaService;
        private readonly RelatorioService _relatorioService;
        private readonly ILogger _logger;
        private IEnumerable<Campanha> _campanhas;
        private IEnumerable<Tabulador.Dominio.Entidades.Usuario> _supervisores;
        private IEnumerable<Tabulador.Dominio.Entidades.Usuario> _operadores;
        private int _totalVenda;
        private int _totalAtendimento;

        #endregion VARIAVEIS

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            this.ShowIcon = false;
            this.MaximizeBox = true;
            this.MinimizeBox = false;

            CarregarCampanhas();
            CarregarSupervisor();
            CarregarOperador();

            AjustarPaineisIniciais();
        }

        private void CarregarCampanhas()
        {
            _campanhas = _campanhaService.Listar(true);
            cmbCampanha.PreencherComTodos(_campanhas, campanha => campanha.Id, campanha => campanha.Nome);
        }

        private void CarregarSupervisor()
        {
            int idCampanha = -1;

            if (!cmbCampanha.TextoEhTodos())
                idCampanha = Convert.ToInt32(cmbCampanha.SelectedValue);

            _supervisores = _usuarioService.ListarSupervisores(ativo: false, idCampanha: idCampanha);
            //_supervisores = _supervisores.Where(x => x.IdPerfil == 3);
            cmbSupervisor.PreencherComTodos(_supervisores, supervisor => supervisor.Id, supervisor => supervisor.Nome);
        }

        private void CarregarOperador()
        {
            int idCampanha = -1;
            int idSupervisor = -1;

            if (!cmbCampanha.TextoEhTodos())
                idCampanha = Convert.ToInt32(cmbCampanha.SelectedValue);

            if (!cmbSupervisor.TextoEhTodos())
                idSupervisor = Convert.ToInt32(cmbSupervisor.SelectedValue);

            _operadores = _usuarioService.ListarOperadores(ativo: false, idCampanha: idCampanha, idSupervisor: idSupervisor);
            cmbOperador.PreencherComTodos(_operadores, operador => operador.Id, operador => operador.Nome);
        }

        private void CarregarCharts()
        {
            int idCampanha = -1;
            int idSupervisor = -1;
            int idOperador = -1;
            DateTime data = DateTime.MinValue;

            if (ParametrosPesquisaValidos())
            {
                idCampanha = int.Parse(cmbCampanha.SelectedValue.ToString());
                idSupervisor = int.Parse(cmbSupervisor.SelectedValue.ToString());
                idOperador = int.Parse(cmbOperador.SelectedValue.ToString());
                data = dtpData.Value;

                _totalAtendimento = 0;
                _totalVenda = 0;

                CarregarResultadoHoraHora(idCampanha, idSupervisor, idOperador, data);
                CarregarAtendimentoPorStatus(idCampanha, idSupervisor, idOperador, data);
                CarregarAtendimentoPorTipo(idCampanha, idSupervisor, idOperador, data);
                CarregarAuditoriaDaVenda(idCampanha, idSupervisor, idOperador, data);

                lblAtendimento.Text = _totalAtendimento.ToString();
                lblVenda.Text = _totalVenda.ToString();

                if(_totalAtendimento > 0)
                {
                    decimal conversao = (Convert.ToDecimal(_totalVenda) / Convert.ToDecimal(_totalAtendimento)) * 100;

                    lblConversao.Text = conversao.ToString("n2");
                }
                else
                {
                    lblConversao.Text = "0";
                }
            }
        }

        private void CarregarResultadoHoraHora(int idCampanha, int idSupervisor, int idOperador, DateTime data)
        {
            DataTable _dt = _relatorioService.RetornarResultadoHoraHora(idCampanha, idSupervisor, idOperador, data);

            DataTable _dtAtendimento = _dt.Copy();
            _dtAtendimento.Clear();
            DataTable _dtVenda = _dt.Copy();
            _dtVenda.Clear();


            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                if (_dt.Rows[i]["tipo"].ToString() == "ATENDIMENTOS")
                {
                    DataRow r = _dtAtendimento.NewRow();
                    r[0] = _dt.Rows[i][0];
                    r[1] = _dt.Rows[i][1];
                    r[2] = _dt.Rows[i][2];
                    _dtAtendimento.Rows.Add(r);
                }
                else if (_dt.Rows[i]["tipo"].ToString() == "VENDAS")
                {
                    DataRow r = _dtVenda.NewRow();
                    r[0] = _dt.Rows[i][0];
                    r[1] = _dt.Rows[i][1];
                    r[2] = _dt.Rows[i][2];
                    _dtVenda.Rows.Add(r);
                }
            }

            //ATENDIMENTOS
            string[] XPointMember = new string[_dtAtendimento.Rows.Count];
            int[] YPointMember = new int[_dtAtendimento.Rows.Count];

            for (int count = 0; count < _dtAtendimento.Rows.Count; count++)
            {
                //storing Values for X axis  
                XPointMember[count] = _dtAtendimento.Rows[count]["hora"].ToString();
                //storing values for Y Axis  
                YPointMember[count] = Convert.ToInt32(_dtAtendimento.Rows[count]["qtd"]);
            }

            //binding chart control  
            chartResultadoHoraHora.Series[0].Points.DataBindXY(XPointMember, YPointMember);

            //VENDAS
            XPointMember = new string[_dtVenda.Rows.Count];
            YPointMember = new int[_dtVenda.Rows.Count];

            for (int count = 0; count < _dtVenda.Rows.Count; count++)
            {
                //storing Values for X axis  
                XPointMember[count] = _dtVenda.Rows[count]["hora"].ToString();
                //storing values for Y Axis  
                YPointMember[count] = Convert.ToInt32(_dtVenda.Rows[count]["qtd"]);
            }
            //binding chart control  
            chartResultadoHoraHora.Series[1].Points.DataBindXY(XPointMember, YPointMember);

            //setting Chart type   
            //chartResultadoHoraHora.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
        }

        private void CarregarAtendimentoPorStatus(int idCampanha, int idSupervisor, int idOperador, DateTime data)
        {
            DataTable _dt = _relatorioService.RetornarAtendimentoPorStatus(idCampanha, idSupervisor, idOperador, data);

            string[] XPointMember = new string[_dt.Rows.Count];
            int[] YPointMember = new int[_dt.Rows.Count];

            for (int count = 0; count < _dt.Rows.Count; count++)
            {
                //storing Values for X axis  
                XPointMember[count] = _dt.Rows[count]["status"].ToString();
                //storing values for Y Axis  
                YPointMember[count] = Convert.ToInt32(_dt.Rows[count]["qtd"]);
            }
            //binding chart control  
            chartFunilPorStatus.Series[0].Points.DataBindXY(XPointMember, YPointMember);
            //setting Chart type   
            //chartFunilPorStatus.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
        }

        private void CarregarAtendimentoPorTipo(int idCampanha, int idSupervisor, int idOperador, DateTime data)
        {
            DataTable _dt = _relatorioService.RetornarAtendimentoPorTipo(idCampanha, idSupervisor, idOperador, data);

            string[] XPointMember = new string[_dt.Rows.Count];
            int[] YPointMember = new int[_dt.Rows.Count];

            for (int count = 0; count < _dt.Rows.Count; count++)
            {
                //storing Values for X axis  
                XPointMember[count] = _dt.Rows[count]["status"].ToString();
                //storing values for Y Axis  
                YPointMember[count] = Convert.ToInt32(_dt.Rows[count]["qtd"]);

                _totalAtendimento += Convert.ToInt32(_dt.Rows[count]["qtd"]);
            }
            //binding chart control  
            chartAtendimentoPorTipo.Series[0].Points.DataBindXY(XPointMember, YPointMember);
            //setting Chart type   
            //chartFunilPorStatus.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
        }

        private void CarregarAuditoriaDaVenda(int idCampanha, int idSupervisor, int idOperador, DateTime data)
        {
            DataTable _dt = _relatorioService.RetornarAuditoriaDaVenda(idCampanha, idSupervisor, idOperador, data);

            string[] XPointMember = new string[_dt.Rows.Count];
            int[] YPointMember = new int[_dt.Rows.Count];

            for (int count = 0; count < _dt.Rows.Count; count++)
            {
                //storing Values for X axis  
                XPointMember[count] = _dt.Rows[count]["tipo"].ToString();
                //storing values for Y Axis  
                YPointMember[count] = Convert.ToInt32(_dt.Rows[count]["qtd"]);

                _totalVenda += Convert.ToInt32(_dt.Rows[count]["qtd"]);
            }
            //binding chart control  
            chartAuditoriaDeVenda.Series[0].Points.DataBindXY(XPointMember, YPointMember);
            //setting Chart type   
            //chartFunilPorStatus.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
        }

        private bool ParametrosPesquisaValidos()
        {
            var mensagens = new List<string>();

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        private void AjustarPaineisIniciais()
        {
            int alturaValida = (this.Height - 10) - (chartResultadoHoraHora.Location.Y + chartResultadoHoraHora.Size.Height);

            int larguraValida = this.Width;

            chartFunilPorStatus.Location = new Point(15, (chartResultadoHoraHora.Location.Y + chartResultadoHoraHora.Size.Height + 10));
            chartFunilPorStatus.Height = (this.Height - 30) - chartFunilPorStatus.Location.Y - 20;
            chartFunilPorStatus.Width = (larguraValida - 69) / 3;

            chartAtendimentoPorTipo.Location = new Point(chartFunilPorStatus.Location.X + chartFunilPorStatus.Width + 15, chartFunilPorStatus.Location.Y);
            chartAtendimentoPorTipo.Size = chartFunilPorStatus.Size;

            chartAuditoriaDeVenda.Location = new Point(chartAtendimentoPorTipo.Location.X + chartAtendimentoPorTipo.Width + 15, chartAtendimentoPorTipo.Location.Y);
            chartAuditoriaDeVenda.Size = chartAtendimentoPorTipo.Size;
        }

        #endregion METODOS

        #region EVENTOS

        private void DashOperacaoForm_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarConfiguracaoInicial();

                btnPesquisar_Click(sender, e);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar as configurações iniciais!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                CarregarCharts();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível realizar a pesquisa!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void cmbSupervisor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CarregarOperador();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível atualizar o supervisor!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCampanha_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CarregarSupervisor();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível atualizar o supervisor!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
                
        private void DashOperacaoForm_Resize(object sender, EventArgs e)
        {
            try
            {
                AjustarPaineisIniciais();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
        
        #endregion EVENTOS
    }
}
