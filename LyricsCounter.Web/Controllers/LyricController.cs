using LyricsCounter.Service;
using LyricsCounter.Service.Entities;
using LyricsCounter.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LyricsCounter.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class LyricController : Controller
    {
        private static int progressPercent;
        private static string _currentArtist;
        private static Task<ArtistLyricsAverage> _currentTask;
        private readonly ISongLookupService _songLookupService;

        public LyricController(ISongLookupService songLookupService)
        {
            _songLookupService = songLookupService ?? throw new System.ArgumentNullException(nameof(songLookupService));
        }

        [HttpGet]
        public async Task<IActionResult> ArtistSearch()
        {
            ArtistLookupModel model = new ArtistLookupModel();
            model.History = await _songLookupService.GetHistory();
            return View("ArtistSearch", model);
        }

        [HttpPost]
        public async Task<IActionResult> ArtistSearch(ArtistLookupModel model)
        {
            if (!ModelState.IsValid)
                return View("ArtistSearch", model);

            var searchResults = await _songLookupService.SearchArtist(model.Artist);
            if (searchResults == null || searchResults.Count == 0)
            {
                throw new NotImplementedException();
            }

            var newModel = new ArtistResultsModel(searchResults);
            return View("ArtistLookupResults", newModel);
        }

        [HttpGet("LyricsCounter/{artistName}/{MBId}")]
        public async Task<IActionResult> LyricsCounter(string artistName, Guid MBId)
        {
            //A new progress object so keep track of how we are getting on
            var progress = new Progress<int>(ProgressHandler);
            _currentArtist = artistName;

            //Call out to the APIs to get the results - store in a hot task
            _currentTask = _songLookupService.CountLyricsForArtist(MBId, progress);

            InProgressModel model = new InProgressModel { Artist = artistName };
            return View("InProgress", model);
        }

        //get the progress to report to the webpage
        public async Task<IActionResult> CurrentProgress()
        {
            //safety check but we should never hit this
            if (_currentTask == null)
                return PartialView("_Error", "We lost track of the query");

            //there was an error running the task, report it back to the UI
            if (_currentTask.IsFaulted)
                return PartialView("_Error", _currentTask.Exception.Message);

            //The task completed successfully!
            if (_currentTask.IsCompleted)
            {
                //grab the results from the repository and present them
                var completedModel = new CompletedModel();
                completedModel.Results = await _songLookupService.GetArtist(_currentArtist);
                return PartialView("_Completed", completedModel);
            }

            //it must still be in progress - let the UI know how it's getting on
            var inProgressModel = new InProgressModel();
            inProgressModel.PercentComplete = progressPercent;
            inProgressModel.Results = await _songLookupService.GetArtist(_currentArtist);
            return PartialView("_InProgress", inProgressModel);
        }

        void ProgressHandler(int value)
        {
            progressPercent = value;
        }
    }
}