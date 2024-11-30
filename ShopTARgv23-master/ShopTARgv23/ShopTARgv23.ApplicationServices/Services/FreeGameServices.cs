using ShopTARgv23.Core.Dto.FreeGamesDto;
using ShopTARgv23.Core.ServiceInterface;
using System.Net;
using System.Text.Json;


namespace ShopTARgv23.ApplicationServices.Services
{
    public class FreeGameServices : IFreeGameServices
    {

        public async Task<List<FreeGamesRootDto>> GetFreeGames()
        {
            var url = "https://www.freetogame.com/api/games";
            List<FreeGamesRootDto> gamesList = new List<FreeGamesRootDto>();

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                var freeGameResults = JsonSerializer
                    .Deserialize<List<FreeGamesRootDto>>(json);

                if (freeGameResults != null)
                {
                    gamesList.AddRange(freeGameResults);
                }
            }

            return gamesList;
        }
    }
}
