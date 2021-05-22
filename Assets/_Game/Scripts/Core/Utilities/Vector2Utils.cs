using UnityEngine;

namespace Tofunaut.Core.Utilities
{
    public static class Vector2Utils
    {
        public static Vector2Int RoundToVector2Int(this Vector2 v)
        {
            return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
        }
    }
}