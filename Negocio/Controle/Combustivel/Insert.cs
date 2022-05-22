using BancoDados;
using Objeto;
using System;
using System.Data;
using System.Text;

namespace Negocio.Controle.Combustivel
{
    public static class Insert
    {
        static CRUD crud = null;
        static StringBuilder strSQL = null;

        public static bool Register(ControleAbastecimentoObj controleAbastecimento)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            strSQL.Append("INSERT INTO ControleAbastecimento(Id_Veiculo, Id_Motorista, KmInicial, Data, Valor, Litros, Descricao) ");
            strSQL.Append("VALUES(@Id_Veiculo, @Id_Motorista, @KmInicial, @Data, @Valor, @Litros, @Descricao)");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Id_Veiculo", controleAbastecimento.Veiculo.Id);
                crud.AddParameter("Id_Motorista", controleAbastecimento.Motorista.Id);
                crud.AddParameter("KmInicial", controleAbastecimento.KmInicio);
                crud.AddParameter("Data", controleAbastecimento.Data);
                crud.AddParameter("Valor", controleAbastecimento.Valor);
                crud.AddParameter("Litros", controleAbastecimento.Litros);
                crud.AddParameter("Descricao", controleAbastecimento.Descricao);
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
