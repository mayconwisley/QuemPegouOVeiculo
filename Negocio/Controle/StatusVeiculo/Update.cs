using BancoDados;
using Objeto;
using System;
using System.Data;
using System.Text;

namespace Negocio.Controle.StatusVeiculo
{
    public static class Update
    {
        static CRUD crud = null;
        static StringBuilder strSQL = null;

        public static bool Register(StatusVeiculoObj statusVeiculo)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            strSQL.Append("UPDATE StatusVeiculo ");
            strSQL.Append("SET Id_Veiculo = @Id_Veiculo, DataHoraInicio = @DataHoraInicio, ");
            strSQL.Append("DataHoraFinal = @DataHoraFinal, Descricao = @Descricao ");
            strSQL.Append("WHERE Id = @Id");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Id_Veiculo", statusVeiculo.Veiculo.Id);
                crud.AddParameter("DataHoraInicio", statusVeiculo.DataHoraInicial);
                crud.AddParameter("DataHoraFinal", statusVeiculo.DataHoraFinal);
                crud.AddParameter("Descricao", statusVeiculo.Descricao);
                crud.AddParameter("Id", statusVeiculo.Id);
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
