using UnityEngine;
using System.Collections.Generic;

public class PickupPedestal : MonoBehaviour {

    public GameObject connectedGear;
    public List<GameObject> potentialGear;
    public float respawnTime = 2.0f;
    float timer = 0.0f;
    Vector3 gearRelativePosition = new Vector3(0.0f, 1.0f, 0.0f);

    void Start () {
        SpawnGear();
    }
	
    
	void Update () {
        if(connectedGear == null)
        {
            timer += Time.deltaTime;
        } 

        if(timer > respawnTime)
        {
            timer = 0.0f;
            SpawnGear();
        }

        if(connectedGear != null)
        {
            connectedGear.transform.Rotate(Vector3.up * Time.deltaTime * 10, Space.World);
        }
	}


    void OnCollisionEnter(Collision hit)
    {
        if (connectedGear != null && hit.collider.tag == "Player")
        {
            hit.collider.GetComponent<EquipmentController>().PickupGear(connectedGear);
            connectedGear = null;
        }
    }

    void SpawnGear()
    {
        int randomIndex = Random.Range(0, potentialGear.Count);
        GameObject instantiatedGear = Instantiate(potentialGear[randomIndex]);
        instantiatedGear.transform.Rotate(Vector3.right * -30, Space.World);
        //instantiatedGear.transform.parent = this.transform;
        instantiatedGear.transform.position = this.transform.position + gearRelativePosition;
        //instantiatedGear.transform.localEulerAngles = gearRelativeAngles;
        connectedGear = instantiatedGear;
    }

}
