using UnityEngine;
using System.Collections;

public class ContinueGameButton : MonoBehaviour {
	
	private string _sceneID;
	
	// Use this for initialization
	void Start () {
		SpriteRenderer sr;
		sr = (SpriteRenderer) gameObject.GetComponent ("SpriteRenderer");
		//Store where we were placed in the editor
		if (PlayerPrefs.GetInt ("currentLevel") == 0) {
			sr.enabled = false;
		} else {
			sr.enabled = true;
			_sceneID = "level_" + (PlayerPrefs.GetInt ("currentLevel")).ToString ();
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown(){
		Application.LoadLevel (_sceneID);
	}
}
