using BancoDados;
using Objeto;
using System;
using System.Data;
using System.Text;

namespace Negocio.Controle.StatusVeiculo
{
    public static class Insert
    {
        static CRUD crud = null;
        static StringBuilder strSQL = null;

        public static bool Register(StatusVeiculoObj statusVeiculo)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            strSQL.Append("INSERT INTO StatusVeiculo(Id_Veiculo, DataHoraInicio, DataHoraFinal, Descricao) ");
            strSQL.Append("VALUES(@Id_Veiculo, @DataHoraInicio, @DataHoraFinal, @Descricao)");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Id_Veiculo", statusVeiculo.Veiculo.Id);
                crud.AddParameter("DataHoraInicio", statusVeiculo.DataHoraInicial);
                crud.AddParameter("DataHoraFinal", statusVeiculo.DataHoraFinal);
                crud.AddParameter("Descricao", statusVeiculo.Descricao);
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
