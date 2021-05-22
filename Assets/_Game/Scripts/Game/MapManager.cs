using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tofunaut.GridRPG.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Tofunaut.GridRPG.Game
{
    public class MapManager
    {
        public Map CurrentMap { get; private set; }
        private Dictionary<object, Map> _loadedMaps;
        private List<Map> _activeMaps;

        public MapManager()
        {
            _loadedMaps = new Dictionary<object, Map>();
            _activeMaps = new List<Map>();
        }

        public async Task SetCurrentMap(MapData mapData)
        {
            _activeMaps.Clear();
            
            if (!_loadedMaps.TryGetValue(mapData.MapAssetReference.RuntimeKey, out var currentMap))
            {
                currentMap = (await Addressables.InstantiateAsync(mapData.MapAssetReference).Task).GetComponent<Map>();
                _loadedMaps.Add(mapData.MapAssetReference.RuntimeKey, currentMap);
            }

            CurrentMap = currentMap;
            _activeMaps.Add(CurrentMap);
            CurrentMap.gameObject.SetActive(true);

            var connectedMaps = CurrentMap.Portals.Select(x => x.ConnectedMap);
            foreach (var connectedMapData in connectedMaps)
            {
                if (!_loadedMaps.TryGetValue(connectedMapData.MapAssetReference.RuntimeKey, out var connectedMap))
                {
                    connectedMap = (await Addressables.InstantiateAsync(mapData.MapAssetReference).Task).GetComponent<Map>();
                    _loadedMaps.Add(connectedMapData.MapAssetReference.RuntimeKey, connectedMap);
                }

                if (connectedMapData.ActiveWhilePreloaded)
                {
                    _activeMaps.Add(connectedMap);
                    connectedMap.gameObject.SetActive(true);
                }
            }

            var toRemove = new List<object>();
            foreach (var runtimeKey in _loadedMaps.Keys)
            {
                var map = _loadedMaps[runtimeKey];
                if (!_activeMaps.Contains(_loadedMaps[runtimeKey]))
                {
                    toRemove.Add(runtimeKey);
                    Object.Destroy(map.gameObject);
                }
            }

            foreach (var runtimeKey in toRemove)
                _loadedMaps.Remove(runtimeKey);
        }
    }
}