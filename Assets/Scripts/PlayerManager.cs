using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerManager : NetworkBehaviour
{
    [SerializeField]
    private GameObject player;
    public bool isLocal = false;

	void Start ()
    {
	    if (isLocalPlayer)
        {
            Camera playerCamera = player.GetComponentInChildren<Camera>();
            playerCamera.enabled = true;
            playerCamera.GetComponent<AudioListener>().enabled = true;

            player.GetComponent<CharacterMovement>().enabled = true;
            player.GetComponent<EquipmentController>().enabled = true;
            player.GetComponent<StatsController>().enabled = true;
            isLocal = true;
        }
        
        GameObject[] weaponSpawners = GameObject.FindGameObjectsWithTag("WeaponSpawner");
        for (int i = 0; i < weaponSpawners.Length; i++)
        {
            PickupPedestal pedestal = weaponSpawners[i].GetComponent<PickupPedestal>();
            if(pedestal != null)
            {
                pedestal.SpawnConnectedGearClient();
            }
        }
	}
}
