using UnityEngine;
using System.Collections;
using RabbitMQ.Client;
using System.Text;

public class Messages : MonoBehaviour
{

	// Use this for initialization
	MessageHandler mh;

	IEnumerator Start ()
	{
		mh = new MessageHandler ();
		//mh.SendMessage ("Coucou, je start");
		yield return StartCoroutine(mh.startListening ());
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
