using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kellojo.Items {

    [CreateAssetMenu(menuName = "Kellojo/Item")]
    public class Item : ScriptableObject {

        public new string name;
        [SerializeField] protected GameObject Prefab;
        [SerializeField] public float BurnTime = 0;

        /// <summary>
        /// Instantiates a visual instance of the item
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public GameObject InstantiateInstance(Vector3 position, Quaternion rotation, Transform parent) {
            GameObject obj = Instantiate(Prefab);
            obj.transform.SetParent(parent);
            obj.transform.position = position;
            obj.transform.rotation = rotation;

            return obj;
        }
    }

}

