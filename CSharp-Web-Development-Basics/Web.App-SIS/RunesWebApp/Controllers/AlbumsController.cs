using RunesWebApp.Extensions;
using RunesWebApp.Models;
using RunesWebApp.Services;
using SIS.Framework.ActionsResults.Base.Contracts;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;
using System;
using System.Linq;
using System.Text;

namespace RunesWebApp.Controllers
{
    public class AlbumsController : BaseController
    {
        private const string AlbumExists = "Album already exists!";
        private const string NoAlbums = "There are currently no albums.";
        private const string AlbumDoesNotExist = "Album does not exist!";
        private const string NoTracks = "There are currently no tracks.";

        private readonly AlbumService albumsService;
        private readonly TrackService trackService;

        public AlbumsController()
        {
            this.albumsService = new AlbumService();
            this.trackService = new TrackService();
        }

        public IActionResult All() => this.View();

        public IHttpResponse All(IHttpRequest request)
        {
            if (!this.IsAuthenticated(request))
            {
                return new RedirectResult("/users/login");
            }

            var albums = this.Db.Albums;

            if (albums.Any())
            {
                var listOfAlbums = string.Empty;
                foreach (var album in albums)
                {
                    // var albumHtml = $"<a href=\"/album/details?id={album.Id}\">{album.Name}</a></li><br/>";
                    var albumHtml = $@"<div><h4><a href=/albums/details?id={album.Id}>{album.Name}</a></h4></div><br/>";
                    listOfAlbums += albumHtml;
                }
                this.ViewBag["albumsList"] = listOfAlbums;
            }
            else
            {
                this.ViewBag["albumsList"] = "There are currently no albums.";
            }

            return this.ViewMethod();
        }

        public IHttpResponse Create(IHttpRequest request)
        {
            if (!this.IsAuthenticated(request))
            {
                return new RedirectResult("/users/login");
            }
            return this.ViewMethod("Create");
        }

        //post album/create
        public IHttpResponse DoCreate(IHttpRequest request)
        {
            var name = request.FormData["name"].ToString().UrlDecode();
            var cover = request.FormData["cover"].ToString().UrlDecode();

            if (this.albumsService.ContainsAlbum(name, cover))
            {
                this.ViewBag["error"] = AlbumExists;
                return this.ViewMethod("Error");
            }

            this.albumsService.AddAlbum(name, cover);
            var response = new RedirectResult("/albums/all");
            return response;
        }

        public IHttpResponse Details(IHttpRequest request)
        {
            if (!this.IsAuthenticated(request))
            {
                return new RedirectResult("/users/login");
            }

            if (!request.QueryData.ContainsKey("id"))
            {
                this.ViewBag["Error"] = AlbumDoesNotExist;
                return this.ViewMethod("Error");
            }

            var albumId = int.Parse(request.QueryData["id"].ToString());
            var album = this.albumsService.GetAlbum(albumId);

            if (album == null)
            {
                this.ViewBag["Error"] = AlbumDoesNotExist;
                return this.ViewMethod("Error");
            }

            this.ViewBag["Cover"] = $"<img src=\"{album.Cover}\" alt=\"{album.Name}\" class=\"img-fluid\">";
            this.ViewBag["Name"] = album.Name;
            this.ViewBag["Price"] = $"${this.albumsService.GetTotalPrice(albumId):F2}";
            this.ViewBag["CreateTrack"] = $"<a class=\"btn btn-primary\" href=\"/tracks/create?albumId={albumId}\" role=\"button\">Create Track</a>";

            var allTracks = this.trackService.GetAllTracks(albumId);
            var sb = new StringBuilder();
            if (allTracks.Any())
            {
                sb.AppendLine("<ol>");
                foreach (var track in allTracks)
                {
                    var trackText = $@"<li><div><a href=/tracks/details?albumId={albumId}&trackId={track.Id}>{track.Name}</a></div></li><br/>";
                    sb.AppendLine(trackText);
                }
                sb.AppendLine("</ol>");

                this.ViewBag["AllTracks"] = sb.ToString();
            }
            else
            {
                this.ViewBag["AllTracks"] = NoTracks;
            }

            return this.ViewMethod("Details");
        }
    }
}

