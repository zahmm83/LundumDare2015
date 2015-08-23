using UnityEngine;
using System.Collections;

public class WeaponGrenade : WeaponController {

    void Awake()
    {
        this.positionOffSet = new Vector3(0.6f, 0.22f, 0.5f);
    }
    
}
