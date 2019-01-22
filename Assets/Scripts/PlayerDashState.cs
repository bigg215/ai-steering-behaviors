using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerDashState : FSMState<PlayerController>
{
    static readonly PlayerDashState instance = new PlayerDashState();

    public static PlayerDashState Instance
    {
        get
        {
            return instance;
        }
    }
    static PlayerDashState() { }
    private PlayerDashState() { }

    public override void Enter(PlayerController e)
    {
        Debug.Log("FSM Entered: Player Dashing");
    }

    public override void Execute(PlayerController e)
    {
        if(!e.isDashing)
        {
            e.StartCoroutine(e.DashPlayer());
        }
    }

    public override void Exit(PlayerController e)
    {
        e.isDashing = false;
    }
}
