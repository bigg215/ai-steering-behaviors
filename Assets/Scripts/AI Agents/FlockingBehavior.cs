using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingBehavior : SteeringBehavior
{

    List<Collider2D> currentNeighbors = new List<Collider2D>();

    private float cWander;
    private float cSeperation;
    private float cAlignment;
    private float cCohesion;
    private float cEvade;

    private new SpriteRenderer renderer;
    private Color intialColor = new Color(1.0f, 1.0f, 1.0f);
    private Color wanderColor = new Color(0.0f, 0.80f, 1.0f);
    private Color seperationColor = new Color(0.15f, 1.0f, 0.0f);
    private Color alignmentColor = new Color(1.0f, 0.54f, 0.0f);
    private Color cohesionColor = new Color(1.0f, 0.96f, 0.0f);
    private Color evadeColor = new Color(1.0f, 0.06f, 0.0f);

    public override void Start()
    {
        base.Start();
        Mass = AIFlockingController.Instance.mass;
        MaxVelocity = AIFlockingController.Instance.maxVelocity;
        MaxForce = AIFlockingController.Instance.maxForce;

        renderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        renderer.color = wanderColor;
    }

    public override void Update()
    {
        base.Update();
        Steering(Calculate());
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Mass = AIFlockingController.Instance.mass;
        MaxVelocity = AIFlockingController.Instance.maxVelocity;
        MaxForce = AIFlockingController.Instance.maxForce;
        currentNeighbors = FindNeighborAgents();
    }

    public Vector3 Calculate()
    {

        cWander = AIFlockingController.Instance.wanderCoeff;
        cSeperation = AIFlockingController.Instance.seperationCoeff;
        cAlignment = AIFlockingController.Instance.alignmentCoeff;
        cCohesion = AIFlockingController.Instance.cohesionCoeff;
        cEvade = AIFlockingController.Instance.evadeCoeff;

        _steeringForce = Vector3.zero;
        Vector3 force;

        force = Evade(target.GetComponent<Rigidbody2D>()) * cEvade;

        if(!AccumulateForce(force))
        {
            return _steeringForce;
        }

        force = Seperation(currentNeighbors) * cSeperation;

        if(!AccumulateForce(force))
        {
            return _steeringForce;
        }

        force = Alignment(currentNeighbors) * cAlignment;

        if(!AccumulateForce(force))
        {
            return _steeringForce;
        }

        force = Cohesion(currentNeighbors) * cCohesion;

        if(!AccumulateForce(force))
        {
            return _steeringForce;
        }

        force = Wander() * cWander;

        if(!AccumulateForce(force))
        {
            return _steeringForce;
        }

        return _steeringForce;
    }

    
}
