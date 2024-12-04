using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Rigidbody2D myrigidbody;
    BoxCollider2D myboxcollider;
    Transform mytransform;
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        myboxcollider = GetComponent<BoxCollider2D>();
        mytransform = GetComponent<Transform>();
    }

    void Update()
    {
        myrigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            moveSpeed = -moveSpeed;
            FlipEnemyFacing();
        }
    }


    void FlipEnemyFacing()
    {
        mytransform.localScale = new Vector2(-(Mathf.Sign(myrigidbody.velocity.x)), 1f);
    }
}
