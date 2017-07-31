using System.Collections.Generic;

namespace LeaRun.WorkFlow
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：LR0101
    /// 日 期：2016.10.26 10:18
    /// 描 述：工作流节点类
    /// </summary>
    public class WFNodeModel
    {
        /// <summary>
        /// 节点Id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 节点类型 开始startround;结束endround;一般stepnode;
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 节点设置信息
        /// </summary>
        public WFNodeSettingModel setInfo { get; set; }
    }
    /// <summary>
    /// 节点设置信息
    /// </summary>
    public class WFNodeSettingModel
    {
        /// <summary>
        /// 节点编号
        /// </summary>
        public string nodeCode { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string nodeMyName { get; set; }
        /// <summary>
        /// 节点驳回类型 0：前一步 1：第一步 2:某一步 3:不处理
        /// </summary>
        public string nodeRejectType { get; set; }
        /// <summary>
        /// 节点驳回步骤
        /// </summary>
        public string nodeRejectStep { get; set; }
        /// <summary>
        /// 节点新增表单
        /// </summary>
        public string nodeForm { get; set; }
        /// <summary>
        /// 节点绑定的类
        /// </summary>
        public string nodeSysFn { get; set; }
        /// <summary>
        /// 当前节点可操作的表单
        /// </summary>
        public List<string> formHaven { get; set; }
        /// <summary>
        /// 节点备注
        /// </summary>
        public string nodeDes { get; set; }
        /// <summary>
        /// 会签类型 0 :所有步骤通过 1:一个步骤通过即可 2:按百分比计算
        /// </summary>
        public string nodeConfluenceType { get; set; }
        /// <summary>
        /// 会签通过比例
        /// </summary>
        public string nodeConfluenceRate { get; set; }
        /// <summary>
        /// 通过后执行sql语句的数据库
        /// </summary>
        public string nodeDbId { get; set; }
        /// <summary>
        /// 通过后执行的数据库
        /// </summary>
        public string nodeSQL { get; set; }
        /// <summary>
        /// 审核人员类型
        /// </summary>
        public string nodeDesignate { get; set; }
        /// <summary>
        /// 指定人员成员
        /// </summary>
        public WFNodeDesignateModel nodeDesignateData { get; set; }
        /// <summary>
        /// 表单查看权限列表
        /// </summary>
        public List<WFNodeFormAuthorizeModel> nodeAuthorizeInfo { get; set; }
    }
    /// <summary>
    /// 节点表单权限信息
    /// </summary>
    public class WFNodeFormAuthorizeModel {
        /// <summary>
        /// 表单Id
        /// </summary>
        public string formId { get; set; }
        /// <summary>
        /// 字段Id
        /// </summary>
        public string fieldid { get; set; }
        /// <summary>
        /// 查看权限
        /// </summary>
        public bool look { get; set; }
        /// <summary>
        /// 编辑权限
        /// </summary>
        public bool edit { get; set; }    
    }
    /// <summary>
    /// 节点审核人员信息
    /// </summary>
    public class WFNodeDesignateModel
    {
        /// <summary>
        /// 角色
        /// </summary>
        public List<string> role { get; set; }
        /// <summary>
        /// 岗位
        /// </summary>
        public List<string> post { get; set; }
        /// <summary>
        /// 用户组
        /// </summary>
        public List<string> usergroup { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public List<string> user { get; set; }
    }
}
