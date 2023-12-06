using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class FormularioDeQualidadeService
    {
        private readonly FormularioDeQualidadeDao _dao;

        public FormularioDeQualidadeService()
        {
            _dao = new FormularioDeQualidadeDao();
        }

        public DataTable Listar(int id, int idCampanha, string nome, bool ativo)
        {
            return _dao.Listar(id, idCampanha, nome, ativo);
        }

        public IEnumerable<FormularioDeQualidade> Listar(int idCampanha, bool ativo)
        {
            return _dao.Listar(-1, idCampanha, ativo);
        }

        public DataTable ListarFaqDoProcedimento(long idAvaliacao, int idItem)
        {
            return _dao.ListarFaqDoProcedimento(idAvaliacao, idItem);
        }

        public FormularioDeQualidade Retornar(int idFormulario)
        {
            return _dao.Listar(idFormulario, -1, true).FirstOrDefault();
        }

        public DataTable RetornarEstrutura(int idFormulario, int idCampanha)
        {
            return _dao.RetornarEstrutura(idFormulario, idCampanha);
        }

        public DataTable RetornarProcedimento(int idFormulario, int idCampanha)
        {
            return _dao.RetornarProcedimento(idFormulario, idCampanha);
        }

        public DataTable RetornarModulo(int idModulo, int idFormularioDeQualidade, bool ativo)
        {
            return _dao.RetornarModulo(idModulo, idFormularioDeQualidade, ativo);
        }

        public DataTable RetornarItem(int idItem, int idModuloDoFormularioDeQualidade, int idFormularioDeQualidade, bool ativo)
        {
            return _dao.RetornarItem(idItem, idModuloDoFormularioDeQualidade, idFormularioDeQualidade, ativo);
        }

        public DataTable RetornarFAQ(int idFAQ, int idProcedimento, bool ativo)
        {
            return _dao.RetornarFAQ(idFAQ, idProcedimento, ativo);
        }

        public int Gravar(FormularioDeQualidade formulario)
        {
            return _dao.Gravar(formulario);
        }

        public DataTable ListarCampanhasDoFormulario(int idFormulario, bool? ativo)
        {
            return _dao.ListarCampanhasDoFormulario(idFormulario, ativo);
        }

        public int GravarModuloDoFormularioDeQualidade(ModuloDoFormularioDeQualidade moduloDoFormularioDeQualidade)
        {
            return _dao.GravarModuloDoFormularioDeQualidade(moduloDoFormularioDeQualidade);
        }

        public void ExcluirModuloDoFormularioDeQualidade(int idModulo)
        {
            _dao.ExcluirModuloDoFormularioDeQualidade(idModulo);
        }

        public int GravarItemDoModuloDoFormularioDeQualidade(ItemDoModuloDoFormularioDeQualidade itemDoModuloDoFormularioDeQualidade)
        {
            return _dao.GravarItemDoModuloDoFormularioDeQualidade(itemDoModuloDoFormularioDeQualidade);
        }

        public void ExcluirItemDoModuloDoFormularioDeQualidade(int idItem)
        {
            _dao.ExcluirItemDoModuloDoFormularioDeQualidade(idItem);
        }

        public int GravarProcedimentoDoItemDoFormularioDeQualidade(ProcedimentoDoItemDoFormularioDeQualidade procedimentoDoItemDoFormularioDeQualidade)
        {
            return _dao.GravarProcedimentoDoItemDoFormularioDeQualidade(procedimentoDoItemDoFormularioDeQualidade);
        }

        public void ExcluirProcedimentoDoItemDoFormularioDeQualidade(int idProcedimento)
        {
            _dao.ExcluirProcedimentoDoItemDoFormularioDeQualidade(idProcedimento);
        }

        public int GravarFaqDoProcedimentoDoFormularioDeQualidade(FaqDoProcedimentoDoFormularioDeQualidade faqDoProcedimentoDoFormularioDeQualidade)
        {
            return _dao.GravarFaqDoProcedimentoDoFormularioDeQualidade(faqDoProcedimentoDoFormularioDeQualidade);
        }

        public void ExcluirFaqDoProcedimentoDoFormularioDeQualidade(int idFAQ)
        {
            _dao.ExcluirFaqDoProcedimentoDoFormularioDeQualidade(idFAQ);
        }        
    }
}
