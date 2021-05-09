using System.Threading.Tasks;

namespace Tofunaut.GridRPG.Interfaces
{
    public interface IPlayerDataService
    {
        Task<PlayerData> Load();
    }
}