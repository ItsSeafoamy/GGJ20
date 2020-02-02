using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {

	public ParticleSystem blueParticles, orangeParticles;
	public bool scoreFromAbove, scoreFromRight, scoreFromBottom, scoreFromLeft;

	public GameObject[] blueBattery, orangeBattery;

	public Part[] toSpawn;
	public float spawnTime;
	private float currentSpawnTime;
	private int numberSpawned;
	public float spawnAngle, spawnForce;

	public bool respawnParts = false;

	private void Update() {
		currentSpawnTime += Time.deltaTime;

		for (int x = 0; x < toSpawn.Length; x++) {
			float timeToSpawn = spawnTime/toSpawn.Length*x;

			if (currentSpawnTime > timeToSpawn && numberSpawned <= x) {
				Part spawned = Instantiate<Part>(toSpawn[x], transform.position, Quaternion.identity);
				Rigidbody2D rb = spawned.GetComponent<Rigidbody2D>();

				float angle = Random.Range(-spawnAngle, spawnAngle)+90;
				Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
				Debug.Log(dir);

				rb.AddForce(dir * spawnForce);
				numberSpawned++;
			}
		}
	}

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

				if (respawnParts) {
					part.Respawn();
				} else {
					Destroy(part.gameObject);
				}
			}
		}
	}
}