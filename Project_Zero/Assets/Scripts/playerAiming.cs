using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAiming : MonoBehaviour {

	public GameObject Parent;		//Set to the highest level player prefab.
	public float offset				= 32f;				//Corrects aim.
	
	private Vector3 mousePosition	= new Vector3();	//3D Coordinates of mouse position.
	private float angle				= 0f;				//Angle between mouse coords and origin of camera.
	private Vector3 gunPosition		= new Vector3();	//The medium to apply the angle to Quaternion.

	// Use this for initialization
	void Start() {
		//Nothing needed here.
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		Turning();
	}
	
	/*******************************************************
	**	Turning();
	**	The purpose of this function is to rotate the PC's
	**	arm so that their gun is directly facing the mouse.
	**	This rotation will also pass on to bullets when
	**	the gun is fired. (That comes later though).
	*******************************************************/
	void Turning() {
		//Get the X and Y pixel coordinates of the mouse based on camera's perspective.
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		//Debug.Log("mousePosition.x = " + mousePosition.x); //Width
		//Debug.Log("mousePosition.y = " + mousePosition.y); //Height
		
		//Gets the angle between the x-axis and hypotenuse of the mouse in degrees.
		angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        
		//Debug.Log("Angle = " + angle);
		
		//If the player is facing left...
		if (mousePosition.x < Parent.transform.position.x) {
			//Set our 3D Vector to contain the angle offset by (180-offset).
			gunPosition.Set(0f, 0f, -offset - angle + 180f);
			
			//The rotation of this limb is set to the mirror angle of the mouse.
			transform.localRotation = Quaternion.Euler(-gunPosition);
		}
		
		//Else they're facing right...
		else {
			//Set our 3D Vector to contain angle with an offset.
			gunPosition.Set(0f, 0f, offset - angle);
			
			//Rotation of this limb is set based on the angle of the mouse.
			transform.localRotation = Quaternion.Euler(gunPosition);
		}
    
		//Debug.Log("Mirror orientation: " + (-offset - angle + 180f));
		//Debug.Log("Normal orientation: " + (offset - angle));
		
	}
}
