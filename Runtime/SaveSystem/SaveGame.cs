using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kellojo.SaveSystem {
    [Serializable]
    public class SaveGame {
        public string name = "Save";
        public string image = string.Empty;
        public int score = 0;
        public DateTime lastPlayedOn = DateTime.Now;

        public Dictionary<string, object> gameState = new Dictionary<string, object>();

        public SaveGame(string name) {
            this.name = name;
        }
    }
}

