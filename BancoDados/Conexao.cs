using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;

namespace BancoDados
{
    public class Conexao
    {
        protected FbConnection fbConnection;
        static string strWay = AppDomain.CurrentDomain.BaseDirectory.ToString();

        private string strConnection = "User=sysdba; Password=masterkey; ServerType=1; DataBase=" + strWay + @"Banco\Pegou.fdb";

        protected bool Connecrion()
        {
            fbConnection = new FbConnection(strConnection);
            try
            {
                fbConnection.Open();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected bool Disconnect()
        {
            try
            {
                if (fbConnection.State != ConnectionState.Closed)
                {
                    fbConnection.Close();
                    fbConnection.Dispose();
                    return true;
                }
                else
                {
                    fbConnection.Dispose();
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


    }
}
