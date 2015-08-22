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

        this.pulseTimer += Time.deltaTime;
        if (this.pulseTimer > this.pulseFrequency)
        {
            this.pulseTimer = 0.0f;
            List<Collider> targetColliders = Physics.OverlapSphere(this.transform.position, pullRadius).ToList();
            foreach (Collider collider in targetColliders)
            {
                Rigidbody target = collider.GetComponent<Rigidbody>();
                if (target != null)
                {
                    target.AddExplosionForce(this.force, this.transform.position, pullRadius);
                }
            }
        }

        this.lifeTimer += Time.deltaTime;
        if (this.lifeTimer > this.lifeTime)
        {
            Destroy(this.gameObject);
        }
        

    }
}
