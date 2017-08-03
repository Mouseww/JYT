using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：孔毛毛
    /// 日 期：2017-08-02 09:41
    /// 描 述：materialRoot
    /// </summary>
    public class materialRootEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 序号Id
        /// </summary>
        /// <returns></returns>
        public string id { get; set; }
        /// <summary>
        /// 最后一个父Id
        /// </summary>
        /// <returns></returns>
        public string parentId { get; set; }
        /// <summary>
        /// 所属父Id
        /// </summary>
        /// <returns></returns>
        public string parentIds { get; set; }
        /// <summary>
        /// 父类型
        /// </summary>
        /// <returns></returns>
        public string parentNames { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        /// <returns></returns>
        public string name { get; set; }
        /// <summary>
        /// 取样标准
        /// </summary>
        /// <returns></returns>
        public string quYangBiaoZuen { get; set; }
        /// <summary>
        /// des
        /// </summary>
        /// <returns></returns>
        public string des { get; set; }
        /// <summary>
        /// 种类
        /// </summary>
        /// <returns></returns>
        public int? sort { get; set; }
        /// <summary>
        /// 有效的合法的
        /// </summary>
        /// <returns></returns>
        public string valid { get; set; }
        /// <summary>
        /// 输入人物
        /// </summary>
        /// <returns></returns>
        public string inputPersonId { get; set; }
        /// <summary>
        /// 单元Id
        /// </summary>
        /// <returns></returns>
        public string unitId { get; set; }
        /// <summary>
        /// 叶子
        /// </summary>
        /// <returns></returns>
        public string isLeaf { get; set; }
        /// <summary>
        /// 检查1
        /// </summary>
        /// <returns></returns>
        public string jianCha1 { get; set; }
        /// <summary>
        /// 检查2
        /// </summary>
        /// <returns></returns>
        public string jianCha2 { get; set; }
        /// <summary>
        /// 检查3
        /// </summary>
        /// <returns></returns>
        public string jianCha3 { get; set; }
        /// <summary>
        /// 其他
        /// </summary>
        /// <returns></returns>
        public string other { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.id = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.id =keyValue;
                                            }
        #endregion
    }
}