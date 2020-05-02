using BancoDados;
using Objeto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Controle.Multa
{
    public static class Insert
    {
        static CRUD crud = null;
        static StringBuilder strSQL = null;

        public static bool Register(ControleMultaObj controleMulta)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            strSQL.Append("INSERT INTO ControleMulta(Id_Veiculo, Id_Motorista,  Data, Valor, Pontos, Descricao) ");
            strSQL.Append("VALUES(@Id_Veiculo, @Id_Motorista, @Data, @Valor, @Pontos, @Descricao)");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Id_Veiculo", controleMulta.Veiculo.Id);
                crud.AddParameter("Id_Motorista", controleMulta.Motorista.Id);
                crud.AddParameter("Data", controleMulta.Data);
                crud.AddParameter("Valor", controleMulta.Valor);
                crud.AddParameter("Pontos", controleMulta.Pontos);
                crud.AddParameter("Descricao", controleMulta.Descricao);
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
