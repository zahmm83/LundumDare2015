using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StatsController : NetworkBehaviour {

    [SyncVar]
    public int health = 500;
    Text healthText;

	// Use this for initialization
	void Start () {
        this.healthText = GameObject.Find("Health Text").GetComponent<Text>();
        SetHealthText();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SetHealthText()
    {
        if (isLocalPlayer)
        {
            this.healthText.text = "Health: " + this.health.ToString();
        }
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
        SetHealthText();

        if (this.health <= 0)
        {
            CmdTellServerYouDied(this.gameObject);
        }
    }

    public void PlayerDied()
    {
        Debug.Log("Hey now, you're supposed to be dead... stop running around.");
    }

    [Command]
    void CmdTellServerYouDied(GameObject player)
    {
        
    }

}
