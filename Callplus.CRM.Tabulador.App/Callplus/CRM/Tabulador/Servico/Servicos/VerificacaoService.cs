using System;
using System.Collections.Generic;
using Callplus.CRM.Tabulador.App.Operacao;
using Callplus.CRM.Tabulador.Dominio.Entidades;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class VerificacaoService
    {
        private readonly VerificacaoDao _Dao;

        public VerificacaoService()
        {
            _Dao = new VerificacaoDao();
        }

        public int IdUsuarioPermissao { get; private set; }

        internal bool PermitirContatoManual(Usuario usuarioLogado)
        {
            SolicitarPermissaoForm solicitarPemissaoForm = new SolicitarPermissaoForm(usuarioLogado);
            var retorno = solicitarPemissaoForm.SolicitarPermissaoDeUsuario(true, true, true);

            if (retorno?.PermissaoConfirmada ?? false)
            {
                IdUsuarioPermissao = (int)retorno.IdUsuarioPermissao;
                return true;
            }

            return false;
        }

        public List<string> VerificarSePodeCriarDiretorio(string diretorio)
        {
            return _Dao.VerificarSePodeCriarDiretorio(diretorio);
        }

        public List<string> VerificarSePodeCriarNomeCampanha(string nomeCampanha)
        {
            return _Dao.VerificarSePodeCriarNomeCampanha(nomeCampanha);
        }
    }
}