using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public Transform attachmentPoint;
    public GameObject initialWeapon;
    private GameObject currentWeapon;

	// Use this for initialization
	void Start () {
        if (initialWeapon != null)
        {
            currentWeapon = Instantiate(initialWeapon, attachmentPoint.position, attachmentPoint.rotation, attachmentPoint.transform);
        }
    }
	
	// Update is called once per frame
	void Update () { }

    public void WeaponSwitch(GameObject newWeapon)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }
        
        currentWeapon = Instantiate(newWeapon, attachmentPoint.position, attachmentPoint.rotation, attachmentPoint.transform);
    }

}
