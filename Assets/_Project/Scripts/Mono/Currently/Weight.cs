using UnityEngine;

public class Weight : MonoBehaviour {

	public float distanceFromChainEnd = 0.6f;

	HingeJoint2D joint;

	[HideInInspector] public bool isLinkBroken = false;

	private void Start()
    {
		GetComponent<SpriteRenderer>().enabled = false;
	}

	public bool IsConnectionRopeEndBroken()
    {
		return isLinkBroken;
	}		

    public void ConnectRopeEnd(Rigidbody2D endRB)
	{
		joint = gameObject.AddComponent<HingeJoint2D>();
		joint.autoConfigureConnectedAnchor = false;
		joint.connectedBody = endRB;
		joint.anchor = Vector2.zero;
		joint.connectedAnchor = new Vector2(0f, -distanceFromChainEnd);
	}
}
