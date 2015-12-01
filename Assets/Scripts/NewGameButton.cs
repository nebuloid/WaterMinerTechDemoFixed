using UnityEngine;
using System.Collections;

public class NewGameButton : MonoBehaviour {
	
	private string _sceneID = "level_1";
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown(){
		PlayerPrefs.DeleteAll ();
        PlayerPrefs.SetInt("lives", 3);
		Application.LoadLevel(_sceneID);
	}
}
