using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HandleGettingHit : NetworkBehaviour {

    [SyncVar(hook = "HandleHit")]
    bool gotHit;

    [SyncVar]
    float dirx;
    [SyncVar]
    float diry;
    [SyncVar]
    float dirz;
    [SyncVar]
    float force;

    public void GetHit(float hitForce, Vector3 hitFrom)
    {
        Vector3 direction = transform.position - hitFrom;

        dirx = direction.x;
        diry = direction.y;
        dirz = direction.z;
        force = hitForce;

        // hacky way to help ensure the direction and force are updated before handling the hit.
        Invoke("delayedHit", 0.1f);
    }

    void delayedHit()
    {
        gotHit = !gotHit;
    }

    public void HandleHit(bool hit)
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(dirx, diry, dirz) * force, ForceMode.Impulse);
    }
}
