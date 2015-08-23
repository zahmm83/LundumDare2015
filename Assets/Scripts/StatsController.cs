using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Linq;

public class StatsController : NetworkBehaviour {

    [SyncVar (hook = "OnHealthChange")]
    public int health = 500;
    Text healthText;

    bool shouldDie = false;
    bool shouldNotDie { get { return !shouldDie; } }
    public bool isDead = false;
    public bool isNotDead { get { return !isDead; } }

    public delegate void DieDelegate();
    public event DieDelegate EventDie;

    public delegate void RespawnDelegate();
    public event RespawnDelegate EventRespawn;

    float respawnTime = 10.0f;
    float respawnTimer = 0.0f;

    float lavaImmunityDuration = 0.5f;
    float lavaImmunityTimer = 0.0f;
    bool immuneToLava = false;
    int lavaDamage = 50;
    

    [SyncVar]
    public int playerScore = 1000;
    public List<string> playersWhoHitMe = new List<string>();

    // Built in functions
    void Start ()
    {
        healthText = GameObject.Find("Health Text").GetComponent<Text>();
        SetHealthText();
	}
	
    void Update()
    {
        if(immuneToLava)
        {
            lavaImmunityTimer += Time.deltaTime;
            immuneToLava = lavaImmunityTimer < lavaImmunityDuration;
        }

        CheckDeathCondition();
        if (isDead)
        {
            RespawnUpdate();
        }
    }

    void OnTriggerStay()
    {
        if (!immuneToLava)
        {
            InformServerAboutDamage(lavaDamage);
            lavaImmunityTimer = 0.0f;
            immuneToLava = true;
        }
    }

    // Score Functions
    public void AddPlayerHitId(string playerId)
    {
        if (isLocalPlayer)
        {
            CmdInformServerWhoHit(playerId);
        }
    }

    [Command]
    void CmdInformServerWhoHit(string playerId)
    {
        playersWhoHitMe.Add(playerId);
    }

    // Death Functions
    void CheckDeathCondition()
    {
        if (health <= 0 && isNotDead && shouldNotDie)
        {
            if (EventDie != null)
            {
                playerScore -= 100;
                if (isServer)
                {
                    float numberOfHitters = playersWhoHitMe.Count;
                    List<string> distinctHitters = playersWhoHitMe.Distinct().ToList();
                    foreach(string hitter in distinctHitters)
                    {
                        float numberOfHits = playersWhoHitMe.Where(id => id == hitter).ToList().Count;
                        float scorePercentage = numberOfHits / numberOfHitters;
                        int scoreChange = (int)Mathf.Ceil(scorePercentage * 100);
                        GameObject player = GameObject.Find(hitter);
                        player.GetComponent<StatsController>().playerScore += scoreChange;
                    }
                }
                EventDie();
            }
        }
    }

    void CheckRespawnCondition()
    {
        if (health > 0 && isDead)
        {
            if (EventRespawn != null)
            {
                respawnTimer = 0.0f;
                EventRespawn();
            }
        }
    }
    
    // Respawn Functions
    void RespawnUpdate()
    {
        respawnTimer += Time.deltaTime;
        SetHealthText();
        if(respawnTimer > respawnTime)
        {
            CmdRespawnOnServer();
            CheckRespawnCondition();
        }
    }
    
    // Damage and health Functions
    public void InformServerAboutDamage(int damage)
    {
        if (isLocalPlayer)
        {
            CmdTellServerYouTookDamage(damage);
        }
    }

    void OnHealthChange(int health)
    {
        this.health = health;
        SetHealthText();
    }

    void SetHealthText()
    {
        if (isLocalPlayer)
        {
            healthText.text = health > 0? "Health: " + health.ToString() : "Respawning in " + Mathf.Ceil(respawnTime - respawnTimer);
        }
    }
    

    [Command]
    void CmdTellServerYouTookDamage(int damage)
    {
        health -= damage;
    }
    
    [Command]
    void CmdRespawnOnServer()
    {
        health = 500;
    }

}
