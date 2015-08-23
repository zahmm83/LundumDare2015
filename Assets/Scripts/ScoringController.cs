using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScoringController : NetworkBehaviour {

    [SyncVar (hook = "SetScoreText")]
    string scores;

    Text scoreText;

    // Use this for initialization
    void Awake()
    {
        scoreText = GameObject.Find("Scores").GetComponent<Text>();
        scores = "Show me some scores";
	}
	
	// Update is called once per frame
	void Update () {
        scores = GenerateScoreText();
    }

    public string GenerateScoreText()
    {
        string text = "";

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            StatsController playerStats = player.GetComponent<StatsController>();
            if(playerStats != null)
            {
                text += player.name + " - " + playerStats.playerScore + "\n";
            }
        }

        return text;
    }

    public void SetScoreText(string scores)
    {
        this.scores = scores;
        scoreText.text = scores;
    }

}
