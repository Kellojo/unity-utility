using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kellojo.Environment {
    [System.Serializable]
    public class Spawnable {
        public GameObject Prefab;

        public float minScale = 1f;
        public float maxScale = 1f;

        public int maxCount = 5;


        public GameObject Instantiate() {
            GameObject obj = Object.Instantiate(Prefab);
            float scale = Random.Range(minScale, maxScale);
            obj.transform.localScale = new Vector3(scale, scale, scale);

            return obj;
        }

    }
}
