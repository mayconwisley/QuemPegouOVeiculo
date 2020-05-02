using BancoDados;
using Objeto;
using System;
using System.Data;
using System.Text;

namespace Negocio.Controle.CNH
{
    public static class Delete
    {
        static CRUD crud = null;
        static StringBuilder strSQL = null;

        public static bool Register(VencimentoCNHObj vencimentoCNH)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            strSQL.Append("DELETE FROM VencimentoCNH ");
            strSQL.Append("WHERE Id = @Id");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Id", vencimentoCNH.Id);
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
