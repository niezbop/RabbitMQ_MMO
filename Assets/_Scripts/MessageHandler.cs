using UnityEngine;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Collections.Generic;

public class MessageHandler {

	private ConnectionFactory cf = new ConnectionFactory();
	private readonly string uri = "amqp://guest:guest@mbpmx.fr:5672/";
	private IConnection connection;
	private IModel channel;
	private string qName;

	public static readonly string POSITION_EXCHANGE_NAME = "positions_topic";
	public static readonly string POSITION_TAG_NAME = "position";
	public static readonly string JOIN_Q_NAME = "join";

	public MessageHandler() {
		cf.Uri = uri;
        cf.SocketFactory = new ConnectionFactory.ObtainSocket(CustomSocketFactory.GetSocket);
        connection = cf.CreateConnection ();
		channel = connection.CreateModel ();

		channel.ExchangeDeclare(
			exchange: POSITION_EXCHANGE_NAME,
			type: "topic",
			durable: true
		);

		qName = channel.QueueDeclare();

		channel.QueueBind(
			queue: qName,
			exchange: POSITION_EXCHANGE_NAME,
			routingKey: POSITION_TAG_NAME
		);
	}

	public void SendPosition(PlayerPosition position) {
		channel.BasicPublish(exchange: "",
			routingKey: POSITION_TAG_NAME,
			basicProperties: null,
			body: position.ToMessageData());
	}

	public void SendMessage(string message) {
		var body = Encoding.UTF8.GetBytes(message);

		channel.BasicPublish(exchange: "",
			routingKey: POSITION_TAG_NAME,
			basicProperties: null,
			body: body);
	}

	// called at the end of the PositionQueue constructor
	// and after the constructor, as a yield return
	// to start listening for incoming messages.
	// Accept callback method?
	public System.Collections.IEnumerator startListening()
	{
		Debug.Log(" [*] Waiting for logs.");

		var consumer = new EventingBasicConsumer(channel);
		consumer.Received += (model, ea) =>
		{
			var body = ea.Body;
			var message = Encoding.UTF8.GetString(body);
			Debug.Log(" [x] {0}" + message);
		};

		channel.BasicConsume(
			queue: qName,
			noAck: true,
			consumer: consumer
		);

		Debug.Log(" Press [enter] to exit.");
		yield return null;
	}
}
