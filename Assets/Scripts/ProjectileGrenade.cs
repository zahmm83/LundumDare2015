using UnityEngine;
using System.Collections;

﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ProjectileGrenade : ProjectileController
{

    void Awake()
    {
        this.speed = 8.0f;
        this.relativeMidpoint = new Vector3(10.0f, 5.0f, 0.0f);
        this.relativeEndpoint = new Vector3(20.0f, 3.75f, 0.0f);
    }
}

    public override void HandleCollision(Collision hit)
    {
        List<Collider> targetColliders = Physics.OverlapSphere(this.transform.position, 30.0f).ToList();
        foreach(Collider collider in targetColliders)
        {
            Debug.Log(collider.tag);
        }
    }
}