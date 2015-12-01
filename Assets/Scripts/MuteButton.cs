using UnityEngine;
using System.Collections;

public class MuteButton : MonoBehaviour {

	public Sprite _unmute;
	public Sprite _mute;
	
	private bool muted = false;

	// Use this for initialization
	void Start () {
		//Store where we were placed in the editor
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown(){
		if (!muted) {
			GetComponent<SpriteRenderer> ().sprite = _unmute;
		} else {
			GetComponent<SpriteRenderer>().sprite = _mute;
		}

		muted = !muted;
	}
}
