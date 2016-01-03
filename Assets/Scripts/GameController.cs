using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Timers;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameController : MonoBehaviour {

	public AudioClip _deathSound;
	public AudioClip deathSound;

	public Text scoreText;
	//public static Timer scoreTimer;

	//the three head game objects indicating player lives
	public GameObject _lifeHead1;
	public GameObject _lifeHead2;
	public GameObject _lifeHead3;

	public Texture nextButton;

    public int _lives;
	private float _scoreFloat;
	private int _scoreInt;
	private int mHighScore;
	private float mInvulnerabilityCountDown;

	//public string _winString;
	public string _level;
	public int _numCans;

	private bool gameOver;
	//this is turned to true when the level is completed
	//private bool isWindowShown;
	private bool isInvulnerable = false;
	private int mCurrentLevel;

	public Rect windowRect = new Rect(Screen.width/2, Screen.height/2, 540, 480);

    private TooBeeController playerController;
	private GameObject waterGoal;
	private InvulnerabilityColision invulnerabilityColision;

	void Start() {
		PlayerPrefs.SetInt ("levelScore", 0);
		mCurrentLevel = PlayerPrefs.GetInt ("currentLevel");
		gameOver = false;
		//isWindowShown = false;
		Load ();
		_scoreFloat = 1000;
		mInvulnerabilityCountDown = 30;
		scoreText.text = "Score: " + _scoreFloat;

		GameObject playerObject = GameObject.FindWithTag ("Player");
		GameObject invulnerabilityObject = GameObject.FindWithTag("Invulnerability");
		if (playerObject != null) {
			playerController = playerObject.GetComponent <TooBeeController>(); //get this instance's own game controller connection
		}

		if (playerController == null) {
			Debug.Log("Cannot find 'GameController' script"); //logging in case unable to find gamecontroller
		}

		//trying to connect invulnerabilityColision to this class
		if (invulnerabilityObject != null) {
			invulnerabilityColision = invulnerabilityObject.GetComponent <InvulnerabilityColision>(); //get this instance's own game controller connection
		}

		if (invulnerabilityColision == null) {
			Debug.Log("Cannot find 'invulnerability' script"); //logging in case unable to find gamecontroller
		}

		UpdateScore();

        _lives = PlayerPrefs.GetInt("lives");
        if (_lives < 3) {
            removeHeads(_lives);
        }

		waterGoal = GameObject.FindWithTag ("Water");
		playerController.setNumCans(_numCans);
	}

	void Update() {
		isInvulnerable = invulnerabilityColision.getIsInvulnerable();
		//if the level isn't finished then decrement the score
		//if (!isWindowShown) {
		DecrementScore();
		//}
		if(gameOver && GetComponent<AudioSource>().loop){
			GetComponent<AudioSource>().loop = false;
		}
		if(isInvulnerable) {
			InvulnerablilityCountdown();
		}
	}
	
    void FixedUpdate() {

        if (Input.GetButton ("Fire1")) { 
			if (gameOver) {
            	Application.LoadLevel ("menu"); // loads a new level (right now it is set to load the same over and over
				return;
			}

			if (playerController.stance - 1 == 0) {
				playerController.MoveMe();
			} else if (playerController.stance - 1 == 1) {
				playerController.Shoot();
			}
        }

		if (waterGoal == null) {
			return;
		}

		if (waterGoal.transform.position.x > playerController.gameObject.transform.position.x - 1 
		    && waterGoal.transform.position.x < playerController.gameObject.transform.position.x + 1
		    && waterGoal.transform.position.y > playerController.gameObject.transform.position.y - 1
		    && waterGoal.transform.position.y < playerController.gameObject.transform.position.y + 1) {
			Debug.Log("waterHIT"); //logging in case unable to find gamecontroller 
			//player dies
			Victory ();
		}
    }
	
	public void Victory() {
		Save ();

		Application.LoadLevel ("inbetween"); // loads a new level (right now it is set to load the same over and over
		return;
		//isWindowShown = true;
		/* 
		 * won equals true is now called 
		 * if the user clicks the 'next level'
		 * button in the gui text box
		 */
		//won = true;
	}

	void UpdateScore() {
		scoreText.text = "Score: " + _scoreInt;
	}

	public void AddScore(int newScoreValue) {
		_scoreFloat += newScoreValue;
		UpdateScore();
	}

	public bool GameOverBool {
        get { return gameOver; }
		set { gameOver = value; }
	}

	public void DecrementLives() {
        _lives--;
        if(_lives==0){
            removeHeads (_lives);
            GameOver ();
        } else if (_lives > 0) {
			removeHeads (_lives);
            PlayerPrefs.SetInt("lives", _lives);
            GetComponent<AudioSource>().clip = _deathSound;
            GetComponent<AudioSource>().Play();
            playerController.Die(); // moves player to starting point
        }
	}

	/*
	 * This is the method that is called in Update();
	 * I use Time.deltaTime * 6 just beacuse it seems 
	 * like a good speed to decrement at.
	 */
	public void DecrementScore() {
		if(!isInvulnerable) {
			_scoreFloat -= Time.deltaTime * 6;
		} else if (isInvulnerable) {
			_scoreFloat -= Time.deltaTime * 3;
		}
		_scoreInt = (int) _scoreFloat;
		if (_scoreFloat < 0) {
			_scoreFloat = 0;
		}
		scoreText.text = "Score: " + _scoreInt;
	}

    private void GameOver() {
        gameOver = true;
        GetComponent<AudioSource>().clip = _deathSound;
        GetComponent<AudioSource>().Play();
        // this line below here
		PlayerPrefs.SetInt ("totalScore", 0);
        PlayerPrefs.SetInt("currentLevel", 0);
        PlayerPrefs.SetInt("lives", 3);
        Application.LoadLevel ("menu"); // loads a new level (right now it is set to load the same over and over
        // prepares the level that will be loaded when player clicks
        //gameOverText.text = "Game Over Man!";
    }


	/*
	 * Called in decrementLives()
	 * this method takes in how many lives the
	 * user currently has and then removes the 
	 * TooBee head from the screen to match it.
	 */
	private void removeHeads(int lives) {
		switch (lives) {
			case 2:
				Destroy (_lifeHead3);
				break;
			case 1:
				Destroy (_lifeHead2);
				break;
			case 0:
				Destroy (_lifeHead1);
				break;
		}
	}

	private void InvulnerablilityCountdown() {
		mInvulnerabilityCountDown -= Time.deltaTime;
		if(mInvulnerabilityCountDown <= 0) {
			Debug.Log("Invulnerable time is finished");
			isInvulnerable = false;
			invulnerabilityColision.setIsInvulnerable(isInvulnerable);
			mInvulnerabilityCountDown = 30;
		}
		//Debug.Log ("Time is Ticking away, " + mInvulnerabilityCountDown);
	}

	public bool getInvulnerabilityStatus() {
		return isInvulnerable;
	}
	/*
	 *This function controls the pop up window that displays when
	 *the level has been beaten.
	 */
	private void OnGUI() {
		//if(isWindowShown){
			//windowRect = GUI.Window(0, new Rect(Screen.width/8, Screen.height/8, (float) (Screen.width * 0.75), (float) (Screen.height * 0.75)), DoMyWindow, "Level Complete");
		//}

	}

	/*
	 * This function adds two text fields and a button to the pop up window.
	 * If the button is clicked it sends the user to the next level.
	 */

	/*
	private void DoMyWindow(int windowID) {
		GUI.TextField(new Rect(10,20, (float) (windowRect.width - 20), (float) (windowRect.height / 3.4)),"Score: " + _scoreInt);
		GUI.TextField(new Rect(10, (float) ((windowRect.height * 0.33) + 12.5), (float) (windowRect.width - 20), (float) (windowRect.height / 3.4)), "High Score: " + mHighScore);
		if (GUI.Button(new Rect(10, (float) ((windowRect.height * 0.66) + 5), (float) (windowRect.width - 20), (float) (windowRect.height / 3.4)), new GUIContent(nextButton))) {
			won = true;
			//print("next level, Score: " + _scoreInt);
		}
	}
*/
	/*
	 * This funciton will run when the level is beaten.
	 * It saves the highScore if the current score is 
	 * higher than it.
	 */
	public void Save() {
		int totalScore = PlayerPrefs.GetInt ("totalScore");
		PlayerPrefs.SetInt("levelScore", _scoreInt);
		PlayerPrefs.SetInt("totalScore", _scoreInt + totalScore);

		totalScore = PlayerPrefs.GetInt ("totalScore");

		if (mHighScore <= PlayerPrefs.GetInt("totalScore")) {
			mHighScore = totalScore;
			Debug.Log("new High Score!" + mHighScore);
			PlayerPrefs.SetInt("highScore", mHighScore);
		} else {
			Debug.Log("no new highScore");
		}

		if (mCurrentLevel == 0) {
			mCurrentLevel++;
		}


		PlayerPrefs.SetInt("currentLevel", (mCurrentLevel + 1));
	}

	/*
	 * This is ran in the on Start function
	 * it loads the highscore from a file
	 */
	public void Load() {
		mHighScore = PlayerPrefs.GetInt("highScore");
	}

}
