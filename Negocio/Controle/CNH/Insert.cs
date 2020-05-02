using BancoDados;
using Objeto;
using System;
using System.Data;
using System.Text;

namespace Negocio.Controle.CNH
{
    public static class Insert
    {
        static CRUD crud = null;
        static StringBuilder strSQL = null;

        public static bool Register(VencimentoCNHObj vencimentoCNH)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            strSQL.Append("INSERT INTO VencimentoCNH(Id_Motorista, Data, Status) ");
            strSQL.Append("VALUES(@Id_Motorista, @Data, @Status)");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Id_Motorista", vencimentoCNH.Motorista.Id);
                crud.AddParameter("Data", vencimentoCNH.Data);
                crud.AddParameter("Status", vencimentoCNH.Status);
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
