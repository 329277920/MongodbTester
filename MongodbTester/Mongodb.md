# Mongodb #
>目录<br>
>[一、概念](1#)<br>
>[二、安装](2#)<br>
>[三、操作](3#)<br>
>[四、.net集成](4#)<br>

<h2 id="1#">一、概念</h2>
*mongodb*<br>
mongodb是一个开源的，文档型数据库，它提供高性能，高可用和自动扩展。在mongodb中，一条数据库记录就是一个类似于json的文档。每个文档可用嵌套子文档和数组。<br>
mongodb支持创建索引，以提升查询速度。<br>
mongodb支持多个数据集副本，以提供高可用。<br>
mongodb支持横向扩展，将分片数据部署到不同的机器上。在3.4以上版本，基于一个shard key来创建分区数据。
在3.4版本以后，mongodb支持为某个已存在的collection创建只读视图，方便查询。<br>


*StorageEngine*<br>
[WiredTiger Storage Engine](https://docs.mongodb.com/manual/core/wiredtiger/)<br>
[In-Memory Storage Engine](https://docs.mongodb.com/manual/core/inmemory/)（Enterprise version 3.2.6及以上版本）<br>

*database*<br>
包含文档集合。

	use MyDb #切换到某个数据库上下文，后续直接用db

*collection*<br>
存储文档，类似关系型数据库的表。

	db.myCollection.insert({ id:1 }) #创建数据、集合和一条数据。
    db.createCollection("myColleciton",[options]) #显示创建一个集合，并指定options，如大小，验证规则，存储引擎，排序规则等。

创建集合的options参考:[https://docs.mongodb.com/manual/reference/method/db.createCollection/#db.createCollection](https://docs.mongodb.com/manual/reference/method/db.createCollection/#db.createCollection)<br>

*view*<br>

*document*<br>
mongodb使用BSON存储记录。BSON相对于JSON,是按原生类型来存储数值，它以二进制序列化格式来存储文档，JSON是转换成字符串，并支持ByteArray。<br>
BSON支持的格式:[https://docs.mongodb.com/manual/reference/bson-types/](https://docs.mongodb.com/manual/reference/bson-types/)<br>

*objectid*<br>
参考:[https://docs.mongodb.com/manual/reference/bson-types/#objectid](https://docs.mongodb.com/manual/reference/bson-types/#objectid)<br>
用于唯一标识一个文档，类似于主键。它可以由客户端指定，缺省由系统生成一个12字节的objectid。在文档中表示为"_id"。
它由时间戳(Unix时间戳)、随机数、累加值组成。因此基于objectId的排序大致相当于按时间排序。**同时mongodb也会在_id列上加入索引。可以通过 db.[索引名称].getindexes() 查看某个集合的索引情况**。

<h2 id="2#">二、安装</h2>
参考:[https://docs.mongodb.com/manual/tutorial/install-mongodb-on-red-hat/](https://docs.mongodb.com/manual/tutorial/install-mongodb-on-red-hat/)<br>
环境:centos7<br>

**使用yum安装步骤如下**：<br>
1、添加yum源，创建"/etc/yum.repos.d/mongodb-org-4.0.repo"，内容如下:

	[mongodb-org-4.0]
	name=MongoDB Repository
	baseurl=https://repo.mongodb.org/yum/redhat/$releasever/mongodb-org/4.0/x86_64/
	gpgcheck=1
	enabled=1
	gpgkey=https://www.mongodb.org/static/pgp/server-4.0.asc

2、安装

	sudo yum install -y mongodb-org

3、校验
  
    systemctl status mongod #自动注册为服务，该命令应能看到 mongod的服务信息。

    rpm -qa |grep mongo #查找自动安装的所有mongodb相关的包

    rpm -ql mongodb-org-server #查看 mongodb-org-server 包信息

4、查看配置

    vi /etc/mongod.conf #绑定端口、日志文件路径、数据库路径等

4、启动服务

	systemctl start mongod

<h2 id="3#">三、操作</h2>


<h2 id="4#">四、.net集成</h2>
此处使用MongoDB.Driver做为客户端，在nuget中安装MongoDB.Driver。<br>
项目地址:[https://github.com/mongodb/mongo-csharp-driver](https://github.com/mongodb/mongo-csharp-driver)。<br>
文档地址:[http://mongodb.github.io/mongo-csharp-driver/2.7/getting_started/installation/](http://mongodb.github.io/mongo-csharp-driver/2.7/getting_started/installation/)。<br>
DML（数据操作）:[http://mongodb.github.io/mongo-csharp-driver/2.7/getting_started/quick_tour/](http://mongodb.github.io/mongo-csharp-driver/2.7/getting_started/quick_tour/)<br>
DDL（数据定义）:[http://mongodb.github.io/mongo-csharp-driver/2.7/getting_started/admin_quick_tour/](http://mongodb.github.io/mongo-csharp-driver/2.7/getting_started/admin_quick_tour/)<br>

*简单实例*

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




