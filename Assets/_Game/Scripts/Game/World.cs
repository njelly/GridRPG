using System;
using System.Collections.Generic;
using System.IO;
using Tofunaut.Core;
using Tofunaut.SharpUnity;
using UnityEngine;

namespace Tofunaut.GridRPG.Game
{
    public class World : SharpGameObject
    {
        public static string WorldStateSavePath { get { return Path.Combine(Application.streamingAssetsPath, "WorldState.txt"); } }

        private readonly WorldState _state;

        private Dictionary<IntVector2, Region> _coordToRegion;

        protected World(WorldState state) : base("World")
        {
            _state = state;

            _coordToRegion = new Dictionary<IntVector2, Region>();
        }

        public void Save(Action onComplete = null)
        {
            string path = WorldStateSavePath;
            string[] lines = { _state.ToString() };
            File.WriteAllLines(path, lines);
            Debug.Log($"saved WorldState data to {path}");
            onComplete?.Invoke();
        }

        public static World Load()
        {
            string path = WorldStateSavePath;

            if (File.Exists(path))
            {
                string data = File.ReadAllText(path).Trim();
                try
                {
                    WorldState worldState = Newtonsoft.Json.JsonConvert.DeserializeObject<WorldState>(data);
                    Debug.Log($"loaded saved WorldState, seed: {worldState.seed}");
                    return new World(worldState);
                }
                catch
                {
                    Debug.LogError("could not deserialize saved world state data");
                    return null;
                }
            }
            else
            {
                Debug.LogFormat($"Could not find file at {path}, so it will be created");

                // check to make sure the directory exists before trying to write a file to it
                if (!Directory.Exists(Application.streamingAssetsPath))
                {
                    Directory.CreateDirectory(Application.streamingAssetsPath);
                }

                WorldState worldState = new WorldState(UnityEngine.Random.Range(int.MinValue, int.MaxValue));
                World toReturn = new World(worldState);
                toReturn.Save();
                return toReturn;
            }
        }
    }
}