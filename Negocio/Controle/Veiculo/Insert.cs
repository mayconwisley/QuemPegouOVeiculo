using BancoDados;
using Objeto;
using System;
using System.Data;
using System.Text;

namespace Negocio.Controle.Veiculo
{
    public static class Insert
    {
        static CRUD crud = null;
        static StringBuilder strSQL = null;

        public static bool Register(ControleVeiculoObj controleVeiculo)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            strSQL.Append("INSERT INTO ControleVeiculo(Id_Veiculo, Id_Motorista, DataHoraSaida,  DataHoraChegada, Descricao, KmInicial, KmFinal, Status) ");
            strSQL.Append("VALUES(@Id_Veiculo, @Id_Motorista, @DataHoraSaida, @DataHoraChegada, @Descricao, @KmInicial, @KmFinal, @Status)");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Id_Veiculo", controleVeiculo.Veiculo.Id);
                crud.AddParameter("Id_Motorista", controleVeiculo.Motorista.Id);
                crud.AddParameter("DataHoraSaida", controleVeiculo.DataHoraSaida);
                crud.AddParameter("DataHoraChegada", controleVeiculo.DataHoraChegada);
                crud.AddParameter("Descricao", controleVeiculo.Descricao);
                crud.AddParameter("KmInicial", controleVeiculo.KmInicial);
                crud.AddParameter("KmFinal", controleVeiculo.KmFinal);
                crud.AddParameter("Status", controleVeiculo.Status);
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
