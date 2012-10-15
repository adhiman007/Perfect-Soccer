using UnityEngine;
using System.Collections;


public class CameraControlScript : MonoBehaviour 
{
	public float speed;

	
	public void Start() {

	}
	
	public void Update() {

		transform.position += transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime; 
		transform.position += transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;

//		rigidbody.velocity = Vector3.zero;
	}
}
