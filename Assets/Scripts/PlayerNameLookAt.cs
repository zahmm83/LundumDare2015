using UnityEngine;
using System.Collections;

public class PlayerNameLookAt : MonoBehaviour
{
    public Transform targetPlayer;
    private Vector3 offset = new Vector3(0, 1, 0);

	void Update ()
    {
	    if (targetPlayer != null)
        {
            transform.rotation = Quaternion.LookRotation((transform.position - targetPlayer.position) - offset);
        }
	}
}
