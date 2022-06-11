using System.Web;
using System.Web.Mvc;
using CD9TSchool.App_Start;

namespace CD9TSchool
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
