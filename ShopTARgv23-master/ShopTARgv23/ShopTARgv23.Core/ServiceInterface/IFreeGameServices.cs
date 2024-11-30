using ShopTARgv23.Core.Dto.FreeGamesDto;

namespace ShopTARgv23.Core.ServiceInterface
{
    public interface IFreeGameServices
    {
        Task<List<FreeGamesRootDto>> GetFreeGames();
    }
}
