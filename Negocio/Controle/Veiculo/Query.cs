using BancoDados;
using System;
using System.Data;
using System.Text;

namespace Negocio.Controle.Veiculo
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

            strSQL.Append("SELECT CV.ID, CV.ID_VEICULO, CV.MODELO, CV.ID_MOTORISTA, CV.NOME, CV.DATAHORASAIDA, CV.DATAHORACHEGADA, ");
            strSQL.Append("CV.DIAS, CV.HORAS, CV.DESCRICAO, CV.KMINICIAL, CV.KMFINAL, CV.KMTOTAL, CV.STATUS ");
            strSQL.Append("FROM VW_CTRLVEICULO CV ");
            strSQL.Append("WHERE LOWER(CV.MODELO) LIKE @search OR LOWER(CV.Descricao) LIKE @search OR LOWER(CV.Nome) LIKE @search ");
            strSQL.Append("ORDER BY CV.DataHoraSaida DESC, CV.Nome ASC");

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

            strSQL.Append("SELECT CV.ID, CV.ID_VEICULO, CV.MODELO, CV.ID_MOTORISTA, CV.NOME, CV.DATAHORASAIDA, CV.DATAHORACHEGADA, ");
            strSQL.Append("CV.DIAS, CV.HORAS, CV.DESCRICAO, CV.KMINICIAL, CV.KMFINAL, CV.KMTOTAL, CV.STATUS ");
            strSQL.Append("FROM VW_CTRLVEICULO CV ");
            strSQL.Append("ORDER BY CV.DataHoraSaida DESC, CV.Nome ASC");

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

            strSQL.Append("SELECT CV.ID, CV.ID_VEICULO, CV.MODELO, CV.ID_MOTORISTA, CV.NOME, CV.DATAHORASAIDA, CV.DATAHORACHEGADA, ");
            strSQL.Append("CV.DIAS, CV.HORAS, CV.DESCRICAO, CV.KMINICIAL, CV.KMFINAL, CV.KMTOTAL, CV.STATUS ");
            strSQL.Append("FROM VW_CTRLVEICULO CV ");
            strSQL.Append("WHERE CV.Id_Veiculo = @Id_Veiculo ");
            strSQL.Append("ORDER BY CV.DataHoraSaida DESC, CV.Nome ASC");

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

            strSQL.Append("SELECT CV.ID, CV.ID_VEICULO, CV.MODELO, CV.ID_MOTORISTA, CV.NOME, CV.DATAHORASAIDA, CV.DATAHORACHEGADA, ");
            strSQL.Append("CV.DIAS, CV.HORAS, CV.DESCRICAO, CV.KMINICIAL, CV.KMFINAL, CV.KMTOTAL, CV.STATUS ");
            strSQL.Append("FROM VW_CTRLVEICULO CV ");
            strSQL.Append("WHERE CV.Id_Motorista = @Id_Motorista ");
            strSQL.Append("ORDER BY CV.DataHoraSaida DESC, CV.Nome ASC");

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

            strSQL.Append("SELECT CV.ID, CV.ID_VEICULO, CV.MODELO, CV.ID_MOTORISTA, CV.NOME, CV.DATAHORASAIDA, CV.DATAHORACHEGADA, ");
            strSQL.Append("CV.DIAS, CV.HORAS, CV.DESCRICAO, CV.KMINICIAL, CV.KMFINAL, CV.KMTOTAL, CV.STATUS ");
            strSQL.Append("FROM VW_CTRLVEICULO CV ");
            strSQL.Append("WHERE CV.Id_Veiculo = @Id_Veiculo ");
            strSQL.Append("AND CV.Id_Motorista = @Id_Motorista ");
            strSQL.Append("ORDER BY CV.DataHoraSaida DESC, CV.Nome ASC");

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
        public static DataTable RegisterDateExit(DateTime dataHoraSaida, DateTime dataHoraSaida1)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT CV.ID, CV.ID_VEICULO, CV.MODELO, CV.ID_MOTORISTA, CV.NOME, CV.DATAHORASAIDA, CV.DATAHORACHEGADA, ");
            strSQL.Append("CV.DIAS, CV.HORAS, CV.DESCRICAO, CV.KMINICIAL, CV.KMFINAL, CV.KMTOTAL, CV.STATUS ");
            strSQL.Append("FROM VW_CTRLVEICULO CV ");
            strSQL.Append("WHERE CV.DataHoraSaida BETWEEN @DataHoraSaida AND @DataHoraSaida1 ");
            strSQL.Append("ORDER BY CV.DataHoraSaida DESC, CV.Nome ASC");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("DataHoraSaida", dataHoraSaida);
                crud.AddParameter("DataHoraSaida1", dataHoraSaida1);
                return dataTable = crud.Query(CommandType.Text, strSQL.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable RegisterDateArrival(DateTime dataHoraChegada, DateTime dataHoraChegada1)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT CV.ID, CV.ID_VEICULO, CV.MODELO, CV.ID_MOTORISTA, CV.NOME, CV.DATAHORASAIDA, CV.DATAHORACHEGADA, ");
            strSQL.Append("CV.DIAS, CV.HORAS, CV.DESCRICAO, CV.KMINICIAL, CV.KMFINAL, CV.KMTOTAL, CV.STATUS ");
            strSQL.Append("FROM VW_CTRLVEICULO CV ");
            strSQL.Append("WHERE CV.DataHoraChegada BETWEEN @DataHoraChegada AND @DataHoraChegada1 ");
            strSQL.Append("ORDER BY CV.DataHoraSaida DESC, CV.Nome ASC");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("DataHoraChegada", dataHoraChegada);
                crud.AddParameter("DataHoraChegada1", dataHoraChegada1);
                return dataTable = crud.Query(CommandType.Text, strSQL.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable RegisterDateExitVehicle(DateTime dataHoraSaida, DateTime dataHoraSaida1, int idVeiculo)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT CV.ID, CV.ID_VEICULO, CV.MODELO, CV.ID_MOTORISTA, CV.NOME, CV.DATAHORASAIDA, CV.DATAHORACHEGADA, ");
            strSQL.Append("CV.DIAS, CV.HORAS, CV.DESCRICAO, CV.KMINICIAL, CV.KMFINAL, CV.KMTOTAL, CV.STATUS ");
            strSQL.Append("FROM VW_CTRLVEICULO CV ");
            strSQL.Append("WHERE CV.DataHoraSaida BETWEEN @DataHoraSaida AND @DataHoraSaida1 ");
            strSQL.Append("AND CV.Id_Veiculo = @Id_Veiculo ");
            strSQL.Append("ORDER BY CV.DataHoraSaida DESC, CV.Nome ASC");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("DataHoraSaida", dataHoraSaida);
                crud.AddParameter("DataHoraSaida1", dataHoraSaida1);
                crud.AddParameter("Id_Veiculo", idVeiculo);
                return dataTable = crud.Query(CommandType.Text, strSQL.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable RegisterDateArrivalVehicle(DateTime dataHoraChegada, DateTime dataHoraChegada1, int idVeiculo)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT CV.ID, CV.ID_VEICULO, CV.MODELO, CV.ID_MOTORISTA, CV.NOME, CV.DATAHORASAIDA, CV.DATAHORACHEGADA, ");
            strSQL.Append("CV.DIAS, CV.HORAS, CV.DESCRICAO, CV.KMINICIAL, CV.KMFINAL, CV.KMTOTAL, CV.STATUS ");
            strSQL.Append("FROM VW_CTRLVEICULO CV ");
            strSQL.Append("WHERE CV.DataHoraChegada BETWEEN @DataHoraChegada AND @DataHoraChegada1 ");
            strSQL.Append("AND CV.Id_Veiculo = @Id_Veiculo ");
            strSQL.Append("ORDER BY CV.DataHoraSaida DESC, CV.Nome ASC");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("DataHoraChegada", dataHoraChegada);
                crud.AddParameter("DataHoraChegada1", dataHoraChegada1);
                crud.AddParameter("Id_Veiculo", idVeiculo);
                return dataTable = crud.Query(CommandType.Text, strSQL.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable RegisterDateExitDriver(DateTime dataHoraSaida, DateTime dataHoraSaida1, int idMotorista)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT CV.ID, CV.ID_VEICULO, CV.MODELO, CV.ID_MOTORISTA, CV.NOME, CV.DATAHORASAIDA, CV.DATAHORACHEGADA, ");
            strSQL.Append("CV.DIAS, CV.HORAS, CV.DESCRICAO, CV.KMINICIAL, CV.KMFINAL, CV.KMTOTAL, CV.STATUS ");
            strSQL.Append("FROM VW_CTRLVEICULO CV ");
            strSQL.Append("WHERE CV.DataHoraSaida BETWEEN @DataHoraSaida AND @DataHoraSaida1 ");
            strSQL.Append("AND CV.Id_Motorista = @Id_Motorista ");
            strSQL.Append("ORDER BY CV.DataHoraSaida DESC, CV.Nome ASC");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("DataHoraSaida", dataHoraSaida);
                crud.AddParameter("DataHoraSaida1", dataHoraSaida1);
                crud.AddParameter("Id_Motorista", idMotorista);
                return dataTable = crud.Query(CommandType.Text, strSQL.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable RegisterDateArrivalDriver(DateTime dataHoraChegada, DateTime dataHoraChegada1, int idMotorista)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT CV.ID, CV.ID_VEICULO, CV.MODELO, CV.ID_MOTORISTA, CV.NOME, CV.DATAHORASAIDA, CV.DATAHORACHEGADA, ");
            strSQL.Append("CV.DIAS, CV.HORAS, CV.DESCRICAO, CV.KMINICIAL, CV.KMFINAL, CV.KMTOTAL, CV.STATUS ");
            strSQL.Append("FROM VW_CTRLVEICULO CV ");
            strSQL.Append("WHERE CV.DataHoraChegada BETWEEN @DataHoraChegada AND @DataHoraChegada1 ");
            strSQL.Append("AND CV.Id_Motorista = @Id_Motorista ");
            strSQL.Append("ORDER BY CV.DataHoraSaida DESC, CV.Nome ASC");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("DataHoraChegada", dataHoraChegada);
                crud.AddParameter("DataHoraChegada1", dataHoraChegada1);
                crud.AddParameter("Id_Motorista", idMotorista);
                return dataTable = crud.Query(CommandType.Text, strSQL.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable RegisterDateExitNull()
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            dataTable = new DataTable();

            strSQL.Append("SELECT CV.ID, CV.ID_VEICULO, CV.MODELO, CV.ID_MOTORISTA, CV.NOME, CV.DATAHORASAIDA, CV.DATAHORACHEGADA, ");
            strSQL.Append("CV.DIAS, CV.HORAS, CV.DESCRICAO, CV.KMINICIAL, CV.KMFINAL, CV.KMTOTAL, CV.STATUS ");
            strSQL.Append("FROM VW_CTRLVEICULO CV ");
            strSQL.Append("WHERE CV.DataHoraChegada IS NULL ");
            strSQL.Append("ORDER BY CV.DataHoraSaida DESC, CV.Nome ASC");

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
