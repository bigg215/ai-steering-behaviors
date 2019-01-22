﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 10f;
    private Rigidbody2D rb2d;
    private bool facingLeft = false;
    private bool facingRight = true;
    private Transform weaponFlip;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal > 0.5f)
        {
            if (facingLeft == true && facingRight == false)
            {
                transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
                weaponFlip = transform.Find("WeaponAttachment").GetChild(0).transform;
                if (weaponFlip != null)
                {
                    weaponFlip.localScale = Vector3.Scale(weaponFlip.localScale, new Vector3(-1, 1, 1));
                }
            }
            facingLeft = false;
            facingRight = true;
        }
        if (moveHorizontal < -0.5f)
        {
            if (facingLeft == false && facingRight == true)
            {
                transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
                weaponFlip = transform.Find("WeaponAttachment").GetChild(0).transform;
                if(weaponFlip != null)
                {
                    weaponFlip.localScale = Vector3.Scale(weaponFlip.localScale, new Vector3(-1, 1, 1));
                }
                
            }
            facingLeft = true;
            facingRight = false;
        }

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.velocity = movement * speed;

    }

    private void FixedUpdate()
    {
        

    }

    public void feedback(float strength)
    {
        rb2d.AddForce(-transform.right * strength);
    }

    public bool IsFlipped()
    {
        return facingLeft;
    }
}