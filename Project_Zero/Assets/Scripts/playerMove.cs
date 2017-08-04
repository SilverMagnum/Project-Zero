using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour {

    //Jumping Variables
    public int maxAirJumpNumber;
    public float jumpForce;
    public float airJumpForce;

    private int airJumpNumber;
    private bool grounded;
    private Vector2 speedV;

    //Running Variables
    public float walkSpeed;

    private Vector2 speedH;

    //Other Variables
    public GameObject hip;
    public AnimationClip walkAnim;
    public AnimationClip standAnim;

    private Animator hipAnim;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        hipAnim = hip.gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        airJumpNumber = maxAirJumpNumber;
        speedV.x = 0;
        speedH.y = 0;
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        grounded = true;
        airJumpNumber = maxAirJumpNumber;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        airJumpNumber = maxAirJumpNumber;
        grounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        grounded = false;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        speedH.x = (Input.GetAxisRaw("Horizontal") * walkSpeed);

        if (speedH.x == 0)
        {
            hipAnim.Play(standAnim.name);
        }
        else
        {
            hipAnim.Play(walkAnim.name);
        }

        rb.AddForce(speedH, ForceMode2D.Impulse);
	}

    private void Jump()
    {
        if(grounded == true)
        {
            speedV.y = jumpForce;
            rb.AddForce(speedV, ForceMode2D.Impulse);
        }
        else if (grounded == false && airJumpNumber > 0)
        {
            airJumpNumber -= 1;
            speedV.y = airJumpForce;
            rb.AddForce(speedV, ForceMode2D.Impulse);
        }
    }
}
