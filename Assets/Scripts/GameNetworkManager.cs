using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class GameNetworkManager : NetworkManager
{
    public string playerName = "Player";
    public string playerCharacter = "triceratops";

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
        {
            NetworkManager.singleton.StartClient();
        }
    }

    public void SetPlayerName()
    {
        playerName = GameObject.Find("PlayerName").transform.FindChild("Text").GetComponent<Text>().text;
    }

    public void SetCharacterSelection(Button button)
    {
        GameObject.Find("ButtonCharacter1").GetComponent<Image>().color = Color.white;
        GameObject.Find("ButtonCharacter2").GetComponent<Image>().color = Color.white;

        button.GetComponent<Image>().color = Color.magenta;

        if (button.name.Equals("ButtonCharacter1"))
            playerCharacter = "triceratops";
        if (button.name.Equals("ButtonCharacter2"))
            playerCharacter = "monster";
    }

    private void SetIPAddress()
    {
        string ipAddress = GameObject.Find("InputIPAddress").transform.FindChild("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAddress;
    }

    private void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    void OnLevelWasLoaded (int level)
    {
        //Menu has to always has to have the index 0 in build settings
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

        GameObject.Find("ButtonPlayerName").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonPlayerName").GetComponent<Button>().onClick.AddListener(SetPlayerName);
    }

    void SetupGameSceneButton()
    {
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
    }
}
