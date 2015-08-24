using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using ObjectMarkup;
using System;

public class ProjectileController : NetworkBehaviour {

    // Base movement variables
    protected Vector3 direction = Vector3.zero;
    public float speed = 15.0f;
    public float force = 10.0f;

    // Trajectory control variables
    protected Vector3 startingPosition = Vector3.zero; // regular coordinates
    protected Vector3 relativeMidpoint = Vector3.zero; // (distance, height, curvature)
    protected Vector3 relativeEndpoint = Vector3.zero; // (distance, height, curvature)

    protected GameObject firedFrom;


    [SyncVar]
    float dirx;
    [SyncVar]
    float diry;
    [SyncVar]
    float dirz;


    // Despawn variables
    public float lifeTime = 10.0f;
    float timer = 0.0f;

    public string shooterId = "";

    void FixedUpdate ()
    {
        timer += Time.deltaTime;
        
        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        Move();
    }

    //void OnCollisionEnter(Collision hit)
    //{
    //    StatsController playerStats = hit.gameObject.GetComponent<StatsController>();
    //    if (playerStats != null)
    //    {
    //        playerStats.AddPlayerHitId(shooterId);
    //    }

    //    // Always handle collision, even if not hitting a player.
    //    HandleCollision(hit.collider);
    //}

    void OnTriggerEnter(Collider target)
    {
        StatsController playerStats = target.gameObject.GetComponent<StatsController>();
        if (playerStats != null)
        {
            playerStats.AddPlayerHitId(shooterId);
        }

        HandleCollision(target);
    }


    public virtual void HandleCollision(Collider target)
    {
        HandleGettingHit hitHandler = target.gameObject.GetComponent<HandleGettingHit>();
        if (hitHandler != null)
        {
            hitHandler.GetHit(force, transform.position - direction);
            Destroy(gameObject);
        }
    }

    // Default move behaviour, follow the trajectory as mapped out by the trajectory control.
    // Default trajectory control is just "go straight."
    public virtual void Move()
    {
        // Clear out projectiles for weapons that have been destroyed.
        if(firedFrom == null)
        {
            Destroy(gameObject);
            return;
        }

        float distanceTraveled = Vector3.Distance(transform.position, startingPosition);

        if (distanceTraveled < relativeMidpoint.x)
        {
            // Move toward the mid point
            float percentageDistanceTraveled = 0;
            if (relativeEndpoint.x - relativeMidpoint.x != 0)
            {
                percentageDistanceTraveled = distanceTraveled / relativeMidpoint.x;
            }
            // Invert the arc by using 1-% instead of %
            Vector3 heightAdjustment = firedFrom.transform.up * (1 - percentageDistanceTraveled) * relativeMidpoint.y;
            Vector3 curveAdjustment = firedFrom.transform.right * (1 - percentageDistanceTraveled) * relativeMidpoint.z;

            transform.position += ((direction + heightAdjustment + curveAdjustment) * speed * Time.deltaTime);
        }
        else
        {
            // Move toward the end point
            float percentageDistanceTraveled = 0;
            if (relativeEndpoint.x - relativeMidpoint.x != 0)
            {
                percentageDistanceTraveled = distanceTraveled / (relativeEndpoint.x - relativeMidpoint.x);
            }
            // Don't want to invert the arc on the way down
            Vector3 heightAdjustment = firedFrom.transform.up * (percentageDistanceTraveled) * (relativeEndpoint.y - relativeMidpoint.y);
            Vector3 curveAdjustment = firedFrom.transform.right * (percentageDistanceTraveled) * (relativeEndpoint.z - relativeMidpoint.z);

            transform.position += ((direction + heightAdjustment + curveAdjustment) * speed * Time.deltaTime);
        }
    }

    // Default initialization, set the direction as weapon forward and position the same as the weapon.
    public virtual void Initialize(GameObject shooter)
    {
        name = "Projectile " + Guid.NewGuid().ToString().Substring(0, 5);
        firedFrom = shooter;
        startingPosition = firedFrom.transform.position;
        direction = shooter.transform.forward;
        direction.y = 0;
        
        direction = Quaternion.AngleAxis(shooter.GetComponentInChildren<CharacterMovement>().shootAngle, shooter.transform.right) * direction;

        var marker_list = shooter.transform.root.GetComponentsInChildren<Marker>();
        Marker spawn_loc = null;
        foreach (var marker in marker_list)
        {
            if (marker.BoneType == "fire_spawn") { spawn_loc = marker; }
        }

        transform.position = spawn_loc.GetPosition() + (spawn_loc.gameObject.transform.forward * 0.6f);
    }
}
