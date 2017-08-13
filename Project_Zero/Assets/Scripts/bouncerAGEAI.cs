using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncerAGEAI : MonoBehaviour {

	public GameObject target;
	public GameObject joint;
	public GameObject bulletOrigin;
	public GameObject bullet;
	
	private bool trackPlayer		=	false;
	private float attackTimer		=	0f;
	private Vector3 lineToTarget;
	private float distanceFromTarget;
	private int shots				=	0;
	private GameObject newBullet;
	private Vector3 origin;					//Holds the coordinates for the bullet spawn.
	private Quaternion direction;			//The angle which the bullet will travel.
	private float angle				=	0f;	//Angle between mouse coords and origin of camera.
	private Quaternion startRotation;
	
	// Use this for initialization
	void Start () {
		startRotation = joint.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		lineToTarget = target.transform.position - transform.position;
		distanceFromTarget = Mathf.Sqrt((lineToTarget.x * lineToTarget.x + lineToTarget.y * lineToTarget.y));
		
		if(distanceFromTarget < 10f) {
			trackPlayer = true;
		}
		
		//Debug.Log("Track Player: " + trackPlayer);
		
		if(trackPlayer == true) {
			attackTimer += Time.deltaTime;
			Debug.Log("Attack Timer: " + attackTimer + "s");
			
			if(	(attackTimer > 1f) &&
				(shots < 3) &&
				((attackTimer - 1f) / (.1f * shots) >= 1f) &&
				(transform.position.x - target.transform.position.x > 0)) {
				shoot();
			}
			
			else if((shots == 3) || (attackTimer > 2f)) {
				attackTimer = 0f;
				shots = 0;
				joint.transform.rotation = startRotation;
			}
		}
	}
	
	public void shoot() {
		//Set the bullet's origin to coordinates of bulletOrigin
		origin = bulletOrigin.GetComponent<Collider2D>().bounds.center;
		
		//Set the direction based on the angle of Shoulder_Left
		direction = joint.transform.rotation;
		
		//Gets the angle between the x-axis and hypotenuse of the mouse in degrees.
		angle = Mathf.Atan2(lineToTarget.y, lineToTarget.x) * Mathf.Rad2Deg;
		
		//Get mouse position based on the origin of the camera.
		//mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		//Debug.Log("Angle = " + angle);
		//Debug.Log("x > 0 is left: " + (transform.position.x - target.transform.position.x));
		//If the player is to the left...
		
		//Offset the direction to match the direction the gun is pointing.
		angle = 180f + angle;
		//Debug.Log("New Angle = " + angle);
		direction = Quaternion.Euler(0f, 0f, angle);
		joint.transform.rotation = direction;

		//Instantiates a bullet and then activates it.
		direction = Quaternion.Euler(0f, 0f, angle - 180f);
		newBullet = Instantiate(bullet, origin, direction);
		newBullet.SetActive(true);
		shots++;
		
		Debug.Log("Shots: " + shots);
	}
}
