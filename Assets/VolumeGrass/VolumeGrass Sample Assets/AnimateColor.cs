using UnityEngine;
using System.Collections;

public class AnimateColor : MonoBehaviour {
	public float speedX=0.1f;
	public float speedZ=0.1f;
	
	Vector4 cv;
	
	// Use this for initialization
	void Start () {
		if (renderer && renderer.material) {
			cv=renderer.material.GetVector("_coloring_noise_tiling");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (renderer && renderer.material) {
			cv.z+=speedX*Time.deltaTime;
			cv.w+=speedZ*Time.deltaTime;
			renderer.material.SetVector("_coloring_noise_tiling", cv);
		}
	}
}
