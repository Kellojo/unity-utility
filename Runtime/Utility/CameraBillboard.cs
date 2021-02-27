using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kellojo.Utility {
    public class CameraBillboard : MonoBehaviour {
        Transform camera;

        private void Awake() {
            camera = Camera.main.transform;
        }

        // Update is called once per frame
        void Update() {
            transform.rotation = Quaternion.LookRotation(transform.position - camera.position);
        }
    }

}