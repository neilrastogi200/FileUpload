using System.Web.Mvc;
using System.Web.Routing;

namespace FileUpload.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "FileUpload", action = "UploadFile", id = UrlParameter.Optional}
                );
        }
    }
}