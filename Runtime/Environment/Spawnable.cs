using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kellojo.Environment {
    [System.Serializable]
    public class Spawnable {
        public GameObject Prefab;

        public Vector2 Scale = new Vector2(1, 1);
        public Vector2 countRange = new Vector2(1, 1);



        public GameObject Instantiate() {
            GameObject obj = Object.Instantiate(Prefab);
            float scale = Random.Range(Scale.x, Scale.y);
            obj.transform.localScale = new Vector3(scale, scale, scale);

            return obj;
        }

        public int GetCount() {
            return Mathf.RoundToInt(Random.Range(countRange.x, countRange.y));
        }

    }
}
