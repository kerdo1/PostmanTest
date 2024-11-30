using Microsoft.AspNetCore.Mvc;
using ShopTARgv23.Core.Dto.ChuckNorrisRootDto;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Models.ChuckNorris;

namespace ShopTARgv23.Controllers
{
    public class ChuckNorrisController : Controller
    {
        private readonly IChuckNorrisServices _chuckNorrisServices;

        public ChuckNorrisController
            (
                IChuckNorrisServices chuckNorrisServices
            )
        {
            _chuckNorrisServices = chuckNorrisServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchChuckNorrisJokes(ChuckNorrisViewModel model)
        {
            return RedirectToAction(nameof(Joke));
        }

        [HttpGet]
        public IActionResult Joke()
        {
            ChuckNorrisResultDto dto = new();

            _chuckNorrisServices.ChuckNorrisResult(dto);
            ChuckNorrisViewModel vm = new();

            vm.Categories = dto.Categories;
            vm.CreatedAt = dto.CreatedAt;
            vm.IconUrl = dto.IconUrl;
            vm.Id = dto.Id;
            vm.UpdatedAt = dto.UpdatedAt;
            vm.Url = dto.Url;
            vm.Value = dto.Value;

            return View(vm);
        }
    }
}
