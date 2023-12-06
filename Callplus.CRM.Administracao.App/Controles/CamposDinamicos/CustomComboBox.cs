using System;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Controles.CamposDinamicos
{
    public partial class CustomComboBox : UserControl
    {
        public string LabelText { get; set; }
        public ComboBox ComboBox
        {
            get { return comboBox1; }
            private set { comboBox1 = value; }
        }

        public CustomComboBox()
        {
            InitializeComponent();
        }

        private void CustomTextBox_Load(object sender, EventArgs e)
        {
            label.Text = LabelText;
        }
    }
}
