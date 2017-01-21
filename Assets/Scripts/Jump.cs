﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    bool grounded, jumped;

    SpriteRenderer turtle;

    // Use this for initialization
    void Start () {
        grounded = true;
        jumped = false;

        turtle = GetComponent<SpriteRenderer>();
    }
    
    // Update is called once per frame
    void FixedUpdate () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1000f, 1 << 8);

        // grounded?
        if (hit.distance < 3f)
            grounded = true;
        else
            grounded = false;

        

        // jump
        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
            GetComponent<Rigidbody2D> ().AddForce (transform.up * 2000);

        // move left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.Translate(-.5f, 0, 0);
            turtle.flipX = true;
        }

        // move right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.Translate(.5f, 0, 0);
            turtle.flipX = false;
        }

        NormalizeSlope();
    }

    void NormalizeSlope()
    {
        // Attempt vertical normalization
        if (grounded)
        {
            int layerMask = 1 << 8;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1000f, layerMask);

            if (hit.collider != null && Mathf.Abs(hit.normal.x) > 0.1f)
            {
                Transform pos = GetComponent<Transform>();
                Rigidbody2D body = GetComponent<Rigidbody2D>();

                float friction = 2f;

                // Apply the opposite force against the slope force 
                // You will need to provide your own slopeFriction to stabalize movement
                body.velocity = new Vector2(body.velocity.x - (hit.normal.x * friction), body.velocity.y);

                //Move Player up or down to compensate for the slope below them
            }
        }
    }
}