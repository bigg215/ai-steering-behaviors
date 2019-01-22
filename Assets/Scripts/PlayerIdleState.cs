using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerIdleState : FSMState<PlayerController>
{
    static readonly PlayerIdleState instance = new PlayerIdleState();

    public static PlayerIdleState Instance
    {
        get
        {
            return instance;
        }
    }
    static PlayerIdleState() { }
    private PlayerIdleState() { }

    public override void Enter(PlayerController e)
    {
        Debug.Log("FSM Entered: Player Idle");
    }

    public override void Execute(PlayerController e)
    {
        e.MovePlayer(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            e.ChangeState(PlayerSprintState.Instance);
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            e.ChangeState(PlayerDashState.Instance);
        }
    }

    public override void Exit(PlayerController e)
    {

    }
}
