namespace LeaRun.Application.Entity.WebApp
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.06.07 10:58
    /// 描 述：用户管理（手机端接口使用）
    /// </summary>
    public class appUserInfoModel
    {
        #region 实体成员
        /// <summary>
        /// 用户主键
        /// </summary>		
        public string F_UserId { get; set; }
        /// <summary>
        /// 登录账户
        /// </summary>		
        public string F_Account { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>		
        public string F_RealName { get; set; }
        /// <summary>
        /// 机构主键
        /// </summary>		
        public string F_OrganizeId { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>		
        public string F_DepartmentId { get; set; }
        #endregion
    }
}
