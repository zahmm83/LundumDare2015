using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class GameNetworkManager : NetworkManager
{
    public void StartupHost()
    {
        SetPort();
        NetworkManager.singleton.StartHost();
    }

    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        if (NetworkManager.singleton.networkAddress.Length > 0)
            NetworkManager.singleton.StartClient();
    }

    private void SetIPAddress()
    {
        string ipAddress = GameObject.Find("InputIPAddress").transform.FindChild("Text").GetComponent<Text>().text;
        if (ipAddress != null || ipAddress.Length > 0)
            NetworkManager.singleton.networkAddress = ipAddress;
    }

    private void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    void OnLevelWasLoaded (int level)
    {
        //Menu has to always has to have the index 0
        if (level == 0)
        {
            SetupMenuSceneButton();
        }
        else
        {
            SetupGameSceneButton();
        }
    }

    void SetupMenuSceneButton()
    {
        GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.AddListener(StartupHost);

        GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.AddListener(JoinGame);
    }

    void SetupGameSceneButton()
    {
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
    }
}
