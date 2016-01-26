using UnityEngine;
using System.Text;
using System.Collections.Generic;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class PositionQueue : MonoBehaviour
{
	private Dictionary<string, List<PlayerPosition>> positions;
	private MessageHandler mh;

	public WorldManager wm;

	public System.Collections.IEnumerator start()
	{
		this.positions = new Dictionary<string, List<PlayerPosition>>();
		mh = new MessageHandler ();
		yield return StartCoroutine(mh.startListening ());
	}

	public void AddPosition(byte[] body)
	{
		PlayerPosition position = PlayerPosition.parseData(body);
		if (position != null && position.id != null) {
			if (positions.ContainsKey (position.id)) {
				positions [position.id].Add (position);
			} else {
				positions [position.id] = new List<PlayerPosition> ();
				positions [position.id].Add (position);
				wm.CreatePlayer ();
			}
		} else {
			Debug.Log ("Message etrange recu: " + body.ToString());
		}
	}

	// Return the dictionary of list of position of all player
	public Dictionary<string, List<PlayerPosition>> getAllPosition()
	{
		return positions;
	}

	// Return the list of position for the given player
	public List<PlayerPosition> getPositionForPlayer(string playerId)
	{
		if (!positions.ContainsKey (playerId))
			return null;

		return positions [playerId];
	}
}

