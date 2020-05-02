using BancoDados;
using Objeto;
using System;
using System.Data;
using System.Text;

namespace Negocio.Veiculo
{
    public static class Delete
    {
        static CRUD crud;
        static StringBuilder strSQL = null;
        public static bool Register(VeiculoObj veiculo)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            strSQL.Append("DELETE FROM Veiculo ");
            strSQL.Append("WHERE Id = @Id");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Id", veiculo.Id);
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
