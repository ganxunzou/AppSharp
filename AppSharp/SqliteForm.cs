using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using AppSharp.sqlite.dbpo;
using AppSharp.sqlite;


namespace AppSharp
{
    public partial class SqliteForm : Form
    {
        static string path = System.Windows.Forms.Application.StartupPath + "/hello.db";
        private IUserDao dao = new UserDaoImpl();

        static void CreateDB()
        {
           
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            cn.Close();
        }


        //---添加表
        static void CreateTable()
        {
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            if (cn.State != System.Data.ConnectionState.Open)
            {
                cn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = cn;
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS t1(id varchar(4),score int)";
                cmd.ExecuteNonQuery();
            }
            cn.Close();
        }

        public SqliteForm()
        {
            InitializeComponent();

            
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserVo v = new UserVo();
            v.Age = 12;
            v.DeptId = "11";
            v.Name = "hello";
            v.UserId = "456";
            v.Sex = "F";

            dao.insert(v);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserVo v = new UserVo();
            v.DeptId = "11";
            v.UserId = "456";

            List<BaseVo> bs = dao.getResults(v);

            Console.WriteLine(bs.Count);
        }
    }
}
