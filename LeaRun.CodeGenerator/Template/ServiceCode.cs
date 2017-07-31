using LeaRun.CodeGenerator.Comm;
using LeaRun.CodeGenerator.Model;
using System;
using System.Data;
using System.Text;

namespace LeaRun.CodeGenerator.Template
{
    /// <summary>
    /// 版 本 LearunADMS 6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.1.15 9:54
    /// 描 述：代码生成模板（后台服务类）
    /// </summary>
    public class ServiceCode
    {
        #region 实体类
        /// <summary>
        /// 生成实体类
        /// </summary>
        /// <param name="gridColumnModel"></param>
        /// <returns></returns>
        public string EntityBuilder(BaseConfigModel baseConfigModel, DataTable dt)
        {
            string strPk = "";
            string strCreateDate = "";
            string strCreateUserId = "";
            string strCreateUserName = "";
            string strModifyDate = "";
            string strModifyUserId = "";
            string strModifyUserName = "";
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("using System;\r\n");
                sb.Append("using System.ComponentModel.DataAnnotations.Schema;\r\n");
                sb.Append("using LeaRun.Application.Code;\r\n\r\n");


                sb.Append("namespace LeaRun.Application.Entity." + baseConfigModel.OutputAreas + "\r\n");
                sb.Append("{\r\n");
                sb.Append("    /// <summary>\r\n");
                sb.Append("    /// 版 本 LearunADMS V6.1.2.0\r\n");
                sb.Append("    /// Copyright (c) 2013-2016 上海力软信息技术有限公司\r\n");
                sb.Append("    /// 创 建：" + baseConfigModel.CreateUser + "\r\n");
                sb.Append("    /// 日 期：" + baseConfigModel.CreateDate + "\r\n");
                sb.Append("    /// 描 述：" + baseConfigModel.Description + "\r\n");
                sb.Append("    /// </summary>\r\n");
                sb.Append("    public class " + baseConfigModel.EntityClassName + " : BaseEntity\r\n");
                sb.Append("    {\r\n");
                sb.Append("        #region 实体成员\r\n");

                //实体字段
                foreach (DataRow rowItem in dt.Rows)
                {
                    string column = rowItem["f_column"].ToString();
                    string remark = rowItem["f_remark"].ToString();
                    string datatype = CommHelper.FindModelsType(rowItem["f_datatype"].ToString());
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// " + remark + "\r\n");
                    sb.Append("        /// </summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    if (baseConfigModel.DataBaseTablePK == column && datatype == "int")
                    {
                        sb.Append("        [DatabaseGenerated(DatabaseGeneratedOption.None)]");
                    }
                    sb.Append("        [Column(\"" + column.ToString().ToUpper() + "\")]\r\n");
                    sb.Append("        public " + datatype + " " + column + " { get; set; }\r\n");

                    #region 判断是否有创建人和修改人字段
                    if (baseConfigModel.DataBaseTablePK == column && datatype == "string")
                    {
                        strPk = "            this." + baseConfigModel.DataBaseTablePK + " = Guid.NewGuid().ToString();\r\n";
                    }
                    if (column == "F_CreateDate")
                    {
                        strCreateDate = "            this.F_CreateDate = DateTime.Now;\r\n";
                    }
                    if (column == "F_CreateUserId")
                    {
                        strCreateUserId = "            this.F_CreateUserId = OperatorProvider.Provider.Current().UserId;\r\n";
                    }
                    if (column == "F_CreateUserName")
                    {
                        strCreateUserName = "            this.F_CreateUserName = OperatorProvider.Provider.Current().UserName;\r\n";
                    }
                    if (column == "F_ModifyDate")
                    {
                        strModifyDate = "            this.F_ModifyDate = DateTime.Now;\r\n";
                    }
                    if (column == "F_ModifyUserId")
                    {
                        strModifyUserId = "            this.F_ModifyUserId = OperatorProvider.Provider.Current().UserId;\r\n";
                    }
                    if (column == "F_ModifyUserName")
                    {
                        strModifyUserName = "            this.F_ModifyUserName = OperatorProvider.Provider.Current().UserName;\r\n";
                    }
                    #endregion
                }

                sb.Append("        #endregion\r\n\r\n");
                sb.Append("        #region 扩展操作\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 新增调用\r\n");
                sb.Append("        /// </summary>\r\n");
                sb.Append("        public override void Create()\r\n");
                sb.Append("        {\r\n");
                sb.Append(strPk);
                sb.Append(strCreateDate);
                sb.Append(strCreateUserId);
                sb.Append(strCreateUserName);
                sb.Append("        }\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 编辑调用\r\n");
                sb.Append("        /// </summary>\r\n");
                sb.Append("        /// <param name=\"keyValue\"></param>\r\n");
                sb.Append("        public override void Modify(" + (strPk != "" ? "string" : "int") + " keyValue)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            this." + baseConfigModel.DataBaseTablePK + " = keyValue;\r\n");
                sb.Append(strModifyDate);
                sb.Append(strModifyUserId);
                sb.Append(strModifyUserName);
                sb.Append("        }\r\n");
                sb.Append("        #endregion\r\n");
                sb.Append("    }\r\n");
                sb.Append("}");
                return sb.ToString();

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 实体映射类
        /// <summary>
        /// 生成实体映射类
        /// </summary>
        /// <param name="baseConfigModel">基本信息</param>
        /// <returns></returns>
        public string EntityMapBuilder(BaseConfigModel baseConfigModel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using LeaRun.Application.Entity." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using System.Data.Entity.ModelConfiguration;\r\n\r\n");

            sb.Append("namespace LeaRun.Application.Mapping." + baseConfigModel.OutputAreas + "\r\n");
            sb.Append("{\r\n");
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// 版 本 LearunADMS V6.1.2.0\r\n");
            sb.Append("    /// Copyright (c) 2013-2016 上海力软信息技术有限公司\r\n");
            sb.Append("    /// 创 建：" + baseConfigModel.CreateUser + "\r\n");
            sb.Append("    /// 日 期：" + baseConfigModel.CreateDate + "\r\n");
            sb.Append("    /// 描 述：" + baseConfigModel.Description + "\r\n");
            sb.Append("    /// </summary>\r\n");
            sb.Append("    public class " + baseConfigModel.MapClassName + " : EntityTypeConfiguration<" + baseConfigModel.EntityClassName + ">\r\n");
            sb.Append("    {\r\n");
            sb.Append("        public " + baseConfigModel.MapClassName + "()\r\n");
            sb.Append("        {\r\n");
            sb.Append("            #region 表、主键\r\n");
            sb.Append("            //表\r\n");
            sb.Append("            this.ToTable(\"" + baseConfigModel.DataBaseTableName.ToUpper() + "\");\r\n");
            sb.Append("            //主键\r\n");
            sb.Append("            this.HasKey(t => t." + baseConfigModel.DataBaseTablePK + ");\r\n");
            sb.Append("            #endregion\r\n\r\n");

            sb.Append("            #region 配置关系\r\n");
            sb.Append("            #endregion\r\n");
            sb.Append("        }\r\n");
            sb.Append("    }\r\n");
            sb.Append("}\r\n");
            return sb.ToString();
        }
        #endregion

        #region 服务类
        /// <summary>
        /// 生成服务类
        /// </summary>
        /// <param name="baseConfigModel"></param>
        /// <returns></returns>
        public string ServiceBuilder(BaseConfigModel baseConfigModel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using LeaRun.Application.Entity." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using LeaRun.Application.IService." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using LeaRun.Data.Repository;\r\n");
            sb.Append("using LeaRun.Util.WebControl;\r\n");
            sb.Append("using System.Collections.Generic;\r\n");
            sb.Append("using System.Linq;\r\n\r\n");

            sb.Append("namespace LeaRun.Application.Service." + baseConfigModel.OutputAreas + "\r\n");
            sb.Append("{\r\n");
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// 版 本 Learun ADMS6.1.2.0\r\n");
            sb.Append("    /// Copyright (c) 2013-2016 上海力软信息技术有限公司\r\n");
            sb.Append("    /// 创 建：" + baseConfigModel.CreateUser + "\r\n");
            sb.Append("    /// 日 期：" + baseConfigModel.CreateDate + "\r\n");
            sb.Append("    /// 描 述：" + baseConfigModel.Description + "\r\n");
            sb.Append("    /// </summary>\r\n");
            sb.Append("    public class " + baseConfigModel.ServiceClassName + " : RepositoryFactory<" + baseConfigModel.EntityClassName + ">, " + baseConfigModel.IServiceClassName + "\r\n");
            sb.Append("    {\r\n");

            sb.Append("        #region 获取数据\r\n");

            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取列表\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"pagination\">分页</param>\r\n");
            sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
            sb.Append("        /// <returns>返回分页列表</returns>\r\n");
            sb.Append("        public IEnumerable<" + baseConfigModel.EntityClassName + "> GetPageList(Pagination pagination, string queryJson)\r\n");
            sb.Append("        {\r\n");
            if (string.IsNullOrEmpty(baseConfigModel.DataBaseLinkName))
            {
                sb.Append("             return this.BaseRepository().FindList(pagination);\r\n");
            }
            else
            {
                sb.Append("             return this.BaseRepository(\"" + baseConfigModel.DataBaseLinkName + "\",\"" + baseConfigModel.DataBaseLinkType + "\").FindList(pagination);\r\n");
            }
            sb.Append("        }\r\n");

            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取列表\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
            sb.Append("        /// <returns>返回列表</returns>\r\n");
            sb.Append("        public IEnumerable<" + baseConfigModel.EntityClassName + "> GetList(string queryJson)\r\n");
            sb.Append("        {\r\n");
            if (string.IsNullOrEmpty(baseConfigModel.DataBaseLinkName))
            {
                sb.Append("            return this.BaseRepository().IQueryable().ToList();\r\n");
            }
            else
            {
                sb.Append("            return this.BaseRepository(\"" + baseConfigModel.DataBaseLinkName + "\",\"" + baseConfigModel.DataBaseLinkType + "\").IQueryable().ToList();\r\n");
            }
            sb.Append("        }\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取实体\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        public " + baseConfigModel.EntityClassName + " GetEntity(string keyValue)\r\n");
            sb.Append("        {\r\n");
            if (string.IsNullOrEmpty(baseConfigModel.DataBaseLinkName))
            {
                sb.Append("            return this.BaseRepository().FindEntity(keyValue);\r\n");
            }
            else
            {
                sb.Append("            return this.BaseRepository(\"" + baseConfigModel.DataBaseLinkName + "\",\"" + baseConfigModel.DataBaseLinkType + "\").FindEntity(keyValue);\r\n");
            }
            sb.Append("        }\r\n");
            sb.Append("        #endregion\r\n\r\n");

            sb.Append("        #region 提交数据\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 删除数据\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
            sb.Append("        public void DeleteEntity(string keyValue)\r\n");
            sb.Append("        {\r\n");
            if (string.IsNullOrEmpty(baseConfigModel.DataBaseLinkName))
            {
                sb.Append("            this.BaseRepository().Delete(keyValue);\r\n");
            }
            else
            {
                sb.Append("            this.BaseRepository(\"" + baseConfigModel.DataBaseLinkName + "\",\"" + baseConfigModel.DataBaseLinkType + "\").Delete(keyValue);\r\n");
            }
            sb.Append("        }\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 保存表单（新增、修改）\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <param name=\"entity\">实体对象</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        public void SaveEntity(string keyValue, " + baseConfigModel.EntityClassName + " entity)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            if (!string.IsNullOrEmpty(keyValue))\r\n");
            sb.Append("            {\r\n");
            sb.Append("                entity.Modify(keyValue);\r\n");
            if (string.IsNullOrEmpty(baseConfigModel.DataBaseLinkName))
            {
                sb.Append("                this.BaseRepository().Update(entity);\r\n");
            }
            else
            {
                sb.Append("                this.BaseRepository(\"" + baseConfigModel.DataBaseLinkName + "\",\"" + baseConfigModel.DataBaseLinkType + "\").Update(entity);\r\n");
            }

            sb.Append("            }\r\n");
            sb.Append("            else\r\n");
            sb.Append("            {\r\n");
            sb.Append("                entity.Create();\r\n");
            if (string.IsNullOrEmpty(baseConfigModel.DataBaseLinkName))
            {
                sb.Append("                this.BaseRepository().Insert(entity);\r\n");
            }
            else
            {
                sb.Append("               this.BaseRepository(\"" + baseConfigModel.DataBaseLinkName + "\",\"" + baseConfigModel.DataBaseLinkType + "\").Insert(entity);\r\n");
            }

            sb.Append("            }\r\n");
            sb.Append("        }\r\n");
            sb.Append("        #endregion\r\n");
            sb.Append("    }\r\n");
            sb.Append("}\r\n");
            return sb.ToString();
        }
        #endregion

        #region 服务接口类
        /// <summary>
        /// 生成服务接口类
        /// </summary>
        /// <param name="baseConfigModel"></param>
        /// <returns></returns>
        public string IServiceBuilder(BaseConfigModel baseConfigModel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using LeaRun.Application.Entity." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using LeaRun.Util.WebControl;\r\n");
            sb.Append("using System.Collections.Generic;\r\n\r\n");

            sb.Append("namespace LeaRun.Application.IService." + baseConfigModel.OutputAreas + "\r\n");
            sb.Append("{\r\n");
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// 版 本 LearunADMS V6.1.2.0\r\n");
            sb.Append("    /// Copyright (c) 2013-2016 上海力软信息技术有限公司\r\n");
            sb.Append("    /// 创 建：" + baseConfigModel.CreateUser + "\r\n");
            sb.Append("    /// 日 期：" + baseConfigModel.CreateDate + "\r\n");
            sb.Append("    /// 描 述：" + baseConfigModel.Description + "\r\n");
            sb.Append("    /// </summary>\r\n");
            sb.Append("    public interface " + baseConfigModel.IServiceClassName + "\r\n");
            sb.Append("    {\r\n");
            sb.Append("        #region 获取数据\r\n");
            
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取列表\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"pagination\">分页</param>\r\n");
            sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
            sb.Append("        /// <returns>返回分页列表</returns>\r\n");
            sb.Append("        IEnumerable<" + baseConfigModel.EntityClassName + "> GetPageList(Pagination pagination, string queryJson);\r\n");
            
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取列表\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
            sb.Append("        /// <returns>返回列表</returns>\r\n");
            sb.Append("        IEnumerable<" + baseConfigModel.EntityClassName + "> GetList(string queryJson);\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取实体\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        " + baseConfigModel.EntityClassName + " GetEntity(string keyValue);\r\n");
            sb.Append("        #endregion\r\n\r\n");

