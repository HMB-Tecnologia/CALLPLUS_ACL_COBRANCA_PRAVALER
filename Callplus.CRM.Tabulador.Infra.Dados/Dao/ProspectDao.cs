using System;
using System.Collections.Generic;
using System.Data;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class ProspectDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public Prospect ObterProspect(long idProspect)
        {
            var sql = "APP_CRM_PROSPECT_RETORNAR_POR_ID";
            var args = new
            {
                Id = idProspect                
            };

            return ExecutarProcedureSingleOrDefault<Prospect>(sql, args);
        }

        public IEnumerable<TelefoneDoProspect> ListarTelefonesDoProspect(long idProspect, bool? ativo)
        {
            var sql = "APP_CRM_TELEFONE_DO_PROSPECT_LISTAR";
            var args = new { IdProspect = idProspect, Ativo = ativo };
            var resultado = ExecutarProcedure<TelefoneDoProspect>(sql, args);
            return resultado;
        }

        public IEnumerable<EnderecoDoProspect> ListarEnderecosDoProspect(long idProspect)
        {
            var sql = "APP_CRM_ENDERECO_DO_PROSPECT_LISTAR";
            var args = new { IdProspect = idProspect};
            var resultado = ExecutarProcedure<EnderecoDoProspect>(sql, args);
            return resultado;
        }

        public long GravarEnderecoDoProspect(EnderecoDoProspect endereco)
        {
            var sql = "APP_CRM_ENDERECO_DO_PROSPECT_GRAVAR";
            var args = new
            {
                Id = endereco.Id,
                IdProspect = endereco.IdProspect,
                CEP = endereco.Cep,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                Complemento = endereco.Complemento,
                Bairro = endereco.Bairro,
                Cidade = endereco.Cidade,
                UF = endereco.Uf,
                PontoDeReferencia = endereco.PontoDeReferencia,
                IdCriador = endereco.IdCriador
            };
            
            return ExecutarProcedureSingleOrDefault<long>(sql, args);
        }

        public string InserirLogSistema(string ip, string texto)
        {
            var sql = $"EXEC APP_CRM_LOG_INSERIR @ip = '{ip}', @texto = '{texto}'";
            return sql;
        }

        public string InserirLogHuawei(string ip, string url)
        {
            var sql = $"EXEC APP_CRM_URL_INSERIR @ip = '{ip}', @url = '{url}'";
            return sql;
        }

        public DataTable RetornarDataTableHuawei(string url)
        {
            var sql = "SELECT Url FROM UrlHuawei";

            var args = new { };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public string RetornarUrlHuawei()
        {
            var sql = "SELECT Url FROM UrlHuawei";
            return sql;
        }

        public DataTable RetornarDataTableHorarioServidor(string url)
        {
            var sql = "SELECT CONVERT(VARCHAR,GETDATE(),121) [Horario]";

            var args = new { };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public string RetornarHorarioServidor()
        {
            var sql = "SELECT CONVERT(VARCHAR,GETDATE(),121) [Horario]";
            return sql;
        }

        public void AtualizarProspectDoProspect(Prospect prospect)
        {
            var sql = "APP_CRM_PROSPECT_ATUALIZAR_PROSPECT_ATENDIMENTO";
            var args = new
            {
                Id = prospect.Id,
                IdCampanha = prospect.IdCampanha,
                IdMailing =  prospect.IdMailing,
                Telefone01 = prospect.Telefone01,
                Telefone02 = prospect.Telefone02,
                Telefone03 = prospect.Telefone03,
                Telefone04 = prospect.Telefone04,
                Telefone05 = prospect.Telefone05,
                Telefone06 = prospect.Telefone06,
                Telefone07 = prospect.Telefone07,
                Telefone08 = prospect.Telefone08,
                Telefone09 = prospect.Telefone09,
                Telefone10 = prospect.Telefone10,
                Telefone11 = prospect.Telefone11,
                Telefone12 = prospect.Telefone12,
                Telefone13 = prospect.Telefone13,
                Telefone14 = prospect.Telefone14,
                Telefone15 = prospect.Telefone15,
                Telefone16 = prospect.Telefone16,
                Telefone17 = prospect.Telefone17,
                Telefone18 = prospect.Telefone18,
                Telefone19 = prospect.Telefone19,
                Telefone20 = prospect.Telefone20,
                Campo001 = prospect.Campo001,
                Campo002 = prospect.Campo002,
                Campo003 = prospect.Campo003,
                Campo004 = prospect.Campo004,
                Campo005 = prospect.Campo005,
                Campo006 = prospect.Campo006,
                Campo007 = prospect.Campo007,
                Campo008 = prospect.Campo008,
                Campo009 = prospect.Campo009,
                Campo010 = prospect.Campo010,
                Campo011 = prospect.Campo011,
                Campo012 = prospect.Campo012,
                Campo013 = prospect.Campo013,
                Campo014 = prospect.Campo014,
                Campo015 = prospect.Campo015,
                Campo016 = prospect.Campo016,
                Campo017 = prospect.Campo017,
                Campo018 = prospect.Campo018,
                Campo019 = prospect.Campo019,
                Campo020 = prospect.Campo020,
                Campo021 = prospect.Campo021,
                Campo022 = prospect.Campo022,
                Campo023 = prospect.Campo023,
                Campo024 = prospect.Campo024,
                Campo025 = prospect.Campo025,
                Campo026 = prospect.Campo026,
                Campo027 = prospect.Campo027,
                Campo028 = prospect.Campo028,
                Campo029 = prospect.Campo029,
                Campo030 = prospect.Campo030,
                Campo031 = prospect.Campo031,
                Campo032 = prospect.Campo032,
                Campo033 = prospect.Campo033,
                Campo034 = prospect.Campo034,
                Campo035 = prospect.Campo035,
                Campo036 = prospect.Campo036,
                Campo037 = prospect.Campo037,
                Campo038 = prospect.Campo038,
                Campo039 = prospect.Campo039,
                Campo040 = prospect.Campo040,
                Campo041 = prospect.Campo041,
                Campo042 = prospect.Campo042,
                Campo043 = prospect.Campo043,
                Campo044 = prospect.Campo044,
                Campo045 = prospect.Campo045,
                Campo046 = prospect.Campo046,
                Campo047 = prospect.Campo047,
                Campo048 = prospect.Campo048,
                Campo049 = prospect.Campo049,
                Campo050 = prospect.Campo050,
                Campo051 = prospect.Campo051,
                Campo052 = prospect.Campo052,
                Campo053 = prospect.Campo053,
                Campo054 = prospect.Campo054,
                Campo055 = prospect.Campo055,
                Campo056 = prospect.Campo056,
                Campo057 = prospect.Campo057,
                Campo058 = prospect.Campo058,
                Campo059 = prospect.Campo059,
                Campo060 = prospect.Campo060,
                Campo061 = prospect.Campo061,
                Campo062 = prospect.Campo062,
                Campo063 = prospect.Campo063,
                Campo064 = prospect.Campo064,
                Campo065 = prospect.Campo065,
                Campo066 = prospect.Campo066,
                Campo067 = prospect.Campo067,
                Campo068 = prospect.Campo068,
                Campo069 = prospect.Campo069,
                Campo070 = prospect.Campo070,
                Campo071 = prospect.Campo071,
                Campo072 = prospect.Campo072,
                Campo073 = prospect.Campo073,
                Campo074 = prospect.Campo074,
                Campo075 = prospect.Campo075,
                Campo076 = prospect.Campo076,
                Campo077 = prospect.Campo077,
                Campo078 = prospect.Campo078,
                Campo079 = prospect.Campo079,
                Campo080 = prospect.Campo080
            };

             ExecutarProcedure(sql, args);
        }


        public long GravarProspect(Prospect prospect)
        {
            var sql = "APP_CRM_PROSPECT_GRAVAR";
            var args = new
            {
                Id = prospect.Id,
                IdCampanha = prospect.IdCampanha,
                IdMailing = prospect.IdMailing,
                Telefone01 = prospect.Telefone01,
                Telefone02 = prospect.Telefone02,
                Telefone03 = prospect.Telefone03,
                Telefone04 = prospect.Telefone04,
                Telefone05 = prospect.Telefone05,
                Telefone06 = prospect.Telefone06,
                Telefone07 = prospect.Telefone07,
                Telefone08 = prospect.Telefone08,
                Telefone09 = prospect.Telefone09,
                Telefone10 = prospect.Telefone10,
                Telefone11 = prospect.Telefone11,
                Telefone12 = prospect.Telefone12,
                Telefone13 = prospect.Telefone13,
                Telefone14 = prospect.Telefone14,
                Telefone15 = prospect.Telefone15,
                Telefone16 = prospect.Telefone16,
                Telefone17 = prospect.Telefone17,
                Telefone18 = prospect.Telefone18,
                Telefone19 = prospect.Telefone19,
                Telefone20 = prospect.Telefone20,
                Campo001 = prospect.Campo001,
                Campo002 = prospect.Campo002,
                Campo003 = prospect.Campo003,
                Campo004 = prospect.Campo004,
                Campo005 = prospect.Campo005,
                Campo006 = prospect.Campo006,
                Campo007 = prospect.Campo007,
                Campo008 = prospect.Campo008,
                Campo009 = prospect.Campo009,
                Campo010 = prospect.Campo010,
                Campo011 = prospect.Campo011,
                Campo012 = prospect.Campo012,
                Campo013 = prospect.Campo013,
                Campo014 = prospect.Campo014,
                Campo015 = prospect.Campo015,
                Campo016 = prospect.Campo016,
                Campo017 = prospect.Campo017,
                Campo018 = prospect.Campo018,
                Campo019 = prospect.Campo019,
                Campo020 = prospect.Campo020,
                Campo021 = prospect.Campo021,
                Campo022 = prospect.Campo022,
                Campo023 = prospect.Campo023,
                Campo024 = prospect.Campo024,
                Campo025 = prospect.Campo025,
                Campo026 = prospect.Campo026,
                Campo027 = prospect.Campo027,
                Campo028 = prospect.Campo028,
                Campo029 = prospect.Campo029,
                Campo030 = prospect.Campo030,
                Campo031 = prospect.Campo031,
                Campo032 = prospect.Campo032,
                Campo033 = prospect.Campo033,
                Campo034 = prospect.Campo034,
                Campo035 = prospect.Campo035,
                Campo036 = prospect.Campo036,
                Campo037 = prospect.Campo037,
                Campo038 = prospect.Campo038,
                Campo039 = prospect.Campo039,
                Campo040 = prospect.Campo040,
                Campo041 = prospect.Campo041,
                Campo042 = prospect.Campo042,
                Campo043 = prospect.Campo043,
                Campo044 = prospect.Campo044,
                Campo045 = prospect.Campo045,
                Campo046 = prospect.Campo046,
                Campo047 = prospect.Campo047,
                Campo048 = prospect.Campo048,
                Campo049 = prospect.Campo049,
                Campo050 = prospect.Campo050,
                Campo051 = prospect.Campo051,
                Campo052 = prospect.Campo052,
                Campo053 = prospect.Campo053,
                Campo054 = prospect.Campo054,
                Campo055 = prospect.Campo055,
                Campo056 = prospect.Campo056,
                Campo057 = prospect.Campo057,
                Campo058 = prospect.Campo058,
                Campo059 = prospect.Campo059,
                Campo060 = prospect.Campo060,
                Campo061 = prospect.Campo061,
                Campo062 = prospect.Campo062,
                Campo063 = prospect.Campo063,
                Campo064 = prospect.Campo064,
                Campo065 = prospect.Campo065,
                Campo066 = prospect.Campo066,
                Campo067 = prospect.Campo067,
                Campo068 = prospect.Campo068,
                Campo069 = prospect.Campo069,
                Campo070 = prospect.Campo070,
                Campo071 = prospect.Campo071,
                Campo072 = prospect.Campo072,
                Campo073 = prospect.Campo073,
                Campo074 = prospect.Campo074,
                Campo075 = prospect.Campo075,
                Campo076 = prospect.Campo076,
                Campo077 = prospect.Campo077,
                Campo078 = prospect.Campo078,
                Campo079 = prospect.Campo079,
                Campo080 = prospect.Campo080
            };

            return ExecutarProcedureSingleOrDefault<long>(sql, args);
        }

        public IEnumerable<string> VerificarSePodeGravar(int idUsuario, int idCampanha)
        {
            var sql = "APP_CRM_PROSPECT_PODE_GRAVAR_CADASTRO_MANUAL";

            var args = new
            {
                idUsuario = idUsuario,
                idCampanha = idCampanha,
            };

            var resultado = ExecutarProcedure<string>(sql, args);
            return resultado;
        }

        public IEnumerable<Profissao> ListarProfissao(bool ativo)
        {
            var sql = "APP_CRM_PROFISSAO_LISTAR";
            var args = new { Ativo = ativo };
            var resultado = ExecutarProcedure<Profissao>(sql, args);
            return resultado;
        }

        public IEnumerable<FaixaDeRenda> ListarFaixaDeRenda(bool ativo)
        {
            var sql = "APP_CRM_FAIXA_RENDA_LISTAR";
            var args = new { Ativo = ativo };
            var resultado = ExecutarProcedure<FaixaDeRenda>(sql, args);
            return resultado;
        }

        public IEnumerable<EstadoCivil> ListarEstadoCivil(bool ativo)
        {
            var sql = "APP_CRM_ESTADO_CIVIL_LISTAR";
            var args = new { Ativo = ativo };
            var resultado = ExecutarProcedure<EstadoCivil>(sql, args);
            return resultado;
        }
    }
    
}
