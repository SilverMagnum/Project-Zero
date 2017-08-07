using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMechanics : MonoBehaviour {

	public float speed			=	50f;
	public int damagePerShot	=	20;
	
	private Rigidbody2D rb2d;		//This bullet's rigid body.
	private Vector2 movement;		//Applied this bullets position.

	// Use this for initialization
	void Start () {
		//Gets the rigid body from this bullet.
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		//Multiplies the bullets direction with 1 unit of distance.
		movement = transform.rotation * Vector2.right;
		
		//Ensures movement is still one unit then multiplies it by speed and time.
		movement = movement.normalized * speed * Time.deltaTime;
		
		//Moves the bullet based on its previous position plus movement.
		rb2d.MovePosition(rb2d.position + movement);
	}
	
	//When this bullet collides with something...
	public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag != "Player") //Check if other collider is not attached to the player
        {
            //Destroys the bullet on impact.
            Destroy(gameObject);
        }

		//--IMPORTANT-->>Reference code for when bullets cause damage to enemies.
		//--IMPORTANT-->>Code should be invoked before bullet destruction.
		//Debug.Log("I'm working!");
		//GameObject something = other.gameObject;
		//EnemyHealth enemyHealth = something.GetComponent <EnemyHealth> ();
		//if(enemyHealth != null) {
			//Debug.Log("BulletMechanics issued a TakeDamage command!");
		//	enemyHealth.TakeDamage(damagePerShot);
		//}
		
		//--IMPORTANT-->>Reference code for doorways and the player...
		//SceneManager.LoadScene(nextScenePath, LoadSceneMode.Single);
    }
}
