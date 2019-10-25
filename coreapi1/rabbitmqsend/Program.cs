using RabbitMQ.Client;
using System;

namespace rabbitmqsend
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("send!");

            //创建连接工厂
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = "kk",//用户名
                Password = "123",//密码
                HostName = "192.168.13.128",//rabbitmq ip
                Port = 5672
            };

            //创建连接
            var connection = factory.CreateConnection();
            //创建通道
            var channel = connection.CreateModel();
            //声明一个队列
            channel.QueueDeclare("hello", false, false, false, null);

            Console.WriteLine("\nRabbitMQ连接成功，请输入消息，输入exit退出！");

            string input;
            do
            {
                input = Console.ReadLine();

                var sendBytes = System.Text.Encoding.UTF8.GetBytes(input);
                //发布消息
                channel.BasicPublish("", "hello", null, sendBytes);
                Console.WriteLine("发送消息：" + input);

            } while (input.Trim().ToLower() != "q");
            channel.Close();
            connection.Close();
        }
    }
}
