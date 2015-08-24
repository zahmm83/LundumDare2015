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
        NetworkManager.singleton.onlineScene = "MainGame";
        SetPlayerName();
        NetworkManager.singleton.StartHost();
    }

    public void StartSingleplayer()
    {
        NetworkManager.singleton.onlineScene = "Testbed";
        NetworkManager.singleton.StartHost();
    }
    
    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        if (NetworkManager.singleton.networkAddress.Length > 0)
        {
            SetPlayerName();
            NetworkManager.singleton.onlineScene = "MainGame";
            NetworkManager.singleton.StartClient();
        }
    }

    public void SetPlayerName()
    {
        string name = GameObject.Find("PlayerName").transform.FindChild("Text").GetComponent<Text>().text;
        if (name == null || name == "")
            playerName = "Player";
        else
            playerName = name;
    }

    public void SetCharacterSelection(Button button)
    {
        GameObject.Find("Triceratot").GetComponent<Image>().color = Color.white;
        GameObject.Find("Squarebeast").GetComponent<Image>().color = Color.white;
        
        button.GetComponent<Image>().color = Color.red;

        if (button.name.Equals("Triceratot"))
            playerCharacter = "triceratops";
        if (button.name.Equals("Squarebeast"))
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

    void OnLevelWasLoaded(int level)
    {
        if (level != 0)
            SetupGameSceneButton();
    }

    void SetupGameSceneButton()
    {
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
    }
}
