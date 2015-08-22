using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class LavaController : NetworkBehaviour {

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
            //string playerUniqueId = hit.transform.name;
            //CmdTellServerWhoTookDamage(playerUniqueId, damage);
            hit.collider.GetComponent<StatsController>().InformServerAboutDamage(damage);
            //Debug.Log("You sir are stepping in lava..");
        }
    }

    //[Command]
    //void CmdTellServerWhoTookDamage (int damage)
    //{
    //    GameObject player = GameObject.Find(uniqueId);
    //    player.GetComponent<StatsController>().InformServerAboutDamage(damage);
    //}

}
