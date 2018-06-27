using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSharp.sqlite
{
    public class UserDaoImpl : DaoImpl, IUserDao
    {
        private static string dbPath = System.Windows.Forms.Application.StartupPath + "/hello.db";
        private static List<string> initSql = new List<string>();

        static UserDaoImpl()
        {
            initSql.Add("CREATE TABLE IF NOT EXISTS UserVo(userId text, name text, sex text ,age INTERGER, deptId text)");
            initSql.Add("CREATE TABLE IF NOT EXISTS DeptVo(deptId text, deptName text)");
        }

        public UserDaoImpl() : base(dbPath, initSql)
        {

        }

        
    }
}
