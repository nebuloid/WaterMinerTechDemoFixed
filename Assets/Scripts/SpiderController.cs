using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class SpiderController : MonoBehaviour {
	
	public float _maxSpeedX = 10f;
	public float _maxSpeedY = 10f;
	public float _flipTimerX = 10f;
	public float _flipTimerY = 10f;
	public int _rotationSpeed = 10;
	public float _dampingLook = 6f;

	public Transform[] waypoints;

	// current waypoint id
	private int waypointId = 0;

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
			//Patrol ();
			GetComponent<Rigidbody2D>().velocity = new Vector2 (mMoveX * _maxSpeedX, mMoveY * _maxSpeedY);
		}
	}
	void Update()
	{
			Patrol();
	}
	
	void OnMouseDown()
	{	
		if (mPlayerObject == null)
			return;
		//UnityEngine.Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - player.rigidbody2D.transform.position.x);
		if (Mathf.Abs (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - mPlayerObject.GetComponent<Rigidbody2D>().transform.position.x) < 2 &&
		    Mathf.Abs (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - mPlayerObject.GetComponent<Rigidbody2D>().transform.position.y) < 2) {
			if (mPlayerAnimator != null)
				mPlayerAnimator.Play("Swing");
		}
	}
	void FlipToadX ()
	{
		mMoveX = -mMoveX;
		if (mMoveX < 0 && !mFacingRight)
			FlipX ();
		else if (mMoveX > 0 && mFacingRight)
			FlipX ();
	}
	
	void FlipToadY ()
	{
		mMoveY = -mMoveY;
	}
	
	void FlipX()
	{
		mFacingRight = !mFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Patrol() 
	{
		if(waypoints.Length == 0) 
		{
			UnityEngine.Debug.Log("Please assign waypoint values in the editor.");
			return;
		}

		if(waypoints.Length != 0) {

			if(waypointId == waypoints.Length) {
				waypointId = 0;
			} else {

				//variable for the current waypoint the spider is heading towards.
				Vector3 target = waypoints[waypointId].position;
				//the distance between spider and waypoint.
				Vector3 moveDirection = target - transform.position;

				transform.position = Vector3.MoveTowards(transform.position, target, 0.5f);

				//if waypoint is reached then increment the waypointId
				if(moveDirection.magnitude < 0.5) {
					//implement pausing here.	
					waypointId ++;
					
				}
			}
		}
	}
}
