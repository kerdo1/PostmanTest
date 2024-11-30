using Microsoft.AspNetCore.Mvc;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Models.FreeGames;


namespace ShopTARgv23.Controllers
{
    public class FreeGamesController : Controller
    {
        private readonly IFreeGameServices _freeGameServices;

        public FreeGamesController
            (
                IFreeGameServices freeGameServices
            )
        {
            _freeGameServices = freeGameServices;
        }

        public async Task<IActionResult> Index()
        {
            var listGames = await _freeGameServices.GetFreeGames();
            
            var viewModelList = listGames.Select(game => new FreeGamesViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Thumbnail = game.Thumbnail,
                ShortDescription = game.ShortDescription,
                GameUrl = game.GameUrl,
                Genre = game.Genre,
                Platform = game.Platform,
                Publisher = game.Publisher,
                Developer = game.Developer,
                ReleaseDate = game.ReleaseDate,
                FreeToGameProfileUrl = game.FreeToGameProfileUrl
            }).ToList();

            return View(viewModelList);
        }
    }
}
