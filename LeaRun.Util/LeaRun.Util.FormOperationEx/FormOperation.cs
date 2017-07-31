using System;
using System.Collections.Generic;
using System.Data;

namespace LeaRun.Util.FormModule
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：LR215
    /// 日 期：2016.09.15 16:58
    /// 描 述：自定义表单处理类
    /// </summary>
    public class FormOperation
    {
        /// <summary>
        /// 创建一个DataTable
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public DataTable CreateTable(List<FormInstanceModel> row)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("leaCustmerFormId", typeof(string));
                foreach (var item in row)
                {
                    dt.Columns.Add(item.field, typeof(string));
                }
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 获取表单数据中的指定字段数据
        /// </summary>
        /// <param name="formData"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(string formData,string key)
        {
            try
            {
                List<FormInstanceModel> fields = formData.ToObject<List<FormInstanceModel>>();
                foreach (var item in fields)
                {
                    if (item.field == key)
                    {
                        return item.realValue;
                    }
                }
                return "";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
