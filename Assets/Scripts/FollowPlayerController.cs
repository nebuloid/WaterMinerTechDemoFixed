﻿using UnityEngine;
using System.Collections;

public class FollowPlayerController : MonoBehaviour {

	//the enemy's target 
	Transform target; 

	//move speed 
	public int moveSpeed = 3; 

	//speed of turning
	public int rotationSpeed = 3; 

	//current transform data of this enemy
	Transform myTransform; 

	//cache transform data for easy access/preformance
	public void Awake() { 
		myTransform = transform;
	} 
	 
		
		public void Start() {
		//target the player
		target = GameObject.FindWithTag("Player").transform; 
			
		}
		
		public void Update () { 
			//rotate to look at the player 
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, 
			Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed*Time.deltaTime);
			//move towards the player
			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
		}
}
