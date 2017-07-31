
namespace LeaRun.Application.Entity.MessageManage
{
    /// <summary>
    /// 即时通信群类
    /// </summary>
    public class IMGroupModel
    {
        /// <summary>
        /// 群组Id
        /// </summary>
        public string F_GroupId { set; get; }
        /// <summary>
        /// 群组名称
        /// </summary>
        public string F_GroupName { set; get; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public string F_UserId { set; get; }
        /// <summary>
        /// 群联系人对应表主键Id
        /// </summary>
        public string F_UserGroupId { set; get; }
    }
}
