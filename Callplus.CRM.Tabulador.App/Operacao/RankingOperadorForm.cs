using Callplus.CRM.Tabulador.Servico.Servicos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Callplus.CRM.Tabulador.App.Ranking
{
    public partial class RankingOperadorForm : Form
    {        
        public RankingOperadorForm()
        {
            InitializeComponent();
        }

        public void ExibirRanking(int idCampanha)
        {
            AtendimentoService atendimentoService = new AtendimentoService();
            dgDados.DataSource = null;
            dgDados.DataSource = atendimentoService.RetornarRankingAtendimento(idCampanha);
            this.ShowDialog();
        }
    }

    public class RankingData
    {
        public string Posicao { get; set; }
        public string Operador { get; set; }
        public int TotalVendas { get; set; }
    }
}
