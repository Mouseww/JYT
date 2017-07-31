
namespace LeaRun.Application.Entity.MessageManage
{
    /// <summary>
    /// 即时通信用户对象类
    /// </summary>
    public class IMUserModel
    {
        #region 实体成员
        /// <summary>
        /// 用户主键
        /// </summary>		
        public string F_UserId { get; set; }
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
        /// <summary>
        /// 性别
        /// </summary>		
        public int F_Gender { get; set; } 
        /// <summary>
        /// 头像
        /// </summary>		
        public string F_HeadIcon { get; set; }
        /// <summary>
        /// 在线状态
        /// </summary>		
        public int F_UserOnLine { get; set; }
        #endregion
    }
}
