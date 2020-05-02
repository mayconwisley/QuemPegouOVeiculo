using BancoDados;
using Objeto;
using System;
using System.Data;
using System.Text;

namespace Negocio.Veiculo
{
    public static class Insert
    {

        static StringBuilder strSQL = null;
        static CRUD crud;
        public static bool Register(VeiculoObj veiculo)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            strSQL.Append("INSERT INTO Veiculo(Placa, Modelo, Chassi, Renavam, Status) ");
            strSQL.Append("VALUES(@Placa, @Modelo, @Chassi, @Renavam, @Status)");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Placa", veiculo.Placa);
                crud.AddParameter("Modelo", veiculo.Modelo);
                crud.AddParameter("Chassi", veiculo.Chassi);
                crud.AddParameter("Renavam", veiculo.Renavam);
                crud.AddParameter("Status", veiculo.Status);
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
