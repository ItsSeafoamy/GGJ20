using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGameHand : MonoBehaviour {

	public Robot target;
	private Vector2 startLocation;
	private Quaternion startRotation;

	private void Start() {
		startLocation = transform.position;
		startRotation = transform.rotation;
	}

	private void Update() {
		if (Game.i.timer > 0) {
			transform.position = Vector2.Lerp(startLocation, target.transform.position, (Game.i.preGameTime - Game.i.timer)/Game.i.preGameTime);
			transform.rotation = Quaternion.Lerp(startRotation, target.transform.rotation, (Game.i.preGameTime - Game.i.timer)/Game.i.preGameTime);
		} else {
			target.gameObject.SetActive(true);
			Destroy(gameObject);
		}
	}
}