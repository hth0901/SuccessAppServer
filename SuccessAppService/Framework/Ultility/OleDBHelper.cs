using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace SuccessAppService.Framework.Ultility
{
    public class OleDBHelper
    {
        #region Constructor

        public OleDBHelper(string fileName)
        {
            string ext = System.IO.Path.GetExtension(fileName);

            //dinh dang excel 2007
            if (ext == ".xlsx")//Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Test.xls;Extended Properties=""Excel 12.0;HDR=No;IMEX=1"""
                ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                                   @"Data Source=" + fileName + ";" +
                                   @"Extended Properties=""Excel 12.0;HDR=No;IMEX=1""";
            else//dinh dang excel 2003
                ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";" +
                                   "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
        }

        #endregion

        #region Properties

        private string commandText = "";
        private string errorMessage = "";
        public string ConnectionString { get; set; }
        public string FileName { get; set; }

        public string ErrorMessage
        {
            get { return errorMessage; }
        }

        public string CommandText
        {
            get { return commandText; }
            set { commandText = value; }
        }

        #endregion

        #region DataTable

        public DataTable GetDataTable()
        {
            var dataTable = new DataTable();
            try
            {
                OleDbConnection oleDbConnection;
                using (oleDbConnection = new OleDbConnection(ConnectionString))
                {
                    oleDbConnection.Open();
                    var command = new OleDbCommand
                                      {
                                          CommandText = commandText,
                                          Connection = oleDbConnection,
                                          CommandType = CommandType.Text
                                      };
                    var oleDbDataAdapter = new OleDbDataAdapter(command);
                    oleDbDataAdapter.Fill(dataTable);
                    oleDbDataAdapter.Dispose();
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Error: " + ex.Message;
            }
            return dataTable;
        }

        public int ExecuteNonQuery()
        {
            errorMessage = "";
            int effectedRecords = 0;

            try
            {
                OleDbConnection oleDbConnection;
                using (oleDbConnection = new OleDbConnection(ConnectionString))
                {
                    oleDbConnection.Open();
                    var command = new OleDbCommand
                                      {
                                          CommandText = commandText,
                                          Connection = oleDbConnection,
                                          CommandType = CommandType.Text
                                      };
                    effectedRecords = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Error: " + ex.Message;
            }
            return effectedRecords;
        }

        #endregion

        //public string GetExcelSheetName()
        //{
        //    OleDbConnection oleDbConnection;
        //    try
        //    {
        //        using (oleDbConnection = new OleDbConnection(ConnectionString))
        //        {
        //            oleDbConnection.Open();
        //            DataTable dataTable = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //            if (dataTable == null)
        //                return null;

        //            return dataTable.Rows[0]["TABLE_NAME"].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public string GetExcelSheetName() 
        {
            OleDbConnection oleDbConnection;
            try
            {
                using (oleDbConnection = new OleDbConnection(ConnectionString))
                {
                    oleDbConnection.Open();

                    //check table 
                    String tableToDelete = "_xlnm#_FilterDatabase";
                    Boolean tableExists = false;
                    DataTable dataTable = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["TABLE_NAME"].ToString() == tableToDelete)
                        {
                            tableExists = true;
                            break;
                        }
                    }

                    if (tableExists)
                    {
                        using (OleDbCommand cmd = new OleDbCommand(string.Format("DROP TABLE {0}", tableToDelete), oleDbConnection))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    //get trust table
                    dataTable = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    if (dataTable == null)
                        return null;

                    return dataTable.Rows[0]["TABLE_NAME"].ToString();
                }

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        //====================================================================================
        //add by ndhung 2016.09.07 -> lấy hết danh sách sheet có trong file excel
        //====================================================================================
        public DataTable GetExcelSheetName_2()
        {
            OleDbConnection oleDbConnection;
            try
            {
                using (oleDbConnection = new OleDbConnection(ConnectionString))
                {
                    oleDbConnection.Open();

                    //check table 
                    String tableToDelete = "_xlnm#_FilterDatabase";
                    Boolean tableExists = false;
                    DataTable dataTable = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["TABLE_NAME"].ToString() == tableToDelete)
                        {
                            tableExists = true;
                            break;
                        }
                    }

                    if (tableExists)
                    {
                        using (OleDbCommand cmd = new OleDbCommand(string.Format("DROP TABLE {0}", tableToDelete), oleDbConnection))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    //get trust table
                    dataTable = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    return dataTable;
                }

            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}