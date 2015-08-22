using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StatsController : NetworkBehaviour {

    [SyncVar (hook = "OnHealthChange")]
    public int health = 500;
    Text healthText;

    //private bool shouldDie = false;
    //public bool isDead = false;

    //public delegate void DieDelegate();
    //public event DieDelegate EventDie;
    

	void Start () {
        this.healthText = GameObject.Find("Health Text").GetComponent<Text>();
        SetHealthText();
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
            this.healthText.text = "Health: " + this.health.ToString();
        }
    }

    public void InformServerAboutDamage(int damage)
    {
        //this.health -= damage;
        if (isLocalPlayer)
        {
            CmdTellServerYouTookDamage(this.name, damage);
        }

        //SetHealthText();

        //if (this.health <= 0)
        //{
        //    CmdTellServerYouDied(this.gameObject);
        //}
    }

    void TakeDamage(int damage)
    {
        this.health -= damage;
    }

    public void PlayerDied()
    {
        Debug.Log("Hey now, you're supposed to be dead... stop running around.");
    }

    [Command]
    void CmdTellServerYouTookDamage(string uniqueId, int damage)
    {
        GameObject player = GameObject.Find(uniqueId);
        player.GetComponent<StatsController>().TakeDamage(damage);
    }

}
