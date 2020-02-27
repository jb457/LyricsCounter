using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LyricsCounter.Web.Models;
using LyricsCounter.Service;
using System.Threading.Tasks;

namespace LyricsCounter.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISongLookupService _songLookupService;

        public HomeController(ISongLookupService songLookupService)
        {
            _songLookupService = songLookupService ?? throw new System.ArgumentNullException(nameof(songLookupService));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Index(ArtistLookupModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var results = await _songLookupService.SearchArtist(model.Artist);
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
