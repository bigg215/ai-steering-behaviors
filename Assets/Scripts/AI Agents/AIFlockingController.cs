using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFlockingController : Singleton<AIFlockingController>
{
    protected AIFlockingController() { }

    public float wanderRadius = 60.0f;
    public float wanderDistance = 40.0f;
    public float wanderJitter = 5.0f;

    public float agentNeighborRadius = 4.0f;

    public float wanderCoeff = 1.0f;
    public float seperationCoeff = 1.0f;
    public float alignmentCoeff = 1.0f;
    public float cohesionCoeff = 1.0f;
    public float evadeCoeff = 1.0f;

    public float maxVelocity = 3.0f;
    public float maxForce = 15.0f;
    public float mass = 15.0f;
}
