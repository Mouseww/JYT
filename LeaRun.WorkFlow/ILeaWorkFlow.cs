using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.WorkFlow
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：LR0101
    /// 日 期：2016.10.26 10:18
    /// 描 述：工作流引擎接口
    /// </summary>
    public interface ILeaWorkFlow
    {
        /// <summary>
        /// 节点审核通过执行方法
        /// </summary>
        /// <param name="processId"></param>
        void Sucess(string processId);
        /// <summary>
        /// 节点审核失败执行方法
        /// </summary>
        /// <param name="processId"></param>
        void Fail(string processId);

    }
}
