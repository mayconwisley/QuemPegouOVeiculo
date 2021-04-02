using BancoDados;
using Objeto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Controle.Combustivel
{
    public static class Update
    {
        static CRUD crud = null;
        static StringBuilder strSQL = null;

        public static bool Register(ControleAbastecimentoObj controleAbastecimento)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            strSQL.Append("UPDATE ControleAbastecimento ");
            strSQL.Append("SET Id_Veiculo = @Id_Veiculo, Id_Motorista = @Id_Motorista, KmInicial = @KmInicial, Data = @Data, Valor = @Valor, Litros = @Litros, Descricao = @Descricao ");
            strSQL.Append("WHERE Id = @Id");

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
                crud.AddParameter("Id", controleAbastecimento.Id);
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
