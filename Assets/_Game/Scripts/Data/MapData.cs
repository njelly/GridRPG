using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Tofunaut.GridRPG.Data
{
    [CreateAssetMenu(menuName = "GridRPG/MapData", fileName = "new MapData")]
    public class MapData : ScriptableObject
    {
        public string DisplayName;
        public AssetReference MapAssetReference;
        public bool ActiveWhilePreloaded = true;
    }
}