using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public static Game i;
	public static int[] scores = new int[] {0, 0};

	public Color[] colours;
	public float screenEdge;
	public float topEdge, bottomEdge;
	public float middleZone;

	private void Start() {
		i = this;
	}

	private void OnGUI() {
		GUI.Label(new Rect(16, 16, 1000, 1000), "Player 1: " + scores[0]);
		GUI.Label(new Rect(Screen.width - 100, 16, 1000, 1000), "Player 2: " + scores[1]);
	}
}