using UnityEngine;
using System.Collections;

namespace ObjectMarkup
{

    public class Marker : MonoBehaviour
    {
        public string BoneType;

        public Vector3 GetPosition()
        {
            return gameObject.transform.position;
        }

        public Quaternion GetRotation()
        {
            return gameObject.transform.rotation;
        }
    }
}
