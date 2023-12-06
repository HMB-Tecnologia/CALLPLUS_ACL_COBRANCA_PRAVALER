using System.Data;
using System.Linq;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using Dapper;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class ContatoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        private long FinalizarContato(Contato contato)
        {
            var procedure = "APP_CRM_CONTATO_GRAVAR";

            var parametros = new DynamicParameters();
            parametros.Add("IDProspect", contato.IdProspect);
            parametros.Add("IDUsuario", contato.IdUsuario);

            parametros.Add("Campo01", contato.Campo01);
            parametros.Add("Campo02", contato.Campo02);
            parametros.Add("Campo03", contato.Campo03);
            parametros.Add("Campo04", contato.Campo04);
            parametros.Add("Campo05", contato.Campo05);
            parametros.Add("Campo06", contato.Campo06);
            parametros.Add("Campo07", contato.Campo07);
            parametros.Add("Campo08", contato.Campo08);
            parametros.Add("Campo09", contato.Campo09);
            parametros.Add("Campo10", contato.Campo10);
            parametros.Add("Campo11", contato.Campo11);
            parametros.Add("Campo12", contato.Campo12);
            parametros.Add("Campo13", contato.Campo13);
            parametros.Add("Campo14", contato.Campo14);
            parametros.Add("Campo15", contato.Campo15);
            parametros.Add("Campo16", contato.Campo16);
            parametros.Add("Campo17", contato.Campo17);
            parametros.Add("Campo18", contato.Campo18);
            parametros.Add("Campo19", contato.Campo19);
            parametros.Add("Campo20", contato.Campo20);
            parametros.Add("Campo21", contato.Campo21);
            parametros.Add("Campo22", contato.Campo22);
            parametros.Add("Campo23", contato.Campo23);
            parametros.Add("Campo24", contato.Campo24);
            parametros.Add("Campo25", contato.Campo25);
            parametros.Add("Campo26", contato.Campo26);
            parametros.Add("Campo27", contato.Campo27);
            parametros.Add("Campo28", contato.Campo28);
            parametros.Add("Campo29", contato.Campo29);
            parametros.Add("Campo30", contato.Campo30);
            parametros.Add("Campo31", contato.Campo31);
            parametros.Add("Campo32", contato.Campo32);
            parametros.Add("Campo33", contato.Campo33);
            parametros.Add("Campo34", contato.Campo34);
            parametros.Add("Campo35", contato.Campo35);
            parametros.Add("Campo36", contato.Campo36);
            parametros.Add("Campo37", contato.Campo37);
            parametros.Add("Campo38", contato.Campo38);
            parametros.Add("Campo39", contato.Campo39);
            parametros.Add("Campo40", contato.Campo40);
            parametros.Add("Campo41", contato.Campo41);
            parametros.Add("Campo42", contato.Campo42);
            parametros.Add("Campo43", contato.Campo43);
            parametros.Add("Campo44", contato.Campo44);
            parametros.Add("Campo45", contato.Campo45);
            parametros.Add("Campo46", contato.Campo46);
            parametros.Add("Campo47", contato.Campo47);
            parametros.Add("Campo48", contato.Campo48);
            parametros.Add("Campo49", contato.Campo49);
            parametros.Add("Campo50", contato.Campo50);
            parametros.Add("Campo51", contato.Campo51);
            parametros.Add("Campo52", contato.Campo52);
            parametros.Add("Campo53", contato.Campo53);
            parametros.Add("Campo54", contato.Campo54);
            parametros.Add("Campo55", contato.Campo55);
            parametros.Add("Campo56", contato.Campo56);
            parametros.Add("Campo57", contato.Campo57);
            parametros.Add("Campo58", contato.Campo58);
            parametros.Add("Campo59", contato.Campo59);
            parametros.Add("Campo60", contato.Campo60);
            parametros.Add("Campo61", contato.Campo61);
            parametros.Add("Campo62", contato.Campo62);
            parametros.Add("Campo63", contato.Campo63);
            parametros.Add("Campo64", contato.Campo64);
            parametros.Add("Campo65", contato.Campo65);
            parametros.Add("Campo66", contato.Campo66);
            parametros.Add("Campo67", contato.Campo67);
            parametros.Add("Campo68", contato.Campo68);
            parametros.Add("Campo69", contato.Campo69);
            parametros.Add("Campo70", contato.Campo70);
            parametros.Add("Campo71", contato.Campo71);
            parametros.Add("Campo72", contato.Campo72);
            parametros.Add("Campo73", contato.Campo73);
            parametros.Add("Campo74", contato.Campo74);
            parametros.Add("Campo75", contato.Campo75);
            parametros.Add("Campo76", contato.Campo76);
            parametros.Add("Campo77", contato.Campo77);
            parametros.Add("Campo78", contato.Campo78);
            parametros.Add("Campo79", contato.Campo79);
            parametros.Add("Campo80", contato.Campo80);

            return ExecutarProcedure<int>(procedure, parametros).SingleOrDefault();
        }
    }
}
