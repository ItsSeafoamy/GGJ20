using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public enum MenuScreen {MAIN, CONTROLS, ABOUT, LEVEL_SELECT, CREDITS}

	private MenuScreen currentScreen;

	public Texture2D background, button, controlsScreen, xButton, aboutScreen, squareButton, levelScreen, creditsScreen;

	public GUISkin blueText, orangeText, xSkin, buttonSkin;

	public Vector2 playButton, controlsButton, aboutButton, exitButton, backButton, level1, level2, level3, creditsButton;

	private void OnGUI() {
		AudioSource audio = GetComponent<AudioSource>();
		Vector3 scale;
		float originalWidth = 1920;
		float originalHeight = 1080;

		scale.x = Screen.width/originalWidth;
		scale.y = Screen.height/originalHeight;
		scale.z = 1;
		Matrix4x4 svMat = GUI.matrix;
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, scale);

		GUI.DrawTexture(new Rect(0, 0, 1920, 1080), background);

		if (currentScreen == MenuScreen.MAIN) {
			GUI.skin = blueText;
			if (GUI.Button(new Rect(playButton, new Vector2(button.width, button.height)), "PLAY")) {
				currentScreen = MenuScreen.LEVEL_SELECT;
				audio.Play();
			}

			GUI.skin = orangeText;
			if (GUI.Button(new Rect(controlsButton, new Vector2(button.width, button.height)), "CONTROLS")) {
				currentScreen = MenuScreen.CONTROLS;
				audio.Play();
			}

			GUI.skin = blueText;
			if (GUI.Button(new Rect(aboutButton, new Vector2(button.width, button.height)), "ABOUT")) {
				currentScreen = MenuScreen.ABOUT;
				audio.Play();
			}

			GUI.skin = orangeText;
			if (GUI.Button(new Rect(exitButton, new Vector2(button.width, button.height)), "QUIT")) {
				Application.Quit();
			}
		} else if (currentScreen == MenuScreen.CONTROLS) {
			GUI.DrawTexture(new Rect(0, 0, 1920, 1080), controlsScreen);

			GUI.skin = xSkin;

			if (GUI.Button(new Rect(backButton, new Vector2(xButton.width, xButton.height)), "")) {
				audio.Play();
				currentScreen = MenuScreen.MAIN;
			}
		} else if (currentScreen == MenuScreen.ABOUT) {
			GUI.DrawTexture(new Rect(0, 0, 1920, 1080), aboutScreen);

			GUI.skin = xSkin;

			if (GUI.Button(new Rect(backButton, new Vector2(xButton.width, xButton.height)), "")) {
				audio.Play();
				currentScreen = MenuScreen.MAIN;
			}

			GUI.skin = blueText;

			if (GUI.Button(new Rect(creditsButton - new Vector2(button.width/2f, 0), new Vector2(button.width, button.height)), "CREDITS")) {
				audio.Play();
				currentScreen = MenuScreen.CREDITS;
			}
		} else if (currentScreen == MenuScreen.LEVEL_SELECT) {
			GUI.DrawTexture(new Rect(0, 0, 1920, 1080), levelScreen);

			GUI.skin = xSkin;

			if (GUI.Button(new Rect(backButton, new Vector2(xButton.width, xButton.height)), "")) {
				audio.Play();
				currentScreen = MenuScreen.MAIN;
			}

			GUI.skin = buttonSkin;

			if (GUI.Button(new Rect(level1, new Vector2(squareButton.width, squareButton.height)), "1")) {
				audio.Play();
				SceneManager.LoadScene("Scene1");
			}
			if (GUI.Button(new Rect(level2, new Vector2(squareButton.width, squareButton.height)), "2")) {
				audio.Play();
				SceneManager.LoadScene("Scene2");
			}
			if (GUI.Button(new Rect(level3, new Vector2(squareButton.width, squareButton.height)), "3")) {
				audio.Play();
				SceneManager.LoadScene("Scene3");
			}
		} else if (currentScreen == MenuScreen.CREDITS) {
			GUI.DrawTexture(new Rect(0, 0, 1920, 1080), creditsScreen);

			GUI.skin = xSkin;

			if (GUI.Button(new Rect(backButton, new Vector2(xButton.width, xButton.height)), "")) {
				audio.Play();
				currentScreen = MenuScreen.ABOUT;
			}
		}
	}
}