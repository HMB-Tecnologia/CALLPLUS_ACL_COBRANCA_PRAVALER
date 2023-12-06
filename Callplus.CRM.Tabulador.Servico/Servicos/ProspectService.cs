using System;
using System.Collections.Generic;
using System.Data;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class ProspectService
    {
        private readonly ProspectDao _prospectDao;

        public ProspectService()
        {
            _prospectDao = new ProspectDao();
        }

        public Prospect RetornarProspect(long idProspect)
        {
            return _prospectDao.ObterProspect(idProspect);
        }

        public IEnumerable<TelefoneDoProspect> ListarTelefoneDoProspect(long idProspect, bool? ativo)
        {
            return _prospectDao.ListarTelefonesDoProspect(idProspect, ativo);
        }

        public IEnumerable<EnderecoDoProspect> ListarEnderecoDoProspect(long idProspect)
        {
            return _prospectDao.ListarEnderecosDoProspect(idProspect);
        }

        public long GravarEnderecoDoProspect(EnderecoDoProspect endereco)
        {
            return _prospectDao.GravarEnderecoDoProspect(endereco);
        }

        public IEnumerable<Profissao> ListarProfissao(bool ativo)
        {
            return _prospectDao.ListarProfissao(ativo);
        }

        public IEnumerable<FaixaDeRenda> ListarFaixaDeRenda(bool ativo)
        {
            return _prospectDao.ListarFaixaDeRenda(ativo);
        }

        public IEnumerable<EstadoCivil> ListarEstadoCivil(bool ativo)
        {
            return _prospectDao.ListarEstadoCivil(ativo);
        }

        public IEnumerable<string> VerificarSePodeGravar(int idUsuario, int idCampanha)
        {
            return _prospectDao.VerificarSePodeGravar(idUsuario, idCampanha);
        }

        public long GravarProspect(Prospect prospect)
        {
            return _prospectDao.GravarProspect(prospect);
        }


        public void AtualizarProspectDoProspect(Prospect prospect)
        {
              _prospectDao.AtualizarProspectDoProspect(prospect);
        }

        public void InserirLogSistema(string ip, string texto)
        {
            _prospectDao.InserirLogSistema(ip, texto);
        }

        public void InserirLogHuawei(string ip, string url)
        {
            _prospectDao.InserirLogHuawei(ip, url);
        }

        public List<string> RetornarUrlHuawei()
        {
            var listaUrl = new List<string>();

            var sql = _prospectDao.RetornarUrlHuawei();

            var dt = _prospectDao.RetornarDataTableHuawei(sql);

            foreach (DataRow linha in dt.Rows)
            {
                var urlHuawei = linha["url"].ToString();
                listaUrl.Add(urlHuawei.ToUpper());
            }

            return listaUrl;
        }
        public DateTime? RetornarHorarioServidor()
        {
            var sql = _prospectDao.RetornarHorarioServidor();

            var datatable = _prospectDao.RetornarDataTableHorarioServidor(sql);

            if (datatable.Rows.Count > 0)
            {
                var data = datatable.Rows[0]["Horario"].ToString();
                DateTime dataServidor = DateTime.Now;

                if (DateTime.TryParse(data, out dataServidor))
                {
                    return dataServidor;
                }
            }
            return null;
        }
    }
}