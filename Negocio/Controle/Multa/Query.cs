using BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Controle.Multa
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

            strSQL.Append("SELECT CM.Id, CM.Id_Veiculo, M.Nome, V.Modelo, CM.Id_Motorista, CM.Data, CM.Valor, CM.Pontos, CM.Descricao ");
            strSQL.Append("FROM ControleMulta CM ");
            strSQL.Append("INNER JOIN Veiculo V ON CM.Id_Veiculo = V.Id ");
            strSQL.Append("INNER JOIN Motorista M ON CM.Id_Motorista = M.Id ");
            strSQL.Append("WHERE LOWER(V.Modelo) LIKE @search OR LOWER(CM.Descricao) LIKE @search OR LOWER(M.Nome) LIKE @search ");
            strSQL.Append("ORDER BY CM.Data DESC, M.Nome ASC");

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

            strSQL.Append("SELECT CM.Id, CM.Id_Veiculo, M.Nome, V.Modelo, CM.Id_Motorista, CM.Data, CM.Valor, CM.Pontos, CM.Descricao ");
            strSQL.Append("FROM ControleMulta CM ");
            strSQL.Append("INNER JOIN Veiculo V ON CM.Id_Veiculo = V.Id ");
            strSQL.Append("INNER JOIN Motorista M ON CM.Id_Motorista = M.Id ");
            strSQL.Append("ORDER BY CM.Data DESC, M.Nome ASC");

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

            strSQL.Append("SELECT CM.Id, CM.Id_Veiculo, M.Nome, V.Modelo, CM.Id_Motorista, CM.Data, CM.Valor, CM.Pontos, CM.Descricao ");
            strSQL.Append("FROM ControleMulta CM ");
            strSQL.Append("INNER JOIN Veiculo V ON CM.Id_Veiculo = V.Id ");
            strSQL.Append("INNER JOIN Motorista M ON CM.Id_Motorista = M.Id ");
            strSQL.Append("WHERE CM.Id_Veiculo = @Id_Veiculo ");
            strSQL.Append("ORDER BY CM.Data DESC, M.Nome ASC");

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

            strSQL.Append("SELECT CM.Id, CM.Id_Veiculo, M.Nome, V.Modelo, CM.Id_Motorista, CM.Data, CM.Valor, CM.Pontos, CM.Descricao ");
            strSQL.Append("FROM ControleMulta CM ");
            strSQL.Append("INNER JOIN Veiculo V ON CM.Id_Veiculo = V.Id ");
            strSQL.Append("INNER JOIN Motorista M ON CM.Id_Motorista = M.Id ");
            strSQL.Append("WHERE CM.Id_Motorista = @Id_Motorista ");
            strSQL.Append("ORDER BY CM.Data DESC, M.Nome ASC");

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

            strSQL.Append("SELECT CM.Id, CM.Id_Veiculo, M.Nome, V.Modelo, CM.Id_Motorista, CM.Data, CM.Valor, CM.Pontos, CM.Descricao ");
            strSQL.Append("FROM ControleMulta CM ");
            strSQL.Append("INNER JOIN Veiculo V ON CM.Id_Veiculo = V.Id ");
            strSQL.Append("INNER JOIN Motorista M ON CM.Id_Motorista = M.Id ");
            strSQL.Append("WHERE CM.Id_Veiculo = @Id_Veiculo ");
            strSQL.Append("AND CM.Id_Motorista = @Id_Motorista ");
            strSQL.Append("ORDER BY CM.Data DESC, M.Nome ASC");

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

            strSQL.Append("SELECT CM.Id, CM.Id_Veiculo, M.Nome, V.Modelo, CM.Id_Motorista, CM.Data, CM.Valor, CM.Pontos, CM.Descricao ");
            strSQL.Append("FROM ControleMulta CM ");
            strSQL.Append("INNER JOIN Veiculo V ON CM.Id_Veiculo = V.Id ");
            strSQL.Append("INNER JOIN Motorista M ON CM.Id_Motorista = M.Id ");
            strSQL.Append("WHERE CM.Data BETWEEN @Data AND @Data1 ");
            strSQL.Append("ORDER BY CM.Data DESC, M.Nome ASC");

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

            strSQL.Append("SELECT CM.Id, CM.Id_Veiculo, M.Nome, V.Modelo, CM.Id_Motorista, CM.Data, CM.Valor, CM.Pontos, CM.Descricao ");
            strSQL.Append("FROM ControleMulta CM ");
            strSQL.Append("INNER JOIN Veiculo V ON CM.Id_Veiculo = V.Id ");
            strSQL.Append("INNER JOIN Motorista M ON CM.Id_Motorista = M.Id ");
            strSQL.Append("WHERE CM.Data BETWEEN @Data AND @Data1 ");
            strSQL.Append("AND CM.Id_Veiculo = @Id_Veiculo ");
            strSQL.Append("ORDER BY CM.Data DESC, M.Nome ASC");

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

            strSQL.Append("SELECT CM.Id, CM.Id_Veiculo, M.Nome, V.Modelo, CM.Id_Motorista, CM.Data, CM.Valor, CM.Pontos, CM.Descricao ");
            strSQL.Append("FROM ControleMulta CM ");
            strSQL.Append("INNER JOIN Veiculo V ON CM.Id_Veiculo = V.Id ");
            strSQL.Append("INNER JOIN Motorista M ON CM.Id_Motorista = M.Id ");
            strSQL.Append("WHERE CM.Data BETWEEN @Data AND @Data1 ");
            strSQL.Append("AND CM.Id_Motorista = @Id_Motorista ");
            strSQL.Append("ORDER BY CM.Data DESC, M.Nome ASC");

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
    }
}
