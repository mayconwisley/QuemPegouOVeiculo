﻿using BancoDados;
using Objeto;
using System;
using System.Data;
using System.Text;

namespace Negocio.Controle.Multa
{
    public static class Update
    {
        static CRUD crud = null;
        static StringBuilder strSQL = null;

        public static bool Register(ControleMultaObj controleMulta)
        {
            crud = new CRUD();
            strSQL = new StringBuilder();
            strSQL.Append("UPDATE ControleMulta ");
            strSQL.Append("SET Id_Veiculo = @Id_Veiculo, Id_Motorista = @Id_Motorista, Data = @Data, Valor = @Valor, Pontos = @Pontos, Descricao = @Descricao ");
            strSQL.Append("WHERE Id = @Id");

            try
            {
                crud.ClearParameter();
                crud.AddParameter("Id_Veiculo", controleMulta.Veiculo.Id);
                crud.AddParameter("Id_Motorista", controleMulta.Motorista.Id);
                crud.AddParameter("Data", controleMulta.Data);
                crud.AddParameter("Valor", controleMulta.Valor);
                crud.AddParameter("Pontos", controleMulta.Pontos);
                crud.AddParameter("Descricao", controleMulta.Descricao);
                crud.AddParameter("Id", controleMulta.Id);
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
