using UnityEngine;
using System.Collections;

public class StatsController : MonoBehaviour {

    public int health = 500;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TakeDamage(int damage)
    {
        this.health -= damage;

        if(this.health <= 0)
        {
            PlayerDied();
        }
    }

    public void PlayerDied()
    {
        Debug.Log("Hey now, you're supposed to be dead... stop running around.");
    }

}
