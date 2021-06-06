using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kellojo.Utility {
    public class ConstantYRotator : MonoBehaviour {
        [SerializeField] public float Speed = 1f;


        void Update() {
            transform.Rotate(Vector3.up, Speed * Time.deltaTime);
        }
    }

}

