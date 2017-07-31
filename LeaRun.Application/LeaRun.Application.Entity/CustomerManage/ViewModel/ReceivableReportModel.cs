using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Application.Entity.CustomerManage.ViewModel
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：佘赐雄
    /// 日 期：2016-04-19 17:40
    /// 描 述：应收账款报表
    /// </summary>
    public class ReceivableReportModel
    {
        #region 实体成员
        /// <summary>
        /// 账款主键
        /// </summary>
        /// <returns></returns>
        public string F_ReceivableId { get; set; }
        /// <summary>
        /// 订单主键
        /// </summary>
        /// <returns></returns>
        public string F_OrderId { get; set; }
        /// <summary>
        /// 订单单号
        /// </summary>
        /// <returns></returns>
        public string F_OrderCode { get; set; }
        /// <summary>
        /// 客户主键
        /// </summary>
        /// <returns></returns>
        public string F_CustomerId { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        /// <returns></returns>
        public string F_CustomerCode { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        /// <returns></returns>
        public string F_CustomerName { get; set; }
        /// <summary>
        /// 收款日期
        /// </summary>
        /// <returns></returns>
        public DateTime? F_PaymentTime { get; set; }
        /// <summary>
        /// 收款金额
        /// </summary>
        /// <returns></returns>
        public decimal? F_PaymentPrice { get; set; }
        /// <summary>
        /// 收款方式
        /// </summary>
        /// <returns></returns>
        public string F_PaymentMode { get; set; }
        /// <summary>
        /// 收款账户
        /// </summary>
        /// <returns></returns>
        public string F_PaymentAccount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string F_Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public string F_CreateUserName { get; set; }
        #endregion
    }
}
