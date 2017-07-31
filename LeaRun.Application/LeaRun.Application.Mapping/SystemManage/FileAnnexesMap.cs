using LeaRun.Application.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SystemManage
{
    /// <summary>
    /// 版 本 LearunADMS 6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：LR0101
    /// 日 期：2016.11.14 17:50
    /// 描 述：文件信息（附件）
    /// </summary>
    public class FileAnnexesMap : EntityTypeConfiguration<FileAnnexesEntity>
    {
        public FileAnnexesMap()
        {
            #region 表、主键
            //表
            this.ToTable("BASE_FIlEANNEXES");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
