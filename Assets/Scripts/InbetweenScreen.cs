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

	void Start ()
	{
		levelScoreText.text = PlayerPrefs.GetInt ("levelScore").ToString ();
		highScoreText.text = PlayerPrefs.GetInt ("highScore").ToString ();
		totalScoreText.text = PlayerPrefs.GetInt ("totalScore").ToString ();
	}
}