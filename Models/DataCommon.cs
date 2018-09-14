using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace EFData.Models
{
    public class DataCommon
    {
      private static  EFdata  db = new EFdata();
        public static bool CheckIpone(string phone)
        {
            var data = from u in db.UserLogin where u.mobileid == phone select u;
            try
            {
                if (data.Count()!=0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }


        }

        public bool SavaUserInfo(UserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserInfo(");
            strSql.Append("name,address,status,partymemberid,partybrach,insertTime,areaid,township,town,partyinsertTime,educationXL,birthday,sexy)");
            strSql.Append(" values (");
            strSql.Append("@name,@address,@status,@partymemberid,@partybrach,@insertTime,@areaid,@township,@town,@partyinsertTime,@educationXL,@birthday,@sexy)");
            SqlParameter[] parameters = {
                    new SqlParameter("@name", SqlDbType.VarChar,15),
                    new SqlParameter("@address", SqlDbType.VarChar,10),
                    new SqlParameter("@status", SqlDbType.VarChar,20),
                    new SqlParameter("@partymemberid", SqlDbType.VarChar,20),
                    new SqlParameter("@partybrach", SqlDbType.VarChar,15),
                    new SqlParameter("@insertTime", SqlDbType.DateTime2),
                    new SqlParameter("@areaid", SqlDbType.VarChar,20),
                    new SqlParameter("@township", SqlDbType.VarChar,20),
                    new SqlParameter("@town", SqlDbType.VarChar,15),
                    new SqlParameter("@partyinsertTime", SqlDbType.DateTime2),
                    new SqlParameter("@educationXL", SqlDbType.VarChar,20),
                    new SqlParameter("@birthday", SqlDbType.DateTime2),
                    new SqlParameter("@sexy",SqlDbType.VarChar,5)
            };
            parameters[0].Value = model.name;
            parameters[1].Value = model.address;
            parameters[2].Value = model.status;
            parameters[3].Value = model.partymemberid;
            parameters[4].Value = model.partybrach;
            parameters[5].Value = model.insertTime;
            parameters[6].Value = model.areaid;
            parameters[7].Value = model.township;
            parameters[8].Value = model.town;
            parameters[9].Value = model.partyinsertTime;
            parameters[10].Value = model.educationXL;
            parameters[11].Value = model.birthday;
            parameters[12].Value = model.sexy;


            int rows = DbHelper.ExcuteNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }



        }

        public static string getJsonByObject(Object obj)
        {
            //实例化DataContractJsonSerializer对象，需要待序列化的对象类型
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            //实例化一个内存流，用于存放序列化后的数据
            MemoryStream stream = new MemoryStream();
            //使用WriteObject序列化对象
            serializer.WriteObject(stream, obj);
            //写入内存流中
            byte[] dataBytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dataBytes, 0, (int)stream.Length);
            //通过UTF8格式转换为字符串
            return Encoding.UTF8.GetString(dataBytes);
        }

    }
}