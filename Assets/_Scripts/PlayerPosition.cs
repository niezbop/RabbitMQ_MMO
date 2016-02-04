using System.Text;

public class PlayerPosition
{
	public string id;
	public float x;
	public float y;
	public float z;

	public PlayerPosition(string id, float x, float y, float z)
	{
		this.id = id;
		this.x = x;
		this.y = y;
		this.z = z;
	}

	static public PlayerPosition parseData(byte[] messageData)
	{
		string message = Encoding.UTF8.GetString (messageData);
		string[] pieces = message.Split(',');

		if (pieces.Length != 4)
		{
			return null;
		}

		float x = float.Parse (pieces [1]);
		float y = float.Parse (pieces [2]);
		float z = float.Parse (pieces [3]);

		return new PlayerPosition (pieces [0], x, y, z);
	}

	// Used to convert position to string 
	public byte[] ToMessageData()
	{
		return Encoding.UTF8.GetBytes( id + "," + x.ToString () + "," + y.ToString () + "," + z.ToString ());
	}
}
