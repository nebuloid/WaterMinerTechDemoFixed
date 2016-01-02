using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	
	//public GameObject player;
	public int _scoreValue;
	private bool mWon = false;
	private bool mIsInvulnerable = false;
	private GameController mGameController;


	void Start () {
		GameObject gameControlObject = GameObject.FindWithTag ("GameController");
		if (gameControlObject != null) {
			mGameController = gameControlObject.GetComponent <GameController>(); //get this instance's own game controller connection
		}
		if (mGameController == null) {
			Debug.Log("Cannot find 'GameController' script"); //logging in case unable to find gamecontroller
		}
	}

	/*void OnTriggerEnter2D(Collider2D other){
		/*
		 * This if statment is used to stop
		 * null reference exceptions when somthing other 
		 * than the player or enemy is hit. for example
		 * throwing a can and it hits the ground.
		 */ /*
		if(other.tag == "Toad" || other.tag == "Player") {
			mIsInvulnerable = mGameController.getInvulnerabilityStatus();
		}

		if (mWon)
			return;
		if (other.tag == "Player" && !mIsInvulnerable) { 
			if (mGameController != null) {
				mGameController.DecrementLives (); // this runs when one collider is a toad and one is the player
			}
		} else if (other.tag == "Toad") {
			if (mGameController != null)
				mGameController.AddScore(other.GetComponent<DestroyByContact>()._scoreValue);
                Debug.Log("Toad Killed: " + other.GetComponent<DestroyByContact>()._scoreValue + "points gained");
			//destroy toad
			Destroy (gameObject);
			//destroy whatever hit toad
			Destroy (other.gameObject);
		} else if (other.tag == "Player" && mIsInvulnerable) {
			if(mGameController != null) {
				Debug.Log("this code runs");
				mGameController.AddScore(this.GetComponent<DestroyByContact>()._scoreValue);
				Destroy (gameObject);
				Debug.Log("Enemy Killed Using Invulnerability power-up: " + this.GetComponent<DestroyByContact>()._scoreValue + "points gained");
			}
		} else {
			gameObject.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
		}
	}*/

	void OnTriggerEnter(Collider other){
		/*
		 * This if statment is used to stop
		 * null reference exceptions when somthing other 
		 * than the player or enemy is hit. for example
		 * throwing a can and it hits the ground.
		 */ 
		if(other.tag == "Toad" || other.tag == "Player") {
			mIsInvulnerable = mGameController.getInvulnerabilityStatus();
		}
		
		if (mWon)
			return;
		if (other.tag == "Player" && !mIsInvulnerable) { 
			if (mGameController != null) {
				mGameController.DecrementLives (); // this runs when one collider is a toad and one is the player
			}
		} else if (other.tag == "Toad") {
			if (mGameController != null)
				mGameController.AddScore(other.GetComponent<DestroyByContact>()._scoreValue);
			Debug.Log("Toad Killed: " + other.GetComponent<DestroyByContact>()._scoreValue + "points gained");
			//destroy toad
			Destroy (gameObject);
			//destroy whatever hit toad
			Destroy (other.gameObject);
		} else if (other.tag == "Player" && mIsInvulnerable) {
			if(mGameController != null) {
				Debug.Log("this code runs");
				mGameController.AddScore(this.GetComponent<DestroyByContact>()._scoreValue);
				Destroy (gameObject);
				Debug.Log("Enemy Killed Using Invulnerability power-up: " + this.GetComponent<DestroyByContact>()._scoreValue + "points gained");
			}
		} else {
			gameObject.transform.GetComponent<Rigidbody>().velocity = new Vector2(0,0);
		}
	}
}
