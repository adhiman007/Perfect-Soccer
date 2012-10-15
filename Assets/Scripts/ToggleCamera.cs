using UnityEngine;
using System.Collections;

public class ToggleCamera : MonoBehaviour 
{
	public GameObject mainCamera;
	public GameObject freeCamera;
	public Rect cameraRect;
	
	public void OnGUI()
	{
		freeCamera.active = GUI.Toggle(cameraRect, freeCamera.active, "Enable Free Camera");
		mainCamera.active = (!freeCamera.active);
		mainCamera.camera.enabled = !freeCamera.active;
	}
}
