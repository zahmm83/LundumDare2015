using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ProjectileGrenade : ProjectileController
{

    float blastRadius = 5.0f;

    void Awake()
    {
        speed = 8.0f;
        //relativeMidpoint = new Vector3(10.0f, 5.0f, 0.0f);
        //relativeEndpoint = new Vector3(20.0f, 3.75f, 0.0f);
        relativeMidpoint = Vector3.zero;
        relativeEndpoint = Vector3.zero;
        force = 500.0f;
    }


    public override void HandleCollision(Collider hit)
    {
        List<Collider> targetColliders = Physics.OverlapSphere(transform.position, blastRadius).ToList();
        foreach(Collider collider in targetColliders)
        {
            Rigidbody target = collider.GetComponent<Rigidbody>();
            if(target != null)
            {
                target.AddExplosionForce(force, transform.position, blastRadius);
            }
        }
    }
}