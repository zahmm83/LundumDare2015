using UnityEngine;
using System.Collections;

public class EquipmentController : MonoBehaviour {

    public GameObject startingGearMain;
    public GameObject startingGearSecondary;

    public GameObject equipedGearMain;
    public GameObject equipedGearSecondary;

    // Use this for initialization
    void Start () {
	    if(startingGearMain != null)
        {
            GameObject instantiatedGear = Instantiate(startingGearMain);
            PickupGear(instantiatedGear);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (equipedGearMain != null && Input.GetMouseButtonDown(0))
        {
            WeaponController mainWeapon = equipedGearMain.GetComponent<WeaponController>();
            mainWeapon.FireWeapon(this.gameObject);
        }
	}

    public void PickupGear(GameObject gear)
    {
        if(equipedGearMain != null)
        {
            Destroy(equipedGearMain);
        }

        equipedGearMain = gear;
        equipedGearMain.transform.parent = this.transform;
        equipedGearMain.transform.forward = this.transform.forward;
        equipedGearMain.transform.localPosition = equipedGearMain.GetComponent<WeaponController>().positionOffSet;
    }


}
