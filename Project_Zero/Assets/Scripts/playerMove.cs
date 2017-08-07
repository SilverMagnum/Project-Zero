using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour {

    public int maxAirJumpNumber;	//Max number mid-air jumps possible.
    public float jumpForce;			//Force applied to player when they jump.
    public float airJumpForce;		//Force applied to player when they jump mid-air.
    public float walkSpeed;			//Force applied to player when they move left or right.
    public GameObject hip;			//Hierarchical superior to both legs.
    public AnimationClip walkAnim;	//Walking animation.
    public AnimationClip standAnim;	//Idle animation.
	
    private int airJumpNumber;		//How many air jumps have been used.
    private bool grounded;			//True if player is on the ground.
    private Vector2 speedV;			//Player's vertical speed.
    private Vector2 speedH;			//Player's horizontal speed.
    private Animator hipAnim;		//Animation state machine.
    private Rigidbody2D rb;			//PC's rigidbody component.

	// Use this for initialization
	void Start () {
        hipAnim = hip.gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        airJumpNumber = maxAirJumpNumber;
        speedV.x = 0;
        speedH.y = 0;
	}

    // Update is called once per frame
    void FixedUpdate () {
        if (Input.GetButtonDown("Jump")) {
            Jump();
        }

        speedH.x = (Input.GetAxisRaw("Horizontal") * walkSpeed);

        if (speedH.x == 0) {
            hipAnim.Play(standAnim.name);
        }
		
        else {
            hipAnim.Play(walkAnim.name);
        }

        rb.AddForce(speedH, ForceMode2D.Impulse);
	}
	
    void OnTriggerEnter2D(Collider2D collision) {
        grounded = true;
        airJumpNumber = maxAirJumpNumber;
    }

    void OnTriggerStay2D(Collider2D collision) {
        airJumpNumber = maxAirJumpNumber;
        grounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        grounded = false;
    }

    private void Jump() {
        if(grounded == true) {
            speedV.y = jumpForce;
            rb.AddForce(speedV, ForceMode2D.Impulse);
        }
		
        else if (grounded == false && airJumpNumber > 0) {
            airJumpNumber -= 1;
            speedV.y = airJumpForce;
            rb.AddForce(speedV, ForceMode2D.Impulse);
        }
    }
}
