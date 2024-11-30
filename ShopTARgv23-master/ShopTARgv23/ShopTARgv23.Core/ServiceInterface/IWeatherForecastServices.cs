using ShopTARgv23.Core.Dto.WeatherDtos;


namespace ShopTARgv23.Core.ServiceInterface
{
    public interface IWeatherForecastServices
    {
        Task<AccuLocationWeatherResultDto> AccuWeatherResult(AccuLocationWeatherResultDto dto);
    }
}
