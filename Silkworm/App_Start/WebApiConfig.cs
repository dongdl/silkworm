using System.Web.Http;
using ATEC.Silkworm.ActionFilters;

namespace ATEC.Silkworm.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new LoggingFilterAttribute());
            config.Filters.Add(new GlobalExceptionAttribute());
        }
    }
}
