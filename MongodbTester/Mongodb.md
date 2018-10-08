# Mongodb #
>目录<br>
>[一、安装](1#)

<h2 id="1#">一、安装</h2>
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

4、启动服务

	systemctl start mongod

