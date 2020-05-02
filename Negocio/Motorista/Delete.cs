﻿using BancoDados;
using Objeto;
using System;
using System.Data;
using System.Text;

namespace Negocio.Motorista
{
    public static class Delete
    {
        static CRUD crud;
        static StringBuilder strSQL = null;

        public static bool Register(MotoristaObj motorista)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();

            strSQL.Append("DELETE FROM Motorista ");
            strSQL.Append("WHERE Id = @Id");

            try
            {
                crud.ClearParameter();
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
