using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Linq;

public class ScoringController : NetworkBehaviour {

    [SyncVar(hook = "SetScoreText")]
    string scores;

    [SyncVar(hook = "SetTimerText")]
    string timeLeft;
    public float gameTimer = 5.0f;

    Text scoreText;
    Text timeText;

    public AudioClip gameOverAudio;
    bool GameIsOver = false;

    void Awake()
    {
        scoreText = GameObject.Find("Scores").GetComponent<Text>();
        timeText = GameObject.Find("Time Left").GetComponent<Text>();
        scores = "Show me some scores";
        timeLeft = "Game Timer";
    }
	
	void FixedUpdate ()
    {
        if (!GameIsOver)
        {
            scores = GenerateScoreText();
            timeLeft = GenerateTimeText();
        }
    }

    public string GenerateScoreText()
    {
        string text = "";

        List< KeyValuePair<string, int> > scoreList = new List< KeyValuePair<string, int> >();

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            PlayerManager playerManager = player.GetComponent<PlayerManager>();
            StatsController playerStats = player.GetComponent<StatsController>();
            if (playerStats != null && playerManager != null)
            {
                scoreList.Add(new KeyValuePair<string, int>(playerManager.playerName, playerStats.playerScore));
                //text += playerManager.playerName + ": " + playerStats.playerScore + "\n";
            }
        }

        scoreList.Sort((x, y) => y.Value.CompareTo(x.Value));

        foreach(KeyValuePair<string, int> score in scoreList)
        {
            text += score.Key + ": " + score.Value + "\n";
        }

        return text;
    }

    public string GenerateTimeText()
    {
        gameTimer -= Time.deltaTime;
        int minutesLeft = (int)Mathf.Floor(gameTimer / 60);
        int secondsLeft = (int)Mathf.Floor(gameTimer - minutesLeft * 60);

        string displayText = "99:99";
        if (gameTimer > 0)
        {
            displayText = minutesLeft.ToString() + ":" + (secondsLeft < 10 ? "0" + secondsLeft.ToString() : secondsLeft.ToString());
        }
        else
        {
            GameIsOver = true;
            Camera.main.GetComponent<AudioSource>().clip = gameOverAudio;
            Camera.main.GetComponent<AudioSource>().Play();
            displayText = "The winner is...\n";
            string topPlayer = "";
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                int topScore = 0;
                StatsController playerStats = player.GetComponent<StatsController>();
                if (playerStats != null && playerStats.playerScore > topScore)
                {
                    // Not the nicest way to get this text, but the fastest :)
                    string playerType = player.GetComponent<PlayerManager>().playerCharacter == "triceratops" ? "Triceratot" : "Square Monster";
                    topScore = playerStats.playerScore;
                    topPlayer = player.GetComponent<PlayerManager>().playerName + " the " + playerType + " (" + topScore + ")";
                }
            }
            displayText += topPlayer;
        }
        return displayText;
    }

    public void SetScoreText(string scores)
    {
        this.scores = scores;
        scoreText.text = scores;
    }

    public void SetTimerText(string timeLeft)
    {
        this.timeLeft = timeLeft;
        timeText.text = timeLeft;
    }

}
