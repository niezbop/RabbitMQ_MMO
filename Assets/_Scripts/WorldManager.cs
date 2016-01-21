using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {
    public Transform player;
	// Use this for initialization
	void Start () {
        Instantiate(player, new Vector3(10, 0, 10), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CreatePlayer()
    {
        Instantiate(player, new Vector3(0,0,0), Quaternion.identity);

    }
}
