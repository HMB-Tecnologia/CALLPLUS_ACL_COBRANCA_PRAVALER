using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CallplusUtil.Forms
{
    public static class CallplusFormsUtil
    {
        private static string _selecione = "SELECIONE...";
        private static string _reticencias = "...";

        public static void ResetarComboComSelecione(ComboBox comboBox, bool habilitar)
        {
            if (comboBox == null) return;

            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke(new MethodInvoker(() => comboBox.Text = _selecione));
            }
            else
            {
                comboBox.Text = _selecione;
            }
            
            ResetarComboBox(comboBox, habilitar);

        }

        public static void ResetarComboComReticencias(ComboBox comboBox, bool habilitar)
        {
            if (comboBox == null) return;
            
            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke(new MethodInvoker(() => comboBox.Text = _reticencias));
            }
            else
            {
                comboBox.Text = _reticencias;
            }

            ResetarComboBox(comboBox, habilitar);
        }

        public static void HabilitarComboBox(ComboBox comboBox)
        {
            if (comboBox == null) return;
            ResetarComboBox(comboBox, habilitar: true);
        }

        public static void DesabilitarComboBox(ComboBox comboBox)
        {
            if (comboBox == null) return;
            ResetarComboBox(comboBox, habilitar: false);
        }

        public static void ResetarComboBox(ComboBox comboBox, bool habilitar)
        {
            if (comboBox == null) return;
            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke(new MethodInvoker(() =>
                {
                    comboBox.Enabled = habilitar;
                    comboBox.DropDownStyle = comboBox.Enabled ? ComboBoxStyle.DropDownList : ComboBoxStyle.DropDownList;
                }));
            }
            else
            {
                comboBox.Enabled = habilitar;
                comboBox.DropDownStyle = comboBox.Enabled ? ComboBoxStyle.DropDownList : ComboBoxStyle.DropDownList;
            }

               
        }

        public static void ResetarTextBox(TextBox textBox, bool habilitar, bool limparTexto = false)
        {
            if (textBox == null) return;
           
            if (limparTexto)
            {
                if (textBox.InvokeRequired)
                {
                    textBox.Invoke(new MethodInvoker(() =>
                    {
                        textBox.Enabled = habilitar;
                        textBox.Text = string.Empty;
                    }));
                }
                else
                {
                    textBox.Enabled = habilitar;
                    textBox.Text = string.Empty;
                }
            }
        }

        public static void ResetarTextBox(TextBox textBox, bool habilitar, bool readOnly, bool limparTexto)
        {
            if (textBox == null) return;
           
            if (limparTexto)
            {
                if (textBox.InvokeRequired)
                {
                    textBox.Enabled = habilitar;
                    textBox.Invoke(new MethodInvoker(() =>
                    {
                        textBox.Enabled = habilitar;
                        textBox.Text = string.Empty;
                        textBox.ReadOnly = readOnly;
                    }));
                }
                else
                {
                    textBox.Enabled = habilitar;
                    textBox.Text = string.Empty;
                    textBox.ReadOnly = readOnly;
                }
            }
            
        }

        public static void ResetarTextBox(TextBox textBox, bool habilitar, string texto)
        {
            if (textBox == null) return;
           
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new MethodInvoker(() =>
                {
                    textBox.Text = texto;
                    textBox.Enabled = habilitar;
                }));
            }
            else
            {
                textBox.Text = texto;
                textBox.Enabled = habilitar;
            }
        }
        
        public static void ResetarMaskedTextBox(MaskedTextBox maskedTextBox, bool habilitar, bool limparTexto = false)
        {
            if (maskedTextBox == null) return;
            maskedTextBox.Enabled = habilitar;
            if (limparTexto)
                maskedTextBox.Text = string.Empty;
        }

        public static void ResetarMaskedTextBox(MaskedTextBox maskedTextBox, bool habilitar, string texto)
        {
            if (maskedTextBox == null) return;
            maskedTextBox.Enabled = habilitar;
            maskedTextBox.Text = texto;
        }

        public static void ResetarCheckBox(CheckBox chekBox, bool habilitar, bool marcar)
        {
            if (chekBox == null) return;
            chekBox.Enabled = habilitar;
            chekBox.Checked = marcar;
        }

        public static void ResetarRadioButton(RadioButton radioButton, bool habilitar, bool marcar)
        {
            if (radioButton == null) return;
            radioButton.Enabled = habilitar;
            radioButton.Checked = marcar;
        }

        public static void ExibirMensagens(List<string> mensagens)
        {
            if (mensagens.Any())
            {
                var msgFinal = string.Join("\n", mensagens);
                MessageBox.Show(msgFinal, "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static string FormatarNomeRegraDaClaro(string texto)
        {
            if (texto == null) return "";
            StringBuilder sbReturn = new StringBuilder();

            var arrayText = texto.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }

            arrayText = sbReturn.ToString().ToCharArray();

            sbReturn.Clear();

            foreach (char item in arrayText)
            {
                if(!Validacoes.Texto.CaractereSomenteLetra(item))
                {
                    sbReturn.Append(item);
                }
            }

            string retorno = sbReturn.ToString();

            while (retorno.Contains("  "))
            {
                retorno = retorno.Replace("  ", " ");
            }

            return retorno;
        }

        public static string FormatarCPF(string texto)
        {
            char[] cpf = texto.ToCharArray();

            string cpfCorrigido = "";

            for (int i = 0; i < cpf.Length; i++)
            {
                if (char.IsNumber(cpf[i]))
                {
                    cpfCorrigido += cpf[i];
                }
            }

            return cpfCorrigido.PadLeft(11, '0');
        }

        public static int RetornarIdade(DateTime data)
        {
            int result = 0;

            try
            {
                TimeSpan ts = DateTime.Today.Subtract(data);

                DateTime idade = new DateTime(ts.Ticks).AddYears(-1).AddDays(-1);

                result = idade.Year;
            }
            catch
            {
                
            }

            return result;
        }

        public static void AjustarTamanhoDoDropDownNoComboBox(object sender, EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =(senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth;
            foreach (object item in ((ComboBox)sender).Items)
            {
                string text = item.ToString();
                newWidth = (int)g.MeasureString(text, font).Width + vertScrollBarWidth;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }
            senderComboBox.DropDownWidth = width;
        }
    }
}
