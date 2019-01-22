using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : PickupItem
{
    public AmmoType ammo;
    public int ammoAmount = 20;

    protected override void PickUpAction()
    {
        base.PickUpAction();
        AmmoInventory ammoInventory = _player.GetComponent<AmmoInventory>();
        ammoInventory.Collect(ammo, ammoAmount);
    }
}
