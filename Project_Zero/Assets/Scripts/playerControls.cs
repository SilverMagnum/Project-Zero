using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControls : MonoBehaviour {

	public GameObject shoulder;
	public float offset				= 19.871f;	//(19.871f + 13.721f)	//Corrects aim.
	//public GameObject parent;		//Set to the highest level player prefab.
	//public Camera playerCam;		//Set to the main camera on the player.
	
	private Vector3 backwards		= new Vector3(-1, 1, 1);	//Invert.
	private Vector3 forwards		= new Vector3(1, 1, 1);		//Normal.
	private Vector3 mousePosition	= new Vector3();	//3D Coordinates of mouse position.
	private float angle				= 0f;				//Angle between mouse coords and origin of camera.
	//private Vector3 gunPosition		= new Vector3();	//The medium to apply the angle to Quaternion.

	
	// Use this for initialization
	void Start () {
		//Does nothing.
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Turning();
	}
	
	/*******************************************************
	**	Turning();
	**	The purpose of this function is invert the
	**	character if they face left... Also:
	**	The purpose of this function is to rotate the PC's
	**	arm so that their gun is directly facing the mouse.
	**	This rotation will also pass on to bullets when
	**	the gun is fired.
	**	---------------------------------------------------
	**	Note, regardless of inversion, angle range:
	**	-70.129 to 109.871
	*******************************************************/
	private void Turning() {
		//Get mouse position based on the origin of the camera.
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		//Localizes mouse position based on player position.
		mousePosition = mousePosition - transform.position;
		
		//Debug.Log("mousePosition.x = " + mousePosition.x); //Width
		//Debug.Log("mousePosition.y = " + mousePosition.y); //Height
		
		//Gets the angle between the x-axis and hypotenuse of the mouse in degrees.
		angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
		
		//Debug.Log("Angle = " + angle);
		
		//If the character faces left...
		if (mousePosition.x < 0) {
			//Invert their X-axis (mirrors them across Y-axis).
			transform.localScale = backwards;
			
			//Change the shoulder angle based on facing left.
			shoulder.transform.rotation = Quaternion.Euler(0f, 0f, angle - (180f - offset));
		}
		
		//Else the character faces right...
		else {
			//Do nothing... more or less...
			transform.localScale = forwards;
			
			//Change the shoulder angle based on facing right.
			shoulder.transform.rotation = Quaternion.Euler(0f, 0f, angle - offset); 
		}
		
		//Debug.Log("Mirror orientation: " + (angle - (180f - offset)));
		//Debug.Log("Normal orientation: " + (angle - offset));
	}
}
