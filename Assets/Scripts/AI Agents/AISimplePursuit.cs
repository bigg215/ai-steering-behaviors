using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISimplePursuit : SteeringBehavior
{
    private Rigidbody2D rb2d;

    public override void Start()
    {
        base.Start();

        if (target != null)
        {
            rb2d = target.GetComponent<Rigidbody2D>();
        }
    }

    public override void Update()
    {
        base.Update();
        Steering(OffsetPursuit(rb2d, new Vector3(-2.0f, 0.0f)));
    }
}
