using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerDeath : NetworkBehaviour {

    StatsController playerHealthScript;
    
	void Start () {
        this.playerHealthScript = GetComponent<StatsController>();
        playerHealthScript.EventDie += DisablePlayer;
	}

    void OnDisable()
    {
        playerHealthScript.EventDie -= DisablePlayer;
    }
	
    void DisablePlayer()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }

        playerHealthScript.isDead = true;
        
    }
}
