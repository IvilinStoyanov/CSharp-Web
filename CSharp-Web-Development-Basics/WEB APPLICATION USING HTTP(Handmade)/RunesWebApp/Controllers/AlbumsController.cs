using RunesWebApp.Models;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;
using System;
using System.Linq;

namespace RunesWebApp.Controllers
{
    public class AlbumsController : BaseController
    {
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
                    var albumHtml = $@"<p><a href=""/albums/details/{album.Id}"">{album.Name}</a></p>";
                    listOfAlbums += albumHtml;
                }
                this.ViewBag["albumsList"] = listOfAlbums;
            }
            else
            {
                this.ViewBag["albumsList"] = "There are currently no albums.";
            }

            return this.View();
        }

        public IHttpResponse Create()
        {
            return View();
        }

        //post album/create
        public IHttpResponse DoCreate(IHttpRequest request)
        {
            var name = request.FormData["name"].ToString().Trim();
            var cover = request.FormData["cover"].ToString();


            // Create user
            var album = new Album
            {
                Name = name,
                Cover = cover
            };
            this.Db.Albums.Add(album);

            try
            {
                this.Db.SaveChanges();
            }
            catch (Exception e)
            {
                // TODO: Log error
                return new BadRequestResult(
                    e.Message,
                    HttpResponseStatusCode.InternalServerError);
            }
            var response = new RedirectResult("/albums/all");

            // Redirect
            return response;
        }
    }
}
