using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {

	public ParticleSystem particles;
	public bool scoreFromAbove, scoreFromRight, scoreFromBottom, scoreFromLeft;

	private void OnTriggerEnter2D(Collider2D collision) {
		Part part = collision.GetComponent<Part>();

		if (part != null) {
			if ((part.GetComponent<Rigidbody2D>().velocity.y < 0 && part.player == part.thrower && scoreFromAbove)
				|| (part.GetComponent<Rigidbody2D>().velocity.x < 0 && part.player == part.thrower && scoreFromRight)
				|| (part.GetComponent<Rigidbody2D>().velocity.y > 0 && part.player == part.thrower && scoreFromBottom)
				|| (part.GetComponent<Rigidbody2D>().velocity.x > 0 && part.player == part.thrower && scoreFromLeft)) {

				Game.scores[part.player]++;
				particles.Play();

				part.Respawn();
			}
		}
	}
}