using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : PickupItem
{
    public int damageAmount = 20;

    protected override void PickUpAction()
    {
        base.PickUpAction();
        PlayerHealth playerHealth = _player.GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(damageAmount);
    }
}
