using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataToDb
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SqlSugarClient db = new SqlSugarClient(
            new ConnectionConfig()
            {
                ConnectionString = "server=192.168.13.128;user=root;password=173355872;database=study",
                DbType = DbType.MySql,//设置数据库类型
                IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
            });
            var client = new ElasticClient(new ConnectionSettings(new Uri( "http://192.168.13.128:9200")).DefaultIndex("novel"));
            var tmp = client.IndexDocument(new Novel() { Content = "天之苍苍，其正色邪", Id = 5,KeyWord= "天之苍苍，其正色邪" });
            tmp.ToString();

            //搜索
            //var documents = client.Search<Novel>(s => s.Query(q => q.Match(m => m.Field(f => f.Content).Query("雎鸠")))).Documents;
            //documents.ToString();

            var id = 1294571;
            var tokenSource = new System.Threading.CancellationTokenSource();
            foreach (var item in new List<string> { "从零开始", "仙逆", "宇宙巨校闪级生" , "修神外传", "凡人修仙传", "将夜", "我欲封天", "武神空间", "永恒圣王", "求魔", "遮天" })
            {
                var models = new List<Novel>();
                var xn = File.ReadAllText($"{item}.txt");
                var i = 0;
                try
                {
                    while (true)
                    {

                        var txt = xn.Substring(i * 100, 100);
                        if (txt.Length > 0)
                        {
                            models.Add(new Novel() { Content = txt,Id= ++id });
                            i++;
                            if (models.Count >= 2000)
                            {
                              var builk =   client.BulkAll(models, f => f, tokenSource.Token);
                                builk.Wait(TimeSpan.FromSeconds(5), s => Console.WriteLine(s.Page));
                                //db.Insertable(models).ExecuteCommand();
                                models.Clear();
                            }
                        }
                        else
                        {
                            break;
                        }
                        Console.WriteLine($"{i * 100 * 100 / xn.Length}%");

                    }
                }
                catch (Exception)
                {

                }
                if (models.Count>0)
                {
                    client.BulkAll(models, f => f).Wait(TimeSpan.FromSeconds(5), s => Console.WriteLine(s.Page));
                }
                //db.Insertable(models).ExecuteCommand();
            }
 

        }
    }
}
