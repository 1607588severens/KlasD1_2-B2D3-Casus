using System.Web;
using System.Web.Mvc;

namespace D1_2_B2D3_Casus_Toetsgenerator
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
