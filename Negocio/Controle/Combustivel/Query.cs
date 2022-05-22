using BancoDados;
using System;
using System.Data;
using System.Text;

namespace Negocio.Controle.Combustivel
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

            strSQL.Append("SELECT CA.Id, CA.Id_Veiculo, V.Modelo, CA.Id_Motorista, M.Nome, CA.KmInicial, CA.Data, CA.Valor, CA.Litros, CA.Descricao  ");
            strSQL.Append("FROM ControleAbastecimento CA ");
            strSQL.Append("INNER JOIN Veiculo V ON CA.Id_Veiculo = V.Id ");
            strSQL.Append("INNER JOIN Motorista M ON CA.Id_Motorista = M.Id ");
            strSQL.Append("WHERE LOWER(V.Modelo) LIKE @search OR LOWER(CA.Descricao) LIKE @search OR LOWER(M.Nome) LIKE @search ");
            strSQL.Append("ORDER BY CA.Data DESC, M.Nome ASC");

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

            strSQL.Append("SELECT CA.Id, CA.Id_Veiculo, V.Modelo, CA.Id_Motorista, M.Nome, CA.KmInicial, CA.Data, CA.Valor, CA.Litros, CA.Descricao  ");
            strSQL.Append("FROM ControleAbastecimento CA ");
            strSQL.Append("INNER JOIN Veiculo V ON CA.Id_Veiculo = V.Id ");
            strSQL.Append("INNER JOIN Motorista M ON CA.Id_Motorista = M.Id ");
            strSQL.Append("ORDER BY CA.Data DESC, M.Nome ASC");

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

            strSQL.Append("SELECT CA.Id, CA.Id_Veiculo, V.Modelo, CA.Id_Motorista, M.Nome, CA.KmInicial, CA.Data, CA.Valor, CA.Litros, CA.Descricao  ");
            strSQL.Append("FROM ControleAbastecimento CA ");
            strSQL.Append("INNER JOIN Veiculo V ON CA.Id_Veiculo = V.Id ");
            strSQL.Append("INNER JOIN Motorista M ON CA.Id_Motorista = M.Id ");
            strSQL.Append("WHERE CA.Id_Veiculo = @Id_Veiculo ");
            strSQL.Append("ORDER BY CA.Data DESC, M.Nome ASC");

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
        public static DataTable RegisterDriver(int idMotorista)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT CA.Id, CA.Id_Veiculo, V.Modelo, CA.Id_Motorista, M.Nome, CA.KmInicial, CA.Data, CA.Valor, CA.Litros, CA.Descricao  ");
            strSQL.Append("FROM ControleAbastecimento CA ");
            strSQL.Append("INNER JOIN Veiculo V ON CA.Id_Veiculo = V.Id ");
            strSQL.Append("INNER JOIN Motorista M ON CA.Id_Motorista = M.Id ");
            strSQL.Append("WHERE CA.Id_Motorista = @Id_Motorista ");
            strSQL.Append("ORDER BY CA.Data DESC, M.Nome ASC");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Id_Motorista", idMotorista);
                return dataTable = crud.Query(CommandType.Text, strSQL.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable RegisterVehicleDriver(int idVeiculo, int idMotorista)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT CA.Id, CA.Id_Veiculo, V.Modelo, CA.Id_Motorista, M.Nome, CA.KmInicial, CA.Data, CA.Valor, CA.Litros, CA.Descricao  ");
            strSQL.Append("FROM ControleAbastecimento CA ");
            strSQL.Append("INNER JOIN Veiculo V ON CA.Id_Veiculo = V.Id ");
            strSQL.Append("INNER JOIN Motorista M ON CA.Id_Motorista = M.Id ");
            strSQL.Append("WHERE CA.Id_Veiculo = @Id_Veiculo ");
            strSQL.Append("AND CA.Id_Motorista = @Id_Motorista ");
            strSQL.Append("ORDER BY CA.Data DESC, M.Nome ASC");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Id_Veiculo", idVeiculo);
                crud.AddParameter("Id_Motorista", idMotorista);
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

            strSQL.Append("SELECT CA.Id, CA.Id_Veiculo, V.Modelo, CA.Id_Motorista, M.Nome, CA.KmInicial, CA.Data, CA.Valor, CA.Litros, CA.Descricao  ");
            strSQL.Append("FROM ControleAbastecimento CA ");
            strSQL.Append("INNER JOIN Veiculo V ON CA.Id_Veiculo = V.Id ");
            strSQL.Append("INNER JOIN Motorista M ON CA.Id_Motorista = M.Id ");
            strSQL.Append("WHERE CA.Data BETWEEN @Data AND @Data1 ");
            strSQL.Append("ORDER BY CA.Data DESC, M.Nome ASC");

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
        public static DataTable RegisterPeriodVehicle(DateTime date, DateTime date1, int idVeiculo)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT CA.Id, CA.Id_Veiculo, V.Modelo, CA.Id_Motorista, M.Nome, CA.KmInicial, CA.Data, CA.Valor, CA.Litros, CA.Descricao  ");
            strSQL.Append("FROM ControleAbastecimento CA ");
            strSQL.Append("INNER JOIN Veiculo V ON CA.Id_Veiculo = V.Id ");
            strSQL.Append("INNER JOIN Motorista M ON CA.Id_Motorista = M.Id ");
            strSQL.Append("WHERE CA.Data BETWEEN @Data AND @Data1 ");
            strSQL.Append("AND CA.Id_Veiculo = @Id_Veiculo ");
            strSQL.Append("ORDER BY CA.Data DESC, M.Nome ASC");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Data", date);
                crud.AddParameter("Data1", date1);
                crud.AddParameter("Id_Veiculo", idVeiculo);
                return dataTable = crud.Query(CommandType.Text, strSQL.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable RegisterPeriodDriver(DateTime date, DateTime date1, int idMotorista)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT CA.Id, CA.Id_Veiculo, V.Modelo, CA.Id_Motorista, M.Nome, CA.KmInicial, CA.Data, CA.Valor, CA.Litros, CA.Descricao  ");
            strSQL.Append("FROM ControleAbastecimento CA ");
            strSQL.Append("INNER JOIN Veiculo V ON CA.Id_Veiculo = V.Id ");
            strSQL.Append("INNER JOIN Motorista M ON CA.Id_Motorista = M.Id ");
            strSQL.Append("WHERE CA.Data BETWEEN @Data AND @Data1 ");
            strSQL.Append("AND CA.Id_Motorista = @Id_Motorista ");
            strSQL.Append("ORDER BY CA.Data DESC, M.Nome ASC");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Data", date);
                crud.AddParameter("Data1", date1);
                crud.AddParameter("Id_Motorista", idMotorista);
                return dataTable = crud.Query(CommandType.Text, strSQL.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string UltimoKmVeiculo(int idVeiculo)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT FIRST(1) CA.KmInicial ");
            strSQL.Append("FROM ControleAbastecimento CA ");
            strSQL.Append("WHERE CA.Id_Veiculo = @Id_Veiculo ");
            strSQL.Append("ORDER BY CA.Id DESC");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Id_Veiculo", idVeiculo);

                if (crud.Run(CommandType.Text, strSQL.ToString()) != null)
                {
                    return crud.Run(CommandType.Text, strSQL.ToString()).ToString();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
