using UnityEngine;

namespace Tofunaut.GridRPG.Data
{
    [CreateAssetMenu(menuName = "GridRPG/MapCatalog", fileName = "new MapCatalog")]
    public class MapCatalog : ScriptableObject
    {
        public MapData[] MapDatas;
    }
}