using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

    protected Vector3 direction;
    protected float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void Initialize(GameObject weapon)
    {
        this.direction = weapon.transform.forward;
        this.transform.position = weapon.transform.position;
    }
}
