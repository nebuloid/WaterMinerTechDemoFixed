using UnityEngine;
using System.Collections;

public class WaterController : MonoBehaviour {
	
	//public GameObject player;	
	private GameController gameController;
	
	void Start (){
		GameObject gameControlObject = GameObject.FindWithTag ("GameController");
		if (gameControlObject != null) {
			gameController = gameControlObject.GetComponent <GameController>(); //get this instance's own game controller connection
		}
		if (gameController == null) {
			Debug.Log("Cannot find 'GameController' script"); //logging in case unable to find gamecontroller
		}
	}
	
	void OnTriggerEnter(Collider other){
		
		if (other.tag == "Player") { 
			//player dies
			gameController.Victory ();
			
		}
		Destroy(transform.GetComponent<Collider>()); 
	}
}
