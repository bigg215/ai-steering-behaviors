using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class WeaponReloadingState : FSMState<ProjectileWeapon>
{
    static readonly WeaponReloadingState instance = new WeaponReloadingState();

    public static WeaponReloadingState Instance
    {
        get
        {
            return instance;
        }
    }
    static WeaponReloadingState() { }
    private WeaponReloadingState() { }

    public override void Enter(ProjectileWeapon w)
    {
        Debug.Log("FSM Entered: Reloading");
    }

    public override void Execute(ProjectileWeapon w)
    {
        if(!w.isReloading)
        {
            w.StartCoroutine(w.ReloadWeaponTimer());
        }
        
    }

    public override void Exit(ProjectileWeapon w)
    {
        w.isReloading = false;
    }

    
}
