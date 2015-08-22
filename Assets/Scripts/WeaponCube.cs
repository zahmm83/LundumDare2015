using UnityEngine;
using System.Collections;

public class WeaponCube : WeaponController {


	// Use this for initialization
	void Awake ()
    {
        this.positionOffSet = new Vector3(0.6f, 0.3f, 0.25f);
    }
	

    public override void FireWeapon(GameObject shooter)
    {
        GameObject firedShot = Instantiate(this.projectile);
        firedShot.GetComponent<ProjectileController>().Initialize(this.gameObject);
        Debug.Log("This is a cube.. I guess you could throw it but it won't fire.");
    }
}
