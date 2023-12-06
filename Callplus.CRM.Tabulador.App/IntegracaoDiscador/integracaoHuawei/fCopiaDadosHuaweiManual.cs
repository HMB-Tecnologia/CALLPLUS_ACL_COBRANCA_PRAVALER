using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Callplus.CRM.Tabulador.App.IntegracaoDiscador.integracaoHuawei
{
    public partial class fCopiaDadosHuaweiManual : Form
    {
        public string DadosHuawei = string.Empty;
        public fCopiaDadosHuaweiManual()
        {
            InitializeComponent();
        }

        private void btnProcessar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDadosHuawei.Text))
            {
                var mensagem = "Preencha o campo com os Dados da Huawei.";
                MessageBox.Show(mensagem, "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDadosHuawei.Focus();
            }
            else
            {
                DadosHuawei = txtDadosHuawei.Text;

                Close();
            }
        }
    }
}
