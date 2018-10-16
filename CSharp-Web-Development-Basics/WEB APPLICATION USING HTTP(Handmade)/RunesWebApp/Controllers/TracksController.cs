using RunesWebApp.Extensions;
using RunesWebApp.Services;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;

namespace RunesWebApp.Controllers
{
    public class TracksController : BaseController
    {
        private const string AlbumDoesNotExist = "Album does not exist!";
        private const string InvalidData = "Invalid data!";
        private const string TrackAlreadyExists = "Track already exists!";
        private const string TrackDoesNotExist = "Track does not exist!";

        private readonly AlbumService albumsService;
        private readonly TrackService trackService;

        public TracksController()
        {
            this.albumsService = new AlbumService();
            this.trackService = new TrackService();
        }

        public IHttpResponse Create(IHttpRequest request)
        {
            if (!this.IsAuthenticated(request))
            {
                return new RedirectResult("/users/login");
            }

            if (!request.QueryData.ContainsKey("albumId"))
            {
                return new RedirectResult("/albums/all");
            }

            var albumId = request.QueryData["albumId"].ToString();
            this.ViewBag["StartForm"] = $"<form method=\"post\" action=\"/tracks/create?albumId={albumId}\">";
            this.ViewBag["EndForm"] = "</form>";

            return this.View("Create");
        }

        public IHttpResponse DoCreate(IHttpRequest request)
        {
            if (!this.IsAuthenticated(request))
            {
                return new RedirectResult("/users/login");
            }

            if (!request.QueryData.ContainsKey("albumId"))
            {
                this.ViewBag["Error"] = AlbumDoesNotExist;
                return this.View("Error");
            }

            var albumId = int.Parse(request.QueryData["albumId"].ToString());
            var album = this.albumsService.GetAlbum(albumId);

            if (album == null)
            {
                this.ViewBag["Error"] = AlbumDoesNotExist;
                return this.View("Error");
            }

            var name = request.FormData["name"].ToString().UrlDecode();
            var link = request.FormData["link"].ToString().UrlDecode();
            var price = decimal.Parse(request.FormData["price"].ToString());


            if (name.Length <= 2 || link.Length <= 1 || price <= 0)
            {
                this.ViewBag["Error"] = InvalidData;
                return this.View("Error");
            }

            if (this.trackService.ContainsTrack(name))
            {
                this.ViewBag["Error"] = TrackAlreadyExists;
                return this.View("Error");
            }

            this.trackService.AddTrack(name, link, price, albumId);
            var response = new RedirectResult($"/albums/details?id={albumId}");

            return response;
        }

        public IHttpResponse Details(IHttpRequest request)
        {
            if (!this.IsAuthenticated(request))
            {
                return new RedirectResult("/users/login");
            }

            if (!request.QueryData.ContainsKey("albumId") || !request.QueryData.ContainsKey("trackId"))
            {
                this.ViewBag["Error"] = AlbumDoesNotExist;
                return this.View("Error");
            }

            var albumId = int.Parse(request.QueryData["albumId"].ToString());
            var trackId = int.Parse(request.QueryData["trackId"].ToString());

            var track = this.trackService.GetTrack(trackId);
            if (track == null)
            {
                this.ViewBag["Error"] = TrackDoesNotExist;
                return this.View("Error");
            }

            this.ViewBag["Name"] = track.Name;
            this.ViewBag["Price"] = $"${track.Price:F2}";
            this.ViewBag["BackTo"] = $"<a class=\"btn btn-primary\" href=\"/albums/details?id={albumId}\" role=\"button\">Back To Album</a>";
         //   this.ViewBag["Video"] = $"<iframe class=\"embed-responsive-item\" src=\"{track.Link}\" allowfullscreen></iframe>";
            string trackVideo = $"<iframe class=\"embed-responsive-item\" src=\"{track.Link}\"></iframe><br/>";
            this.ViewBag["Video"] = trackVideo;


            return this.View("Details");
        }
    }
}

