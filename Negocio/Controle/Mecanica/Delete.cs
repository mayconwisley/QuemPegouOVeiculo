using BancoDados;
using Objeto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Controle.Mecanica
{
   public static class Delete
    {
        static CRUD crud = null;
        static StringBuilder strSQL = null;

        public static bool Register(ControleManutencaoObj controleManutencao)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            strSQL.Append("DELETE FROM ControleManutencao ");
            strSQL.Append("WHERE Id = @Id");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Id", controleManutencao.Id);
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
