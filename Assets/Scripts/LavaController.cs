using UnityEngine;
using System.Collections;

public class LavaController : MonoBehaviour {

    int damage = 100;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision hit)
    {
        if (hit.collider.tag == "Player")
        {
            hit.collider.GetComponent<StatsController>().InformServerAboutDamage(damage);
            // TODO Bounce the player whenever they take damage, both resets the collision
            // and looks neat ;p though breaks if the player somehow lands flat without
            // bouncing (hits their head or something).
            hit.collider.GetComponent<Rigidbody>().AddForce(hit.transform.up * 125, ForceMode.Force);
        }
    }

}
