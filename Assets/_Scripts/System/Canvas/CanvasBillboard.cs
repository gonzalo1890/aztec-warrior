using UnityEngine;
using System.Collections;


public class CanvasBillboard : MonoBehaviour
{
	public Camera my_camera;
	public virtual void Start()
	{
		my_camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	public virtual void Update()
	{
		Quaternion cameraRot = Quaternion.Euler(my_camera.transform.rotation.eulerAngles.x, my_camera.transform.rotation.eulerAngles.y, 0f);
		transform.LookAt(transform.position + cameraRot * Vector3.back,	cameraRot * Vector3.down);
	}
}