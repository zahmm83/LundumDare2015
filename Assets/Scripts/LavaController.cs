using UnityEngine;
using System.Collections;

public class LavaController : MonoBehaviour {

    int damage = 20;

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
            hit.collider.GetComponent<StatsController>().TakeDamage(damage);
            Debug.Log("You sir are stepping in lava..");
        }
    }

}
