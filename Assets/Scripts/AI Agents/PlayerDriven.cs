using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDriven : SteeringBehavior
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        Vector3 steeringInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Steering(steeringInput);
    }
}
