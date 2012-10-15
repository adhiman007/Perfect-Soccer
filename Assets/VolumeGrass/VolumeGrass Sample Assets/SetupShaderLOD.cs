using UnityEngine;
using System.Collections;

public class SetupShaderLOD : MonoBehaviour {
	
	public int ShaderLOD=700;
	
	// switch to subshader dedicated for this demo scene
	void Start() {
		if (renderer && renderer.material) {
			renderer.material.shader.maximumLOD=ShaderLOD;
		}
	}
}
