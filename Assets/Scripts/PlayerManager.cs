using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerManager : NetworkBehaviour
{
    [SerializeField]
    private GameObject player;

	void Start ()
    {
	    if (isLocalPlayer)
        {
            Camera playerCamera = player.GetComponentInChildren<Camera>();
            playerCamera.enabled = true;
            playerCamera.GetComponent<AudioListener>().enabled = true;

            player.GetComponent<CharacterMovement>().enabled = true;
            player.GetComponent<EquipmentController>().enabled = true;
        }
        
        GameObject[] weaponSpawners = GameObject.FindGameObjectsWithTag("WeaponSpawner");
        for (int i = 0; i < weaponSpawners.Length; i++)
        {
            weaponSpawners[i].GetComponent<PickupPedestal>().SpawnConnectedGearClient();
        }
	}
}
