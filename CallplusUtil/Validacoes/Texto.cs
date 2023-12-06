using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CallplusUtil.Validacoes
{
    public static class Texto
    {
        public static bool CepPossuiFormatoValido(string cep)
        {

            //FORMATO 00000-000
            bool formato1 = System.Text.RegularExpressions.Regex.IsMatch(cep, ("[0-9]{5}-[0-9]{3}"));

            //FORMATO 00000000
            bool formato2 = System.Text.RegularExpressions.Regex.IsMatch(cep, ("[0-9]{5}[0-9]{3}"));

            return formato1 || formato2;
        }

        public static bool CaractereNumerico(char key)
        {
            var result = false;

            if (!char.IsNumber(key) && key != (char)Keys.Back && key != (char)Keys.Enter)
            {
                if ((key == 3) || (key == 1) || (key == 22))
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }

            return result;
        }

        public static bool CaractereSomenteLetra(char key)
        {
            bool result = false;

            if (!char.IsLetter(key) && !char.IsControl(key) && !char.IsWhiteSpace(key))
                result = true;

            return result;
        }

        public static bool TelefonePossuiFormatoValido(string numero)
        {
            return TelefoneFixoPossuiFormatoValido(numero) || TelefoneCelularPossuiFormatoValido(numero);
        }

        public static bool TelefoneCelularPossuiFormatoValido(string numero)
        {
            /*Padrão telefone movel:
            ^               - Início da string.
            [1 - 9]{2}      - Dois dígitos de 1 a 9.Não existem códigos de DDD com o dígito 0.
            [9]             - O primeiro dígito. Deverá ser 9.
            [0 - 9]{9}      - Os demais 8 dígitos, totalizando 9 dígitos.
            $               - Final da string.
            */

            string padraoCelular = @"^[1-9]{2}[9][0-9]{8}";

            return Regex.IsMatch(numero, padraoCelular);
        }

        public static bool TelefoneFixoPossuiFormatoValido(string numero)
        {
            /*Padrão telefone fixo:
           ^               - Início da string.
           [1 - 9]{2}      - Dois dígitos de 1 a 9.Não existem códigos de DDD com o dígito 0.
           [2 - 9]         - O primeiro dígito. Nunca será 0 ou 1.
           [0 - 9]{7}      - Os demais 7 dígitos, totalizando 8 dígitos.
           $               - Final da string.
           */

            string padraoTelefone = @"^[1-9]{2}[2-9][0-9]{7}$";

            return Regex.IsMatch(numero, padraoTelefone);
        }

        public static bool EmailPosuiFormatoValido(string email)
        {
            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            bool isValid = regex.IsMatch(email);
            return isValid;
        }

        public static bool CpfPossuiFormatoValido(string cpf)
        {
            int[] mt1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mt2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string TempCPF;
            string Digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf == "11111111111" || cpf == "22222222222" || cpf == "33333333333" || cpf == "44444444444" || cpf == "55555555555"
                || cpf == "66666666666" || cpf == "77777777777" || cpf == "88888888888" || cpf == "99999999999" || cpf == "00000000000")
            {
                return false;
            }

            if (cpf.Length != 11)
                return false;

            TempCPF = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(TempCPF[i].ToString()) * mt1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            Digito = resto.ToString();
            TempCPF = TempCPF + Digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(TempCPF[i].ToString()) * mt2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            Digito = Digito + resto.ToString();

            return cpf.EndsWith(Digito);
        }

        public static bool DataEhValida(string data)
        {
            bool result = false;
            DateTime date;

            if (DateTime.TryParse(data, out date))
            {
                result = true;
            }

            return result;
        }
    }
}
