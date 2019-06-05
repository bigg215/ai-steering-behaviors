using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Vector3[] points;
    public bool looped = false;

    private int currentWaypoint;
    [HideInInspector] public bool isFinished; 

    public void Awake()
    {
        currentWaypoint = 0;
        isFinished = false;
    }

    public Vector3 CurrentWaypoint()
    {
        return points[currentWaypoint];
    }

    public void SetNextWaypoint()
    {
        if(currentWaypoint < points.Length - 1)
        {
            currentWaypoint += 1;
        } else
        {
            if (looped)
            {
                currentWaypoint = 0;
            }
            else
            {
                isFinished = true;
            }
        }
    }

    public void AddWaypoint()
    {
        Vector3 point;

        if (points.Length == 0)
        {
            point = new Vector3(0.0f, 0.0f);
        } else
        {
            point = points[points.Length - 1];
            point.x += 1.0f;
        }

        Array.Resize(ref points, points.Length + 1);

        points[points.Length - 1] = point;
    }
}
