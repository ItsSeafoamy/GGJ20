using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public static Game i;
	public static int[] scores = new int[] {0, 0};
	public static int winner;

	public GUISkin skin, rightAlign;

	public Color[] colours;
	public float screenEdge;
	public float topEdge, bottomEdge;
	public float middleZone;

	[System.NonSerialized] public float timer;
	public float preGameTime;

	private void Start() {
		i = this;

		scores = new int[] { 0, 0 };

		timer = preGameTime;
	}

	private void Update() {
		timer -= Time.deltaTime;
	}

	//private void OnGUI() {
	//	GUI.skin = skin;
	//	GUI.color = colours[0];
	//	GUI.Label(new Rect(16, 16, 1000, 1000), "Player 1: " + scores[0]);

	//	GUI.skin = rightAlign;
	//	GUI.color = colours[1];
	//	GUI.Label(new Rect(Screen.width - 216, 16, 200, 1000), "Player 2: " + scores[1]);
	//}
}