using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {

	public ParticleSystem blueParticles, orangeParticles;
	public bool scoreFromAbove, scoreFromRight, scoreFromBottom, scoreFromLeft;

	public GameObject[] blueBattery, orangeBattery;

	private void OnTriggerEnter2D(Collider2D collision) {
		Part part = collision.GetComponent<Part>();

		if (part != null) {
			if ((part.GetComponent<Rigidbody2D>().velocity.y < 0 && part.player == part.thrower && scoreFromAbove)
				|| (part.GetComponent<Rigidbody2D>().velocity.x < 0 && part.player == part.thrower && scoreFromRight)
				|| (part.GetComponent<Rigidbody2D>().velocity.y > 0 && part.player == part.thrower && scoreFromBottom)
				|| (part.GetComponent<Rigidbody2D>().velocity.x > 0 && part.player == part.thrower && scoreFromLeft)) {

				if (part.player == 0) {
					blueParticles.Play();
					blueBattery[Game.scores[part.player]].SetActive(true);
				} else if (part.player == 1) {
					orangeParticles.Play();
					orangeBattery[Game.scores[part.player]].SetActive(true);
				}

				Game.scores[part.player]++;

				if (Game.scores[part.player] >= blueBattery.Length) {
					//End Game
				}

				part.Respawn();
			}
		}
	}
}