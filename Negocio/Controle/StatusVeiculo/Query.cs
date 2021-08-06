using BancoDados;
using System;
using System.Data;
using System.Text;

namespace Negocio.Controle.StatusVeiculo
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

            strSQL.Append("SELECT S.Id, S.Id_Veiculo, V.Modelo, S.DataHoraInicio, S.DataHoraFinal, S.Descricao ");
            strSQL.Append("FROM StatusVeiculo S ");
            strSQL.Append("INNER JOIN Veiculo V ON S.Id_Veiculo = V.Id ");
            strSQL.Append("WHERE LOWER(V.Modelo) LIKE @search OR LOWER(S.Descricao) LIKE @search ");
            strSQL.Append("ORDER BY V.Modelo ASC ");

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

            strSQL.Append("SELECT S.Id, S.Id_Veiculo, V.Modelo, S.DataHoraInicio, S.DataHoraFinal, S.Descricao ");
            strSQL.Append("FROM StatusVeiculo S ");
            strSQL.Append("INNER JOIN Veiculo V ON S.Id_Veiculo = V.Id ");
            strSQL.Append("ORDER BY V.Modelo ASC ");

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

            strSQL.Append("SELECT S.Id, S.Id_Veiculo, V.Modelo, S.DataHoraInicio, S.DataHoraFinal, S.Descricao ");
            strSQL.Append("FROM StatusVeiculo S ");
            strSQL.Append("INNER JOIN Veiculo V ON S.Id_Veiculo = V.Id ");
            strSQL.Append("WHERE S.Id_Veiculo = @Id_Veiculo ");
            strSQL.Append("ORDER BY V.Modelo ASC ");

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
        public static DataTable RegisterDateStart(DateTime dtInicio, DateTime dtInicio1)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT S.Id, S.Id_Veiculo, V.Modelo, S.DataHoraInicio, S.DataHoraFinal, S.Descricao ");
            strSQL.Append("FROM StatusVeiculo S ");
            strSQL.Append("INNER JOIN Veiculo V ON S.Id_Veiculo = V.Id ");
            strSQL.Append("WHERE  CAST(S.DataHoraInicio AS DATE) BETWEEN @DataHoraInicio AND @DataHoraInicio1 ");
            strSQL.Append("ORDER BY V.Modelo ASC ");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("DataHoraInicio", dtInicio);
                crud.AddParameter("DataHoraInicio1", dtInicio1);
                return dataTable = crud.Query(CommandType.Text, strSQL.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable RegisterDateFinal(DateTime dtFinal, DateTime dtFinal1)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT S.Id, S.Id_Veiculo, V.Modelo, S.DataHoraInicio, S.DataHoraFinal, S.Descricao ");
            strSQL.Append("FROM StatusVeiculo S ");
            strSQL.Append("INNER JOIN Veiculo V ON S.Id_Veiculo = V.Id ");
            strSQL.Append("WHERE  CAST(S.DataHoraFinal AS DATE) BETWEEN @DataHoraFinal AND @DataHoraFinal1 ");
            strSQL.Append("ORDER BY V.Modelo ASC ");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("DataHoraFinal", dtFinal);
                crud.AddParameter("DataHoraFinal1", dtFinal1);
                return dataTable = crud.Query(CommandType.Text, strSQL.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable RegisterDateFinalNull()
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT S.Id, S.Id_Veiculo, V.Modelo, S.DataHoraInicio, S.DataHoraFinal, S.Descricao ");
            strSQL.Append("FROM StatusVeiculo S ");
            strSQL.Append("INNER JOIN Veiculo V ON S.Id_Veiculo = V.Id ");
            strSQL.Append("WHERE S.DataHoraFinal IS NULL ");
            strSQL.Append("ORDER BY V.Modelo ASC ");

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
