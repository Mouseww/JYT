﻿using LeaRun.Application.Entity.MessageManage;
using LeaRun.Application.IService.MessageManage;
using LeaRun.Data;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace LeaRun.Application.Service.MessageManage
{
    /// <summary>
    /// 版 本 V6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2015.11.26 11:14
    /// 描 述：即时通信用户管理
    /// </summary>
    public class IMUserService : RepositoryFactory, IMsgUserService
    {
        /// <summary>
        /// 获取联系人列表（即时通信）
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IMUserModel> GetList(string OrganizeId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  u.F_UserId ,
                                    u.F_RealName ,
                                    o.F_FullName AS F_OrganizeId ,
                                    d.F_FullName AS F_DepartmentId ,
                                    u.F_HeadIcon  
                            FROM    Base_User u
                                    LEFT JOIN Base_Organize o ON o.F_OrganizeId = u.F_OrganizeId
                                    LEFT JOIN Base_Department d ON d.F_DepartmentId = u.F_DepartmentId
                            WHERE   1=1");
            var parameter = new List<DbParameter>();
            //公司主键
            if (!OrganizeId.IsEmpty())
            {
                strSql.Append(" AND u.F_OrganizeId = @OrganizeId");
                parameter.Add(DbParameters.CreateDbParameter("@OrganizeId", OrganizeId));
            }
            strSql.Append(" AND u.F_UserId <> 'System'");
            strSql.Append(" order by d.F_FullName");
            return this.BaseRepository().FindList<IMUserModel>(strSql.ToString(), parameter.ToArray());
        }
    }
}
