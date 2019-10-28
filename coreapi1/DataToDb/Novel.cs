using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataToDb
{
    //如果实体类名称和表名不一致可以加上SugarTable特性指定表名
    public class Novel
    {
        //指定主键和自增列，当然数据库中也要设置主键和自增列才会有效
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string Content { get; set; }

        public string KeyWord { set; get; }
    }
    //当然也支持自定义特性， 这里就不细讲了
}
