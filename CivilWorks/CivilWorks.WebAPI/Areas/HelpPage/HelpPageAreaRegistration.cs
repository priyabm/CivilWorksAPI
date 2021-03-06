using System.Web.Http;
using System.Web.Mvc;

namespace CivilWorks.WebAPI.Areas.HelpPage
{
    public class HelpPageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "HelpPage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                "HelpPage_Default",
                "Help/{action}/{apiId}",
                new { controller = "Help", action = "Index", apiId = UrlParameter.Optional });


            //context.MapRoute(
            //    "Home_default",
            //    "Home/{action}/{id}",
            //    new { action = "Index", id = "" }
            //);

            // HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}