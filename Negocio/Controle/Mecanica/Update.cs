using BancoDados;
using Objeto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Controle.Mecanica
{
    public static class Update
    {
        static CRUD crud = null;
        static StringBuilder strSQL = null;

        public static bool Register(ControleManutencaoObj controleManutencao)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            strSQL.Append("UPDATE ControleManutencao ");
            strSQL.Append("SET Id_Veiculo = @Id_Veiculo, Data = @Data, Valor = @Valor, Descricao = @Descricao ");
            strSQL.Append("WHERE Id = @Id");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Id_Veiculo", controleManutencao.Veiculo.Id);
                crud.AddParameter("Data", controleManutencao.Data);
                crud.AddParameter("Valor", controleManutencao.Valor);
                crud.AddParameter("Descricao", controleManutencao.Descricao);
                crud.AddParameter("Id", controleManutencao.Id);
                crud.Run(CommandType.Text, strSQL.ToString());

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
