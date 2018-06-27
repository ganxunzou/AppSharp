using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppSharp.utils
{
    public class ObjectUtils
    {
        public static Dictionary<string,object> EachObjProperties(Object obj)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Type type = obj.GetType();
            System.Reflection.PropertyInfo[] ps = type.GetProperties();
            foreach (PropertyInfo i in ps)
            {
                if (i.PropertyType != typeof(object))//属性的类型判断
                {
                    object value = i.GetValue(obj, null);
                    string name = i.Name;

                    dict.Add(name, value);
                }
            }

            return dict;
        }
    }
}
