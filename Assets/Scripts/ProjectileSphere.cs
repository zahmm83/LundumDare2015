using UnityEngine;
using System.Collections;

public class ProjectileSphere : ProjectileController
{

	// Use this for initialization
	void Awake () {
        this.speed = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += (this.direction * this.speed);
	}
}
