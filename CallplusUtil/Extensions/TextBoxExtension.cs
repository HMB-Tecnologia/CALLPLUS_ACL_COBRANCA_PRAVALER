using TextBox = System.Windows.Forms.TextBox;

namespace CallplusUtil.Extensions
{
    public static class TextBoxExtension
    {
        public static void Resetar(this TextBox textBox, bool habilitar, bool limparTexto = false)
        {
            if (textBox == null) return;
            textBox.Enabled = habilitar;
            if (limparTexto)
                textBox.Text = string.Empty;
        }

        public static void Resetar(this TextBox textBox, bool habilitar, bool readOnly, bool limparTexto)
        {
            if (textBox == null) return;
            textBox.Enabled = habilitar;
            if (limparTexto)
                textBox.Text = string.Empty;
            textBox.ReadOnly = readOnly;
        }

        public static void Resetar(this TextBox textBox, bool habilitar, string texto)
        {
            if (textBox == null) return;
            textBox.Enabled = habilitar;
            textBox.Text = texto;
        }
    }
}
