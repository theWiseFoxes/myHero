using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectJumpBehaviour : MonoBehaviour
{

    //better jump
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    Rigidbody2D myRigidbody;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (myRigidbody.velocity.y < 0)
        {
            myRigidbody.velocity = Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
    }


}
