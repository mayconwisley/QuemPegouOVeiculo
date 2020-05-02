using BancoDados;
using Objeto;
using System;
using System.Data;
using System.Text;

namespace Negocio.Veiculo
{
    public static class Update
    {

        static CRUD crud;
        static StringBuilder strSQL = null;
        public static bool Register(VeiculoObj veiculo)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            strSQL.Append("UPDATE Veiculo ");
            strSQL.Append("SET Placa = @Placa, Modelo = @Modelo, Chassi = @Chassi, Renavam = @Renavam, Status = @Status ");
            strSQL.Append("WHERE Id = @Id");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Placa", veiculo.Placa);
                crud.AddParameter("Modelo", veiculo.Modelo);
                crud.AddParameter("Chassi", veiculo.Chassi);
                crud.AddParameter("Renavam", veiculo.Renavam);
                crud.AddParameter("Status", veiculo.Status);
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