            sb.Append("        #region 提交数据\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 删除数据\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
            sb.Append("        void DeleteEntity(string keyValue);\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 保存表单（新增、修改）\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <param name=\"entity\">实体对象</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        void SaveEntity(string keyValue, " + baseConfigModel.EntityClassName + " entity);\r\n");
            sb.Append("        #endregion\r\n");
            sb.Append("    }\r\n");
            sb.Append("}\r\n");
            return sb.ToString();
        }
        #endregion

        #region 业务类
        /// <summary>
        /// 生成业务类
        /// </summary>
        /// <param name="baseConfigModel"></param>
        /// <returns></returns>
        public string BusinesBuilder(BaseConfigModel baseConfigModel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using LeaRun.Application.Entity." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using LeaRun.Application.IService." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using LeaRun.Application.Service." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using LeaRun.Util.WebControl;\r\n");
            sb.Append("using System.Collections.Generic;\r\n");
            sb.Append("using System;\r\n\r\n");

            sb.Append("namespace LeaRun.Application.Busines." + baseConfigModel.OutputAreas + "\r\n");
            sb.Append("{\r\n");
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// 版 本 LearunADMS V6.1.2.0\r\n");
            sb.Append("    /// Copyright (c) 2013-2016 上海力软信息技术有限公司\r\n");
            sb.Append("    /// 创 建：" + baseConfigModel.CreateUser + "\r\n");
            sb.Append("    /// 日 期：" + baseConfigModel.CreateDate + "\r\n");
            sb.Append("    /// 描 述：" + baseConfigModel.Description + "\r\n");
            sb.Append("    /// </summary>\r\n");
            sb.Append("    public class " + baseConfigModel.BusinesClassName + "\r\n");
            sb.Append("    {\r\n");
            sb.Append("        private " + baseConfigModel.IServiceClassName + " service = new " + baseConfigModel.ServiceClassName + "();\r\n\r\n");

