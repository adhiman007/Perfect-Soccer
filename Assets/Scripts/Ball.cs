using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	public GUIText goalText;
	
	private bool ballKicked = false;
	private bool inAir = false;
	private Vector3 originalPosition;
	private bool inGoal;
	
	
	public void Start()
	{
		originalPosition = transform.position;
	}
	
	public void kickBall(Vector3 kickForce, Vector3 kickTorque)
	{
		ballKicked = true;	
		rigidbody.isKinematic = false;
		rigidbody.AddForce(kickForce, ForceMode.Impulse);
		rigidbody.AddTorque(kickTorque);
	}
	
	public void resetBall()
	{
		ballKicked = false;
		rigidbody.isKinematic = true;
		transform.position = originalPosition;
		goalText.enabled = false;
	}
	
	public void FixedUpdate()
	{
		if(inAir && ballKicked)
		{
			liftForce();
			airDragForce();
		}
	}
	
	private void liftForce()
	{
		rigidbody.AddForce(0.0025f * Vector3.Cross(rigidbody.angularVelocity, rigidbody.velocity), 
						   ForceMode.Force);
	}
	
	private void airDragForce()
	{
		rigidbody.AddForce(-rigidbody.velocity.normalized * (0.5f * 1.2041f * Mathf.Pow(rigidbody.velocity.magnitude, 2.0f) * 0.226f * 0.25f), 
						   ForceMode.Force);
	}
	
	public void OnCollisionExit(Collision collision)
	{
		if(collision.collider.CompareTag("Terrain"))
		{
			inAir = true;
		}
	}
	
	public void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.CompareTag("Terrain"))
		{
			if(inGoal)
			{
				goal();
			}
			inAir = false;
		}
	}
	
	public void OnTriggerEnter(Collider collider)
	{
		if(collider.CompareTag("Goal"))
		{
			inGoal = true;
			if(!inAir)
			{
				goal();
			}
		}
	}
	
	public void OnTriggerExit(Collider collider)
	{
		if(collider.CompareTag("Goal"))
		{
			inGoal = false;
		}
	}
	
	private void goal()
	{
		goalText.enabled = true;
	}
}
