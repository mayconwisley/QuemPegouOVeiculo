using BancoDados;
using Objeto;
using System;
using System.Data;
using System.Text;

namespace Negocio.Motorista
{
    public static class Update
    {
        static CRUD crud;
        static StringBuilder strSQL = null;

        public static bool Register(MotoristaObj motorista)
        {

            crud = new CRUD();
            strSQL = new StringBuilder();

            strSQL.Append("UPDATE Motorista ");
            strSQL.Append("SET Nome = @Nome, NumCNH = @NumCNH, VencimentoCNH = @VencimentoCNH, CategoriaCNH = @CategoriaCNH, CPF = @CPF, RG = @RG, Ativo = @Ativo ");
            strSQL.Append("WHERE Id = @Id");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Nome", motorista.Nome);
                crud.AddParameter("NumCNH", motorista.CNH);
                crud.AddParameter("VencimentoCNH", motorista.VencimentoCNH);
                crud.AddParameter("CategoriaCNH", motorista.CategoriaCNH);
                crud.AddParameter("CPF", motorista.CPF);
                crud.AddParameter("RG", motorista.RG);
                crud.AddParameter("Ativo", motorista.Ativo);
                crud.AddParameter("Id", motorista.Id);

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
