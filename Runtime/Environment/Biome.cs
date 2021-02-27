using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kellojo.Environment {

    [CreateAssetMenu(menuName = "Kellojo/Enrionment/Biome")]
    public class Biome : ScriptableObject {

        public new string name;
        public List<Spawnable> spawnables;

    }

}

