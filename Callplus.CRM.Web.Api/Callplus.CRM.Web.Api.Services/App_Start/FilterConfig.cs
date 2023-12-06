using System.Web;
using System.Web.Mvc;

namespace Callplus.CRM.Web.Api.Services
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
