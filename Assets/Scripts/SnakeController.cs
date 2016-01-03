using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class SnakeController : MonoBehaviour {
	
	public float _maxSpeedX = 10f;
	public float _maxSpeedY = 10f;
	public float _flipTimerX = 10f;
	public float _flipTimerY = 10f;
	
	private bool mFacingRight = false;
	private GameObject mPlayerObject;
	private Animator mAnim;
	
	private Stopwatch mTimer;
	private Animator mPlayerAnimator;
	private float mMoveX = 1;
	private float mMoveY = 1;
	private GameController mGameController;
	
	// Use this for initialization
	void Start () {
		
		mAnim = GetComponent<Animator> ();
		mPlayerObject = GameObject.FindWithTag ("Player");
		GameObject gameControlObject = GameObject.FindWithTag ("GameController");
		if (gameControlObject != null) {
			mGameController = gameControlObject.GetComponent <GameController>(); //get this instance's own game controller connection
		}
		if (mGameController == null) {
			UnityEngine.Debug.Log("Cannot find 'GameController' script"); //logging in case unable to find gamecontroller
		}
		
		mPlayerAnimator = mPlayerObject.GetComponent<Animator>();
		
		mTimer = new Stopwatch ();
		mTimer.Start();
		
		InvokeRepeating("FlipToadX", _flipTimerX, _flipTimerX);
		InvokeRepeating("FlipToadY", _flipTimerY, _flipTimerY);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		bool dead = mGameController.GameOverBool;
		if(! dead){
			mAnim.SetFloat ("Speed", 1);
			GetComponent<Rigidbody>().velocity = new Vector2 (mMoveX * _maxSpeedX, mMoveY * _maxSpeedY);
		}
	}
	
	void Update()
	{
		
	}
	
	void OnMouseDown()
	{	
		if (mPlayerObject == null)
			return;
		//UnityEngine.Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - player.rigidbody2D.transform.position.x);
		if (Mathf.Abs (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - mPlayerObject.GetComponent<Rigidbody>().transform.position.x) < 2 &&
		    Mathf.Abs (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - mPlayerObject.GetComponent<Rigidbody>().transform.position.y) < 2) {
			if (mPlayerAnimator != null)
				mPlayerAnimator.Play("Swing");
		}
	}
	
	void FlipToadX() {
		mMoveX = -mMoveX;
		if (mMoveX < 0 && !mFacingRight)
			FlipX ();
		else if (mMoveX > 0 && mFacingRight)
			FlipX ();
	}
	
	void FlipToadY() {
		mMoveY = -mMoveY;
	}
	
	void FlipX(){ // snake model is set up different, to flip visual x here we actually need to set the z property scale instead
		mFacingRight = !mFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.z *= -1;
		transform.localScale = theScale;
	}
}
