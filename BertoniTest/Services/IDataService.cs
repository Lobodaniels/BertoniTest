using BertoniTest.Models;
using System.Collections.Generic;

namespace BertoniTest.Services {
    public interface IDataService {
        List<Album> GetAlbums();
        List<Photo> GetPhotos(int albumIf);
        List<Comment> GetComments(int photoId);
    }

}
