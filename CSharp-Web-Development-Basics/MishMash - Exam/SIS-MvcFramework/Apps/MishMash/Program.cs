using SIS.MvcFramework;

namespace MishMash
{
    class Program
    {
        static void Main(string[] args)
        {
            WebHost.Start(new Startup());
        }
    }
}
