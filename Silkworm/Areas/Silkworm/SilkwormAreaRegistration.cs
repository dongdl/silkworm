using System.Web.Http;
using System.Web.Mvc;

namespace ATEC.Silkworm.Areas.Silkworm
{
    public class SilkwormAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Silkworm";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "SilkwormHome_Default",
                url: "Silkworm/{action}/{apiId}",
                defaults: new { controller = "Home", action = "Index", apiId = UrlParameter.Optional },
                 namespaces: new[] { "ATEC.Silkworm.Areas.Silkworm.Controllers" });

            context.MapRoute(
               name: "Login_Default",
               url: "Silkworm1/Login",
               defaults: new { controller = "Home", action = "Index", apiId = UrlParameter.Optional },
                namespaces: new[] { "ATEC.Silkworm.Areas.Silkworm.Controllers" });

            SilkwormConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}