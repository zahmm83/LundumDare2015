using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

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

    void Start () {
        healthText = GameObject.Find("Health Text").GetComponent<Text>();
        SetHealthText();
	}
	
    void Update()
    {
        CheckDeathCondition();
        if (isDead)
        {
            RespawnUpdate();
        }
    }

    void CheckDeathCondition()
    {
        if (health <= 0 && isNotDead && shouldNotDie)
        {
            if (EventDie != null)
            {
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
