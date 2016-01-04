using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class GroundController : MonoBehaviour {
	public Sprite[] _sprites;
	public float _framesPerSecond;
    private Animator playerAnimator;
	private SpriteRenderer spriteRenderer;
	//private BoxCollider boxCol;
	private bool touched = false;
	private bool grounded = false;
	private Stopwatch timer;
	//private float groundRadius = 0.2f;
	private GameObject player; // player object for moving 
	private TooBeeController playerController;
	private GameController gameController;
	public Sprite[] _rockBlocks;

	// Use this for initialization
	void Start () {
		GameObject gameControlObject = GameObject.FindWithTag ("GameController");
		if (gameControlObject != null) {
			gameController = gameControlObject.GetComponent <GameController>(); //get this instance's own game controller connection
		}
		if (gameController == null) {
			UnityEngine.Debug.Log("Cannot find 'GameController' script"); //logging in case unable to find gamecontroller
		}

		player = GameObject.FindWithTag ("Player");
		if (player != null) {
			playerController = player.GetComponent <TooBeeController>(); //get this instance's own game controller connection
		}

		playerAnimator = player.GetComponent<Animator>();
		timer = new Stopwatch ();
		spriteRenderer = GetComponent<Renderer>() as SpriteRenderer;
		//boxCol = GetComponent<BoxCollider>() as BoxCollider;
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerDistance()){
			grounded = true;
		}else{
			grounded = false;
		}
	}

	// FixedUpdate is run at specific time intervals.
	void FixedUpdate () {
		bool dead = gameController.GameOverBool;
		if(! dead){
			if(grounded){
				if (touched) {
					int index = (int)(0.01f * timer.ElapsedMilliseconds);
					//UnityEngine.Debug.Log(boxCol);
					if(index == _sprites.Length){
						Destroy(gameObject);
					}else if(index < _sprites.Length){
						spriteRenderer.sprite = _sprites [index];
					}
				}
			}else{
				touched = false;
				if(timer.IsRunning){
					timer.Stop();
					timer.Reset();
					spriteRenderer.sprite = _sprites [0];
				}
			}
		}
	}
	
	public bool PlayerDistance(){
		if(Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.y), new Vector2(transform.position.x, transform.position.y)) < 5.7f){
			return true;
		}else{
			return false;
		}
	}

	void OnMouseDown(){
		//UnityEngine.Debug.Log("player = "+ player.transform.position);
		//UnityEngine.Debug.Log(Vector2.Distance(player.transform.position, transform.position));
		//UnityEngine.Debug.Log(spriteRenderer.bounds.extents.x * 2);

		bool dead = gameController.GameOverBool;
		
		if (player == null || dead)
			return;

		int stance = playerController.Stance;
		if (grounded && stance == 1){
			if (playerAnimator != null)
				playerAnimator.SetTrigger("Swing");

			if (timer != null)
				timer.Start();

			touched = true;
		} 		
	}
}