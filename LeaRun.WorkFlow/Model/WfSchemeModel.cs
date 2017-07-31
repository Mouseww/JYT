using System.Collections.Generic;
namespace LeaRun.WorkFlow
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：LR0101
    /// 日 期：2016.10.26 10:18
    /// 描 述：工作流模板类
    /// </summary>
    public class WFSchemeModel
    {
        /// <summary>
        /// 节点数据
        /// </summary>
        public List<WFNodeModel> nodes { get; set; }
        /// <summary>
        /// 线条数据
        /// </summary>
        public List<WFLineModel> lines { get; set; }
    }
}
