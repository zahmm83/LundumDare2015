using UnityEngine;
using System.Collections;


namespace ObjectMarkup
{
    public class NoRender_Player : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            var manager = transform.root.GetComponent<PlayerManager>();

            if (manager.isLocalPlayer)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
            }
        }
    }
}
