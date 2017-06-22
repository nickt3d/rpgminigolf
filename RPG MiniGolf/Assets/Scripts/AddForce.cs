using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour {
	
	float shotPower = 1000;
	float maxShotPower = 2000; //1000 = ~33 units
	Vector3 direction = new Vector3 (0, 0, 1);

	Rigidbody rb;

	void Start(){
		rb = GetComponent<Rigidbody>();
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			rb.AddForce(direction * shotPower);
		}
	}

	void setDirection(Vector3 dir){
		direction = dir;
	}

	void setPower(float power){
		if(power > maxShotPower){
			power = maxShotPower;
		}

		shotPower = power;
	}
}
