using System.Web;
using System.Web.Mvc;

namespace D2_B2D3_Toetsgenerator
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
