using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;

namespace BancoDados
{
    public class CRUD : Conexao
    {
        FbParameterCollection fbParameterCollection = new FbCommand().Parameters;
        FbCommand fbCommand = null;

        public void ClearParameter()
        {
            fbParameterCollection.Clear();
        }

        public void AddParameter(string parameter, object value)
        {
            fbParameterCollection.Add(new FbParameter(parameter, value));
        }

        public object Run(CommandType commandType, string sql)
        {
            if (Connecrion())
            {
                try
                {
                    fbCommand = fbConnection.CreateCommand();
                    fbCommand.CommandType = commandType;
                    fbCommand.CommandText = sql;
                    fbCommand.CommandTimeout = 7200;

                    foreach (FbParameter item in fbParameterCollection)
                    {
                        fbCommand.Parameters.Add(new FbParameter(item.ParameterName, item.Value));
                    }
                    return fbCommand.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    Disconnect();
                }
            }
            else
            {
                return false;
            }
        }

        public DataTable Query(CommandType commandType, string sql)
        {
            if (Connecrion())
            {
                try
                {
                    fbCommand = fbConnection.CreateCommand();
                    fbCommand.CommandType = commandType;
                    fbCommand.CommandText = sql;
                    fbCommand.CommandTimeout = 7200;

                    foreach (FbParameter item in fbParameterCollection)
                    {
                        fbCommand.Parameters.Add(new FbParameter(item.ParameterName, item.Value));
                    }
                    FbDataAdapter fbDataAdapter = new FbDataAdapter(fbCommand);
                    DataTable dataTable = new DataTable();
                    fbDataAdapter.Fill(dataTable);

                    return dataTable;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    Disconnect();
                }
            }
            else
            {
                return null;
            }
        }

    }
}
