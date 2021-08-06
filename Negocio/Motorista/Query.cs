using BancoDados;
using System;
using System.Data;
using System.Text;

namespace Negocio.Motorista
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

            strSQL.Append("SELECT Id, Nome, NumCNH, VencimentoCNH, CategoriaCNH, CPF, RG, IIF(Ativo = 'A', 'Ativo', 'Desativado') AS Ativo ");
            strSQL.Append("FROM Motorista ");
            strSQL.Append("WHERE LOWER(Nome) LIKE @search OR LOWER(NumCNH) LIKE @search OR LOWER(CPF) LIKE @search OR LOWER(RG) LIKE @search ");
            strSQL.Append("ORDER BY Nome ASC, CPF ASC ");

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
        public static DataTable RegisterDriverActive(string search)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT Id, Nome, NumCNH AS CNH, VencimentoCNH, CategoriaCNH, CPF, RG, IIF(Ativo = 'A', 'Ativo', 'Desativado') AS Ativo ");
            strSQL.Append("FROM Motorista ");
            strSQL.Append("WHERE Motorista.Ativo LIKE @search ");
            strSQL.Append("ORDER BY Nome ASC, CPF ASC ");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("search", search);
                return dataTable = crud.Query(CommandType.Text, strSQL.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
        public static DataTable IdAndNameActive()
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT Id, Nome ");
            strSQL.Append("FROM Motorista ");
            strSQL.Append("WHERE Ativo = 'A' ");
            strSQL.Append("ORDER BY Nome ASC");

            try
            {
                crud.ClearParameter();
                return dataTable = crud.Query(CommandType.Text, strSQL.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
