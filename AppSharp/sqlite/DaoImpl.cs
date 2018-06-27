using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSharp.sqlite.dbpo;
using System.Data.SQLite;
using AppSharp.utils;
using System.Reflection;

namespace AppSharp.sqlite
{
    public class DaoImpl : IDao
    {
        private SQLiteConnection conn = null;

        public DaoImpl(string dbPath, List<String> initSql)
        {
            conn = new SQLiteConnection("data source=" + dbPath);
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
                initDb(initSql);
            }
        }

        private void initDb(List<String> initSql)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = conn;
            for (int i = 0; i < initSql.Count; i++)
            {
                cmd.CommandText = initSql[i];
                cmd.ExecuteNonQuery();
            }
        }

        public bool insert(BaseVo baseVo)
        {
            bool needInsert = false;
            string tableName = getTableNameByVo(baseVo);
            Dictionary<string, object> dict = ObjectUtils.EachObjProperties(baseVo);

            string sql = "insert into " + tableName ;
            string fields = " ( ";
            string values = "values ( ";

            foreach (var prop in dict)
            {
                needInsert = true;
                Console.WriteLine(prop.Key, prop.Value);
                fields += prop.Key + ", ";
                values += (prop.Value != null ? "\""+prop.Value.ToString()+ "\""  : "") + ", " ;
            }

            if(!needInsert)
                return false;

            fields = fields.Substring(0, fields.Length - 2) +" ) ";
            values = values.Substring(0, values.Length - 2) + " );";

            sql += fields + values;

            int ret = executeNonQuery(sql);

            return ret != 0;
        }

        public List<BaseVo> getResults(BaseVo baseVo)
        {
            bool needQuery = false;
            List<BaseVo> result = new List<BaseVo>();
            string tableName = getTableNameByVo(baseVo);
            Dictionary<string, object> dict = ObjectUtils.EachObjProperties(baseVo);
            string fields = "";
            string where = " where 1=1 ";

            List<string> fieldList = new List<string>();// 字段名列表，有序

            foreach (var prop in dict)
            {
                needQuery = true;
                Console.WriteLine(prop.Key, prop.Value);

                fields += prop.Key + ", ";
                fieldList.Add(prop.Key);

                Console.WriteLine(prop.Value);
                
                PropertyInfo pInfo = baseVo.GetType().GetProperty(prop.Key);
                
                if(pInfo.PropertyType == typeof(string))
                {
                    if (prop.Value != null && prop.Value.ToString() != "")
                    {
                        where += " and " + prop.Key + " = " + prop.Value.ToString();
                    }
                }
                else if (pInfo.PropertyType == typeof(int))
                {
                    if (int.Parse(prop.Value.ToString()) != 0)
                    {
                        where += " and " + prop.Key + " = " + prop.Value.ToString();
                    }
                }
            }

            if (!needQuery)
                return result;

            fields = fields.Substring(0, fields.Length - 2);


            string sql = "select " + fields + " from " + tableName + where;

            string typeName = baseVo.GetType().ToString();
            Type type = Type.GetType(typeName);
            
            SQLiteDataReader sr = executeReader(sql);
            while (sr.Read())
            {
                var obj = type.Assembly.CreateInstance(typeName) as BaseVo;

                for (int i = 0; i < fieldList.Count; i++)
                {
                    string field = fieldList[i];
                    string value = sr[field].ToString();

                    PropertyInfo pInfo = obj.GetType().GetProperty(field);

                    Console.WriteLine(pInfo.PropertyType);
                    if(pInfo.PropertyType == typeof(string))
                    {
                        pInfo.SetValue(obj, value, null);
                    }
                    else if (pInfo.PropertyType == typeof(int))
                    {
                        pInfo.SetValue(obj, int.Parse(value), null);
                    }
                    else
                    {
                        pInfo.SetValue(obj, value, null);
                    }
                }

                result.Add(obj as BaseVo);
            }
            
            return result;
        }


        public bool remove(BaseVo baseVo)
        {
            throw new NotImplementedException();
        }

        public bool update(BaseVo baseVo)
        {
            throw new NotImplementedException();
        }

        private string getTableNameByVo(BaseVo baseVo)
        {
            string type = baseVo.GetType().ToString();
            string table = type.Substring(type.LastIndexOf('.')+1);
            return table;
        }


        public int executeNonQuery(string sql)
        {
            try
            {
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }

        public SQLiteDataReader executeReader(string sql)
        {
            try
            {
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
