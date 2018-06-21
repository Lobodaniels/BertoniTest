using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BertoniTest.Models {
    public class HomeViewModel {
        public HomeViewModel() {
            AlbumsItems = new List<SelectListItem>();
        }
        public List<SelectListItem> AlbumsItems { get; set; }
        public string SelectedAlbum { get; set; }
    }
}
