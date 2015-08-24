using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuButtons : MonoBehaviour
{
    public void OnLevelWasLoaded(int level)
    {
        //Menu has to always have the index 0 in build settings
        if (level == 0)
            SetupMenuButtons();
    }

    public void SetupMenuButtons()
    {
        GameNetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<GameNetworkManager>();

        GameObject.Find("ButtonSingleplayer").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonSingleplayer").GetComponent<Button>().onClick.AddListener(networkManager.StartSingleplayer);

        GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.AddListener(networkManager.StartupHost);

        GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.AddListener(networkManager.JoinGame);

        Button triceratot = GameObject.Find("Triceratot").GetComponent<Button>();
        triceratot.onClick.RemoveAllListeners();
        triceratot.onClick.AddListener(() => networkManager.SetCharacterSelection(triceratot));

        Button squareBeast = GameObject.Find("Squarebeast").GetComponent<Button>();
        squareBeast.onClick.RemoveAllListeners();
        squareBeast.onClick.AddListener(() => networkManager.SetCharacterSelection(squareBeast));
    }
}
