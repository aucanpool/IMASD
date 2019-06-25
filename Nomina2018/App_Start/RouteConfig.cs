
using System.Web.Routing;

namespace Nomina2018.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");
            
            routes.MapPageRoute(
        "Home",
        "Home/Index/{*queryvalues}",
        "~/Home.aspx"
    );
        }
    }
}