using RunesWebApp.Controllers;
using SIS.HTTP.Enums;
using SIS.WebServer;
using SIS.WebServer.Results;
using SIS.WebServer.Routing;

namespace RunesWebApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            ConfigureRouting(serverRoutingTable);


            Server server = new Server(8000, serverRoutingTable);

            server.Run();
        }

        private static void ConfigureRouting(ServerRoutingTable serverRoutingTable)
        {
            // GET
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/home/index"] =
                request => new RedirectResult("/");
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/"] =
                request => new HomeController().Index(request);
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/users/login"] =
                request => new UsersController().Login(request);
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/users/register"] =
                request => new UsersController().Register(request);
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/albums/all"] =
                request => new AlbumsController().All(request);
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/albums/create"] = request =>
                  new AlbumsController().Create();

            //post
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/users/login"] =
                request => new UsersController().PostLogin(request);
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/users/register"] =
                request => new UsersController().PostRegister(request);
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/albums/create"] = request =>
                 new AlbumsController().DoCreate(request);
        }
    }
}
