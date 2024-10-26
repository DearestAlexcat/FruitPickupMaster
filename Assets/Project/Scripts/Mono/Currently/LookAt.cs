using UnityEngine;

public class LookAt : MonoBehaviour
{
	public RectTransform obj;

	public void Awake()
	{
		obj.forward = Camera.main.transform.position - obj.transform.position;
	}
}
