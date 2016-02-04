using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	public string id;

	private MessageHandler mh;
    private Vector3 moveDirection = Vector3.zero;
	private PositionQueue posQ = PositionQueue.Instance;
	private PlayerPosition lastPos;

	IEnumerator Start()
	{
		Application.runInBackground = true;
		id = System.Guid.NewGuid().ToString();
		lastPos = new PlayerPosition (id, 0, 0, 0);
		posQ.AddPosition (new PlayerPosition (id, 0, 0, 0));
		mh = new MessageHandler ();
		yield return StartCoroutine(mh.startListening ());
	}

	void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

		// Send position
		PlayerPosition pos = new PlayerPosition(id, transform.position.x, transform.position.y, transform.position.z);
		if (pos.x != lastPos.x || pos.y != lastPos.y || pos.z != lastPos.z) {
			lastPos = pos;
			mh.SendPosition (pos);
		}
    }
}
