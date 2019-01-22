using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    protected GameObject _player;

    void OnTriggerEnter2D(Collider2D col)
    {
        _player = col.gameObject;
        PlayerController playerController = _player.GetComponent<PlayerController>();
        
        if (playerController != null)
        {
            
            PickUpAction();
            Destroy(this.gameObject);
        }
    }

    protected virtual void PickUpAction()
    {
        Debug.Log("Item Pickup Triggered");
    }
}
