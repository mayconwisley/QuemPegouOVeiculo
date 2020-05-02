using BancoDados;
using Objeto;
using System;
using System.Data;
using System.Text;

namespace Negocio.Motorista
{
    public static class Insert
    {
        static CRUD crud;
        static StringBuilder strSQL = null;

        public static bool Register(MotoristaObj motorista)
        {

            crud = new CRUD();
            strSQL = new StringBuilder();

            strSQL.Append("INSERT INTO Motorista(Nome, NumCNH, VencimentoCNH, CategoriaCNH, CPF, RG, Ativo) ");
            strSQL.Append("VALUES(@Nome, @NumCNH, @VencimentoCNH, @CategoriaCNH, @CPF, @RG, @Ativo)");

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
