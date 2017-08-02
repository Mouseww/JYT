using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-08-02 11:24
    /// 描 述：Base_Post
    /// </summary>
    public class Base_PostEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 帖子表主键
        /// </summary>
        /// <returns></returns>
        public string F_PostID { get; set; }
        /// <summary>
        /// 建议表ID
        /// </summary>
        /// <returns></returns>
        public string Proposal_ID { get; set; }
        /// <summary>
        /// 文档表ID
        /// </summary>
        /// <returns></returns>
        public string Document_ID { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        /// <returns></returns>
        public string NO { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        /// <returns></returns>
        public string OrdinalTitle { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        /// <returns></returns>
        public string Contents { get; set; }
        /// <summary>
        /// 文档来源
        /// </summary>
        /// <returns></returns>
        public string DocumentName { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        /// <returns></returns>
        public DateTime? UploadTime { get; set; }
        /// <summary>
        /// 访问量
        /// </summary>
        /// <returns></returns>
        public int? Traffic { get; set; }
        /// <summary>
        /// 建议
        /// </summary>
        /// <returns></returns>
        public string Proposal { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.F_PostID = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.F_PostID = keyValue;
                                            }
        #endregion
    }
}