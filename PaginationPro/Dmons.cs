using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace PaginationPro
{
    /// <summary>
    /// 
    ///</summary>
    [SugarTable("dmons")]
    public class Dmons
    {
        /// <summary>
        /// 监控点名称 
        ///</summary>
         [SugarColumn(ColumnName="Dmondtov2Name"    )]
         public string Dmondtov2Name { get; set; }
        /// <summary>
        /// 小数位 
        ///</summary>
         [SugarColumn(ColumnName="FractionalDigits"    )]
         public string FractionalDigits { get; set; }
        /// <summary>
        /// 监控点分组ID 
        ///</summary>
         [SugarColumn(ColumnName="GroupId"    )]
         public long? GroupId { get; set; }
        /// <summary>
        /// 监控点分组名称 
        ///</summary>
         [SugarColumn(ColumnName="GroupName"    )]
         public string GroupName { get; set; }
        /// <summary>
        /// 单位 
        ///</summary>
         [SugarColumn(ColumnName="Unit"    )]
         public string Unit { get; set; }
        /// <summary>
        /// 读写模式 
        ///</summary>
         [SugarColumn(ColumnName="Privilege"    )]
         public string Privilege { get; set; }
        /// <summary>
        /// 省流量模式 
        ///</summary>
         [SugarColumn(ColumnName="TrafficSaving"    )]
         public int? TrafficSaving { get; set; }
        /// <summary>
        /// 死区值 
        ///</summary>
         [SugarColumn(ColumnName="DeadValue"    )]
         public decimal? DeadValue { get; set; }
        /// <summary>
        /// 备注 
        ///</summary>
         [SugarColumn(ColumnName="Memo"    )]
         public string Memo { get; set; }
        /// <summary>
        /// 编码方式 
        ///</summary>
         [SugarColumn(ColumnName="Encoding"    )]
         public string Encoding { get; set; }
        /// <summary>
        /// 字节序 
        ///</summary>
         [SugarColumn(ColumnName="StringByteOrder"    )]
         public string StringByteOrder { get; set; }
        /// <summary>
        /// 字符个数 
        ///</summary>
         [SugarColumn(ColumnName="CharCount"    )]
         public int? CharCount { get; set; }
        /// <summary>
        /// 监控点Id 
        ///</summary>
         [SugarColumn(ColumnName="Id"    )]
         public long? Id { get; set; }
        /// <summary>
        /// 设备是否移除 
        ///</summary>
         [SugarColumn(ColumnName="IsDeviceChanged"    )]
         public int? IsDeviceChanged { get; set; }
        /// <summary>
        /// 条目状态 
        ///</summary>
         [SugarColumn(ColumnName="TaskState"    )]
         public string TaskState { get; set; }
        /// <summary>
        /// 代理主键
        ///</summary>
        [SugarColumn(ColumnName = "This_id", IsPrimaryKey = true)]
        public int? This_id { get; set; }
    }
}
