using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using ObjectMarkup;

public class EquipmentController : NetworkBehaviour {

    public GameObject startingGearMain;
    public GameObject startingGearSecondary;

    public GameObject equipedGearMain;
    public GameObject equipedGearSecondary;

    [SyncVar (hook = "Fire")]
    bool fireWeapon;
    bool canFire = true;

    float weaponCooldownTimer = 0.0f;

    // Poor Michael :(
    void Awake () {
	    if(startingGearMain != null)
        {
            GameObject instantiatedGear = Instantiate(startingGearMain);
            PickupGear(instantiatedGear);
        }
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
            && isLocalPlayer)
        {
            canFire = false;
            CmdTellServerYouAreShooting();
        }
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

        var marker_list = transform.root.GetComponentsInChildren<Marker>();
        Marker grip = null;
        foreach(var marker in marker_list)
        {
            if(marker.BoneType == "right_grip") { grip = marker; }
        }

        Marker grip_attach = gear.GetComponentInChildren<Marker>();

        equipedGearMain = gear;
        equipedGearMain.transform.parent = grip.transform;
        grip_attach.transform.rotation = grip.GetRotation();
        grip_attach.transform.position = grip.GetPosition();
    }
}
