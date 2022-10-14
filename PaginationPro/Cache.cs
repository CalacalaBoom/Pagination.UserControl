using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginationPro
{
    public static class Cache
    {
        public static DataTable PageCache { get; set; }=new DataTable();
        
        public static SqlSugarClient DB = new SqlSugarClient(new ConnectionConfig()
        {
            ConnectionString = "server=localhost;Database=fbox;Uid=root;Pwd=0502",
            DbType = SqlSugar.DbType.MySql,
            IsAutoCloseConnection = false
        });
    }
}
