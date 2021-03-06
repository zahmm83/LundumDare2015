﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerRespawn : NetworkBehaviour {

    private StatsController playerHealthScript;
    
	void Start () {
        playerHealthScript = GetComponent<StatsController>();
        playerHealthScript.EventRespawn += EnablePlayer;
    }
    void OnDisable()
    {
        playerHealthScript.EventRespawn -= EnablePlayer;
    }

    void EnablePlayer()
    {
        GameObject[] spawnLocations = GameObject.FindGameObjectsWithTag("Respawn");
        int randomIndex = Random.Range(0, spawnLocations.Length);

        transform.position = spawnLocations[randomIndex].transform.position;
        if (isLocalPlayer)
            transform.FindChild("PlayerNameCanvas").GetComponent<Canvas>().enabled = false;
        else
            transform.FindChild("PlayerNameCanvas").GetComponent<Canvas>().enabled = true;

        PlayerManager manager = GetComponent<PlayerManager>();
        manager.ActivatePlayerRenderers(manager.playerCharacter);
        //Renderer[] renderers = GetComponentsInChildren<Renderer>();
        //foreach (Renderer renderer in renderers)
        //{
        //    renderer.enabled = true;
        //}

        playerHealthScript.isDead = false;
    }
}
