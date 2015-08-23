using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class WeaponCube : WeaponController {


	void Awake ()
    {
        this.positionOffSet = new Vector3(0.6f, 0.3f, 0.25f);
    }
	
    [Command]
    void CmdSpawnProjectileNetwork()
    {

    }
}
