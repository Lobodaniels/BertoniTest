using BertoniTest.Models;
using BertoniTest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace BertoniTest.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataService _dataService;

        public HomeController(IDataService dataService, ILogger<HomeController> logger) {
            _logger = logger;
            _dataService = dataService;
        }
        public IActionResult Index() {
            HomeViewModel model = new HomeViewModel();

            List<Album> albums = new List<Album>();
            try {
                albums = _dataService.GetAlbums();
            } catch (Exception ex) {
                _logger.LogError(ex, "Problem Calling data service", string.Empty);
            }

            model.AlbumsItems.Add(new SelectListItem() { Text = "Select Album", Value = "-1", Selected = true });

            foreach (var album in albums) {
                model.AlbumsItems.Add(new SelectListItem() { Text = album.Title, Value = album.Id.ToString() });
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult LoadData(int albumId) {
            var photos = new List<Photo>();
            if (albumId != -1) {
                try {
                    photos = _dataService.GetPhotos(albumId);
                } catch (Exception ex) {
                    _logger.LogError(ex, "Problem Calling data service", string.Empty);
                }
            }

            return Json(new { data = photos });
        }

        [HttpPost]
        public ActionResult LoadComments(int photoId) {
            var comments = new List<Comment>();
            if (photoId != 0) {
                try {
                    comments = _dataService.GetComments(photoId);
                } catch (Exception ex) {
                    _logger.LogError(ex, "Problem Calling data service", string.Empty);
                }
            }

            return Json(new { data = comments });
        }

    }
}
