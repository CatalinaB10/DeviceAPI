using DeviceAPI.Models;

using RabbitMQ.Client;
using System.Diagnostics;
using System.Text;


namespace DeviceAPI.Services;
public class QueueService : IQueueService
{ 
    public void SendToQueue(Device device, string action)
    {
        ConnectionFactory factory = new();
        factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
        factory.ClientProvidedName = "Device Sender";

        IConnection cnn = factory.CreateConnection();
        IModel channel = cnn.CreateModel();

        //string exchangeName = "device_topic";
        //string routingkey = "device." + action;
        string queueName = "DeviceTopicQueue";

        //channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
        channel.QueueDeclare(queueName, exclusive: false, autoDelete: false);
        //channel.QueueBind(queueName, exchangeName, routingkey, null);

        var json = Newtonsoft.Json.JsonConvert.SerializeObject(device);
        var body = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish(exchange: "", routingKey: "DeviceTopicQueue", body: body);

       
    }
}
