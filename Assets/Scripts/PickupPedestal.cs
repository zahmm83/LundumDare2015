using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PickupPedestal : NetworkBehaviour {
    [SyncVar(hook = "SpawnNewGear")]
    public int indexConnectedGear;
    public GameObject connectedGear;
    public List<GameObject> potentialGear;
    public float respawnTime = 2.0f;
    float timer = 0.0f;
    Vector3 gearRelativePosition = new Vector3(0.0f, 0.5f, 0.0f);

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
            indexConnectedGear = -1;
            connectedGear = null;
        }
    }

    void SpawnGear()
    {
        if (isServer)
        {
            int randomIndex = Random.Range(0, potentialGear.Count);
            CmdGetConnectedGear(randomIndex);
        }
    }

    [Command]
    void CmdGetConnectedGear(int index)
    {
        indexConnectedGear = index;
        //NetworkServer.Spawn(connectedGear);
    }


    public void SpawnConnectedGearClient()
    {
        if (connectedGear != null)
        {
            Destroy(connectedGear);
        }
        connectedGear = Instantiate(potentialGear[indexConnectedGear], transform.position + gearRelativePosition, Quaternion.identity) as GameObject;
        connectedGear.transform.Rotate(Vector3.right * -30, Space.World);
    }

    public void SpawnNewGear(int indexConnectedGear)
    {
        if (indexConnectedGear == -1)
        {
            // This only happens cause we need to force-change the index value, it happens on
            // pickup of the item and we dont want the weapon spawning yet so just get out.
            return;
        }
        if (connectedGear != null)
        {
            Destroy(connectedGear);
        }
        connectedGear = Instantiate(potentialGear[indexConnectedGear], transform.position + gearRelativePosition, Quaternion.identity) as GameObject;
        connectedGear.transform.Rotate(Vector3.right * -30, Space.World);
    }
}
