using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerSprintState : FSMState<PlayerController>
{
    static readonly PlayerSprintState instance = new PlayerSprintState();

    public static PlayerSprintState Instance
    {
        get
        {
            return instance;
        }
    }
    static PlayerSprintState() { }
    private PlayerSprintState() { }

    public override void Enter(PlayerController e)
    {
        Debug.Log("FSM Entered: Player Sprint");
    }

    public override void Execute(PlayerController e)
    {
        if (!e.isSprinting)
        {
            e.SprintPlayer(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            e.isSprinting = true;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            e.ChangeState(PlayerIdleState.Instance);
        }
    }

    public override void Exit(PlayerController e)
    {
        e.isSprinting = false;
    }
}
