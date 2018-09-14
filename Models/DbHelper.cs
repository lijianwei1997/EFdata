using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.OleDb;


namespace EFData
{

    /// <summary>
    /// DBhelper 的摘要说明
    /// </summary>
    public class DbHelper
    {
        //ata Source=.\SQLEXPRESS;AttachDbFilename=F:\QMDownload\App_Data\医院访问系统.mdf;Integrated Security=True;Connect Timeout=30
        public static string Insert = "insert";
        public static string Update = "update";
       // public static readonly string str = "Server=DESKTOP-P3T1EEK\\SQLEXPRESS;Data Source=DESKTOP-P3T1EEK\\SQLEXPRESS;Initial Catalog=Course;Integrated Security=True";
        public static readonly string str = ConfigurationManager.ConnectionStrings["EFdata"].ToString();
        public static int ExcuteNonQuery(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    if (ps != null)
                   { cmd.Parameters.AddRange(ps); }


                 return cmd.ExecuteNonQuery();

                }
            }
        }//增删改查

        public static SqlDataReader ExcuteReader(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection con = new SqlConnection(str))
            {

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    if (ps != null)
                    {
                        cmd.Parameters.AddRange(ps);
                    }
                    try
                    {
                        con.Open();
                        return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    }
                    catch (Exception ex)
                    {
                        con.Close();
                        con.Dispose();
                        throw ex;
                    }



                }




            }



        }//重新封装ExcuteReader()

        public static object ExcuteScalar(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    if (ps != null)
                    {
                        cmd.Parameters.AddRange(ps);
                    }
                     

                    return cmd.ExecuteScalar();
                }

            }




        }//返回读取数据的第一行第一列；
         //返回一个数据库
        public static DataTable Excutetable(string sql, params SqlParameter[] ps)
        {
            DataTable dt = new DataTable();//创建数据表
            using (SqlDataAdapter sda = new SqlDataAdapter(sql, str))
            {
                sda.SelectCommand.Parameters.AddRange(ps);
                sda.Fill(dt);

            }
            return dt;

        }

        //重新封装ExecureNonExcuteSet();ExecuteNonExcuteSet的返回值是受影响的行数；
        public static int ExcuteNonQuery(string sql)
        {
            //连接数据库
            using (SqlConnection con = new SqlConnection(str))
            {
                //执行数据库语句
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();//打开数据库

                    return  cmd.ExecuteNonQuery();
                }
            }
        }

        //重新封装ExcuteScalar();ExcuteScalar的返回的是一个Object；
        public static object ExcuteScalar(string sql)
        {//连接数据库
            using (SqlConnection con = new SqlConnection(str))
            {//执行数据库语句
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    return cmd.ExecuteScalar();
                    //此方法主要用于查询上
                }
            }

        }


        //重新封装SQldatareader  reader 中带有数据
        public static SqlDataReader ExcuteReader(string sql)
        {
            SqlConnection con = new SqlConnection(str);//连接数据库
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {  //执行SQl语句

                try
                {
                    con.Open();
                    return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                }
                catch (Exception ex)
                {
                    con.Close();
                    con.Dispose();
                    throw ex;
                }


            }






        }

        public static DataTable Excutetable(string sql)
        {
            DataTable dt = new DataTable();//创建数据表
            using (SqlDataAdapter sda = new SqlDataAdapter(sql, str))
            {

                sda.Fill(dt);

            }
            return dt;

        }
        public static DataSet ExcuteSet(string sql)
        {
            DataSet dt = new DataSet();//创建数据表
            using (SqlDataAdapter sda = new SqlDataAdapter(sql, str))
            {

                sda.Fill(dt);

            }
            return dt;

        }

        public static bool Exists(string sql, params SqlParameter[] ps)
        {

            using (SqlConnection con = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    if (ps != null)
                    { cmd.Parameters.AddRange(ps); }


                    if ((int)cmd.ExecuteScalar() >= 0)
                    {
                        return true;
                    }
                    else
                    {

                        return false;
                    }

                }
            }

        }
        public static DataSet ExcelSqlConnection(string filepath, string tableName)
        {


            try
            {
                string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
                OleDbConnection ExcelConn = new OleDbConnection(strCon);
                //try
                //{
                string strCom = string.Format("SELECT * FROM [Sheet1$]");
                ExcelConn.Open();
                OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, ExcelConn);
                DataSet ds = new DataSet();
                myCommand.Fill(ds, "[" + tableName + "$]");
                ExcelConn.Close();
                return ds;
            }
            catch
            {
                string strCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath + ";Extended Properties='Excel 12.0;'";
                OleDbConnection ExcelConn = new OleDbConnection(strCon);
                //try
                //{
                string strCom = string.Format("SELECT * FROM [Sheet1$]");
                ExcelConn.Open();
                OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, ExcelConn);
                DataSet ds = new DataSet();
                myCommand.Fill(ds, "[" + tableName + "$]");
                ExcelConn.Close();
                return ds;
            }


            //}
            //catch
            //{
            //    ExcelConn.Close();
            //    return null;
            //}
        }

    }
}