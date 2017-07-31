
using System.Collections.Generic;
namespace LeaRun.Application.Entity.AppManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：lr215
    /// 日 期：2016-09-02 17:12
    /// 描 述：App_ProjectModel
    /// </summary>
    public class AppProjectModel
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string F_Name { get; set; }
        /// <summary>
        /// 项目Logo
        /// </summary>
        public string F_Icon { get; set; }
        /// <summary>
        /// 是否有tab页
        /// </summary>
        public int? F_IsTabed { get; set; }
        /// <summary>
        /// 项目模板
        /// </summary>
        public List<AppTemplatesEntity> F_Templates { get; set; }
    }
}
