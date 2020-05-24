using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameramovement : MonoBehaviour {
	public Vector3 velocity;
	public float smoothness;
//	public float smoothness;
	public int Targetpos;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow)){ 
			transform.position = Vector3.SmoothDamp (transform.position, new Vector3 
			(transform.position.x - Targetpos, transform.position.y,transform.position.z), ref velocity, smoothness);
		}

		if(Input.GetKey(KeyCode.RightArrow)){
			transform.position = Vector3.SmoothDamp (transform.position, new Vector3 
				(transform.position.x + Targetpos, transform.position.y,transform.position.z), ref velocity, smoothness);
		}
	}
}
