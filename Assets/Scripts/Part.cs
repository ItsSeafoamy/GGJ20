using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour {

	public int player;
	public int thrower;

	private void Start() {
		GetComponent<SpriteRenderer>().material.color = Game.i.colours[player];
	}

	private void Update() {
		if (transform.position.x < -Game.i.screenEdge || transform.position.x > Game.i.screenEdge || transform.position.y < Game.i.bottomEdge) {
			Respawn();
		}
	}

	public void Respawn() {
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		GetComponent<Rigidbody2D>().angularVelocity = 0;

		Vector2 pos = transform.position;
		pos.y = Game.i.topEdge;
		pos.x = Random.Range(-Game.i.screenEdge*0.8f, -Game.i.middleZone);
		if (Random.Range(0, 2) == 1) pos.x = -pos.x;

		transform.position = pos;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		Rigidbody2D rb = GetComponent<Rigidbody2D>();

		//rb.velocity = collision.GetContact(0).normal * rb.velocity.magnitude * 10;
	}
}