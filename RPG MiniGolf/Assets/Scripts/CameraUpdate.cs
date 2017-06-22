using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUpdate : MonoBehaviour {

	private float zoomDistance = 2.5f;
	private float minDistance = 0.5f;
	private float maxDistance = 6f; 
	private float camDistance = 5f;
	private float camHeight = 5f;

	public float zoomSpeed = 0.05f;
	public float moveSpeed = 0.05f;

	private float pointerX;
    private float pointerY;
	private float deltaPointPosX;
	private float deltaPointPosY;

	public Transform target;
	GameObject focus;
	Vector3 newTargetPos;

	RaycastHit hit;
	GameObject focusObj;

	bool shooting;

	void Start(){
		 focus = new GameObject();
	}
	// Update is called once per frame
	void Update () {		
	
	/**************** Touch Input *****************/
	#if UNITY_ANDROID
		/*** Camera Zoom ***/
		if(Input.touchCount == 2){
			
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			zoomDistance += deltaMagnitudeDiff/2 * zoomSpeed;

		/*** Camera Movement ***/
		}
		
		if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved){
			//TODO: convert the touch and drag positions to world space and move the camera target/camera based on this
         	pointerX = Input.touches[0].deltaPosition.x * moveSpeed;
         	pointerY = Input.touches[0].deltaPosition.y * moveSpeed * -1;

		} else {
			pointerX = 0;
			pointerY = 0;
		}

		/*** Lock onto the tapped object ***/

		#endif

	/**************** Mouse Input ******************/
	#if UNITY_EDITOR_WIN
		if(Input.GetAxis("Mouse ScrollWheel") != 0){	
			zoomDistance += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
		}

		/* Cast a ray on the mouse click and find the object hit*/
		if(Input.GetMouseButtonDown(0)){
			focusObj = CastRay();	
		}

		if(Input.GetMouseButton(0)){

			//Move Camera
			pointerX = Input.GetAxis("Mouse X");
			pointerY = Input.GetAxis("Mouse Y");
			newTargetPos = new Vector3 (target.position.x + pointerX * -1, 0, target.position.z - pointerY);
			
		} else {
			if(target.position == newTargetPos){
				pointerX = 0;
				pointerY = 0;
				newTargetPos = new Vector3 (target.position.x + pointerX * -1, 0, target.position.z - pointerY);
			}
		}

		if(Input.GetMouseButtonUp(0)){
			//Check if the mouse release in the same object clicked
			if(focusObj.tag == "ball") {
				newTargetPos = new Vector3 (focusObj.transform.position.x, 0, focusObj.transform.position.z);
			}
		}

	#endif	

		/********Clamp Zoom Distance************/
		if(zoomDistance > maxDistance){
			zoomDistance = maxDistance;
		}
		if(zoomDistance < minDistance){
			zoomDistance = minDistance;
		}

		/*** Move camera target position ****/
		target.transform.position = Vector3.Lerp(target.position, newTargetPos, moveSpeed);


	}


	void LateUpdate() {

		/*** Set the camera position ***/
		transform.position = new Vector3(target.position.x, target.position.y + camHeight  * zoomDistance, target.position.z - camDistance  * zoomDistance);
		transform.LookAt(target);
	}

	/*** Check for focus object ***/
	GameObject CastRay(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast (ray.origin, ray.direction*5000, out hit, 25)) {
			Debug.DrawRay(ray.origin, ray.direction*5000, Color.blue, 0.2f);
			return hit.collider.gameObject;
		} else {
			return focus;
		}
		
	}
	
}

