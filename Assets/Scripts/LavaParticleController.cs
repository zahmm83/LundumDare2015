using UnityEngine;
using System.Collections;

public class LavaParticleController : MonoBehaviour
{
    public GameObject lavaParticles;
    private Vector3 offset = new Vector3(0, -1, 0);

    void OnCollisionEnter(Collision lava)
    {
        if (lava.gameObject.tag.Equals("Lava"))
        {
            GameObject particles = Instantiate(lavaParticles, transform.position - offset, Quaternion.identity) as GameObject;
            Destroy(particles, 3);
        }
    }
}
