using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ProjectileGrenade : ProjectileController
{

    public float blastRadius;

    void Awake()
    {

    }

    public override void AdditionalMotion()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 750, Space.World);
        transform.Rotate(Vector3.right * Time.deltaTime * 750, Space.World);
        transform.Rotate(Vector3.forward * Time.deltaTime * 750, Space.World);
    }

    public override void HandleCollision(Collider hit)
    {
        List<Collider> targetColliders = Physics.OverlapSphere(transform.position, blastRadius).ToList();
        foreach(Collider collider in targetColliders)
        {
            Rigidbody target = collider.GetComponent<Rigidbody>();
            if(target != null)
            {
                target.AddExplosionForce(force, transform.position, blastRadius, 0, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }
}