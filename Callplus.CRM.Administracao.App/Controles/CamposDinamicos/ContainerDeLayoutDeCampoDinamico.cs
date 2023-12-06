using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Callplus.CRM.Tabulador.Dominio.Entidades.LayoutDinamico;
using CallplusUtil.Extensions;

namespace Callplus.CRM.Administracao.App.Controles.CamposDinamicos
{
    public partial class ContainerDeLayoutDeCampoDinamico : UserControl 
    {
        private const int TotalDeColunas = 12;
        private LayoutDeCampoDinamico _layoutDeCampoDinamico;

        public ContainerDeLayoutDeCampoDinamico()
        {
            InitializeComponent();

            this.tableLayout.ColumnCount = TotalDeColunas;
            this.AutoSize = true;
            AutoScroll = true;
            this.tableLayout.Padding = new Padding(2, 0, 2, 0);
            tableLayout.Margin = new Padding(0);
        }

        private void LayoutCampoDinamicoContainer_Load(object sender, EventArgs e)
        {


        }

        public void CarregarLayout(LayoutDeCampoDinamico layout)
        {
            if (layout == null) throw new ArgumentException(nameof(layout));

            RemoverTodos();
            ConfigurarLayout(layout);
        }

        public void PreencherCampos(IEnumerable<ValorDeCampoDinamico> valores)
        {
            foreach (var valorDeCampo in valores)
            {
                var controlesEncontrados = tableLayout.Controls.Find(valorDeCampo.IdCampo, true);

                foreach (var controleEncontrado in controlesEncontrados)
                {
                    if (controleEncontrado is CustomTextBox)
                    {
                        var textBox = (controleEncontrado as CustomTextBox);
                        string texto = valorDeCampo.Valor;
                        if (valorDeCampo.Valor.Length > textBox.TextBoxMaxLength)
                        {
                            texto = valorDeCampo.Valor.Substring(0, textBox.TextBoxMaxLength);
                        }

                        textBox.TextBoxText = texto;
                        continue;
                    }

                    if (controleEncontrado is CustomComboBox)
                    {
                        var customCombo = (controleEncontrado as CustomComboBox);
                        customCombo.ComboBox.ResetarComSelecione(habilitar: customCombo.Enabled);
                        continue;
                    }
                }

            }
        }

        private void ConfigurarLayout(LayoutDeCampoDinamico layout)
        {
            _layoutDeCampoDinamico = layout;
            tableLayout.RowCount = layout.Linhas.Count;
            //tableLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            TableLayoutRowStyleCollection styles = tableLayout.RowStyles;
            foreach (RowStyle style in styles)
            {
                // Set the row height to 20 pixels.
                style.SizeType = SizeType.Absolute;
                style.Height = 20;
            }

            AjustarEstiloDasColunas();

            var linhas = layout.Linhas;
            for (int contLinhas = 0; contLinhas < layout.Linhas.Count; contLinhas++)
            {
                var linhaCampoDinamico = linhas[contLinhas];

                var campos = linhaCampoDinamico.Campos.OrderBy(x => x.Ordem).ToList();
                for (var contColunas = 0; contColunas < campos.Count; contColunas++)
                {
                    var campoDinamico = campos[contColunas];

                    Control controle = CriarControle(campoDinamico);

                    tableLayout.Controls.Add(controle);
                    tableLayout.SetColumn(controle, contColunas);
                    tableLayout.SetRow(controle, contLinhas);
                    tableLayout.SetColumnSpan(controle, campoDinamico.Tamanho);

                    controle.Margin = new Padding(3, 0, 3, 0);
                }
            }

            AjustarEstiloDasColunas();
            tableLayout.Select();
        }

        private Control CriarControle(CampoDinamico campo)
        {
            Control controle;

            switch (campo.TipoExibicao)
            {
                case TipoExibicaoCampoDinamico.TextBox: controle = CriarCustomTextBox(campo); break;
                case TipoExibicaoCampoDinamico.ComboBox: controle = CriarCustomComboBox(campo); break;
                default: throw new ArgumentException("TipoExibicao do campo dinâmico não possui um controle customizado associado.");
            }
            controle.Name = campo.IdCampo.ToString();
            //controle.Enabled = campo.Habilitado;
            return controle;
        }

        private Control CriarCustomComboBox(CampoDinamico campo)
        {
            var customCombo = new CustomComboBox();

            //TODO: INCLUIR PARSE DOS VALORES JSON PARA PREENCHER O COMBOBOX

            customCombo.LabelText = campo.Label;
            customCombo.ComboBox.PreencherComSelecione(new List<KeyValuePair<string, string>>());
            customCombo.ComboBox.ResetarComSelecione(true);
            customCombo.Dock = DockStyle.Fill;


            return customCombo;
        }

        private CustomTextBox CriarCustomTextBox(CampoDinamico campoDinamico)
        {
            var customTextBox = new CustomTextBox();
            customTextBox.TextBoxReadOnly = campoDinamico.SomenteLeitura;
            customTextBox.Dock = DockStyle.Fill;
            customTextBox.LabelText = campoDinamico.Label.Trim();
            customTextBox.TextBoxMaxLength = campoDinamico.TamanhoTexto ?? 32767;
            return customTextBox;

        }

        //Ajusta o estilo das colunas para que o conteúdo fique com tamanhos iguais.
        //Não remover, não editar. Sério.
        private void AjustarEstiloDasColunas()
        {
            tableLayout.ColumnStyles.Clear();
            for (int i = 0; i < tableLayout.ColumnCount; i++)
            {
                tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1));
            }

            tableLayout.RowStyles.Clear();
            for (int i = 0; i < tableLayout.RowCount; i++)
            {
                tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent));
            }
        }

        public void LimparTodos()
        {
            foreach (Control control in tableLayout.Controls)
            {
                (control as CustomTextBox)?.Resetar();

                (control as CustomComboBox)?.ComboBox.ResetarComSelecione(habilitar: control.Enabled);
            }
        }

        public void RemoverTodos()
        {
            tableLayout.Controls.Clear();
        }
    }


}
