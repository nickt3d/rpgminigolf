using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDebug : MonoBehaviour {
	
	private string posDebugString;
	bool drawDebug1 = false;
	bool drawDebug2 = false;

	bool onAndroid = false;

	void Start(){
		Input.simulateMouseWithTouches = true;

		
	}

	// Update is called once per frame
	void Update () {
		posDebugString = "CameraPos | X:" + transform.position.x.ToString() + " | Y:" + transform.position.y.ToString() + " | Z:" + transform.position.z.ToString();
		if(Input.GetMouseButton(0)){
			drawDebug2 = true;
		} else {
			drawDebug2 = false;
		}

		if(onAndroid){
			Touch touch = Input.GetTouch(0);
		
			if(touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled){
				drawDebug1 = true;
			} else {
				drawDebug1 = false;
			}
		}
	}

	void OnGUI(){
		GUI.Box(new Rect (10, 10, 500, 20), posDebugString);
		if(drawDebug1){
			GUI.Box(new Rect (10, 20, 500, 20), "Touched the screen...");
		}
		if(drawDebug2){
			GUI.Box(new Rect (10, 20, 500, 20), "Simulating mouse...");
		}
	}
}
