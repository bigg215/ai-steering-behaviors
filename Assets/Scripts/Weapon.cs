using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float feedbackAmount = 10f;
    private PlayerController player;

    public virtual void Start()
    {

    }
    
    // Update is called once per frame
	public virtual void Update ()
    {
    
    }

    public void Knockback()
    {
        player = transform.root.GetComponent<PlayerController>();
        player.feedback(feedbackAmount);
    }

    public virtual void WeaponUse()
    {
        Knockback();
    }

    public virtual void ReloadWeapon() { }

}
