using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class WeaponIdleState : FSMState<ProjectileWeapon>
{
    static readonly WeaponIdleState instance = new WeaponIdleState();

    public static WeaponIdleState Instance
    {
        get
        {
            return instance;
        }
    }
    static WeaponIdleState() { }
    private WeaponIdleState() { }

    public override void Enter(ProjectileWeapon w)
    {
        Debug.Log("FSM Entered: Idle");
    }

    public override void Execute(ProjectileWeapon w)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (w.currentMagazine > 0)
            {
                if(!w._player.isSprinting && !w._player.isDashing)
                {
                    w.ChangeState(WeaponShootingState.Instance);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if ((w.currentMagazine < w.magazineSize) && (w.ammoInventory.GetStock(w.ammoType) > 0))
            {
                if(!w._player.isSprinting && !w._player.isDashing)
                {
                    w.ChangeState(WeaponReloadingState.Instance);
                }
            }
        }
    }

    public override void Exit(ProjectileWeapon w)
    {
        
    }
}
