using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Timers;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class InbetweenScreen : MonoBehaviour
{
	public Text levelScoreText;
	public Text totalScoreText;
	public Text highScoreText;
	public Sprite[] infoPanels;
	public GameObject infoHolder = null;
	
	void Start() {
		levelScoreText.text = PlayerPrefs.GetInt ("levelScore").ToString ();
		highScoreText.text = PlayerPrefs.GetInt ("highScore").ToString ();
		totalScoreText.text = PlayerPrefs.GetInt ("totalScore").ToString ();
		
		int currentLev = PlayerPrefs.GetInt ("currentLevel") - 2;
		Debug.Log ("level = " + currentLev);
		if (currentLev > 25) {
			currentLev -= 24;
		} else if (currentLev > 17) {
			currentLev -= 16;
		} else if (currentLev > 9) {
			currentLev -= 8;
		}
		Debug.Log ("level minust = " + currentLev);
		infoHolder.GetComponent<SpriteRenderer>().sprite = infoPanels[currentLev];
	}
}