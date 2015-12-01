using UnityEngine;
using System.Collections;

public class TooBeeController : MonoBehaviour {
    private Vector2 mStartingPosition;
    public Vector2 getStartingPoint() { return mStartingPosition; }

	public float maxSpeed = 10f;

	bool facingRight = true;

	Animator anim;

	//constants
	private const int MOVE_STANCE = 1;
	private const int SHOOT_STANCE = 2;

	//move with click
	private Vector3 moveDirection;
	private float moveLocationX;
	private float moveLocationY;
	private bool moving;
	private Vector3 mTargetPoint;

	//toobee shots
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public Vector2 direction;
	public float nextFire;
	public AudioClip shotSound;

	//change stance
	public int stance = 1;
	private int numStances = 2;

	private bool flipOk = false;
	private bool mFirstTouch = false;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();

		//move with click
		moveDirection = Vector3.right;
		moveLocationX = transform.position.x;
		moving = false;

		//toobee shots
		direction = new Vector2 (0.0f, 0.0f);
        mStartingPosition = new Vector2 (transform.position.x, transform.position.y);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
	
		float move = Input.GetAxis ("Horizontal");
		if (mTargetPoint != default(Vector3) && mFirstTouch == true) {
			//Debug.Log (Mathf.Abs (mTargetPoint.x - transform.position.x));
			anim.SetFloat ("Speed", Mathf.Abs (mTargetPoint.x - transform.position.x));
			//Debug.Log("move: " + move);
		}
		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if (stance - 1 == 0) {
			MoveMe();
		}

		if (move > 0 && !facingRight) {
            Flip ();
        } else if (move < 0 && facingRight) {
            Flip ();
        }


	}

	public void Shoot(){
		if (Time.time > nextFire) {
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//make sure mouse is not over player...this wont work for touch

			direction = new Vector2 (mousePosition.x - transform.position.x, mousePosition.y - transform.position.y).normalized;

			nextFire = Time.time + fireRate;
			Vector3 startPosition = new Vector3 (shotSpawn.position.x, shotSpawn.position.y, 0.0f);

			float distX = Mathf.Abs(mousePosition.x - transform.position.x);
			float distY = Mathf.Abs(mousePosition.y - transform.position.y);

			if (distX > 4 || distY > 4) {
				anim.Play("Throw");
				GameObject clone = (GameObject) Instantiate(shot, startPosition, shotSpawn.rotation);
				
				clone.GetComponent<Rigidbody2D>().AddForce (direction * 1000.0f);
				GetComponent<AudioSource>().clip = shotSound;
				GetComponent<AudioSource>().Play();			
			}
		}
	}

	public void setTargetPoint(Vector3 point) {
		mTargetPoint = point;
		moving = true;
		flipOk = true;
		mFirstTouch = true;
	}

	public void MoveMe(){
		Vector3 currentPosition = transform.position;
		if (moving) {
			Vector3 moveToward = mTargetPoint;
			//Debug.Log (mTargetPoint);
			moveDirection = moveToward - currentPosition;
			moveDirection.z = 0; 
			moveLocationX = moveToward.x;
			moveLocationY = moveToward.y;
			moveDirection.Normalize ();

			if (moveToward.x > currentPosition.x && !facingRight) {
				Flip ();
			} else if (moveToward.x < currentPosition.x && facingRight) {
				Flip ();
            }

			Vector3 target = moveDirection * maxSpeed + currentPosition;
			transform.position = Vector3.Lerp (currentPosition, target, Time.deltaTime);
			//Debug.Log ("currentPosition.x = " + currentPosition.x + ", moveLocationX = " + moveLocationX);
			if (currentPosition.x < moveLocationX + 0.1f && currentPosition.x > moveLocationX - 0.1f &&
			    currentPosition.y < moveLocationY + 0.1f && currentPosition.y > moveLocationY - 0.1f) {
				moving = false;
			}
		}
	}

	void Flip(){
		if (!flipOk) {
            return;
        }

		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		flipOk = false;
	}

	public Vector2 Direction
	{
		get { return direction; }
		set { direction = value; }
	}

	public int Stance
	{
		get { return stance; }
		set { stance = value; }
	}

	void OnMouseDown(){	
		//change stance
		if(stance == numStances){
			stance = MOVE_STANCE;
		}else{
			stance++;
		}
		GetComponent<Animator> ().SetInteger ("Stance", stance - 1);
	}

	public void Die(){
        // this takes the starting point saved at the beginning of level and sets the player transform's position to it
        // transforms handle manipulating prefabs and sprites and other game objects
        // the 3 coordinates are x for horizontal position, y for vertical, and z for depth position
        // we don't care about depth, so I set it to whatever it currently is
        // the z positions on stuff in the scene and also prefabs DOES matter though...
        // as it controls what is visually in the front of the scene and what is background
        // there can be as many layers as different z positions entered into the scene and the prefabs
        transform.position = new Vector3 (mStartingPosition.x, mStartingPosition.y, transform.position.z);
	}
}
