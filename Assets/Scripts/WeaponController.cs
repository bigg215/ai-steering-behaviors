using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public Transform attachmentPoint;
    public GameObject initialWeapon;
    private GameObject currentWeapon;
    private PlayerController _player;

    // Use this for initialization

    private void Awake()
    {
        _player = GetComponent<PlayerController>();
    }

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

        if (_player.IsFlipped())
        {
            currentWeapon.transform.localScale = Vector3.Scale(currentWeapon.transform.localScale, new Vector3(-1, 1, 1));
        }
    }

}
