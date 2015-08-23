using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CharacterId : NetworkBehaviour {

    [SyncVar]
    string playerUniqueIdentity;
    NetworkInstanceId playerNetId;
    Transform myTransform;

    public override void OnStartLocalPlayer()
    {
        GetNetIdentity();
        SetIdentity();
    }
            
    void Awake () {
        myTransform = transform;
	}
	
    void Update()
    {
        if (myTransform.name == "" || myTransform.name == "Player_Triceratops(Clone)")
        {
            SetIdentity();
        }
    }


    string MakeUniqueIdentity()
    {
        return "Player " + playerNetId.ToString();
    }
    
    void SetIdentity()
    {
        if (!isLocalPlayer)
        {
            myTransform.name = playerUniqueIdentity;
        }
        else
        {
            myTransform.name = MakeUniqueIdentity();
        }
    }

    [Client]
    void GetNetIdentity()
    {
        playerNetId = GetComponent<NetworkIdentity>().netId;
        CmdTellServerMyIdentity(MakeUniqueIdentity());
    }

    [Command]
    void CmdTellServerMyIdentity(string identity)
    {
        playerUniqueIdentity = identity;
    }
}
