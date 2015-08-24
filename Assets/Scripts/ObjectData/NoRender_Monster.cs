using UnityEngine;
using System.Collections;


namespace ObjectMarkup
{

    public class NoRender_Monster : MonoBehaviour
    {
        public string doRenderName;

        // Use this for initialization
        void Start()
        {
            var manager = transform.root.GetComponent<PlayerManager>();
            if (manager != null)
            {
                doRenderName = manager.playerCharacter;
            }

            if(doRenderName != gameObject.GetComponent<RenderName>().renderName)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
            }
        }
    }
}
