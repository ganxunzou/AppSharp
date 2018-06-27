using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSharp.sqlite.dbpo
{
    /// <summary>
    /// 部门
    /// </summary>
    public class DeptVo : BaseVo
    {
        private string deptId;
        private string deptName;

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

        public string DeptName
        {
            get
            {
                return deptName;
            }

            set
            {
                deptName = value;
            }
        }
    }
}
