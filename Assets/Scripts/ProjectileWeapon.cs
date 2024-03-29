﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileWeapon : Weapon {

    private FiniteStateMachine<ProjectileWeapon> FSM;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public AmmoType ammoType;
    public int ammoPerShot = 1;
    public int projectileCount = 1;
    public float projectileDeviationAngle = 0.0f;
    public int magazineSize = 15;
    public float reloadTime = 1.0f;
    public float fireRate = 0.0f;
    private Text ammoHUD;
    private Text magazineHUD;

    [HideInInspector] public bool hasFired;
    [HideInInspector] public bool isReloading;
    [HideInInspector] public int currentMagazine = 0;
    [HideInInspector] public float lastFired;
    [HideInInspector] public AmmoInventory ammoInventory;

    public bool Fire(AmmoInventory ammo)
    {
        int shotsfired = ammo.Spend(ammoType, ammoPerShot);
        return shotsfired > 0;
    }

    public int Reload(AmmoInventory ammo)
    {
       
        int ammoRequested = magazineSize - currentMagazine;
        int loadMagazine = ammo.Spend(ammoType, ammoRequested);

        return loadMagazine;
    }

    public override void Awake()
    {
        base.Awake();
   
        ammoInventory = transform.root.gameObject.GetComponent<AmmoInventory>();
        ammoHUD = GameObject.Find("/HUDCanvas/AmmoUI/AmmoDisplay").GetComponent<Text>();
        magazineHUD = GameObject.Find("/HUDCanvas/AmmoUI/MagazineDisplay").GetComponent<Text>();
        FSM = new FiniteStateMachine<ProjectileWeapon>();
        FSM.Configure(this, WeaponIdleState.Instance);

        Debug.Log("Weapon Attached. Awake");
    }

    public void ChangeState(FSMState<ProjectileWeapon> e)
    {
        FSM.ChangeState(e);
    }

    public override void Start()
    {
        base.Start();

        hasFired = false;
        isReloading = false;
        this.ReloadWeapon();

        ammoHUD.enabled = true;
        magazineHUD.enabled = true;

        this.UpdateAmmoCounts();
    }

    private void OnDestroy()
    {
        ammoInventory.Collect(ammoType, currentMagazine);
    }

    public void UpdateAmmoCounts()
    {
        ammoHUD.text = ammoInventory.GetStock(ammoType).ToString() + " / " + ammoInventory.GetMax(ammoType).ToString();
        magazineHUD.text = currentMagazine.ToString();
    }

    public override void Update()
    {
        base.Update();

        FSM.Update();

        this.UpdateAmmoCounts();
    }

    public override void WeaponUse()
    {
        base.WeaponUse();

        Shoot();        
    }

    public override void ReloadWeapon()
    {
        base.ReloadWeapon();

        currentMagazine += Reload(ammoInventory);
    }

    public void Shoot()
    {
        for (int i = 0; i < projectileCount; i++)
        {

            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, z: Random.Range(-projectileDeviationAngle, projectileDeviationAngle)));

        }
        currentMagazine -= ammoPerShot;
    }

    public IEnumerator ReloadWeaponTimer()
    {
        this.isReloading = true;

        yield return new WaitForSeconds(reloadTime);
        this.ReloadWeapon();
        this.ChangeState(WeaponIdleState.Instance);
        yield return new WaitForSeconds(0.1f);
        
    }
}
