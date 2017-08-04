using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMechanics : MonoBehaviour {

	public float speed =		50f;
	public int damagePerShot =	20;
	
	private Rigidbody2D rb2d;
	private Vector2 movement;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		movement = transform.rotation * Vector2.right;
		movement = movement.normalized * speed * Time.deltaTime;
		rb2d.MovePosition(rb2d.position + movement);
	}
	
	void OnCollisionEnter2D(Collision2D other) {
		//Debug.Log("I'm working!");
		//GameObject something = other.gameObject;
		//EnemyHealth enemyHealth = something.GetComponent <EnemyHealth> ();
		//if(enemyHealth != null) {
			//Debug.Log("BulletMechanics issued a TakeDamage command!");
		//	enemyHealth.TakeDamage(damagePerShot);
		//}

		Destroy(gameObject);
		//SceneManager.LoadScene(nextScenePath, LoadSceneMode.Single);
    }
}
