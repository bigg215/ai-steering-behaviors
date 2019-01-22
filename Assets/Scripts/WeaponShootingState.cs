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
        
        if (!w.hasFired && w.fireRate == 0)
        {
            w.WeaponUse();
            w.hasFired = true;
        } else
        {
            if ((Time.time - w.lastFired > 1 / w.fireRate) && w.currentMagazine != 0)
            {
                w.lastFired = Time.time;
                w.WeaponUse();
            }
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