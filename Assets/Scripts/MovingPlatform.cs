using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public bool up = false;
	public bool right = false;

	public float vertSpeed, horizSpeed;
	public float high, low, leftMost, rightMost;

	private void Update() {
		if (up) {
			transform.position += new Vector3(0, vertSpeed*Time.deltaTime);

			if (transform.position.y > high) up = false;
		} else {
			transform.position -= new Vector3(0, vertSpeed*Time.deltaTime);

			if (transform.position.y < low) up = true;
		}

		if (right) {
			transform.position += new Vector3(horizSpeed*Time.deltaTime, 0);

			if (transform.position.x > rightMost) right = false;
		} else {
			transform.position -= new Vector3(horizSpeed*Time.deltaTime, 0);

			if (transform.position.x < leftMost) right = true;
		}
	}
}