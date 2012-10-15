using UnityEngine;
using System.Collections;

public class Webcam : MonoBehaviour
{ 
	public float waitTime = 1.0f;
	public Renderer camRenderer;
	public int cameraIndex = 0;
	
	private WebCamTexture webCamTexture;
	// Use this for initialization
	void OnEnable () 
	{
		displayWebcamDetails();
		createWebCamTexture();
	}
	
	private void displayWebcamDetails()
	{
		WebCamDevice[] devices;
		devices = WebCamTexture.devices;
		
		Debug.Log(devices.Length);
		
		foreach(WebCamDevice cam in devices)
		{
			Debug.Log(cam.name);
		}
	}
	
	private void createWebCamTexture()
	{
		webCamTexture = new WebCamTexture(WebCamTexture.devices[cameraIndex].name, 1024, 768, 60);
		camRenderer.sharedMaterial.mainTexture = webCamTexture;
		webCamTexture.Play();
	}
	
	public void OnDisable()
	{
		Debug.Log("Stop");
		webCamTexture.Stop();
	}
}
