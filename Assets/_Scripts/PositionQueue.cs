using UnityEngine;
using System.Text;
using System.Collections.Generic;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class PositionQueue
{
	private static PositionQueue instance;

	private Dictionary<string, PlayerPosition> positions;

	private PositionQueue()
	{
		positions = new Dictionary<string, PlayerPosition>();
	}

	public static PositionQueue Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new PositionQueue();
			}

			return instance;
		}
	}

	public void AddPosition(byte[] body)
	{
		PlayerPosition position = PlayerPosition.parseData(body);

		if (position != null && position.id != null)
		{
			positions [position.id] = position;
		}
		else
		{
			Debug.Log ("Message etrange recu: " + body.ToString());
		}
	}

	public void AddPosition(PlayerPosition position)
	{

		if (position != null && position.id != null && position.id != "")
		{
			positions [position.id] = position;
		}
	}

	// Return the dictionary of list of position of all player
	public Dictionary<string, PlayerPosition> getAllPosition()
	{
		return positions;
	}

	// Return the list of position for the given player
	public PlayerPosition getPositionForPlayer(string playerId)
	{
		if (!positions.ContainsKey (playerId))
			return null;

		return positions [playerId];
	}
}

