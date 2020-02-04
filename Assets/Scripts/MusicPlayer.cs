using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

	public static bool created = false;

	private void Start() {
		if (!created) {
			DontDestroyOnLoad(gameObject);
			created = true;
		} else {
			Destroy(gameObject);
		}
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			SceneManager.LoadScene("Menu");
		}
	}
}