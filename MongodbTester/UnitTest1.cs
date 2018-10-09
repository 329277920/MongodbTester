using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongodbTester
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSimple()
        {
            // MongoClient 会保持一个与连接字符串对应的连接池，一个进程内只需要初始化一个实例即可
            // 多个连接字符串相同的实例会共享同一个连接池
            var client = new MongoClient("mongodb://192.168.10.75:27017");

            // 获取一个数据库实例，如果不存在，将在第一次使用时创建
            var db = client.GetDatabase("MyDb");

            // 获取一个集合实例，如果不存在，将在第一次使用时创建
            var collection = db.GetCollection<BsonDocument>("MyCollection");

            // 新增或更新一个文档
            var filter = Builders<BsonDocument>.Filter.Eq("_id", 20);
            var update = Builders<BsonDocument>.Update.Set("name", "cnf").AddToSet("age", 20);
            var opts = new UpdateOptions() { IsUpsert = true };
            collection.UpdateOneAsync(filter, update, opts).GetAwaiter().GetResult();

            // 查找文档
            var document = collection.FindAsync(filter).GetAwaiter().GetResult().FirstOrDefault();

            Debug.WriteLine(document.ToJson());
        }
    }
}
