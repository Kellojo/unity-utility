using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Kellojo.Items {

    [CreateAssetMenu(menuName = "Kellojo/Recepie")]
    public class Recepie : ScriptableObject {
        [SerializeField] public ERecepieTye RecepieTye = ERecepieTye.Crafting;
        [SerializeField] List<Item> Ingredients;
        [SerializeField] public float Duration = 5f;
        [SerializeField] public List<Item> Results;

        /// <summary>
        /// Get's all recepies available in the project
        /// </summary>
        /// <returns></returns>
        public static Recepie[] GetAllRecepies() {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(Recepie).Name);  //FindAssets uses tags check documentation for more info
            Recepie[] a = new Recepie[guids.Length];
            for (int i = 0; i < guids.Length; i++) {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<Recepie>(path);
            }

            return a;
        }
        /// <summary>
        /// Get's all recepies available in the project
        /// </summary>
        /// <returns></returns>
        public static Recepie[] GetAllRecepiesResultingIn(Item item) {
            List<Recepie> all = GetAllRecepies().ToList();
            return all.Where(recepie => recepie.Results.Contains(item)).ToArray();
        }

    }

}
