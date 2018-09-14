using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EFData.Models;

namespace EFData.Controllers
{
    public class HomeController : Controller
    {

        public static int id;
        #region 批量导入基站
        private EFdata db = new EFdata();
        public ActionResult StationImport()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StationImport(HttpPostedFileBase filebase)
        {
            HttpPostedFileBase file = Request.Files["files"];
            string filename;
            string FileName;
            string savePath;
            if (file == null || file.ContentLength <= 0)
            {
                ViewBag.error = "文件不能为空";
                return View();
            }
            else
            {
                 filename = Path.GetFileName(file.FileName);
                int filesize = file.ContentLength;//获取上传文件的大小单位为字节byte
                string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
                int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

                FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                if (!FileType.Contains(fileEx))
                {
                    ViewBag.error = "文件类型不对，只能导入xls和xlsx格式的文件";
                    return View();
                }
                if (filesize >= Maxsize)
                {
                    ViewBag.error = "上传文件超过4M，不能上传";
                    return View();
                }
                string path = AppDomain.CurrentDomain.BaseDirectory + "uploads/excel/";
                savePath = Path.Combine(path, FileName);
                file.SaveAs(savePath);
            }

            //string result = string.Empty;
            string strConn;
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + savePath + ";" + "Extended Properties=Excel 8.0";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            OleDbDataAdapter myCommand = new OleDbDataAdapter("select * from [党员信息$]", strConn);
            DataSet myDataSet = new DataSet();
            try
            {
                myCommand.Fill(myDataSet, "ExcelInfo");
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
            DataTable table = myDataSet.Tables["ExcelInfo"].DefaultView.ToTable();
            id = getId();
            int count = 1;


            for (int i = 0; i < table.Rows.Count; i++)
            {

                UserInfo u = GetUserInfo(table.Rows[i]);
                UserLogin User = SetUserLogin(table.Rows[i]);
                //try
                //{
                if (User.mobileid == "")
                {
                    Error er = new Error();
                    er.errorData = DataCommon.getJsonByObject(User);
                    er.errorReason = "电话号码为空不能注册！要补齐电话号码！（" + filename + ")";
                    er.errorTable = "用户登录表";
                    er.errorTime = DateTime.Now;
                    db.Errors.Add(er);
                    db.SaveChanges();
                    continue;
                }


                if (DataCommon.CheckIpone(User.mobileid))
                {
                    UserLogin uL = db.UserLogin.Where(a => a.mobileid == User.mobileid).FirstOrDefault();
                    UserInfo uI = db.UserInfo.Where(a => a.userid == uL.id).FirstOrDefault();
                    if (uI == null)
                    {


                    }
                    else
                    {
                        uI.address = u.address;
                        uI.areaid = u.areaid;
                        uI.company = u.company;
                        uI.duty = u.duty;
                        uI.imgheader = u.imgheader;
                        uI.insertTime = u.insertTime;
                        uI.name = u.name;
                        uI.nationalityGJ = u.nationalityGJ;
                        uI.nationMZ = u.nationMZ;
                        uI.partybrach = u.partybrach;
                        uI.partyinsertTime = u.partyinsertTime;
                        uI.partymemberid = u.partymemberid;
                        uI.qqnumber = u.qqnumber;
                        uI.status = u.status;
                        uI.town = u.town;
                        uI.township = u.township;
                        uI.wxnumber = u.wxnumber;
                        uI.wxopenId = u.wxopenId;
                        uI.zfbnumber = u.zfbnumber;
                        uI.educationXL = u.educationXL;
                        uI.sexy = u.sexy;
                        uI.birthday = u.birthday;
                        db.Set<UserInfo>().Attach(uI);
                        db.Entry<UserInfo>(uI).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    Error er = new Error();
                    er.errorData = DataCommon.getJsonByObject(User);
                    er.errorReason = "手机号重复注册！（" + filename + ")";
                    er.errorTable = "用户登录表";
                    er.errorTime = DateTime.Now;
                    db.Errors.Add(er);
                    db.SaveChanges();


                }
                else
                {
                    db.UserLogin.Add(User);
                    db.UserInfo.Add(u);
                    db.SaveChanges();
                    id++;
                    count++;
                }


                ////}
                ////catch
                ////{
                //    Error er = new Error();
                //    er.errorData = DataCommon.getJsonByObject(User);
                //    er.errorReason = "未知错误！";
                //    er.errorTable = "用户信息表";
                //    er.errorTime = DateTime.Now;
                //    db.Errors.Add(er);
                //    db.SaveChanges();

                //}


            }

            if (count == table.Rows.Count)
            {
                ViewBag.error = "存入成功！";

            }
            else if (count == 1)
            {
                ViewBag.error = "存入失败导入零条数据！";
            }
            else
            {

                ViewBag.error = "部分导入不成功！可能是电话号码重复注册！详情看错误日志！";
            }
            System.Threading.Thread.Sleep(2000);
            return View();
        }
        #endregion
        //获取地区ID
        private int GetAreaID(string township, string town)
        {
            string sql = "select id from uv_area where areaname='" + township + "' and name='" + town + "'";
            int id = Convert.ToInt32(DbHelper.ExcuteScalar(sql));

            return id;
        }
        //获从行中获取用户的信息
        public UserInfo GetUserInfo(DataRow dr)
        {
            UserInfo user = new UserInfo();
            //获取地区名称

            user.userid = id;
            user.name = dr["姓名"].ToString();
            user.sexy = dr["性别"].ToString();
            user.status = dr["人员类别"].ToString() == "正式党员" ? 1 : 0;
            user.partybrach = dr["所在党支部"].ToString();
            user.insertTime = DateTime.Now;
            user.areaid = GetAreaID(dr["乡镇"].ToString().Trim(), dr["村"].ToString().Trim());
            // user.areaid =db.Database.SqlQuery("select id from uv_area where areaname=@p0 and name=@p1", dr["乡镇"].ToString().Trim(),dr["村"].ToString().Trim());
            user.township = dr["乡镇"].ToString().Trim();
            user.town = dr["村"].ToString().Trim();
            user.partyinsertTime = Convert.ToDateTime(dr["加入党组织日期"].ToString());
            user.educationXL = dr["学历"].ToString();
            user.birthday = dr["出生日期"].ToString();
            user.qqnumber = "";
            user.address = dr["家庭住址"].ToString();
            user.partymemberid = 0;
            user.company = "";
            user.duty = "";
            user.imgheader = "";
            user.nationalityGJ = "";
            user.wxnumber = "";
            user.wxopenId = "";
            user.zfbnumber = "";
           

            return user;

        }


        public UserLogin SetUserLogin(DataRow dr)
        {
            UserLogin user = new UserLogin();
            user.loginname = dr["姓名"].ToString();
            user.loginpwd = "123456";
            user.pid = "";
            user.mobileid = dr["联系电话"].ToString();
            user.roleid = 0;
            user.status = 0;
            user.md5pass = "E10ADC3949BA59ABBE56E057F20F883E";
            user.id = id;

            return user;

            //获取地区名称


        }




        //获得用户Id
        public int getId()
        {
            try
            {
                string sql = " select MAX(id) from UserLogin";
                return Convert.ToInt32(DbHelper.ExcuteScalar(sql)) + 1;
            }
            catch
            {
                return 1;
            }
        }
    }
}