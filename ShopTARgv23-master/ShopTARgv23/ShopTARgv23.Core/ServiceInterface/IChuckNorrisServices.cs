using ShopTARgv23.Core.Dto.ChuckNorrisRootDto;

namespace ShopTARgv23.Core.ServiceInterface
{
    public interface IChuckNorrisServices
    {
        Task<ChuckNorrisResultDto> ChuckNorrisResult(ChuckNorrisResultDto dto);
    }
}
