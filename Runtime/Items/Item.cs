using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Kellojo.Items {

    [CreateAssetMenu(menuName = "Kellojo/Item")]
    public class Item : ScriptableObject {

        public new string name;
        [SerializeField] public Sprite Icon;
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

        /// <summary>
        /// Get's all items available in the project
        /// </summary>
        /// <returns></returns>
        public static Item[] GetAllItems() {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(Item).Name);  //FindAssets uses tags check documentation for more info
            Item[] a = new Item[guids.Length];
            for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<Item>(path);
            }

            return a;
        }
    }

}

