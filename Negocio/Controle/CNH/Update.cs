using BancoDados;
using Objeto;
using System;
using System.Data;
using System.Text;

namespace Negocio.Controle.CNH
{
    public static class Update
    {
        static CRUD crud = null;
        static StringBuilder strSQL = null;

        public static bool Register(VencimentoCNHObj vencimentoCNH)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            strSQL.Append("UPDATE VencimentoCNH ");
            strSQL.Append("SET Id_Motorista = @Id_Motorista, Data = @Data, Status = @Status ");
            strSQL.Append("WHERE Id = @Id");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Id_Motorista", vencimentoCNH.Motorista.Id);
                crud.AddParameter("Data", vencimentoCNH.Data);
                crud.AddParameter("Status", vencimentoCNH.Status);
                crud.AddParameter("Id", vencimentoCNH.Id);
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
