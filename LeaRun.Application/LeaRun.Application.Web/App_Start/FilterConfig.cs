using System.Web.Mvc;

namespace LeaRun.Application.Web
{
    /// <summary>
    /// 过滤器
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// 注册过滤
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandlerErrorAttribute());
        }
    }
}