using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MQTTnet.Core.Client;
using MQTTnet;
using MQTTnet.Core;
using MQTTnet.Core.Protocol;

namespace MQTT.Client
{
    public partial class Form1 : Form
    {
        public static IMqttClient mqttClient = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        { 
            try
            {
                var mqttFactory = new MqttFactory();
                mqttClient = mqttFactory.CreateMqttClient() as MqttClient;

                mqttClient.ApplicationMessageReceived += MqttClient_ApplicationMessageReceived;
                mqttClient.Connected += MqttClient_Connected;
                mqttClient.Disconnected += MqttClient_Disconnected;
                //MqttClientTcpOptions
                var clientOptions = new MqttClientOptions() { ClientId = Guid.NewGuid().ToString().Substring(5) };
                mqttClient.ConnectAsync(clientOptions);
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }

        private void MqttClient_Disconnected(object sender, MqttClientDisconnectedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MqttClient_Connected(object sender, MqttClientConnectedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MqttClient_ApplicationMessageReceived(object sender, MQTTnet.Core.MqttApplicationMessageReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnSubscrible_Click(object sender, EventArgs e)
        {
            mqttClient.SubscribeAsync(new List<TopicFilter> { new TopicFilter("test",MqttQualityOfServiceLevel.AtMostOnce)});
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            var message = new MqttApplicationMessage() { Topic = "test", Payload = Encoding.UTF8.GetBytes("test"),Retain = false ,QualityOfServiceLevel = MqttQualityOfServiceLevel.AtMostOnce};
            mqttClient.PublishAsync(message);
        }
    }
}
