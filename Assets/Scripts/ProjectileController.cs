using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

    // Base movement variables
    protected Vector3 direction = Vector3.zero;
    protected float speed = 1.0f;

    // Trajectory control variables
    protected Vector3 startingPosition = Vector3.zero; // regular coordinates
    protected Vector3 relativeMidpoint = Vector3.zero; // (distance, height, curvature)
    protected Vector3 relativeEndpoint = Vector3.zero; // (distance, height, curvature)

    protected GameObject weaponFiredFrom;

    // Despawn variables
    float lifeTime = 10.0f;
    float timer = 0.0f;


    void FixedUpdate ()
    {
        this.timer += Time.deltaTime;
        
        if (this.timer > lifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        Move();
    }

    void OnCollisionEnter(Collision hit)
    {
        HandleCollision(hit);
    }

    public virtual void HandleCollision(Collision hit)
    {
        hit.
        Debug.Log(hit);
    }

    // Default move behaviour, follow the trajectory as mapped out by the trajectory control.
    // Default trajectory control is just "go straight."
    public virtual void Move()
    {
        // Clear out projectiles for weapons that have been destroyed.
        if(weaponFiredFrom == null)
        {
            Destroy(this.gameObject);
            return;
        }

        float distanceTraveled = Vector3.Distance(this.transform.position, this.startingPosition);

        if (distanceTraveled < relativeMidpoint.x)
        {
            // Move toward the mid point
            float percentageDistanceTraveled = 0;
            if (this.relativeEndpoint.x - relativeMidpoint.x != 0)
            {
                percentageDistanceTraveled = distanceTraveled / this.relativeMidpoint.x;
            }
            // Invert the arc by using 1-% instead of %
            Vector3 heightAdjustment = this.weaponFiredFrom.transform.up * (1 - percentageDistanceTraveled) * relativeMidpoint.y;
            Vector3 curveAdjustment = this.weaponFiredFrom.transform.right * (1 - percentageDistanceTraveled) * relativeMidpoint.z;
            //float curveAdjustment = (1 - percentageDistanceTraveled) * relativeMidpoint.z;

            //Vector3 directionModifier = new Vector3(0.0f, heightAdjustment, curveAdjustment);
            this.transform.position += ((this.direction + heightAdjustment + curveAdjustment) * this.speed * Time.deltaTime);
        }
        else
        {
            // Move toward the end point
            float percentageDistanceTraveled = 0;
            if (this.relativeEndpoint.x - relativeMidpoint.x != 0)
            {
                percentageDistanceTraveled = distanceTraveled / (this.relativeEndpoint.x - relativeMidpoint.x);
            }
            // Don't want to invert the arc on the way down
            Vector3 heightAdjustment = this.weaponFiredFrom.transform.up * (percentageDistanceTraveled) * (relativeEndpoint.y - relativeMidpoint.y);
            Vector3 curveAdjustment = this.weaponFiredFrom.transform.right * (percentageDistanceTraveled) * (relativeEndpoint.z - relativeMidpoint.z);

            //Vector3 directionModifier = new Vector3(0.0f, heightAdjustment, curveAdjustment);
            this.transform.position += ((this.direction + heightAdjustment + curveAdjustment) * this.speed * Time.deltaTime);
        }
    }

    // Default initialization, set the direction as weapon forward and position the same as the weapon.
    public virtual void Initialize(GameObject weapon)
    {
        this.weaponFiredFrom = weapon;
        this.startingPosition = weaponFiredFrom.transform.position;
        this.direction = weaponFiredFrom.transform.forward;
        this.transform.position = weaponFiredFrom.transform.position;
    }
}
