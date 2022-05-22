using BancoDados;
using Objeto;
using System;
using System.Data;
using System.Text;

namespace Negocio.Controle.Mecanica
{
    public static class Insert
    {
        static CRUD crud = null;
        static StringBuilder strSQL = null;

        public static bool Register(ControleManutencaoObj controleManutencao)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            strSQL.Append("INSERT INTO ControleManutencao(Id_Veiculo, Data, Valor, Descricao) ");
            strSQL.Append("VALUES(@Id_Veiculo, @Data, @Valor, @Descricao)");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Id_Veiculo", controleManutencao.Veiculo.Id);
                crud.AddParameter("Data", controleManutencao.Data);
                crud.AddParameter("Valor", controleManutencao.Valor);
                crud.AddParameter("Descricao", controleManutencao.Descricao);
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
