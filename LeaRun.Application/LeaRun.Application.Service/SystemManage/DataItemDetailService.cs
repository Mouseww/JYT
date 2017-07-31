using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.Entity.SystemManage.ViewModel;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaRun.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.11.17 9:56
    /// 描 述：数据字典明细
    /// </summary>
    public class DataItemDetailService : RepositoryFactory<DataItemDetailEntity>, IDataItemDetailService
    {
        #region 获取数据
        /// <summary>
        /// 明细列表
        /// </summary>
        /// <param name="itemId">分类Id</param>
        /// <returns></returns>
        public IEnumerable<DataItemDetailEntity> GetList(string itemId)
        {
            return this.BaseRepository().IQueryable(t => t.F_ItemId == itemId).OrderBy(t => t.F_SortCode).ToList();
        }
        /// <summary>
        /// 明细实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DataItemDetailEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 获取数据字典列表（给绑定下拉框提供的）
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataItemModel> GetDataItemList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  i.F_ItemId ,
                                    i.F_ItemCode AS F_EnCode ,
                                    d.F_ItemDetailId ,
                                    d.F_ParentId ,
                                    d.F_ItemCode ,
                                    d.F_ItemName ,
                                    d.F_ItemValue ,
                                    d.F_QuickQuery ,
                                    d.F_SimpleSpelling ,
                                    d.F_IsDefault ,
                                    d.F_SortCode ,
                                    d.F_EnabledMark
                            FROM    Base_DataItemDetail d
                                    LEFT JOIN Base_DataItem i ON i.F_ItemId = d.F_ItemId
                            WHERE   1 = 1
                                    AND d.F_EnabledMark = 1
                                    AND d.F_DeleteMark = 0
                                    AND i.F_ItemCode IS NOT NULL
                            ORDER BY d.F_SortCode ASC");
            return new RepositoryFactory().BaseRepository().FindList<DataItemModel>(strSql.ToString());
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 项目值不能重复
        /// </summary>
        /// <param name="itemValue">项目值</param>
        /// <param name="keyValue">主键</param>
        /// <param name="itemId">分类Id</param>
        /// <returns></returns>
        public bool ExistItemValue(string itemValue, string keyValue, string itemId)
        {
            var expression = LinqExtensions.True<DataItemDetailEntity>();
            expression = expression.And(t => t.F_ItemValue == itemValue).And(t => t.F_ItemId == itemId);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.F_ItemDetailId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// 项目名不能重复
        /// </summary>
        /// <param name="itemName">项目名</param>
        /// <param name="keyValue">主键</param>
        /// <param name="itemId">分类Id</param>
        /// <returns></returns>
        public bool ExistItemName(string itemName, string keyValue, string itemId)
        {
            var expression = LinqExtensions.True<DataItemDetailEntity>();
            expression = expression.And(t => t.F_ItemName == itemName).And(t => t.F_ItemId == itemId);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.F_ItemDetailId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除明细
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存明细表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="dataItemDetailEntity">明细实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DataItemDetailEntity dataItemDetailEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                dataItemDetailEntity.Modify(keyValue);
                this.BaseRepository().Update(dataItemDetailEntity);
            }
            else
            {
                dataItemDetailEntity.Create();
                this.BaseRepository().Insert(dataItemDetailEntity);
            }
        }
        #endregion
    }
}
