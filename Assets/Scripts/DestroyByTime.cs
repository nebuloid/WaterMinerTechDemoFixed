using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {

	public float _lifetime;

	void Start ()
	{
		Destroy (gameObject, _lifetime);
	}
}
