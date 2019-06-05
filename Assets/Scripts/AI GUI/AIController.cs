using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private float hWanderRadiusValue;
    private float hWanderDistanceValue;
    private float hWanderJitterValue;
    private float hNeighborhoodRadius;
    private float hWanderCoeff;
    private float hSeperationCoeff;
    private float hAlignmentCoeff;
    private float hCohesionCoeff;
    private float hEvadeCoeff;
    private float hMass;
    private float hMaxForce;
    private float hMaxVelocity;
    
    void Start()
    {
        hWanderRadiusValue = AIFlockingController.Instance.wanderRadius;
        hWanderDistanceValue = AIFlockingController.Instance.wanderDistance;
        hWanderJitterValue = AIFlockingController.Instance.wanderJitter;
        hNeighborhoodRadius = AIFlockingController.Instance.agentNeighborRadius;

        hWanderCoeff = AIFlockingController.Instance.wanderCoeff;
        hSeperationCoeff = AIFlockingController.Instance.seperationCoeff;
        hAlignmentCoeff = AIFlockingController.Instance.alignmentCoeff;
        hCohesionCoeff = AIFlockingController.Instance.cohesionCoeff;
        hEvadeCoeff = AIFlockingController.Instance.evadeCoeff;

        hMass = AIFlockingController.Instance.mass;
        hMaxForce = AIFlockingController.Instance.maxForce;
        hMaxVelocity = AIFlockingController.Instance.maxVelocity;
    }

    void OnGUI()
    {
        GUI.Box(new Rect(5, 5, 175, 185), "Wander Control");

        GUI.Label(new Rect(10, 25, 150, 30), "Wander Radius: " + hWanderRadiusValue.ToString("0.00"));
        hWanderRadiusValue = GUI.HorizontalSlider(new Rect(10, 55, 165, 30), hWanderRadiusValue, 0.0f, 100.0f);

        GUI.Label(new Rect(10, 85, 175, 30), "Wander Distance: " + hWanderDistanceValue.ToString("0.00"));
        hWanderDistanceValue = GUI.HorizontalSlider(new Rect(10, 115, 165, 30), hWanderDistanceValue, 0.0f, 100.0f);

        GUI.Label(new Rect(10, 145, 150, 30), "Wander Jitter: " + hWanderJitterValue.ToString("0.00"));
        hWanderJitterValue = GUI.HorizontalSlider(new Rect(10, 175, 165, 30), hWanderJitterValue, 0.0f, 100.0f);

        GUI.Box(new Rect(5, 195, 175, 65), "Neighboorhood Radius");

        GUI.Label(new Rect(10, 215, 150, 30), "Agent Radius: " + hNeighborhoodRadius.ToString("0.00"));
        hNeighborhoodRadius = GUI.HorizontalSlider(new Rect(10, 245, 165, 30), hNeighborhoodRadius, 0.0f, 20.0f);

        GUI.Box(new Rect(5, 265, 175, 305), "Steering Coefficients");

        GUI.Label(new Rect(10, 285, 150, 30), "Wander: " + hWanderCoeff.ToString("0.00"));
        hWanderCoeff = GUI.HorizontalSlider(new Rect(10, 315, 165, 30), hWanderCoeff, 0.0f, 5.0f);

        GUI.Label(new Rect(10, 345, 150, 30), "Seperation: " + hSeperationCoeff.ToString("0.00"));
        hSeperationCoeff = GUI.HorizontalSlider(new Rect(10, 375, 165, 30), hSeperationCoeff, 0.0f, 5.0f);

        GUI.Label(new Rect(10, 405, 150, 30), "Alignment: " + hAlignmentCoeff.ToString("0.00"));
        hAlignmentCoeff = GUI.HorizontalSlider(new Rect(10, 435, 165, 30), hAlignmentCoeff, 0.0f, 5.0f);

        GUI.Label(new Rect(10, 465, 150, 30), "Cohesion: " + hCohesionCoeff.ToString("0.00"));
        hCohesionCoeff = GUI.HorizontalSlider(new Rect(10, 495, 165, 30), hCohesionCoeff, 0.0f, 5.0f);

        GUI.Label(new Rect(10, 525, 150, 30), "Evade: " + hEvadeCoeff.ToString("0.00"));
        hEvadeCoeff = GUI.HorizontalSlider(new Rect(10, 555, 165, 30), hEvadeCoeff, 0.0f, 5.0f);

        GUI.Box(new Rect(5, 575, 175, 185), "Max Velocity / Force");

        GUI.Label(new Rect(10, 595, 150, 30), "Mass: " + hMass.ToString("0.00"));
        hMass = GUI.HorizontalSlider(new Rect(10, 625, 165, 30), hMass, 1.0f, 25.0f);

        GUI.Label(new Rect(10, 655, 150, 30), "Max Velocity: " + hMaxVelocity.ToString("0.00"));
        hMaxVelocity = GUI.HorizontalSlider(new Rect(10, 685, 165, 30), hMaxVelocity, 1.0f, 25.0f);

        GUI.Label(new Rect(10, 715, 150, 30), "Max Force: " + hMaxForce.ToString("0.00"));
        hMaxForce = GUI.HorizontalSlider(new Rect(10, 745, 165, 30), hMaxForce, 1.0f, 50.0f);


        AIFlockingController.Instance.wanderRadius = hWanderRadiusValue;
        AIFlockingController.Instance.wanderDistance = hWanderDistanceValue;
        AIFlockingController.Instance.wanderJitter = hWanderJitterValue;
        AIFlockingController.Instance.agentNeighborRadius = hNeighborhoodRadius;

        AIFlockingController.Instance.wanderCoeff = hWanderCoeff;
        AIFlockingController.Instance.seperationCoeff = hSeperationCoeff;
        AIFlockingController.Instance.alignmentCoeff = hAlignmentCoeff;
        AIFlockingController.Instance.cohesionCoeff = hCohesionCoeff;
        AIFlockingController.Instance.evadeCoeff = hEvadeCoeff;

        AIFlockingController.Instance.mass = hMass;
        AIFlockingController.Instance.maxVelocity = hMaxVelocity;
        AIFlockingController.Instance.maxForce = hMaxForce;
    }
}
