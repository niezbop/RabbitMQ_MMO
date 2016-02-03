using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour {
	public GameObject characterModel;
	public MovementController mainPlayer;

	private PositionQueue pq = PositionQueue.Instance;
	private List<string> playerIds;

	// Use this for initialization
	void Start ()
	{
		playerIds = new List<string> ();
		playerIds.Add (mainPlayer.id);
    }
	
	// Update is called once per frame
	void Update ()
	{
		if (playerIds.Count < pq.getAllPosition ().Count)
		{
			// Add all the new player ids and create them
			foreach (PlayerPosition pos in pq.getAllPosition().Values)
			{
				if (!playerIds.Contains(pos.id))
				{
					playerIds.Add (pos.id);
					this.CreatePlayer (pos);
				}
			}
		}
	}

	public void CreatePlayer(PlayerPosition pos)
    {
		GameObject obj = (GameObject) Instantiate(characterModel, new Vector3(pos.x,0,pos.y), Quaternion.identity);

		Character suppo = obj.GetComponent<Character> ();
		suppo.id = pos.id;
		suppo.lastPosition = pos;
    }
}
