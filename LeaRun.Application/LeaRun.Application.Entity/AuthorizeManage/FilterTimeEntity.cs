using LeaRun.Application.Code;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaRun.Application.Entity.AuthorizeManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.11.20 13:32
    /// 描 述：过滤时段
    /// </summary>
    public class FilterTimeEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 过滤时段主键
        /// </summary>
        /// <returns></returns>
        [Column("F_FILTERTIMEID")]
        public string F_FilterTimeId { get; set; }
        /// <summary>
        /// 对象类型
        /// </summary>
        /// <returns></returns>
        [Column("F_OBJECTTYPE")]
        public string F_ObjectType { get; set; }
        /// <summary>
        /// 对象Id
        /// </summary>
        /// <returns></returns>
        [Column("F_OBJECTID")]
        public string F_ObjectId { get; set; }
        /// <summary>
        /// 访问
        /// </summary>
        /// <returns></returns>
        [Column("F_VISITTYPE")]
        public int? F_VisitType { get; set; }
        /// <summary>
        /// 星期一
        /// </summary>
        /// <returns></returns>
        [Column("F_WEEKDAY1")]
        public string F_WeekDay1 { get; set; }
        /// <summary>
        /// 星期二
        /// </summary>
        /// <returns></returns>
        [Column("F_WEEKDAY2")]
        public string F_WeekDay2 { get; set; }
        /// <summary>
        /// 星期三
        /// </summary>
        /// <returns></returns>
        [Column("F_WEEKDAY3")]
        public string F_WeekDay3 { get; set; }
        /// <summary>
        /// 星期四
        /// </summary>
        /// <returns></returns>
        [Column("F_WEEKDAY4")]
        public string F_WeekDay4 { get; set; }
        /// <summary>
        /// 星期五
        /// </summary>
        /// <returns></returns>
        [Column("F_WEEKDAY5")]
        public string F_WeekDay5 { get; set; }
        /// <summary>
        /// 星期六
        /// </summary>
        /// <returns></returns>
        [Column("F_WEEKDAY6")]
        public string F_WeekDay6 { get; set; }
        /// <summary>
        /// 星期日
        /// </summary>
        /// <returns></returns>
        [Column("F_WEEKDAY7")]
        public string F_WeekDay7 { get; set; }
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
            this.F_FilterTimeId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
            this.F_CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.F_CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.F_DeleteMark = 0;
            this.F_EnabledMark = 1;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.F_FilterTimeId = keyValue;
            this.F_ModifyDate = DateTime.Now;
            this.F_ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.F_ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}
