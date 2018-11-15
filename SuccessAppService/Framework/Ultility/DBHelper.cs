using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SuccessAppService.Framework.Ultility
{
    
    [Serializable]
    public class DBHelper : IDeserializationCallback
    {
        #region------ error sql property----

        //ErrorMessage SQL Property
        public static string errorMessageSql = "";

        //ErrorCode SQL Property
        public static int errorCodeSql;

        #endregion

        [NonSerialized]
        private static SqlConnection connect;
        private static SqlTransaction trans;
        private static SqlCommand cmd;

        #region IDeserializationCallback Members

        void IDeserializationCallback.OnDeserialization(object sender)
        {
            var connHelper = new ConnectionHelper();
            connect = connHelper.Connect();
        }

        #endregion

        //set value para = DB.Null if para=null
        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            foreach (SqlParameter p in commandParameters)
            {
                //check for derived output value with no value assigned
                if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                {
                    p.Value = DBNull.Value;
                }
                command.Parameters.Add(p);
            }
        }

        //Update,Insert,Delete with Store Procedure or Text
        public static bool ExecuteNonQuery_SP(string spName, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(spName, CommandType.StoredProcedure, parameters);
        }

        public static bool ExecuteNonQuery_Text(string strSQLQuery, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(strSQLQuery, CommandType.Text, parameters);
        }

        public static bool ExecuteNonQuery(string strSQL, CommandType type, params SqlParameter[] parameters)
        {
            bool success = false;
            var cnnHelper = new ConnectionHelper();
            using (SqlConnection cnn = cnnHelper.Connect())
            {
                trans = cnn.BeginTransaction();
                cmd = new SqlCommand();
                cmd.CommandText = strSQL;
                cmd.CommandType = type;
                cmd.Connection = cnn;
                cmd.Transaction = trans;
                cmd.CommandTimeout = 0;

                //Add parameters
                if (parameters != null)
                {
                    AttachParameters(cmd, parameters);
                }
                try
                {
                    int rowEffected = cmd.ExecuteNonQuery();
                    trans.Commit();
                    success = (rowEffected > 0);
                }
                catch (SqlException sex)
                {
                    trans.Rollback();
                    throw sex;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
                finally
                {
                    cnn.Close();
                }
            }
            return success;
        }

        //Get DataSet with SP or Text
        public static DataSet getDataSet_SP(string spName, params SqlParameter[] parameters)
        {
            return getDataSet(spName, CommandType.StoredProcedure, parameters);
        }

        public static DataSet getDataSet_Text(string strSQLQuery, params SqlParameter[] parameters)
        {
            return getDataSet(strSQLQuery, CommandType.Text, parameters);
        }

        public static DataSet getDataSet(string strSQL, CommandType type, params SqlParameter[] parameters)
        {
            var ds = new DataSet();
            var cnnHelper = new ConnectionHelper();
            using (SqlConnection cnn = cnnHelper.Connect())
            {
                var cmd = new SqlCommand();
                cmd.CommandText = strSQL;
                cmd.CommandType = type;
                var adt = new SqlDataAdapter(cmd);
                cmd.Connection = cnn;
                cmd.CommandTimeout = 0;
                if (parameters != null)
                {
                    AttachParameters(cmd, parameters);
                }
                try
                {
                    adt.Fill(ds);
                }

                catch (SqlException sex)
                {
                    throw sex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cnn.Close();
                }
            }
            return ds;
        }

        //Get DataTable with SP or Text
        public static DataTable getDataTable_SP(string spName, SqlParameter[] parameters)
        {
            return getDataTable(spName, CommandType.StoredProcedure, parameters);
        }

        public static DataTable getDataTable_Text(string strSQLQuery, SqlParameter[] parameters)
        {
            return getDataTable(strSQLQuery, CommandType.Text, parameters);
        }

        public static DataTable getDataTable(string strSQL, CommandType type, SqlParameter[] parameters)
        {
            var table = new DataTable();
            var cnnHelper = new ConnectionHelper();
            using (SqlConnection cnn = cnnHelper.Connect())
            {
                try
                {
                    var cmd = new SqlCommand();
                    cmd.CommandText = strSQL;
                    cmd.CommandType = type;
                    var adt = new SqlDataAdapter(cmd);
                    cmd.Connection = cnn;
                    cmd.CommandTimeout = 0;
                    if (parameters != null)
                    {
                        AttachParameters(cmd, parameters);
                    }
                    table.Locale = CultureInfo.InvariantCulture;
                    adt.Fill(table);
                }
                catch (SqlException sex)
                {
                    throw sex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cnn.Close();
                }
            }
            return table;
        }

        //Get value of a cell in query result with SP or Text
        public static object ExcecuteScalar_SP(string spName, params SqlParameter[] parameters)
        {
            return ExcecuteScalar(spName, CommandType.StoredProcedure, parameters);
        }

        public static object ExcecuteScalar_Text(string strSQLQuery, params SqlParameter[] parameters)
        {
            return ExcecuteScalar(strSQLQuery, CommandType.Text, parameters);
        }

        public static object ExcecuteScalar(string strSQL, CommandType type, params SqlParameter[] parameters)
        {
            object value;
            var cnnHelper = new ConnectionHelper();
            using (SqlConnection cnn = cnnHelper.Connect())
            {
                var cmd = new SqlCommand();
                cmd.CommandText = strSQL;
                cmd.CommandType = type;
                cmd.Connection = cnn;

                //Add parameters
                if (parameters != null)
                {
                    AttachParameters(cmd, parameters);
                }
                try
                {
                    value = cmd.ExecuteScalar();
                }
                catch (SqlException sex)
                {
                    throw sex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cnn.Close();
                }
            }
            return value;
        }

        /*get DataReader in query result with SP or Text.
            Remember:When using SqlDataReader's object.We should call SqlDataReader.Close() when we does not use it more. 
            */

        public static SqlDataReader getDataReader_SP(string spName, params SqlParameter[] parameters)
        {
            return getDataReader(spName, CommandType.StoredProcedure, parameters);
        }

        public static SqlDataReader getDataReader_Text(string strSQLQuery, params SqlParameter[] parameters)
        {
            return getDataReader(strSQLQuery, CommandType.Text, parameters);
        }

        public static SqlDataReader getDataReader(string strSQL, CommandType type, params SqlParameter[] parameters)
        {
            SqlDataReader reader = null;
            var cnnHelper = new ConnectionHelper();

            SqlConnection cnn = cnnHelper.Connect();

            var cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.CommandType = type;
            cmd.Connection = cnn;

            //Add parameters
            if (parameters != null)
            {
                AttachParameters(cmd, parameters);
            }
            try
            {
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (SqlException sex)
            {
                throw sex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return reader;
        }

        //lay user name khi bit loginID
        public static String GetName(String sql, params SqlParameter[] parameters)
        {
            String result = null;

            var cnnHelper = new ConnectionHelper();

            SqlConnection cnn = cnnHelper.Connect();
            var cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            //Add parameters
            if (parameters != null)
            {
                AttachParameters(cmd, parameters);
            }
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        result = dr[0].ToString();
                    }
                }
            }
            catch (SqlException sex)
            {
                throw sex;
            }
            return result;
        }
    }

    public class ConnectionHelper
    {
        public SqlConnection Connect()
        {
            var _SqlConnection = new SqlConnection();
            String _MainConnectionString = ConfigurationManager.ConnectionStrings["SUCCESSAPP"].ToString();
            try
            {
                _SqlConnection = new SqlConnection(_MainConnectionString);
                if (_SqlConnection.State != ConnectionState.Open)
                {
                    _SqlConnection.Open();
                }
            }
            catch (SqlException sex)
            {
                //Log here
                throw sex;
            }
            return _SqlConnection;
        }
    }
    
}