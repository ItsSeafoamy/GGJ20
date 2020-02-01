using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public bool up = false;
	public float speed;
	public float high, low;

	private void Update() {
		if (up) {
			transform.position += new Vector3(0, speed*Time.deltaTime);

			if (transform.position.y > high) up = false;
		} else {
			transform.position -= new Vector3(0, speed*Time.deltaTime);

			if (transform.position.y < low) up = true;
		}
	}
}