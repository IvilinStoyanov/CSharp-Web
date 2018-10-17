using RunesWebApp.Controllers;
using SIS.Framework;
using SIS.Framework.Routes;
using SIS.HTTP.Enums;
using SIS.WebServer;
using SIS.WebServer.Api;
using SIS.WebServer.Results;
using SIS.WebServer.Routing;

namespace RunesWebApp
{
    public class Program
    {
        private const int serverPort = 8000;

        static void Main(string[] args)
        {
            var handler = new ControllerRouter();

            Server server = new Server(serverPort, handler);

            var engine = new MvcEngine();
            engine.Run(server);

            //  ServerRoutingTable serverRoutingTable = new ServerRoutingTable();
        }

        private static void ConfigureRouting(ServerRoutingTable serverRoutingTable)
        {
            // HomeController
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/home/index"] =
                request => new RedirectResult("/");
            //serverRoutingTable.Routes[HttpRequestMethod.Get]["/"] =
            //    request => new HomeController().Index(request);

            // UserController
            //serverRoutingTable.Routes[HttpRequestMethod.Get]["/users/login"] =
            //    request => new UsersController().Login(request);
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/users/register"] =
                request => new UsersController().Register(request);
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/users/logout"] =
              request => new UsersController().Logout(request);
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/users/login"] =
                request => new UsersController().DoLogin(request);
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/users/register"] =
                request => new UsersController().DoRegister(request);

            //AlbumController
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/albums/all"] =
                request => new AlbumsController().All(request);
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/albums/create"] =
                request => new AlbumsController().Create(request);
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/albums/details"] =
                request => new AlbumsController().Details(request);
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/albums/create"] = request =>
                 new AlbumsController().DoCreate(request);

            // TrackController
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/tracks/create"] =
                request => new TracksController().Create(request);
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/tracks/create"] =
                request => new TracksController().DoCreate(request);
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/tracks/details"] =
                request => new TracksController().Details(request);
        }
    }
}
