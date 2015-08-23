using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BlackHoleBehaviour : MonoBehaviour {

    float pulseFrequency = 1.0f;
    float pulseTimer = 1.1f;

    float lifeTime = 5.0f;
    float lifeTimer = 0.0f;

    float pullRadius = 10.0f;
    float force = -3000.0f;


    // Update is called once per frame
    void Update () {

        pulseTimer += Time.deltaTime;
        if (pulseTimer > pulseFrequency)
        {
            pulseTimer = 0.0f;
            List<Collider> targetColliders = Physics.OverlapSphere(transform.position, pullRadius).ToList();
            foreach (Collider collider in targetColliders)
            {
                Rigidbody target = collider.GetComponent<Rigidbody>();
                if (target != null)
                {
                    target.AddExplosionForce(force, transform.position, pullRadius);
                }
            }
        }

        lifeTimer += Time.deltaTime;
        if (lifeTimer > lifeTime)
        {
            Destroy(gameObject);
        }
        

    }
}
