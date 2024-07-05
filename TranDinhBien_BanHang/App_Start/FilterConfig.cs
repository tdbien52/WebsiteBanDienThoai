using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranDinhBien_BanHang.Filters;

namespace TranDinhBien_BanHang.App_Start
{
    public class FilterConfig
    {
        public static void GlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute() { ExceptionType = typeof(Exception),View="Error404"});
        }
    }
}