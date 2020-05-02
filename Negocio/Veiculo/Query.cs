using BancoDados;
using System;
using System.Data;
using System.Text;

namespace Negocio.Veiculo
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

            strSQL.Append("SELECT Id, Placa, Modelo, Chassi, Renavam, IIF(Status = 'A', 'Ativo', 'Desativado') AS Status ");
            strSQL.Append("FROM Veiculo ");
            strSQL.Append("WHERE LOWER(Placa) LIKE @search OR LOWER(Modelo) LIKE @search OR LOWER(Renavam) LIKE @search ");
            strSQL.Append("ORDER BY Modelo ASC, Placa ASC ");

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

        public static DataTable RegisterVehicleStaus(string search)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT Id, Placa, Modelo, Chassi, Renavam, IIF(Status = 'A', 'Ativo', 'Desativado') AS Status ");
            strSQL.Append("FROM Veiculo ");
            strSQL.Append("WHERE Veiculo.Status LIKE @search ");
            strSQL.Append("ORDER BY Modelo ASC, Placa ASC ");

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

        public static DataTable IdAndModelActive()
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT Id, Modelo ");
            strSQL.Append("FROM Veiculo ");
            strSQL.Append("WHERE Status = 'A' ");
            strSQL.Append("ORDER BY Modelo ASC");

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
        public static string UltimoKmVeiculo(int idVeiculo)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT FIRST(1) CV.KmFinal ");
            strSQL.Append("FROM ControleVeiculo CV ");
            strSQL.Append("WHERE CV.Id_Veiculo = @Id_Veiculo ");
            strSQL.Append("ORDER BY CV.Id DESC");

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
