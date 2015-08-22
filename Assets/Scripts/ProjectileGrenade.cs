using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ProjectileGrenade : ProjectileController
{

    float blastRadius = 5.0f;

    void Awake()
    {
        this.speed = 8.0f;
        this.relativeMidpoint = new Vector3(10.0f, 5.0f, 0.0f);
        this.relativeEndpoint = new Vector3(20.0f, 3.75f, 0.0f);
        this.force = 500.0f;
    }


    public override void HandleCollision(Collision hit)
    {
        List<Collider> targetColliders = Physics.OverlapSphere(this.transform.position, blastRadius).ToList();
        foreach(Collider collider in targetColliders)
        {
            Rigidbody target = collider.GetComponent<Rigidbody>();
            if(target != null)
            {
                target.AddExplosionForce(this.force, this.transform.position, blastRadius);
            }
        }
    }

}
