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

    // Use this for initialization
    void Awake () {
	    if(startingGearMain != null)
        {
            GameObject instantiatedGear = Instantiate(startingGearMain);
            PickupGear(instantiatedGear);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (equipedGearMain != null && Input.GetMouseButtonDown(0) && GetComponent<StatsController>().isNotDead)
        {
            if (isLocalPlayer)
            {
                CmdTellServerYouAreShooting();
            }
        }
	}

    [Command]
    void CmdTellServerYouAreShooting()
    {
        fireWeapon = !fireWeapon;
    }

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
