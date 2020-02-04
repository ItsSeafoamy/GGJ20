using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour {

	public Texture2D blue, orange;

	public Vector2 backButton;
	public Texture2D xButton;
	public GUISkin xSkin;

	public void OnGUI() {
		if (Game.winner == 0) GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blue);
		if (Game.winner == 1) GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), orange);

		GUI.skin = xSkin;

		if (GUI.Button(new Rect(backButton, new Vector2(xButton.width, xButton.height)), "")) {
			SceneManager.LoadScene("Menu");
		}
	}
}
