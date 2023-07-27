using UnityEngine;

public class LookAt : MonoBehaviour
{
	[Header("LINKS")]
	public RectTransform obj;

	public void Awake()
	{
		//obj.LookAt(Camera.main.transform, Vector3.forward);
		obj.forward = Camera.main.transform.position - obj.transform.position;
	}
}
