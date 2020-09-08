using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {
	public Transform target;
	public float distance = 10.0f;
	public float height = 10.0f;
	public float damping = 5.0f;
	public bool smoothRotation = true;
	public float rotationDamping = 10.0f;
	
	void FixedUpdate () {
		Vector3 wantedPosition = target.TransformPoint(0, height, -distance);
        Vector3 test;
        test.x = 0;
        test.y = 0.5f;
        test.z = 0;
		transform.position = Vector3.Lerp (transform.position-test, wantedPosition, Time.deltaTime * damping);
		
		if (smoothRotation) {
			Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
			transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
		}
		
		else transform.LookAt (target, target.up);
	}

	void Update() {
		if ( Input.GetKeyDown(KeyCode.Escape  )) {Application.Quit();}
	}
}