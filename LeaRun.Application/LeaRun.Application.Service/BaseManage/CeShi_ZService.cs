using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.IService.BaseManage;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;

using LeaRun.Util;
using LeaRun.Util.Extension;

namespace LeaRun.Application.Service.BaseManage
{
    /// <summary>
    /// �� �� LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 �Ϻ�������Ϣ�������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2017-08-02 16:44
    /// �� ��������
    /// </summary>
    public class CeShi_ZService : RepositoryFactory, CeShi_ZIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<CeShi_ZEntity> GetPageList(Pagination pagination, string queryJson)
        {
             return this.BaseRepository("","").FindList<CeShi_ZEntity>(pagination);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public CeShi_ZEntity GetEntity(string keyValue)
        {
             return this.BaseRepository().FindEntity<CeShi_ZEntity>(keyValue);
        }
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public IEnumerable<CeShi_FEntity> GetDetails(string keyValue)
        {
            return this.BaseRepository().FindList<CeShi_FEntity>("select * from CeShi_F where Z_Id='"+keyValue +"'");        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
             IRepository db = this.BaseRepository().BeginTrans();
             try
             {
                 db.Delete<CeShi_ZEntity>(keyValue);
                 db.Delete<CeShi_FEntity>(t => t.Z_Id.Equals(keyValue));
                 db.Commit();
             }
             catch (Exception)
             {
                 db.Rollback();
                 throw;
             }
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, CeShi_ZEntity entity,List<CeShi_FEntity> entryList)
        {
        IRepository db = this.BaseRepository().BeginTrans();
        try
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //����
                entity.Modify(keyValue);
                db.Update(entity);
                //��ϸ
                db.Delete<CeShi_FEntity>(t => t.Z_Id.Equals(keyValue));
                foreach (CeShi_FEntity item in entryList)
                {
                    item.Create();
                    item.Z_Id = entity.Z_Id;
                    db.Insert(item);
                }
            }
            else
            {
                //����
                entity.Create();
                db.Insert(entity);
                //��ϸ
                foreach (CeShi_FEntity item in entryList)
                {
                    item.Create();
                    item.Z_Id = entity.Z_Id;
                    db.Insert(item);
                }
            }
            db.Commit();
        }
        catch (Exception)
        {
            db.Rollback();
            throw;
        }
        }
        #endregion
    }
}
