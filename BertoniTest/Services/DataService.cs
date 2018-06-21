using BertoniTest.Extensions;
using BertoniTest.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace BertoniTest.Services {
    public class DataService : IDataService {
        private readonly IConfiguration _configuration;
        public DataService(IConfiguration configuration) {
            _configuration = configuration;
        }
        public List<Album> GetAlbums() {
            using (HttpClient client = new HttpClient()) {
                List<Album> albums = new List<Album>();

                HttpResponseMessage response = client.GetAsync(_configuration["AlbumsUrl"]).Result;
                if (response.IsSuccessStatusCode) {
                    albums = response.Content.ReadAsJsonAsync<List<Album>>().Result;
                } else {
                    throw new Exception(string.Format("Could not connect  with  data api, rason: {0}, code: {1}", response.ReasonPhrase, response.StatusCode));
                }

                return albums;
            }
        }

        public List<Photo> GetPhotos(int albumId) {
            using (HttpClient client = new HttpClient()) {
                List<Photo> photos = new List<Photo>();

                HttpResponseMessage response = client.GetAsync(_configuration["PhotosUrl"]).Result;
                if (response.IsSuccessStatusCode) {
                    photos = response.Content.ReadAsJsonAsync<List<Photo>>().Result;
                    if (photos.Count > 0) {
                        photos = photos.Where(p => p.AlbumId == albumId).ToList();
                    }

                } else {
                    throw new Exception(string.Format("Could not connect  with  data api, rason: {0}, code: {1}", response.ReasonPhrase, response.StatusCode));
                }

                return photos;
            }
        }

        public List<Comment> GetComments(int photoId) {
            using (HttpClient client = new HttpClient()) {
                var comments = new List<Comment>();

                HttpResponseMessage response = client.GetAsync(_configuration["CommentsUrl"]).Result;
                if (response.IsSuccessStatusCode) {
                    comments = response.Content.ReadAsJsonAsync<List<Comment>>().Result;
                    if (comments.Count > 0) {
                        comments = comments.Where(p => p.Id == photoId).ToList();
                    }

                } else {
                    throw new Exception(string.Format("Could not connect  with  data api, rason: {0}, code: {1}", response.ReasonPhrase, response.StatusCode));
                }

                return comments;
            }
        }
    }

}
