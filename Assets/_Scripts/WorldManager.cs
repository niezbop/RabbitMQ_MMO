using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {
    public Transform characterModel;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CreatePlayer()
    {
        Instantiate(characterModel, new Vector3(0,0,0), Quaternion.identity);
    }
}
