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
        }
	}
}
