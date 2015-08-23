using UnityEngine;
using System.Collections;

public class WeaponSphere : WeaponController {
    

	// Use this for initialization
	void Awake ()
    {
        this.positionOffSet = new Vector3(0, 1.5f, 0);
    }
	
}
