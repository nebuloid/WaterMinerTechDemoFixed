using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    private const double IDEAL_SCREEN_RATIO = 1.6;

	public GameObject _cameraTarget; // object to look at or follow
	public float _smoothTime = 0.1f;    // time for dampen
	public float _cameraHeight = 2.5f; // height of camera adjustable
	public Vector2 _velocity; // speed of camera movement
	
	private Transform mCameraTransform; // camera Transform
	private GameController mGameController;
	private double mScreenRatio;
	
	// Use this for initialization
	void Start()
	{
		mCameraTransform = transform;
		GameObject gameControlObject = GameObject.FindWithTag ("GameController");
		if (gameControlObject != null) {
			mGameController = gameControlObject.GetComponent <GameController>(); //get this instance's own game controller connection
		}
		if (mGameController == null) {
			UnityEngine.Debug.Log("Cannot find 'GameController' script"); //logging in case unable to find gamecontroller
		}

		mScreenRatio = (float) Screen.width / (float) Screen.height;
		if (mScreenRatio > 1.6) {
			ResizeScreenMore();
		} else {
			ResizeScreenLess();
		}
	}
	
	// Update is called once per frame
	void Update()
	{

	}

	void FixedUpdate () {
		bool dead = mGameController.GameOverBool;
		if(! dead){
			if (_cameraTarget.transform.position.y > -25.75) {
				mCameraTransform.position = new Vector3(mCameraTransform.position.x, 
				                                        Mathf.SmoothDamp(mCameraTransform.position.y, _cameraTarget.transform.position.y, ref _velocity.y, _smoothTime), 
			                                       		mCameraTransform.position.z);
			}
		}
	}

	void ResizeScreenLess() {
		Camera.main.orthographicSize *= (float) ((IDEAL_SCREEN_RATIO + Mathf.Abs ((float)(mScreenRatio - IDEAL_SCREEN_RATIO))) / IDEAL_SCREEN_RATIO);
	}

	void ResizeScreenMore() {
		Camera.main.orthographicSize /= (float) ((IDEAL_SCREEN_RATIO + Mathf.Abs ((float)(mScreenRatio - IDEAL_SCREEN_RATIO))) / IDEAL_SCREEN_RATIO);
	}
}