            sb.Append("        #region 获取数据\r\n");
            
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取列表\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"pagination\">分页</param>\r\n");
            sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
            sb.Append("        /// <returns>返回分页列表</returns>\r\n");
            sb.Append("        public IEnumerable<" + baseConfigModel.EntityClassName + "> GetPageList(Pagination pagination, string queryJson)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            return service.GetPageList(pagination, queryJson);\r\n");
            sb.Append("        }\r\n");
            
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取列表\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
            sb.Append("        /// <returns>返回列表</returns>\r\n");
            sb.Append("        public IEnumerable<" + baseConfigModel.EntityClassName + "> GetList(string queryJson)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            return service.GetList(queryJson);\r\n");
            sb.Append("        }\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取实体\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        public " + baseConfigModel.EntityClassName + " GetEntity(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            return service.GetEntity(keyValue);\r\n");
            sb.Append("        }\r\n");
            sb.Append("        #endregion\r\n\r\n");

            sb.Append("        #region 提交数据\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 删除数据\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
            sb.Append("        public void DeleteEntity(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            try\r\n");
            sb.Append("            {\r\n");
            sb.Append("                service.DeleteEntity(keyValue);\r\n");
            sb.Append("            }\r\n");
            sb.Append("            catch (Exception)\r\n");
            sb.Append("            {\r\n");
            sb.Append("                throw;\r\n");
            sb.Append("            }\r\n");
            sb.Append("        }\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 保存表单（新增、修改）\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <param name=\"entity\">实体对象</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        public void SaveEntity(string keyValue, " + baseConfigModel.EntityClassName + " entity)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            try\r\n");
            sb.Append("            {\r\n");
            sb.Append("                service.SaveEntity(keyValue, entity);\r\n");
            sb.Append("            }\r\n");
            sb.Append("            catch (Exception)\r\n");
            sb.Append("            {\r\n");
            sb.Append("                throw;\r\n");
            sb.Append("            }\r\n");
            sb.Append("        }\r\n");
            sb.Append("        #endregion\r\n");
            sb.Append("    }\r\n");
            sb.Append("}\r\n");

            return sb.ToString();
        }
        #endregion
    }
}
