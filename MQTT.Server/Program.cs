using MQTTnet;
using MQTTnet.Core.Server;
using System;
using System.Threading;

namespace MQTT.Server
{
    class Program
    {
        static void Main(string[] args)
        {
           
            new Thread(StartMQTTServer).Start();
        }

        private static void StartMQTTServer()
        {
            try
            {
                var mqttFactory = new MqttFactory();
                var server = mqttFactory.CreateMqttServer();
                server.ApplicationMessageReceived += Server_ApplicationMessageReceived;
                server.ClientConnected += Server_ClientConnected;
                server.ClientDisconnected += Server_ClientDisconnected;
                var mqOptions = new MqttServerOptions();
                 server.StartAsync(mqOptions);
                Console.WriteLine("服务成功开启");
                Console.ReadLine();
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }        
        }

        private static void Server_ClientDisconnected(object sender, MqttClientDisconnectedEventArgs e)
        {
            Console.WriteLine("client {0} connected",e.Client.ClientId);
        }

        private static void Server_ClientConnected(object sender, MqttClientConnectedEventArgs e)
        {
            Console.WriteLine("client {0} disconnected", e.Client.ClientId);
        }

        private static void Server_ApplicationMessageReceived(object sender, MQTTnet.Core.MqttApplicationMessageReceivedEventArgs e)
        {
            Console.WriteLine("client {0} send message {1}",e.ClientId, e.ApplicationMessage );
        }
    }
}
