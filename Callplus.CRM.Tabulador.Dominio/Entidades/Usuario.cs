using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public int IdPerfil { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSupervisor { get; set; }
        public int? IdEscalaDeTrabalho { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool PermiteExportacao { get; set; }
        public bool ReceberAvaliacaoDeQualidade { get; set; }
        public bool Ativo { get; set; }
        public bool SenhaExpirada { get; set; }
        public bool GerarNota { get; set; }
        public bool Protegido { get; set; }
        public int IdCriador { get; set; }
        public DateTime DataCriacao { get; set; }
        public int IdModificador { get; set; }
        public DateTime DataModificacao { get; set; }
        public string Observacao { get; set; }
        public string Empresa { get; set; }
        public DateTime TerminoDeTurno { get; set; }
        public long? CPF { get; set; }
        public DateTime? DataNascimento { get; set; }
        public bool Selecionado { get; set; }

        public Tipos.Perfil perfil
        {
            get { return (Tipos.Perfil)IdPerfil; }
        }

        private int _afterCall;
        public int afterCall
        {
            get { return _afterCall; }
            set { _afterCall = value; }
        }

        private int _IDUsuario;
        public int IDUsuario
        {
            get { return _IDUsuario; }
            set { _IDUsuario = value; }
        }

        public bool alterarProdutoBKO { get; set; }
    }
}
