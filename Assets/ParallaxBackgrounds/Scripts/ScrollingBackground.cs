using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

	public float BackgroundSize;
	public float parallaxSpeed;

	Transform cameraTransform;
	Transform[] layers;
	float viewZone = 30;
	int leftIndex;
	int rightIndex;

	float lastCameraX;

	void Start(){
		cameraTransform = Camera.main.transform;
		lastCameraX = cameraTransform.position.x;
		layers = new Transform[transform.childCount];
		for (int i=0; i< transform.childCount; i++){
			layers [i] = transform.GetChild (i);
		}

		leftIndex = 0;
		rightIndex = layers.Length - 1;
	}
	void Update(){

		float deltaX = cameraTransform.position.x - lastCameraX;
		transform.position += Vector3.right * (deltaX * parallaxSpeed);
		lastCameraX = cameraTransform.position.x;

		if (cameraTransform.position.x < (layers [leftIndex].transform.position.x + viewZone)) {
			scrollLeft ();
		}
		if (cameraTransform.position.x > (layers [leftIndex].transform.position.x + viewZone)) {
			scrollRight ();
		}
	}

	void scrollLeft(){
		int lastRight = rightIndex;
        Vector3 posFix = Vector3.right * (layers[leftIndex].position.x - BackgroundSize);
        posFix.z = layers[rightIndex].position.z;
        posFix.y = layers[rightIndex].position.y;
        layers[rightIndex].position = posFix;

        leftIndex = rightIndex;
		rightIndex--;
		if(rightIndex < 0){
			rightIndex = layers.Length - 1;
		}
	}
	void scrollRight(){
		int Lastleft = leftIndex;
        Vector3 posFix = Vector3.right * (layers[rightIndex].position.x + BackgroundSize);
        posFix.z = layers[rightIndex].position.z;
        posFix.y = layers[rightIndex].position.y;
        layers[leftIndex].position = posFix;

        rightIndex = leftIndex;
		leftIndex++;
		if(rightIndex == layers.Length -1){
			leftIndex = 0;
		}	
	}
}