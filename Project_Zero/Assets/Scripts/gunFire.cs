using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunFire : MonoBehaviour {

	public float timeBetweenBullets =	0.15f;
	//public float range				=	100f;
	public GameObject bullet;
	public GameObject bulletOrigin;
	public GameObject parent;		//Set to the highest level player prefab.
	
	
	private float timer				=	0f;
	private GameObject newBullet;
	private Vector3 origin;
	private Quaternion direction;
	private Vector3 mousePosition	=	new Vector3();	//3D Coordinates of mouse position.
	private const float offset		=	19.871f;
	//private const Vector3 posOffset =	new Vector3((1.7222f - 0.145f), (0.4f - 0.25673f), 0f);
	//private 
	
	// Use this for initialization
	void Start () {
		//Does nothing.
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		
		if(Input.GetMouseButton(0) && timer >= timeBetweenBullets) {
			Shoot();
		}
	}
	
	void Shoot() {
		timer = 0f;		
		//origin = transform.position;
		Collider2D temp = bulletOrigin.GetComponent<Collider2D>();
		origin = temp.bounds.center;
		direction = transform.rotation;
		
		//Get mouse position based on the origin of the camera.
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		if (mousePosition.x < parent.transform.position.x) {
			direction = Quaternion.Euler(0f, 0f, ((180f - direction.eulerAngles.z) - offset));
		}
		
		else {
			direction = Quaternion.Euler(0f, 0f, 360f - direction.eulerAngles.z + offset);
		}
		//Debug.Log("Quaternion Rotation x = " + direction.eulerAngles.x);
		//Debug.Log("Quaternion Rotation y = " + direction.eulerAngles.y);
		Debug.Log("Quaternion Rotation z = " + direction.eulerAngles.z);
		
		newBullet = Instantiate(bullet, origin, direction);
		newBullet.SetActive(true);
	}
}
