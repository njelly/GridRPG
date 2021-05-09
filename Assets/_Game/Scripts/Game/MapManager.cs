using System.Collections.Generic;

namespace Tofunaut.GridRPG.Game
{
    public class MapManager
    {
        private Map _activeMap;
        private Dictionary<string, Map> _loadedMaps;

        public MapManager()
        {
            _loadedMaps = new Dictionary<string, Map>();
        }

        public async void SetActiveMap(string mapAssetPath)
        {
            
        }
    }
}