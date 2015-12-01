using UnityEngine;
using System.Collections;

public class LevelButton : MonoBehaviour {

	public string _sceneID;

	// Use this for initialization
	void Start () {
		//Store where we were placed in the editor
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown(){
		Application.LoadLevel(_sceneID);
	}
}
