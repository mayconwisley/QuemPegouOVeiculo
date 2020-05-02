using BancoDados;
using Objeto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Controle.Veiculo
{
    public static class Update
    {
        static CRUD crud = null;
        static StringBuilder strSQL = null;

        public static bool Register(ControleVeiculoObj controleVeiculo)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            strSQL.Append("UPDATE ControleVeiculo ");
            strSQL.Append("SET Id_Veiculo = @Id_Veiculo, Id_Motorista = @Id_Motorista, DataHoraSaida = @DataHoraSaida, ");
            strSQL.Append("DataHoraChegada = @DataHoraChegada, Descricao = @Descricao, KmInicial = @KmInicial, KmFinal =@KmFinal, Status = @Status ");
            strSQL.Append("WHERE Id = @Id");

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
                crud.AddParameter("Id", controleVeiculo.Id);
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
