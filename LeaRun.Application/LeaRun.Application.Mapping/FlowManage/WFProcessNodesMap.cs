using LeaRun.Application.Entity.FlowManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.FlowManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈彬彬
    /// 编辑人：LR0101 2016.10.27 15.56
    /// 日 期：2016.03.18 09:58
    /// 描 述：工作流实例流转节点
    /// </summary>
    public class WFProcessNodesMap : EntityTypeConfiguration<WFProcessNodesEntity>
    {
        public WFProcessNodesMap(){
            #region 表、主键
            //表
            this.ToTable("WF_PROCESSNODES");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
