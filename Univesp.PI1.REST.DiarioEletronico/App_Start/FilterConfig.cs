using System.Web.Mvc;

namespace Univesp.PI1.REST.DiarioEletronico
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
