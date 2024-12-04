using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerMovement player;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        transform.localScale = new Vector2(Mathf.Sign(player.transform.localScale.x), 1f);
    }
}
