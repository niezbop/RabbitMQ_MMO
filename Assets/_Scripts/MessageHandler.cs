using UnityEngine;
using System.Collections;
using RabbitMQ.Client;
using System.Text;

public class MessageHandler {

	private ConnectionFactory cf = new ConnectionFactory();
	private string uri = "amqp://guest:guest@mbpmx.fr:5672/";
	private IConnection connection;
	private IModel channel;

	public MessageHandler() {
		cf.Uri = uri;
		connection = cf.CreateConnection ();
		channel = connection.CreateModel ();
		channel.QueueDeclare(queue: "hello",
			durable: false,
			exclusive: false,
			autoDelete: false,
			arguments: null);
	}

	public void SendMessage(string message) {
		var body = Encoding.UTF8.GetBytes(message);

		channel.BasicPublish(exchange: "",
			routingKey: "hello",
			basicProperties: null,
			body: body);
	}
}
