using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ProjectileSphere : ProjectileController
{
    public GameObject blackHole;

	// Use this for initialization
	void Awake () {
        speed = 5.0f;
        relativeMidpoint = new Vector3(8.0f, 8.0f, 12.0f);
        relativeEndpoint = new Vector3(20.0f, 4.0f, 6.0f);
    }


    public override void HandleCollision(Collision hit)
    {
        GameObject blackHole = Instantiate(this.blackHole);
        blackHole.transform.position = transform.position + new Vector3(0.0f, 1.5f, 0.0f);

        Destroy(gameObject);
    }
}
