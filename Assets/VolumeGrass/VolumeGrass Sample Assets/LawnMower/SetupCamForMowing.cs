using UnityEngine;
using System.Collections;

public class SetupCamForMowing : MonoBehaviour {
	
	private bool clearFlagsInited=false;
	public VolumeGrass lawnObject;
	
	private bool res_changed=true;
	private float res_w=0,res_h=0;
		
	void OnPostRender() {
		if (!clearFlagsInited) {
			clearFlagsInited=true;
			if (lawnObject && camera) {
				GameObject go=lawnObject.gameObject;
				if (go.renderer && go.renderer.material) {
					Vector4 tiling=go.renderer.material.GetVector("_auxtex_tiling");
					tiling.x*=camera.rect.width;
					tiling.y*=camera.rect.height;
					go.renderer.material.SetVector("_auxtex_tiling", tiling);
				}
			}
		}
		if (res_changed) {
			if (camera!=null) {
				camera.clearFlags=CameraClearFlags.Nothing;
			}
			res_changed=false;
		}
	}
	
	void Update() {
		if ((Screen.width!=res_w) || (Screen.height!=res_h)) {
			res_changed=true;
			res_w=Screen.width;
			res_h=Screen.height;			
			if (camera!=null) {
				camera.clearFlags=CameraClearFlags.SolidColor;
			}
		}		
	}
}