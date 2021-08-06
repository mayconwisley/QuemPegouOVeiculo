using BancoDados;
using System;
using System.Data;
using System.Text;

namespace Negocio.Controle.Mecanica
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

            strSQL.Append("SELECT CM.Id, CM.Id_Veiculo, V.Modelo, CM.Data, CM.Valor, CM.Descricao ");
            strSQL.Append("FROM ControleManutencao CM ");
            strSQL.Append("INNER JOIN Veiculo V ON CM.Id_Veiculo = V.Id ");
            strSQL.Append("WHERE LOWER(V.Modelo) LIKE @search OR LOWER(CM.Descricao) LIKE @search ");
            strSQL.Append("ORDER BY CM.Data DESC, V.Modelo ASC");

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
        public static DataTable RegisterAll()
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT CM.Id, CM.Id_Veiculo, V.Modelo, CM.Data, CM.Valor, CM.Descricao ");
            strSQL.Append("FROM ControleManutencao CM ");
            strSQL.Append("INNER JOIN Veiculo V ON CM.Id_Veiculo = V.Id ");
            strSQL.Append("ORDER BY CM.Data DESC, V.Modelo ASC");

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
        public static DataTable RegisterVehicle(int idVeiculo)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT CM.Id, CM.Id_Veiculo, V.Modelo, CM.Data, CM.Valor, CM.Descricao ");
            strSQL.Append("FROM ControleManutencao CM ");
            strSQL.Append("INNER JOIN Veiculo V ON CM.Id_Veiculo = V.Id ");
            strSQL.Append("WHERE CM.Id_Veiculo = @Id_Veiculo ");
            strSQL.Append("ORDER BY CM.Data DESC, V.Modelo ASC");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Id_Veiculo", idVeiculo);
                return dataTable = crud.Query(CommandType.Text, strSQL.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable RegisterPeriod(DateTime date, DateTime date1)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT CM.Id, CM.Id_Veiculo, V.Modelo, CM.Data, CM.Valor, CM.Descricao ");
            strSQL.Append("FROM ControleManutencao CM ");
            strSQL.Append("INNER JOIN Veiculo V ON CM.Id_Veiculo = V.Id ");
            strSQL.Append("WHERE CM.Data BETWEEN @Data AND @Data1 ");
            strSQL.Append("ORDER BY CM.Data DESC, V.Modelo ASC");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Data", date);
                crud.AddParameter("Data1", date1);
                return dataTable = crud.Query(CommandType.Text, strSQL.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
