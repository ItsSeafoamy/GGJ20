using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {

	public float force;

	private void OnCollisionEnter2D(Collision2D collision) {
		Robot robot = collision.collider.GetComponent<Robot>();

		if (robot != null) {
			robot.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force));
		}
	}
}