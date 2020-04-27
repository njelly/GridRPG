using System.Collections.Generic;
using UnityEngine;

namespace Tofunaut.GridRPG.Game
{
    public static class SpriteAtlasManager
    {
        private static Sprite[] _sprites;

        public static bool IsLoaded => _sprites != null;

        public static void Load()
        {
            AppManager.AssetManager.LoadList("sprite_atlas", (bool successful, List<Sprite> payload) =>
            {
                if (successful)
                {
                    _sprites = payload.ToArray();
                }
            });
        }

        public static Sprite Get(int index)
        {
            if (!IsLoaded)
            {
                Debug.LogError("The sprite atlas is not loaded");
                return null;
            }
            if (index < 0 || index >= _sprites.Length)
            {
                Debug.LogError($"the index {index} must be between 0 and {_sprites.Length} (exclusive)");
                return null;
            }

            return _sprites[index];
        }
    }
}