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

    ////JetPack
    //[SerializeField] private float jetPackForce = 3f;
    //[SerializeField] private float rotationSpeed = 3f;
    //[SerializeField] private float normalizeRotationSpeed = 3f;
    //[SerializeField] private Transform groundPosition;

    float moveInput;
    float jetPackActivation;
	bool isAlive = true;
    float gravityScaleAtStart;
    ////Jetpack
    //bool isJump = false;
    //private float timer = 0;

    Rigidbody2D myRigidbody;
    Animator myAnimator;
	CapsuleCollider2D myBodyCollider;
	BoxCollider2D myFeet;

    //Jetpack
    //private Collider2D[] isGrounded = new Collider2D[1];


    void Start(){
		myRigidbody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		myBodyCollider = GetComponent<CapsuleCollider2D>();
		myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;

	}

	void Update(){

        moveInput = Input.GetAxis("Horizontal");
        jetPackActivation = Input.GetAxis("Jetpack");
		if(!isAlive) {return;}
		Run();
        Jump();
        FlipSprite();

    }

    //private void FixedUpdate()
    //{
    //    isGrounded[0] = null;
    //    //Physics2D.OverlapBoxNonAlloc(groundPosition.position, new Vector2(myFeet))

    //    if (!isJump)
    //    {
    //        myRigidbody.velocity = new Vector2(moveInput * runSpeed, myRigidbody.velocity.y);
    //    }else if (isJump)
    //    {
    //        Vector3 rotation = new Vector3(0, 0, -moveInput * rotationSpeed);

    //        myRigidbody.freezeRotation = false;
    //        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * normalizeRotationSpeed);
    //        myRigidbody.AddForce(transform.rotation * Vector2.up * jetPackForce);

    //        //decrease fuel amount

    //    }
    //}

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
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return;}

        if (Input.GetButtonDown("Jump"))
        {
            //Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            //myRigidbody.velocity += jumpVelocityToAdd;
            GetComponent < Rigidbody2D>().velocity = Vector2.up * jumpSpeed;
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

    //private void Flight()
    //{
    //    if (!isJump)
    //    {

    //    }
    //}

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
