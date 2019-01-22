using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : PickupItem
{
    public GameObject weaponPickup;

    protected override void PickUpAction()
    {
        base.PickUpAction();
        WeaponController weapon = _player.GetComponent<WeaponController>();
        weapon.WeaponSwitch(weaponPickup);
    }
}
