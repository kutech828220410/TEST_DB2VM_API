using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IBM.Data.DB2.Core;
using System.Data;
using System.Configuration;
using Basic;
using SQLUI;
using Oracle.ManagedDataAccess.Client;
using System.Text;
using HIS_DB_Lib;
namespace DB2VM.Controller
{
   

    [Route("dbvm/[controller]")]
    [ApiController]
    public class BBCMController : ControllerBase
    {
        private SQLControl sQLControl_UDSDBBCM = new SQLControl(MySQL_server, MySQL_database, "medicine_page_cloud", MySQL_userid, MySQL_password, (uint)MySQL_port.StringToInt32(), MySql.Data.MySqlClient.MySqlSslMode.None);

        static string MySQL_server = $"{ConfigurationManager.AppSettings["MySQL_server"]}";
        static string MySQL_database = $"{ConfigurationManager.AppSettings["MySQL_database"]}";
        static string MySQL_userid = $"{ConfigurationManager.AppSettings["MySQL_user"]}";
        static string MySQL_password = $"{ConfigurationManager.AppSettings["MySQL_password"]}";
        static string MySQL_port = $"{ConfigurationManager.AppSettings["MySQL_port"]}";

        [Route("/{Code}")]
        [HttpGet]
        public string Get(string? Code)
        {
            List<object[]> list_BBCM = new List<object[]>();
            if(Code.StringIsEmpty())
            {
                list_BBCM = sQLControl_UDSDBBCM.GetAllRows(null);
            }
            else
            {
                list_BBCM = sQLControl_UDSDBBCM.GetRowsByDefult(null, (int)enum_雲端藥檔.藥品碼, Code);
            }
           
            List<object[]> list_BBCM_buf = new List<object[]>();
            List<object[]> list_BBCM_Add = new List<object[]>();
            List<object[]> list_BBCM_Replace = new List<object[]>();
            List<medClass> medClasses = list_BBCM.SQLToClass<medClass, enum_雲端藥檔>();


            return medClasses.JsonSerializationt(); 
        }
    }
}
