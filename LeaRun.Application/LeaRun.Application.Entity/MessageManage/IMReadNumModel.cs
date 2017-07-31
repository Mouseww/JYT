
using System;
namespace LeaRun.Application.Entity.MessageManage
{
    /// <summary>
    /// 即时通信读取消息对象
    /// </summary>
    public class IMReadNumModel
    {
        /// <summary>
        /// 消息发送者
        /// </summary>
        public string F_SendId { set; get; }
        /// <summary>
        /// 消息接收者
        /// </summary>
        public string F_UserId { set; get; }
        /// <summary>
        /// 消息数量
        /// </summary>
        public int F_UnReadNum { set; get; }
        /// <summary>
        /// 联系人Id
        /// </summary>
        public string F_OtherId { set; get; }
        /// <summary>
        /// 消息内容(最近的一条消息)
        /// </summary>
        public string F_IMContent { set; get; }
        /// <summary>
        /// 最近一条消息的时间
        /// </summary>
        public DateTime F_IMCreateTime { set; get; }
    }
}
