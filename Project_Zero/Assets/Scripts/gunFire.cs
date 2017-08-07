using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunFire : MonoBehaviour {

	public float timeBetweenBullets =	0.15f;
	public GameObject bullet;			//Object the gun will fire.
	public GameObject bulletOrigin;		//Where the bullet will shoot from.
	public GameObject parent;			//Set to the highest level player prefab.
	
	private float timer				=	0f;
	private GameObject newBullet;		//Used to activate an instantiated bullet.
	private Vector3 origin;				//Holds the coordinates for the bullet spawn.
	private Quaternion direction;		//The angle which the bullet will travel.
	private Vector3 mousePosition	=	new Vector3();	//3D Coordinates of mouse position.

	private const float offset		=	19.871f;

	// Use this for initialization
	void Start () {
		//Does nothing.
	}
	
	// Update is called once per frame
	void Update () {
		//Adds the time it takes to calculate and output each frame to timer.
		timer += Time.deltaTime;
		
		//If the player left clicks and it's been long enough since the last bullet...
		if(Input.GetMouseButton(0) && timer >= timeBetweenBullets) {
			Shoot();
		}
	}
	
	private void Shoot() {
		//Reset timer
		timer = 0f;	
		
		//Set the bullet's origin to coordinates of bulletOrigin
		origin = bulletOrigin.GetComponent<Collider2D>().bounds.center;
		
		//Set the direction based on the angle of Shoulder_Left
		direction = transform.rotation;
		
		//Get mouse position based on the origin of the camera.
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		//If the player is facing left...
		if (mousePosition.x - parent.transform.position.x < 0) {
			//Offset the direction to match the direction the gun is pointing.
			direction = Quaternion.Euler(0f, 0f, direction.eulerAngles.z - (180f + offset));//((180f - direction.eulerAngles.z) - offset)); 
		}
		
		//Else the player is facing right...
		else {
			//Offset the direction to match the direction the gun is pointing.
			direction = Quaternion.Euler(0f, 0f, direction.eulerAngles.z + offset);
		}
		
		//Debug.Log("Quaternion Rotation x = " + direction.eulerAngles.x); //Should be 0.
		//Debug.Log("Quaternion Rotation y = " + direction.eulerAngles.y); //Should be 0.
		//Debug.Log("Quaternion Rotation z = " + direction.eulerAngles.z);
		
		//Instantiates a bullet and then activates it.
		newBullet = Instantiate(bullet, origin, direction);
		newBullet.SetActive(true);
	}
}
