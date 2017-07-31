using LeaRun.Application.Code;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaRun.Application.Entity.PublicInfoManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.12.7 16:40
    /// 描 述：新闻中心
    /// </summary>
    public class NewsEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 新闻主键
        /// </summary>
        /// <returns></returns>
        [Column("F_NEWSID")]
        public string F_NewsId { get; set; }
        /// <summary>
        /// 类型（1-新闻2-公告）
        /// </summary>
        /// <returns></returns>
        [Column("F_TYPEID")]
        public int? F_TypeId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>
        /// <returns></returns>
        [Column("F_PARENTID")]
        public string F_ParentId { get; set; }
        /// <summary>
        /// 所属类别
        /// </summary>
        /// <returns></returns>
        [Column("F_CATEGORY")]
        public string F_Category { get; set; }
        /// <summary>
        /// 完整标题
        /// </summary>
        /// <returns></returns>
        [Column("F_FULLHEAD")]
        public string F_FullHead { get; set; }
        /// <summary>
        /// 标题颜色
        /// </summary>
        /// <returns></returns>
        [Column("F_FULLHEADCOLOR")]
        public string F_FullHeadColor { get; set; }
        /// <summary>
        /// 简略标题
        /// </summary>
        /// <returns></returns>
        [Column("F_BRIEFHEAD")]
        public string F_BriefHead { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        /// <returns></returns>
        [Column("F_AUTHORNAME")]
        public string F_AuthorName { get; set; }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        [Column("F_COMPILENAME")]
        public string F_CompileName { get; set; }
        /// <summary>
        /// Tag词
        /// </summary>
        /// <returns></returns>
        [Column("F_TAGWORD")]
        public string F_TagWord { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        /// <returns></returns>
        [Column("F_KEYWORD")]
        public string F_Keyword { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        /// <returns></returns>
        [Column("F_SOURCENAME")]
        public string F_SourceName { get; set; }
        /// <summary>
        /// 来源地址
        /// </summary>
        /// <returns></returns>
        [Column("F_SOURCEADDRESS")]
        public string F_SourceAddress { get; set; }
        /// <summary>
        /// 新闻内容
        /// </summary>
        /// <returns></returns>
        [Column("F_NEWSCONTENT")]
        public string F_NewsContent { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>
        /// <returns></returns>
        [Column("F_PV")]
        public int? F_PV { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        /// <returns></returns>
        [Column("F_RELEASETIME")]
        public DateTime? F_ReleaseTime { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        [Column("F_SORTCODE")]
        public int? F_SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        [Column("F_DELETEMARK")]
        public int? F_DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        [Column("F_ENABLEDMARK")]
        public int? F_EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYDATE")]
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYUSERID")]
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYUSERNAME")]
        public string F_ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.F_NewsId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
            this.F_ReleaseTime = DateTime.Now;
            this.F_CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.F_CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.F_DeleteMark = 0;
            this.F_EnabledMark = 1;
            this.F_PV = 0;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.F_NewsId = keyValue;
            this.F_ModifyDate = DateTime.Now;
            this.F_ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.F_ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}
