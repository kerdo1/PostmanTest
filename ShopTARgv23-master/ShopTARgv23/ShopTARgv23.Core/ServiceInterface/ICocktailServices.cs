using ShopTARgv23.Core.Dto.CocktailDto;


namespace ShopTARgv23.Core.ServiceInterface
{
    public interface ICocktailServices
    {
        Task<CocktailResultDto> GetCocktails(CocktailResultDto dto);
    }
}
