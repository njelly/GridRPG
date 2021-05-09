using System.Threading.Tasks;
using Tofunaut.GridRPG.Interfaces;

namespace Tofunaut.GridRPG
{
    public class PlayerPrefsPlayerDataService : IPlayerDataService
    {
        private PlayerData _playerData;
        
        public async Task<PlayerData> Load()
        {
            if (_playerData != null)
                return _playerData;

            _playerData = new PlayerData();

            return _playerData;
        }
    }
}