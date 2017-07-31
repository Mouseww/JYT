using System;

namespace LeaRun.Application.Entity.MessageManage
{
    /// <summary>
    /// 消息内容
    /// <author>
    ///		<name>chen</name>
    ///		<date>2015.08.01 14:00</date>
    /// </author>
    /// </summary>
    public class IMReadModel
    {
        /// <summary>
        /// 消息Id
        /// </summary>
        public string F_ReadId { get; set; }
        /// <summary>
        /// 信息内容ID
        /// </summary>
        public string F_ContentId { get; set; }
        /// <summary>
        /// 消息接收者Id
        /// </summary>
        public string F_UserId { get; set; }
        /// <summary>
        /// 消息发送者Id
        /// </summary>
        public string F_SendId { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string F_Content { get; set; }
        /// <summary>
        /// 消息时间
        /// </summary>
        public DateTime F_CreateDate { get; set; }
    }
}
