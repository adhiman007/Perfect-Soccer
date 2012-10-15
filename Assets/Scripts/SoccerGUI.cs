using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SoccerGUI : MonoBehaviour 
{
	public Rect kickButtonRect;
	public Rect horizontalForceRect;
	public Rect verticalForceRect;
	public Rect kickForceRect;
	public Rect horizontalForceTextRect;
	public Rect verticalForceTextRect;
	public Rect kickForceTextRect;
	public GUIStyle textStyle;
	public Rect resetRect;
	public Rect xTorqueRect;
	public Rect yTorqueRect;
	public Rect zTorqueRect;
	public Rect xTorqueTextRect;
	public Rect yTorqueTextRect;
	public Rect zTorqueTextRect;
	public Ball ballScript;
	public Rect windowRect;
	public TrailRenderer trailRenderer;
	public Rect showWindowRect;
	public MouseOrbit mouseOrbit;
	public GameObject crowdAudio;
	public Rect playSoundRect;
	
	private float forwardKickForce = 0.0f;
	private float horizontalKickForce = 0.0f;
	private float verticalKickForce = 0.0f;
	private float xTorque = 0.0f;
	private float yTorque = 0.0f;
	private float zTorque = 0.0f;
	private bool showWindow = true;
	private bool playSound = true;
	
	public void OnGUI()
	{
		showWindow = GUI.Toggle(showWindowRect, showWindow, "Show Window");
		playSound = GUI.Toggle(playSoundRect, playSound, "Play Sound");
		crowdAudio.active = playSound;
		
		if(showWindow)
		{
			windowRect = GUI.Window(0, windowRect, drawWindow, "Kick Controls");
		}
			
	}
	
	public void drawWindow(int windowID)
	{
		GUI.DragWindow(new Rect(0.0f, 0.0f, windowRect.width, 20.0f));
		GUILayout.BeginHorizontal();	
			GUILayout.BeginVertical();
				if(GUILayout.Button("Kick"))
				{
					ballScript.kickBall(new Vector3(horizontalKickForce, verticalKickForce, forwardKickForce), 
										new Vector3(xTorque, yTorque, zTorque));
					trailRenderer.time = 5.0f;
				}
				
				if(GUILayout.Button("Reset"))
				{
					trailRenderer.time = 0.0f;
					horizontalKickForce = 0.0f;
					verticalKickForce = 0.0f;
					forwardKickForce = 0.0f;
					xTorque = 0.0f;
					yTorque = 0.0f;
					zTorque = 0.0f;
					ballScript.resetBall();
				}
			GUILayout.EndVertical();
		
			GUILayout.BeginVertical();
				GUILayout.Label("Horizontal Force: " + horizontalKickForce, textStyle);
				horizontalKickForce = GUILayout.HorizontalSlider(horizontalKickForce, -5.0f, 5.0f);
				
				GUILayout.Label("Vertical Force: " + verticalKickForce, textStyle);
				verticalKickForce = GUILayout.HorizontalSlider(verticalKickForce, 0.0f, 10.0f);
				
				GUILayout.Label("Forward Force: " + forwardKickForce, textStyle);
				forwardKickForce = GUILayout.HorizontalSlider(forwardKickForce, 0.0f, 10.0f);
				
				GUILayout.Label("X Torque: " + xTorque, textStyle);
				xTorque = GUILayout.HorizontalSlider(xTorque, -5.0f, 5.0f);
				
				GUILayout.Label("Y Torque: " + yTorque, textStyle);
				yTorque = GUILayout.HorizontalSlider(yTorque, -5.0f, 5.0f);
				
				GUILayout.Label("Z Torque: " + zTorque, textStyle);
				zTorque = GUILayout.HorizontalSlider(zTorque, -5.0f, 5.0f);
		
				GUILayout.Label("Zoom", textStyle);
				mouseOrbit.distance = GUILayout.HorizontalSlider(mouseOrbit.distance, 1.0f, 10.0f);
		
			GUILayout.EndVertical();
			trailRenderer.enabled = GUILayout.Toggle(trailRenderer.enabled, "Enable Trail");
		GUILayout.EndHorizontal();		
	}

}
