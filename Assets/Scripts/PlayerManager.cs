using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PlayerManager : NetworkBehaviour
{
    [SerializeField]
    private GameObject player;
    public bool isLocal = false;
    private bool showDisconnectMenu = false;

	void Start ()
    {
	    if (isLocalPlayer)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Camera playerCamera = player.GetComponentInChildren<Camera>();
            playerCamera.enabled = true;
            playerCamera.GetComponent<AudioListener>().enabled = true;

            player.GetComponent<CharacterMovement>().enabled = true;
            player.GetComponent<EquipmentController>().enabled = true;
            //player.GetComponent<StatsController>().enabled = true;
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

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            showDisconnectMenu = !showDisconnectMenu;
            GameObject disconnectButton = GameObject.Find("ButtonDisconnect");

            if (showDisconnectMenu)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = showDisconnectMenu;

            disconnectButton.GetComponent<Image>().enabled = showDisconnectMenu;
            disconnectButton.GetComponent<Button>().enabled = showDisconnectMenu;
            disconnectButton.transform.FindChild("Text").GetComponent<Text>().enabled = showDisconnectMenu;
        }
    }
}
