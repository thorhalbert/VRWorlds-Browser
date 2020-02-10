using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWorlds.Browser.Avatar
{
    public class AvatarController : MonoBehaviour
    {
        public float CharacterHeight = 1.8f;
        public float EyeHeight = 1.8f - .1f;
        public GameObject CameraRig = null;

        // Start is called before the first frame update
        void Start()
        {
            var trans = CameraRig.GetComponent<Transform>();
            trans.transform.SetPositionAndRotation(new Vector3(0, EyeHeight), new Quaternion());
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}