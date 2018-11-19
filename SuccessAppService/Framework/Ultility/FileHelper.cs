using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Text;
using SuccessAppService.Framework.Ultility;


namespace SuccessAppService.Framework.Ultility
{
    [Serializable]
    public class FileHelper
    {
        //upload file
        public static bool UploadFile(string FullFolder, FileUpload FileUp)
        {
            bool IsSuccess = false;
            try
            {
                if (Directory.Exists(FullFolder) == false)
                    Directory.CreateDirectory(FullFolder);


                //check extension of file
                if (IsFileUp(FileUp.FileName) == true)
                {
                    FileUp.SaveAs(FullFolder + "/" + FileUp.FileName);
                    IsSuccess = true;
                }
            }
            catch
            {
                IsSuccess = false;
            }
            return IsSuccess;

        }

        //Upload and rename file
        public static bool UploadFile(string FullFolder, FileUpload FileUp, string ReName)
        {
            bool IsSuccess = false;
            try
            {
                string FileName = FileUp.FileName;
                if (string.IsNullOrEmpty(FileName))
                    return false;

                if (Directory.Exists(FullFolder) == false)
                    Directory.CreateDirectory(FullFolder);

                //check extension of file
                if (IsFileUp(FileName) == true)
                {
                    // string ext = GetExt(FileName);
                    FileUp.SaveAs(FullFolder + "/" + ReName);
                    IsSuccess = true;
                }
            }
            catch
            {
                return false;
            }
            return IsSuccess;

        }



        //get ext of string file
        public static string GetExt(string strFile)
        {
            string Ext = string.Empty;
            if (!string.IsNullOrEmpty(strFile))
            {
                int indexdot = strFile.LastIndexOf('.');
                if (indexdot < 0)
                    return string.Empty;
                else
                {
                    indexdot = indexdot + 1;
                    Ext = strFile.Substring(indexdot, strFile.Length - indexdot).ToLower();
                }
            }
            return Ext;
        }

        //check file upload
        public static bool IsFileUp(string strFile)
        {
            string ext = GetExt(strFile);

            if (!string.IsNullOrEmpty(ext))
            {
                string Partent = @"(7z|aiff|asf|avi|bmp|csv|doc|fla|flv|gif|gz|gzip|jpeg|jpg|mid|mov|mp3|mp4|mpc|mpeg|mpg|ods|odt|pdf|png|ppt|pxd|qt|ram|rar|rm|rmi|rmvb|rtf|sdc|sitd|swf|sxc|sxw|tar|tgz|tif|tiff|txt|vsd|wav|wma|wmv|xls|xlsx|xml|zip)$";
                return Regex.IsMatch(ext, Partent);
            }
            else
            {
                return false;
            }

        }

        //thumbnail
        public static Bitmap CreateThumbnail(string lcFilename, int lnWidth, int lnHeight)
        {

            Bitmap bmpOut = null;

            try
            {
                Bitmap loBMP = new Bitmap(lcFilename);
                ImageFormat loFormat = loBMP.RawFormat;

                decimal lnRatio;
                int lnNewWidth = 0;
                int lnNewHeight = 0;

                if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)
                    return loBMP;

                if (loBMP.Width > loBMP.Height)
                {
                    lnRatio = (decimal)lnWidth / loBMP.Width;
                    lnNewWidth = lnWidth;
                    decimal lnTemp = loBMP.Height * lnRatio;
                    lnNewHeight = (int)lnTemp;
                }
                else
                {
                    lnRatio = (decimal)lnHeight / loBMP.Height;
                    lnNewHeight = lnHeight;
                    decimal lnTemp = loBMP.Width * lnRatio;
                    lnNewWidth = (int)lnTemp;
                }


                bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);

