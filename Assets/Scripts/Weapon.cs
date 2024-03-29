﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float feedbackAmount = 10f;
    [HideInInspector] public PlayerController _player;

    public virtual void Awake()
    {
        _player = transform.root.GetComponent<PlayerController>();
    }

    public virtual void Start()
    {

    }
    
    // Update is called once per frame
	public virtual void Update ()
    {
    
    }

    public void Knockback()
    {
        _player.feedback(feedbackAmount);
    }

    public virtual void WeaponUse()
    {
        Knockback();
    }

    public virtual void ReloadWeapon() { }

}
