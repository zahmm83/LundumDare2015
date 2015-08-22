using UnityEngine;
using System.Collections;

public class ProjectileSphere : ProjectileController
{

	// Use this for initialization
	void Awake () {
        this.speed = 5.0f;
        this.relativeMidpoint = new Vector3(8.0f, 8.0f, 12.0f);
        this.relativeEndpoint = new Vector3(20.0f, 4.0f, 6.0f);
    }
	
}
