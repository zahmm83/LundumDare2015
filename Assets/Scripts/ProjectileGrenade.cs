using UnityEngine;
using System.Collections;

public class ProjectileGrenade : ProjectileController
{

    void Awake()
    {
        this.speed = 3.0f;
        this.relativeMidpoint = new Vector3(8.0f, 8.0f, 0.0f);
        this.relativeEndpoint = new Vector3(20.0f, 2.0f, 0.0f);
    }
    
}
