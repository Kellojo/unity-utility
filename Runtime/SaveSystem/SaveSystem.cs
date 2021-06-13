using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace Kellojo.SaveSystem {
    public class SaveSystem : MonoBehaviour {

        public static string SaveLocation {
            get {
                return Application.persistentDataPath + "/saves/";
            }
        }
        public static string SaveGameExtension {
            get {
                return ".savegame";
            }
        }
        public static string DEFAULT_SAVE_NAME = "Save #";

        /// <summary>
        /// Loads a given save game
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static SaveGame Load(string saveName) {
            EnsureSaveDirectoryExists();
            string file = GetSaveLocation(saveName);

            if (!File.Exists(file)) {
                return new SaveGame(saveName);
            } 

            using (FileStream stream = File.Open(file, FileMode.Open)) {
                var formatter = new BinaryFormatter();
                return (SaveGame)formatter.Deserialize(stream);
            }
        }
        /// <summary>
        /// Saves a given savegame
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="state"></param>
        public static void Save(SaveGame state) {
            EnsureSaveDirectoryExists();
            string file = GetSaveLocation(state.name);

            using (FileStream stream = File.Open(file, FileMode.Create)) {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }
        /// <summary>
        /// Ensures the save directory exists
        /// </summary>
        public static void EnsureSaveDirectoryExists() {
            if (!Directory.Exists(SaveLocation)) {
                Directory.CreateDirectory(SaveLocation);
            }
        }
        /// <summary>
        /// Get's all available save games
        /// </summary>
        /// <returns></returns>
        public static List<SaveGame> GetAllSaveGames() {
            List<SaveGame> saveGames = new List<SaveGame>();

            string[] files = Directory.GetFiles(SaveLocation);
            foreach (string file in files) {
                saveGames.Add(Load(Path.GetFileNameWithoutExtension(file)));
            }

            return saveGames;
        }
        public static string GetSaveLocation(string saveGameName) {
            return SaveLocation + saveGameName + SaveGameExtension;
        }

        public static SaveGame CreateNewSaveGame() {
            var saveGame = GetAllSaveGames();
            return new SaveGame(DEFAULT_SAVE_NAME + saveGame.Count);
        }

        void CaptureState(SaveGame saveGame) {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>()) {
                saveGame.gameState[saveable.Id] = saveable.CaptureState();
            }
        }
        public static void RestoreState(SaveGame saveGame) {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>()) {

                if (saveGame.gameState.TryGetValue(saveable.Id, out object value)) {
                    saveable.RestoreState(value);
                }
            }
        }


        [ContextMenu("Perform Test Saving")]
        void PerformTestSave() {
            var saveGame = Load(DEFAULT_SAVE_NAME);
            CaptureState(saveGame);
            Save(saveGame);
        }
        [ContextMenu("Perform Test Loading")]
        void PerformTestLoad() {
            var saveGame = Load(DEFAULT_SAVE_NAME);
            RestoreState(saveGame);
        }

    }

}

