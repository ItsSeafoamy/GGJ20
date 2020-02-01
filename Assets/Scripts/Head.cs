using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D collision) {
		Part part = collision.GetComponent<Part>();

		if (part != null) {
			if (part.GetComponent<Rigidbody2D>().velocity.y < 0) {
				Game.scores[part.player]++;
				Destroy(part.gameObject);
			}
		}
	}
}