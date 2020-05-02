using BancoDados;
using System;
using System.Data;
using System.Text;

namespace Negocio.Controle.CNH
{
    public static class Query
    {
        static CRUD crud = null;
        static StringBuilder strSQL = null;
        static DataTable dataTable = null;
        public static DataTable Register(string search)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT V.Id, V.Data, V.Id_Motorista, M.Nome, IIF(V.Status = 'V', 'Vencido', 'Não Vencido') AS Status ");
            strSQL.Append("FROM VencimentoCNH V ");
            strSQL.Append("INNER JOIN Motorista M ON V.Id_Motorista = M.Id ");
            strSQL.Append("WHERE LOWER(M.Nome) LIKE @search ");
            strSQL.Append("ORDER BY M.Nome ASC");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("search", search.ToLower());
                return dataTable = crud.Query(CommandType.Text, strSQL.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
    }
}
