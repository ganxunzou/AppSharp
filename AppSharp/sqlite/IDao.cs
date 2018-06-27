using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSharp.sqlite.dbpo;
using System.Collections;
using System.Data.SQLite;

namespace AppSharp.sqlite
{
    public interface IDao
    {
        bool insert(BaseVo baseVo);
        bool update(BaseVo baseVo);
        bool remove(BaseVo baseVo);
        List<BaseVo> getResults(BaseVo basevo);
        int executeNonQuery(string sql);
        SQLiteDataReader executeReader(string sql);

    }
}
