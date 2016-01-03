using UnityEngine;
using System.Collections;

public class GridSquareController : MonoBehaviour {
	private GameObject mPlayer; // player object for moving 
	private TooBeeController mPlayerController;
	private GameController mGameController;
	// Use this for initialization
	void Start () {
		GameObject gameControlObject = GameObject.FindWithTag ("GameController");
		if (gameControlObject != null) {
			mGameController = gameControlObject.GetComponent <GameController>(); //get this instance's own game controller connection
		}
		if (mGameController == null) {
			UnityEngine.Debug.Log("Cannot find 'GameController' script"); //logging in case unable to find gamecontroller
		}
		
		mPlayer = GameObject.FindWithTag ("Player");
		if (mPlayer != null) {
			mPlayerController = mPlayer.GetComponent <TooBeeController>(); //get this instance's own game controller connection
		}

		//UnityEngine.Debug.Log(spriteRenderer.bounds.extents.x);
		//CapsuleCollider cap = (CapsuleCollider) mPlayer.GetComponent("CapsuleCollider");

		//Physics.IgnoreCollision(cap, transform.GetComponent<BoxCollider>());
	}
	
	void OnMouseDown(){
		//UnityEngine.Debug.Log("player = "+ player.transform.position);
		//UnityEngine.Debug.Log(Vector2.Distance(player.transform.position, transform.position));
		//UnityEngine.Debug.Log(spriteRenderer.bounds.extents.x * 2);
		
		bool dead = mGameController.GameOverBool;
		
		if (mPlayer == null || dead)
			return;
		
		int stance = mPlayerController.Stance;
		if (stance == 1) {
			mPlayerController.setTargetPoint(transform.position);
		}
	}
}
