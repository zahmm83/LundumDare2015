using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class WeaponController : NetworkBehaviour {

    public Vector3 positionOffSet = Vector3.zero;
    public GameObject projectile;

    public virtual void FireWeapon(GameObject shooter)
    {
        Debug.Log("Default Fire Behaviour");
    }
}
