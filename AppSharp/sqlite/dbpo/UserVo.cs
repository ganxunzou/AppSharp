using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSharp.sqlite.dbpo
{
    /// <summary>
    /// 人员
    /// </summary>
    public class UserVo : BaseVo
    {
        private string userId;
        private string name;
        private string sex;
        private int age;
        private string deptId;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Sex
        {
            get
            {
                return sex;
            }

            set
            {
                sex = value;
            }
        }

        public int Age
        {
            get
            {
                return age;
            }

            set
            {
                age = value;
            }
        }

        public string DeptId
        {
            get
            {
                return deptId;
            }

            set
            {
                deptId = value;
            }
        }

        public string UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value;
            }
        }
    }
}
