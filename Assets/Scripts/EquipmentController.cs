﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using ObjectMarkup;
using UnityEngine.UI;

public class EquipmentController : NetworkBehaviour {

    public GameObject startingGearMain;
    public GameObject startingGearSecondary;

    public GameObject equipedGearMain;
    public GameObject equipedGearSecondary;

    public bool isInMenu = false;

    private float aim_timer = 0.0f;

    [SyncVar (hook = "Fire")]
    bool fireWeapon;
    
    //[SyncVar(hook = "HandleHit")]
    //bool hitTarget;

    //[SyncVar]
    //string hitId;

    //[SyncVar]
    //string projectileId;

    bool canFire = true;
    
    Animator anim { get { return transform.root.GetComponent<CharacterMovement>().anim; } }

    float weaponCooldownTimer = 0.0f;

    // Poor Michael :(
    void Awake () {
	    if(startingGearMain != null)
        {
            ResetGear();
        }
	}

    public void ResetGear()
    {
        GameObject instantiatedGear = Instantiate(startingGearMain);
        PickupGear(instantiatedGear);
    }
	
	void Update () {
        if (!canFire)
        {
            weaponCooldownTimer += Time.deltaTime;
            WeaponController weaponController = equipedGearMain.GetComponent<WeaponController>();
            if(weaponController != null && weaponCooldownTimer > weaponController.cooldown)
            {
                canFire = true;
                weaponCooldownTimer = 0.0f;
            }
        }

        if (Input.GetMouseButton(0) 
            && canFire 
            && equipedGearMain != null 
            && GetComponent<StatsController>().isNotDead
            && isLocalPlayer
            && !isInMenu)
            {
            canFire = false;
                CmdTellServerYouAreShooting();
            }

        if(aim_timer > 3) { anim.SetBool("shooting", false); }
        aim_timer += Time.deltaTime;

        anim.SetBool("has_weapon", equipedGearMain.GetComponent<Marker>().BoneType == "gun");
	}

    [Command]
    void CmdTellServerYouAreShooting()
    {
        // All we really care about is that the hook function gets run
        fireWeapon = !fireWeapon;
    }

    // This hook function will be run whenever the server updates the fireWeapon boolean.
    void Fire(bool fireWeapon)
    {
        WeaponController mainWeapon = equipedGearMain.GetComponent<WeaponController>();
        mainWeapon.FireWeapon(gameObject);
        anim.SetBool("shooting", true);
        aim_timer = 0.0f;
    }

    public void PickupGear(GameObject gear)
    {
        if (GetComponent<StatsController>().isDead)
        {
            // Dead people don't get weapons..
            return;        
        }

        if(equipedGearMain != null)
        {
            Destroy(equipedGearMain);
        }

        Marker grip = GetMarker(transform.root, "right_grip");
        Marker grip_attach = GetMarker(gear.transform.root, "grip_attach");
        Marker gun_type = GetMarker(gear.transform.root, "gun");

        equipedGearMain = gear;
        equipedGearMain.transform.parent = grip.transform;
        grip_attach.transform.rotation = grip.GetRotation();
        grip_attach.transform.position = grip.GetPosition();

        if (isLocalPlayer)
        {
            GameObject weaponIcon = GameObject.Find("WeaponIcon");

            if (gun_type != null)
            {
                weaponIcon.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("GunIcon");
            }
            else
            {
                weaponIcon.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("PillowIcon");
            }
        }
    }

    Marker GetMarker(Transform root, string name)
    {
        var marker_list = root.GetComponentsInChildren<Marker>();
        Marker return_marker = null;
        foreach (var marker in marker_list)
        {
            if (marker.BoneType == name) { return_marker = marker; }
        }
        return return_marker;
    }
}
