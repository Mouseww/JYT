using LeaRun.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-06-17 10:34
    /// 描 述：订单管理
    /// </summary>
    public class Client_OrderEntryMap : EntityTypeConfiguration<Client_OrderEntryEntity>
    {
        public Client_OrderEntryMap()
        {
            #region 表、主键
            //表
            this.ToTable("Client_OrderEntry");
            //主键
            this.HasKey(t => t.F_OrderEntryId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
