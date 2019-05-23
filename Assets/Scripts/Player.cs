using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] float runSpeed = 7f;
    [SerializeField] float jumpSpeed = 13f;
    [SerializeField] float timeDieDeelay = 0.5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);

    float moveInput;
    public float jumpInput;
    //float jetPackActivation;
	bool isAlive = true;
    float gravityScaleAtStart;
    //Extra Jump
    private int extraJumps;
    public int extraJumpValue = 1;


    Rigidbody2D myRigidbody;
    Animator myAnimator;
	CapsuleCollider2D myBodyCollider;
	BoxCollider2D myFeet;

    void Start(){
		myRigidbody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		myBodyCollider = GetComponent<CapsuleCollider2D>();
		myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;

        extraJumps = extraJumpValue;

    }

	void Update(){

        moveInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetAxis("Jump");
        //jetPackActivation = Input.GetAxis("Jetpack");

        if (!isAlive) {return;}
		Run();
        Jump();
        FlipSprite();

    }

    void Run(){
        //Get the horizontal axis
        float controlThrow = moveInput;

		//Create the velocity
		Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
		myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);

	}

    void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        {
            extraJumps = extraJumpValue; 
            //return;
        }

        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            //Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            //myRigidbody.velocity += jumpVelocityToAdd;
            myRigidbody.velocity = Vector2.up * jumpSpeed;
            extraJumps--;
        }else if (Input.GetButtonDown("Jump") && extraJumps == 0 && myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myRigidbody.velocity = Vector2.up * jumpSpeed;
        }

    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    //private void Die()
    //{
    //    if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")))
    //    {
    //        isAlive = false;
    //        myAnimator.SetTrigger("DieAnimation");
    //        PerformDieAnimation();
    //        GetComponent<Rigidbody2D>().velocity = deathKick;
    //        FindObjectOfType<GameSession>().ProcessPlayerDeath();

    //    }
    //}

    //private void PerformDieAnimation()
    //{
    //    Time.timeScale = timeDieDeelay;
    //    yield return new WaitForSecondsRealtime(timeDieDeelay);
    //    Time.timeScale = 1f;
    //}
}

/*
If player jumps change the jump variable to true. If player touches the floor change the variable to false. Lets add
    */
