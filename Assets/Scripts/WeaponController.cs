using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class WeaponController : NetworkBehaviour {

    public Vector3 positionOffSet = Vector3.zero;
    public GameObject projectile;

    public float cooldown;
    
    public virtual void FireWeapon(GameObject shooter)
    {
        GameObject firedShot = Instantiate(projectile);
        firedShot.GetComponent<ProjectileController>().shooterId = shooter.name;
        firedShot.GetComponent<ProjectileController>().Initialize(shooter);
    }
}
