using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;

	public string id;
	public PlayerPosition lastPosition;

	private Vector3 moveDirection = Vector3.zero;
	private PositionQueue posQ = PositionQueue.Instance;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Here, you update the position.
		PlayerPosition position = posQ.getPositionForPlayer(id);

		if ( lastPosition != null && ( lastPosition.x != position.x || lastPosition.y != position.y || lastPosition.z != position.z ) )
		{
			// move
			CharacterController controller = GetComponent<CharacterController>();

			if (controller.isGrounded)
			{
				moveDirection = new Vector3(position.x - transform.position.x, position.y-transform.position.y, position.z - transform.position.z);
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= speed;

			}
			moveDirection.y -= gravity * Time.deltaTime;

			controller.Move(moveDirection * Time.deltaTime);

			lastPosition = new PlayerPosition(id, transform.position.x, transform.position.y, transform.position.z);
		}
	}
}
