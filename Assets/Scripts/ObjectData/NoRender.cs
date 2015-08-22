using UnityEngine;
using System.Collections;


namespace ObjectMarkup
{
    public class NoRender : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
}
