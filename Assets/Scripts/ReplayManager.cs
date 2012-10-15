using UnityEngine;
using System.Collections;

public class ReplayManager : MonoBehaviour 
{

	public Transform[] replayObjects;
	//public bool record = false;
	public bool playRecording = false;
	public bool pausePlayback = false;
	public bool reverseRecording = false;
	public int recordSpeed = 30;
	public int playbackSpeed = 30;
	public Rect replayScrubRect;
	public Rect windowRect;
	
	private ArrayList[] savedReplay;
	private int frameCount = 0;
	private bool isRecording = false;
	private int playbackPosition;
	private bool isPlaying = false;
	private int recordingLength = 0;
	
	public void Start()
	{
		savedReplay = new ArrayList[replayObjects.Length];
		
		for(int i = 0; i < savedReplay.Length; i++)
		{
			savedReplay[i] = new ArrayList();
		}
	}
	
	public void OnGUI()
	{
		
		windowRect = GUI.Window(1, windowRect, drawReplayWindow, "Replay Controls");
			
	}
	
	public void drawReplayWindow(int windowID)
	{
		GUI.DragWindow(new Rect(0.0f, 0.0f, windowRect.width, 20.0f));
		
		Replay tempReplay;
		GUILayout.BeginVertical();
			GUILayout.Toggle(isRecording, "Recording Replay");
			if(GUILayout.Button("Record"))
			{
				if(isRecording)
				{
					isRecording = false;
				}
				else
				{
					isRecording = true;
					StartCoroutine(recordReplay());
				}
			}
			
			if(!isRecording)
			{
				if(GUILayout.Button("Play"))
				{
					stopPlayBacks();
					StartCoroutine(playReplay());
				}
				
				if(GUILayout.Button("Reverse Playback"))
				{
					stopPlayBacks();
					StartCoroutine(reverseReplay());
				}
		
				pausePlayback = GUILayout.Toggle(pausePlayback, "Pause");
			
				if(pausePlayback)
				{
					GUILayout.Label("Replay Position");
					playbackPosition = (int)GUILayout.HorizontalSlider(playbackPosition, 0, recordingLength - 1);
					
					for(int j = 0; j < replayObjects.Length; j++)
					{
						tempReplay = (Replay)savedReplay[j][playbackPosition];
						replayObjects[j].position = tempReplay.position;
						replayObjects[j].rotation = tempReplay.rotation;
					}
				}
			}
		GUILayout.EndVertical();
	}
	
	public IEnumerator recordReplay()
	{		
		for(int i = 0; i < savedReplay.Length; i++)
		{
			savedReplay[i] = new ArrayList();
		}
		
		recordingLength = 0;
		Debug.Log("record");
				
		while(isRecording)
		{
			for(int j = 0; j < replayObjects.Length; j++)
			{
				savedReplay[j].Add(new Replay(replayObjects[j].position, replayObjects[j].rotation));
				++frameCount;
			}
			
			++recordingLength;
			yield return new WaitForSeconds(1.0f/recordSpeed);
		}
	}
	
	
	public void stopPlayBacks()
	{
		StopCoroutine("playReplay");
		StopCoroutine("reverseReplay");
	}
	
	
	public IEnumerator playReplay()
	{
		Debug.Log("Play");
		Replay tempReplay;
		
		
		foreach(Transform t in replayObjects)
		{
			if(t.rigidbody != null)
			{
				t.rigidbody.isKinematic = true;
			}
			if(t.animation != null)
			{
				t.animation.Stop();
			}
		}
		
		if(!isPlaying)
		{
			playbackPosition = 0;
			isPlaying = true;
		}
		
		while(playbackPosition < recordingLength)
		{
			for(int j = 0; j < replayObjects.Length; j++)
			{
				tempReplay = (Replay)savedReplay[j][playbackPosition];
				replayObjects[j].position = tempReplay.position;
				replayObjects[j].rotation = tempReplay.rotation;
				while(pausePlayback)
				{
					yield return new WaitForEndOfFrame();
				}	
			}
			++playbackPosition;
			yield return new WaitForSeconds(1.0f / playbackSpeed);
		}
		
		isPlaying = false;
		stopPlayBacks();
	}
	
	public IEnumerator reverseReplay()
	{
		Debug.Log("Reverse");
		Replay tempReplay;
		
		
		foreach(Transform t in replayObjects)
		{
			if(t.rigidbody != null)
			{
				t.rigidbody.isKinematic = true;
			}
			if(t.animation != null)
			{
				t.animation.Stop();
			}
		}
		
		if(!isPlaying)
		{
			playbackPosition = recordingLength - 1;
			isPlaying = true;
		}
		
		while(playbackPosition >= 0)
		{
			for(int j = 0; j < replayObjects.Length; j++)
			{
				tempReplay = (Replay)savedReplay[j][playbackPosition];
				replayObjects[j].position = tempReplay.position;
				replayObjects[j].rotation = tempReplay.rotation;
				while(pausePlayback)
				{
					yield return new WaitForEndOfFrame();
				}	
			}
			
			if(playbackPosition > 0)
			{
				--playbackPosition;
			}
			
			yield return new WaitForSeconds(1.0f / playbackSpeed);
		}
		
		isPlaying = false;
		stopPlayBacks();
	}
	
	public class Replay
	{
		public Vector3 position;
		public Quaternion rotation;
		
		public Replay(Vector3 position, Quaternion rotation)
		{
			this.position = position;
			this.rotation = rotation;
		}		
	}
}