                loBMP.Dispose();
            }
            catch
            {
                return null;
            }
            return bmpOut;
        }

        //Check file exist
        public static bool CheckExistFile(string lcFileName)
        {
            try
            {
                FileInfo fo = new FileInfo(lcFileName);
                return fo.Exists;
            }
            catch
            {
                return false;
            }
        }

        //Read content from file html
        public static string ReadHtml(string FullPath)
        {

            string Body = string.Empty;
            string html = ReadDoc(FullPath);
            if (!string.IsNullOrEmpty(html))
            {
                string pattern = "<body>((.|\\n)*?)</body>";
                Regex regEx = new Regex(pattern, RegexOptions.IgnoreCase);
                Match match = regEx.Match(html);
                if (match != null && match.Success && match.Groups[1].Success)
                {
                    Body = match.Groups[1].Value;
                }
            }
            return Body;
        }

        //Read content from file doc
        public static string ReadDoc(string FullPath)
        {
            string strContents = string.Empty;
            try
            {
                FileInfo fi = new FileInfo(FullPath);
                if (File.Exists(FullPath))
                {
                    StreamReader sr = File.OpenText(FullPath);
                    strContents = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch
            {
            }
            return strContents;
        }

        //get byte from image
        public static byte[] getImage(FileUpload fu)
        {
            int len;
            byte[] pic = null;
            if (fu.HasFile)
            {
                len = fu.PostedFile.ContentLength;
                pic = new byte[len];
                fu.PostedFile.InputStream.Read(pic, 0, len);
            }
            return pic;
        }

        //Export Excel
        //Note: Render control before export excel, insert code:
        //public override void VerifyRenderingInServerForm(Control control) { return; } 
        public static void ExportExcel(string FileName, GridView gv, System.Data.DataTable dt)
        {
            if (string.IsNullOrEmpty(FileName))
                FileName = "Report-" + DateTime.Now.ToString("yyyyMMdd-hhmmss");

            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + FileName + ".xls");
            HttpContext.Current.Response.ContentType = "application/vnd.xls";
            HttpContext.Current.Response.Charset = "utf-8";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.IO.StringWriter sw = new System.IO.StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw); //grid.AllowPaging = false; //grid.DataBind(); 

            gv.DataSource = dt;
            gv.DataBind();
            gv.RenderControl(htw);

            HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">");
            HttpContext.Current.Response.Output.Write(sw.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        //Download
        public static void DownloadFile(string FullFile)
        {
            if (File.Exists(FullFile))
            {
                string fileName = Path.GetFileName(FullFile);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + "");

               
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.WriteFile(FullFile);
            }
        }

        //Download
        public static void DownloadFile(string FullFile, string FileName)
        {
            if (File.Exists(FullFile))
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + "");

                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.WriteFile(FullFile);
            }
        }
        // Import from Excel file into DataSet
        public static DataSet GetExcel(string fileName)
        {
            //Application oXL;
            Workbook oWB;
            Worksheet oSheet;
            Range oRng;
            try
            {
                //, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0
                //  creat a Application object
                Microsoft.Office.Interop.Excel.Application oXL = new Microsoft.Office.Interop.Excel.Application();
                //   get   WorkBook  object
                oWB = oXL.Workbooks.Open(fileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                        Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                        Missing.Value, true);
                 

                //   get   WorkSheet object 
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.Sheets[1];
                System.Data.DataTable dt = new System.Data.DataTable("dtExcel");
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                DataRow dr;


                StringBuilder sb = new StringBuilder();
                int jValue = oSheet.UsedRange.Cells.Columns.Count;
                int iValue = oSheet.UsedRange.Cells.Rows.Count;
                //  get data columns
                for (int j = 1; j <= jValue; j++)
                {
                    dt.Columns.Add("column" + j, System.Type.GetType("System.String"));
                }


                //string colString = sb.ToString().Trim();
                //string[] colArray = colString.Split(':');


                //  get data in cell - EXCEPT THE FIRST ROW 
                for (int i = 2; i <= iValue; i++)
                {
                    dr = ds.Tables["dtExcel"].NewRow();
                    for (int j = 1; j <= jValue; j++)
                    {
                        oRng = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[i, j];
                        string strValue = oRng.Text.ToString();
                        dr["column" + j] = strValue;
                    }
                    ds.Tables["dtExcel"].Rows.Add(dr);
                }
                oXL.Quit(); // Kill EXCEL.EXE process
                return ds;
            }
            catch (Exception ex)
            {
                //Label1.Text += ex.Message.ToString();
                
                return null;
            }
            finally
            {
                //Dispose();
            }

        }

        public static System.Data.DataTable GetOrderDistributionFromExcel(string fileName, string pmonth)
        {
            var oleDbHelper = new OleDBHelper(fileName);
            string sheetname = oleDbHelper.GetExcelSheetName();
            oleDbHelper.CommandText = "select * from [" + sheetname + "]" + "where Monthapp=" + pmonth;
            System.Data.DataTable dataTable = oleDbHelper.GetDataTable();
            return dataTable;
        }
        public static System.Data.DataTable GetdataFromExcel(string fileName)
        {
            var oleDbHelper = new OleDBHelper(fileName);
            string sheetname = oleDbHelper.GetExcelSheetName();
            if (sheetname == null)
                sheetname = "Worksheet1";
            oleDbHelper.CommandText = "select * from [" + sheetname + "]";
            System.Data.DataTable dataTable = oleDbHelper.GetDataTable();
            return dataTable;
        }

        //====================================================================================
        //add by ndhung 2016.09.07 -> lấy toàn bộ table của các sheet đang có trong file
        //====================================================================================
        public static System.Data.DataSet GetdataFromExcel_2(string fileName)
        {
            var oleDbHelper = new OleDBHelper(fileName);
            System.Data.DataTable dbsheet = oleDbHelper.GetExcelSheetName_2();

            var dbSet = new System.Data.DataSet();
            foreach (DataRow row in dbsheet.Rows)
            {
                oleDbHelper.CommandText = "select * from [" + row["TABLE_NAME"].ToString() + "]";
                System.Data.DataTable dataTable = oleDbHelper.GetDataTable();
                dbSet.Tables.Add(dataTable);
            }
            
            return dbSet;
        }


        /// <summary>
        /// ADD BY NDHUNG 2017.10.19 -> THÊM ĐỂ DÙNG CHỨC NĂNG ĐỌC FILE EXCEL BÊN THỨ 3, DO OLEDB BỊ MICROSOFT ĐỔI
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        /*
        public static System.Data.DataTable GetDataFromExcel_v3(string filepath)
        {
            try
            {
                FileStream stream = File.Open(filepath, FileMode.Open, FileAccess.Read);

                string ext = System.IO.Path.GetExtension(filepath);
                Excel.IExcelDataReader excelReader;

                //dinh dang excel 2007
                if (ext == ".xlsx")
                    excelReader = Excel.ExcelReaderFactory.CreateOpenXmlReader(stream);
                else //dinh dang excel 97 - 2003
                    excelReader = Excel.ExcelReaderFactory.CreateBinaryReader(stream);

                excelReader.IsFirstRowAsColumnNames = true;
                DataSet result = excelReader.AsDataSet();
                var dt = result.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }*/
    }
}
