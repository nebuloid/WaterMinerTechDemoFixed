using UnityEngine;
using System.Collections;

public class CameraScaler : MonoBehaviour {
	//private Transform cameraTransform; // camera Transform
	private double screenRatio;
	// Use this for initialization
	void Start () {
		//cameraTransform = transform;
		if (Screen.width / Screen.height > .67 ||
		    Screen.width / Screen.height < .65) {
			ResizeScreen();
			RepositionScreen();
		}
	}

	void ResizeScreen() {
		screenRatio = (float) Screen.width / (float) Screen.height;
		Camera.main.orthographicSize *= (float) ((.666666666 + Mathf.Abs ((float)(screenRatio - .666666666))) / .666666666);
	}

	void RepositionScreen() {
		Vector3 posTransform = Camera.main.transform.position;
		posTransform.y /= (float) ((.666666666 + Mathf.Abs ((float)(screenRatio - .666666666))) / .666666666);
		Camera.main.transform.position = posTransform;
	}
}
