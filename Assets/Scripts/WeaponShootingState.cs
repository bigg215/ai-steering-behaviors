using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class WeaponShootingState : FSMState<ProjectileWeapon>
{
    static readonly WeaponShootingState instance = new WeaponShootingState();

    public static WeaponShootingState Instance
    {
        get
        {
            return instance;
        }
    }
    static WeaponShootingState() { }
    private WeaponShootingState() { }

    public override void Enter(ProjectileWeapon w)
    {
        Debug.Log("FSM Entered: Shooting");
    }

    public override void Execute(ProjectileWeapon w)
    {
        
        if (!w.hasFired)
        {
            w.WeaponUse();
            w.hasFired = true;
        }
       
        if (Input.GetButtonUp("Fire1"))
        {
            w.ChangeState(WeaponIdleState.Instance);
        }
        
    }

    public override void Exit(ProjectileWeapon w)
    {
        w.hasFired = false;
    }
}