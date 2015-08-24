using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PlayerManager : NetworkBehaviour
{
    [SerializeField]
    private GameObject player;
    public bool isLocal = false;
    [SyncVar(hook = "NameChange")]
    public string playerName;
    public string playerCharacter;
    public int numberOfPlayers = 0;
    private bool showDisconnectMenu = false;

	void Start ()
    {
        if (isLocalPlayer)
        {
            playerCharacter = GameObject.Find("NetworkManager").GetComponent<GameNetworkManager>().playerCharacter;
            playerName = GameObject.Find("NetworkManager").GetComponent<GameNetworkManager>().playerName;
            transform.FindChild("PlayerNameCanvas").GetComponent<Canvas>().enabled = false;
            CmdGiveServerPlayerName(playerName);

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
            if (pedestal != null)
            {
                pedestal.SpawnConnectedGearClient();
            }
        }
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            GameObject[] currentPlayers = GameObject.FindGameObjectsWithTag("Player");
            if (currentPlayers.Length != numberOfPlayers)
            {
                for (int i = 0; i < currentPlayers.Length; i++)
                {
                    GameObject currentPlayer = currentPlayers[i];
                    currentPlayer.transform.FindChild("PlayerNameCanvas").GetComponent<PlayerNameLookAt>().targetPlayer = transform;
                }
                numberOfPlayers = currentPlayers.Length;
            }
        }

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

    [Command]
    void CmdGiveServerPlayerName(string name)
    {
        playerName = name;
    }

    void NameChange(string name)
    {
        playerName = name;
        transform.FindChild("PlayerNameCanvas").transform.FindChild("Text").GetComponent<Text>().text = name;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length - 1; i++)
        {
            PlayerManager currentPlayer = players[i].GetComponent<PlayerManager>();
            string currentPlayerName = currentPlayer.playerName;
            players[i].transform.FindChild("PlayerNameCanvas").transform.FindChild("Text").GetComponent<Text>().text = currentPlayerName;

            //players[i].transform.FindChild("PlayerNameCanvas").GetComponent<PlayerNameLookAt>().targetPlayer = transform;
        }
    }
}
