using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour {

	int score = 0;

	void Start () {
		score = PlayerPrefs.GetInt ("Score");
	}
	
//	void OnGUI(){
//		GUI.Label (new Rect (Screen.width / 2 - 40, 50, 80, 30), "GAME OVER");
//		GUI.Label (new Rect (Screen.width / 2 - 40, 300, 80, 30), "Score: " + score);
//	}

	void Update(){

		if (Input.anyKey) {
			Application.LoadLevel (0);
		}
	}

}
