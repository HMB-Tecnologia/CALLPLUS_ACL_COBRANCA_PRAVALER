using System;
using System.Threading;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App
{
	public partial class LoadingForm : Form
	{
		public LoadingForm(string texto)
		{

			InitializeComponent();
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			lblTitulo.Text = texto;
		}

		private void Loading_Load(object sender, System.EventArgs e)
		{
	
		}

		public void FecharFormLoad()
		{
			Invoke(new EventHandler(delegate { Close(); }));
		}

		//desabilita botão fechar
		private const int WS_SYSMENU = 0x80000;
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.Style &= ~WS_SYSMENU;
				return cp;
			}
		}

		//Desabilita mover o form
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x0112;
			const int SC_MOVE = 0xF010;

			switch (m.Msg)
			{
				case WM_SYSCOMMAND:
					int command = m.WParam.ToInt32() & 0xfff0;
					if (command == SC_MOVE)
						return;
					break;
			}
			base.WndProc(ref m);
		}
	}
}
