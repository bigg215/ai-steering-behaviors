using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWaypointFollower : SteeringBehavior
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        Steering(FollowPath());
    }
}
