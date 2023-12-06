using System;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Controles.CamposDinamicos
{
    public partial class CustomTextBox : UserControl
    {
        public string LabelText
        {
            get { return label.Text; }
            set { label.Text = $"{value}"; }
        }

        public string TextBoxText
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }

        public bool TextBoxReadOnly
        {
            get { return textBox.ReadOnly; }
            set { textBox.ReadOnly = value; }
        }

        public int TextBoxMaxLength
        {
            get { return textBox.MaxLength; }
            set { textBox.MaxLength = value; }
        }

        public CustomTextBox()
        {
            InitializeComponent();
        }

        private void CustomTextBox_Load(object sender, EventArgs e)
        {

        }

        public void Resetar()
        {
            TextBoxText = string.Empty;
        }
    }
}
