using System;
using System.Collections.Generic;
using LeaRun.Util;
using System.Text;
using LeaRun.Util.FormModule;

namespace LeaRun.WorkFlow
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：LR0101
    /// 日 期：2016.10.26 10:18
    /// 描 述：工作流引擎运行方法类
    /// </summary>
    public class WfRuntime : IWfRuntime
    {
        private WFSchemeModel wfSchemeModel = null;

        public WfRuntime(string wfSchemeCotent) {
            wfSchemeModel = wfSchemeCotent.ToObject<WFSchemeModel>();
        }

        #region 获取数据
        /// <summary>
        /// 获取开始节点
        /// </summary>
        /// <returns></returns>
        public WFNodeModel getStartNode()
        {
            try
            {
                if(wfSchemeModel != null)
                {
                    foreach (var item in wfSchemeModel.nodes)
                    {
                        if (item.type == "startround")
                        {
                            return item;
                        }
                    }
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取节点实体
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public WFNodeModel getCurrentNode(string nodeId)
        {
            try
            {
                if (wfSchemeModel != null)
                {
                    foreach (var item in wfSchemeModel.nodes)
                    {
                        if (item.id == nodeId)
                        {
                            return item;
                        }
                    }
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取下一个节点
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        public List<WFNodeModel> getNextNodes(string nodeId, string formData)
        {
            try
            {
                List<WFNodeModel> wfNodeModel = new List<WFNodeModel>();
                if (wfSchemeModel != null)
                {
                    Dictionary<string, WFNodeModel> nodeDictionary = GetNodeDictionary();
                    foreach (var lineOne in wfSchemeModel.lines)
                    {
                        if (lineOne.from == nodeId)
                        {
                            if (lineOne.setInfo != null)
                            {
                                if (lineOne.setInfo.conditionJson.Count > 0)
                                {
                                    if (!string.IsNullOrEmpty(formData) && IsLineOK(formData, lineOne.setInfo.conditionJson))
                                    {
                                        wfNodeModel.Add(nodeDictionary[lineOne.to]);
                                    }
                                }
                                else
                                {
                                    wfNodeModel.Add(nodeDictionary[lineOne.to]);
                                }
                            }
                            else
                            {
                                wfNodeModel.Add(nodeDictionary[lineOne.to]);
                            }
                        }
                    }
                }
                return wfNodeModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取下一个节点
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="formDataList"></param>
        /// <returns></returns>
        public List<WFNodeModel> getNextNodes(string nodeId, List<WFVerificationFormModel> formDataList)
        {
            try
            {
                List<WFNodeModel> wfNodeModel = new List<WFNodeModel>();
                if (wfSchemeModel != null)
                {
                    Dictionary<string, WFNodeModel> nodeDictionary = GetNodeDictionary();
                    foreach (var lineOne in wfSchemeModel.lines)
                    {
                        if (lineOne.from == nodeId)
                        {
                            if (lineOne.setInfo != null)
                            {
                                if (lineOne.setInfo.conditionJson.Count > 0)
                                {
                                    if (IsLineOK(formDataList, lineOne.setInfo.conditionJson))
                                    {
                                        wfNodeModel.Add(nodeDictionary[lineOne.to]);
                                    }
                                }
                                else
                                {
                                    wfNodeModel.Add(nodeDictionary[lineOne.to]);
                                }
                            }
                            else
                            {
                                wfNodeModel.Add(nodeDictionary[lineOne.to]);
                            }
                        }
                    }
                }
                return wfNodeModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取此节点上一节点数
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public int getToNodeNum(string nodeId)
        {
            try
            {
                int num = 0;
                foreach (var item in wfSchemeModel.lines)
                {
                    if (item.to == nodeId)
                    {
                        num++;
                    }
                }
                return num;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取SQL语句
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="pkName"></param>
        /// <param name="frmData"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public string SqlBuider(dynamic tablename, string pkName, string frmData, string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
                var frmDataParam = frmData.ToJObject();
                string sqlvalues = "'" + keyValue + "'";
                foreach (var item in frmDataParam)
                {
                    if (item.Key != "__RequestVerificationToken")
                    {
                        pkName += "," + item.Key;
                        if (item.Value.Type.ToString() == "String")
                        {
                            sqlvalues += ",'" + item.Value + "'";
                        }
                        else
                        {
                            sqlvalues += "," + item.Value;
                        }
                    }
                }
                strSql.Append(string.Format("insert into " + tablename + " ({0})values({1})", pkName, sqlvalues));
                return strSql.ToString();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 一般处理方法
        /// <summary>
        /// 获取工作流节点的字典列表:key节点id
        /// </summary>
        /// <param name="schemeContentJson"></param>
        /// <returns></returns>
        private Dictionary<string, WFNodeModel> GetNodeDictionary()
        {
            Dictionary<string, WFNodeModel> nodeDictionary = new Dictionary<string, WFNodeModel>();
            foreach (var item in wfSchemeModel.nodes)
            {
                if (!nodeDictionary.ContainsKey(item.id))
                {
                    nodeDictionary.Add(item.id, item);
                }
            }
            return nodeDictionary;
        }
        /// <summary>
        /// 判断该线条是否通过
        /// </summary>
        /// <param name="formData"></param>
        /// <param name="conditionJson"></param>
        /// <returns></returns>
        private bool IsLineOK(string formData, List<WFLineConditionModel> conditionJson)
        {
            bool res = false;
            //dynamic formDataJson = formData.ToJObject();//获取数据内容

            FormOperation formOperation = new FormOperation();
            foreach (var item in conditionJson)//轮询该线条上的所有条件
            {
                string fieldId = item.FieldId.Split('|')[1];
                string formvalue = formOperation.GetValue(formData, fieldId); //   formDataJson[fieldId].ToString();
                if (!string.IsNullOrEmpty(formvalue))
                {
                    bool compareValue = LineCompared(formvalue, item.FilterId, item.FilterValue);
                    if (item.Logic == "AND")
                    {
                        res = compareValue;
                        if (!compareValue)
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (compareValue)
                        {
                            res = compareValue;
                        }
                    }
                }
            }
            return res;
        }
        /// <summary>
        /// 判断该线条是否通过
        /// </summary>
        /// <param name="formDataList"></param>
        /// <param name="conditionJson"></param>
        /// <returns></returns>
        private bool IsLineOK(List<WFVerificationFormModel> formDataList, List<WFLineConditionModel> conditionJson)
        {
            bool res = false;
            string formId = "", formData = "[]";
            FormOperation formOperation = new FormOperation();
            foreach (var item in conditionJson)//轮询该线条上的所有条件
            {
                string fieldId = item.FieldId.Split('|')[1];
                string formId1 = item.FieldId.Split('|')[0];
                if (formId1 != formId)
                {
                    formId = formId1;
                    formData = GetFormData(formId, formDataList);
                }
                string formvalue = formOperation.GetValue(formData, fieldId); 
                if (!string.IsNullOrEmpty(formvalue))
                {
                    bool compareValue = LineCompared(formvalue, item.FilterId, item.FilterValue);
                    if (item.Logic == "AND")
                    {
                        res = compareValue;
                        if (!compareValue)
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (compareValue)
                        {
                            res = compareValue;
                        }
                    }
                }
            }
            return res;
        }
        /// <summary>
        /// 获取表单数据
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="formDataList"></param>
        /// <returns></returns>
        private string GetFormData(string formId,List<WFVerificationFormModel> formDataList)
        {
            string res = "{}";
            foreach (var item in formDataList)
            {
                if (item.formId == formId)
                {
                    res = item.formData;
                    break;
                }
            }
            return res;
        }
        /// <summary>
        /// 工作流流转条件比较函数
        /// </summary>
        /// <param name="frmvalue">表单数据</param>
        /// <param name="operation"></param>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        private bool LineCompared(string frmvalue, string operation, string paramValue)
        {
            bool res = false;
            switch (operation)
            {
                case "Equal"://等于
                    if (frmvalue == paramValue)
                    {
                        res = true;
                    }
                    break;
                case "NotEqual"://不等于
                    if (frmvalue != paramValue)
                    {
                        res = true;
                    }
                    break;
                case "Greater"://大于
                    if (decimal.Parse(frmvalue) > decimal.Parse(paramValue))
                    {
                        res = true;
                    }
                    break;
                case "GreaterThan"://大于等于
                    if (decimal.Parse(frmvalue) >= decimal.Parse(paramValue))
                    {
                        res = true;
                    }
                    break;
                case "Less"://小于
                    if (decimal.Parse(frmvalue) < decimal.Parse(paramValue))
                    {
                        res = true;
                    }
                    break;
                case "LessThan"://小于等于
                    if (decimal.Parse(frmvalue) <= decimal.Parse(paramValue))
                    {
                        res = true;
                    }
                    break;
                case "Null"://为空
                    if (string.IsNullOrEmpty(frmvalue))
                    {
                        res = true;
                    }
                    break;
                case "NotNull"://不为空
                    if (!string.IsNullOrEmpty(frmvalue))
                    {
                        res = true;
                    }
                    break;
                case "Like"://包含
                    if (frmvalue.IndexOf(paramValue) != -1)
                    {
                        res = true;
                    }
                    break;
                case "NotLike"://不包含
                    if (frmvalue.IndexOf(paramValue) == -1)
                    {
                        res = true;
                    }
                    break;
            }
            return res;
        }
        #endregion
    }
}
