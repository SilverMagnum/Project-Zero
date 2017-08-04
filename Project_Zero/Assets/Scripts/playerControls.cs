using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControls : MonoBehaviour {

	private Vector3 mousePosition	= new Vector3();			//Mouse position.
	private Vector3 backwards		= new Vector3(-1, 1, 1);	//Invert.
	private Vector3 forwards		= new Vector3(1, 1, 1);		//Normal.

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Turning();
	}
	
	/*******************************************************
	**	Turning();
	**	The purpose of this function is invert the
	**	character if they face left.
	*******************************************************/
	void Turning() {
		//Get mouse position based on the origin of the camera.
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		//If the character faces left...
		if (mousePosition.x < transform.position.x) {
			//Invert their X-axis (mirrors them across Y-axis).
			transform.localScale = backwards;
		}
		
		//Else the character faces right...
		else {
			//Do nothing... more or less...
			transform.localScale = forwards;
		}
	}
}
