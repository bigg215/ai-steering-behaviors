﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehavior : MonoBehaviour
{
    public float Mass = 15;
    public float MaxVelocity = 3;
    public float MaxForce = 15;

    private Vector3 velocity;
    public Transform target;

    private void Start()
    {
        velocity = Vector3.zero;
    }

    private void Update()
    {
        Vector3 desiredVelocity = target.transform.position - transform.position;
        desiredVelocity = desiredVelocity.normalized * MaxVelocity;

        Vector3 steering = desiredVelocity - velocity;
        steering = Vector3.ClampMagnitude(steering, MaxForce);
        steering /= Mass;

        velocity = Vector3.ClampMagnitude(velocity + steering, MaxVelocity);
        
        transform.position += velocity * Time.deltaTime;
        transform.right = velocity.normalized;

        Debug.DrawRay(transform.position, velocity.normalized * 2, Color.green);
        Debug.DrawRay(transform.position, desiredVelocity.normalized * 2, Color.magenta);

    }
}