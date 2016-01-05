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
	public GameObject infoHolder;

	void Start() {
		levelScoreText.text = PlayerPrefs.GetInt ("levelScore").ToString ();
		highScoreText.text = PlayerPrefs.GetInt ("highScore").ToString ();
		totalScoreText.text = PlayerPrefs.GetInt ("totalScore").ToString ();

		int currentLev = PlayerPrefs.GetInt ("currentLevel") - 2;
		if (currentLev > 9) {
			currentLev -= 6;
		} else if (currentLev > 18) {
			currentLev -= 15;
		} else if (currentLev > 27) {
			currentLev -= 24;
		}
		infoHolder.GetComponent<SpriteRenderer>().sprite = infoPanels[currentLev];
	}
}