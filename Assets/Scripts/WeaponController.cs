using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public Vector3 positionOffSet = Vector3.zero;
    public GameObject projectile;
    
    public virtual void FireWeapon(GameObject shooter)
    {
        GameObject firedShot = Instantiate(projectile);
        firedShot.GetComponent<ProjectileController>().Initialize(shooter);
    }
}
