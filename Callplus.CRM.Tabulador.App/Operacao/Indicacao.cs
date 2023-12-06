using Callplus.CRM.Tabulador.Dominio.Dto;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using CallplusUtil.Validacoes;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Callplus.CRM.Tabulador.App.Controles.CamposDinamicos;
using Callplus.CRM.Tabulador.Dominio.Tipos;
using v1Tabulare_z13.operador;
using Callplus.CRM.Tabulador.App.Util.CorreiosActionline;

namespace Callplus.CRM.Tabulador.App.Operacao
{
    public partial class IndicacaoForm : Form
    {
        public IndicacaoForm(Usuario usuario, Prospect _prospectDoAtendimento, long idAtendimentoEmAndamento)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _indicacaoService = new IndicacaoService();
            _usuario = usuario;
            _prospect = _prospectDoAtendimento;
            _idAtendimentoEmAndamento = idAtendimentoEmAndamento;
            InitializeComponent();
        }

        public bool Atualizar { get; set; }

        private readonly ILogger _logger;
        private readonly Prospect _prospect;
        private readonly long _idAtendimentoEmAndamento;
        private readonly Usuario _usuario;
        private readonly IndicacaoService _indicacaoService;
        public delegate void PararTempoHandler(int? idUsuarioAprovacao);
        public event PararTempoHandler PararTempoEvent;

        private void PararTempo()
        {
            SolicitarPermissaoForm solicitarPemissaoForm = new SolicitarPermissaoForm(_usuario);
            var retorno = solicitarPemissaoForm.SolicitarPermissaoDeUsuario(true, true);

            if (retorno?.PermissaoConfirmada ?? false)
            {
                PararTempoEvent?.Invoke(retorno.IdUsuarioPermissao);
            }
        }

        private void tsOferta_bntPararTempo_Click_Click(object sender, EventArgs e)
        {
            try
            {
                PararTempo();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar o checklist!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                SalvarIndicacao();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível salvar a indicacao!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SalvarIndicacao()
        {
            if (AtendeRegrasDeGravacao())
            {
                var indicacao = InstanciarNovaIndicacao();
                _indicacaoService.SalvarIndicacao(indicacao);        
                MessageBox.Show("Salvo com sucesso!");
                Atualizar = true;
                Hide();
                Close();
            }
        }

        private Indicacao InstanciarNovaIndicacao()
        {
            var _indicacao = new Indicacao
            {
                descricao = txtObservacao.Text,
                idProspect = _prospect.Id,
                idAtendimento = _idAtendimentoEmAndamento,
                quantidadeDeIndicacoes = int.Parse(txtQuantidadeDeIndicacoes.Text)
            };

            return _indicacao;
        }

        private bool AtendeRegrasDeGravacao()
        {
            var mensagens = new List<string>();

            bool QuantidadeDeinDicacoesENulaOuVaziaOuIgualZero = string.IsNullOrEmpty(txtQuantidadeDeIndicacoes.Text) || txtQuantidadeDeIndicacoes.Text == "0";
            
            if (QuantidadeDeinDicacoesENulaOuVaziaOuIgualZero)
            {
                lblIndicacacao.ForeColor = Color.Red;
                mensagens.Add("[Número de indicações] deve ser informado!");
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        private void txtQuantidadeDeIndicacao_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtQuantidadeDeIndicacoes.Text) || txtQuantidadeDeIndicacoes.Text == "0")
            {
                lblIndicacacao.ForeColor = Color.Red;
                txtQuantidadeDeIndicacoes.Focus();
                MessageBox.Show("[Número de indicações] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                lblIndicacacao.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtQuantidadeDeIndicacoes_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtQuantidadeDeIndicacoes.ForeColor = SystemColors.WindowText;
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }
    }
}