using LeaRun.Application.Busines.FormManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.FlowManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using Nancy.Responses.Negotiation;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace LeaRun.Application.AppSerivce.Modules
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：力软01
    /// 日 期：2016.06.13 10:50
    /// 描 述：获取流程信息
    /// </summary>
    public class WorkFlowModule : BaseModule
    {
        //我的流程  草稿流程
        private WFProcessBLL wfProcessBll = new WFProcessBLL();
        //发起流程
        private WFSchemeInfoBLL wfFlowInfoBLL = new WFSchemeInfoBLL();
        //工作委托
        private WFDelegate wfDelegate = new WFDelegate();

        public WorkFlowModule()
            : base("/learun/api")
        {
            Post["/flowDesign/designList"] = DesignList;//发起流程列表
            Post["/flowDesign/saveFlowDesign"] = SaveFlowDesign;//发起流程保存
            Post["/flowRoughdraft/roughdraftList"] = RoughdraftList;//草稿流程列表
            Post["/flowRoughdraft/deleteFlowRoughdraft"] = DeleteFlowRoughdraft;//删除草稿流程
            Post["/flowProcess/processList"] = ProcessList;//我的流程列表
            Post["/flowBefProcess/befprocessList"] = BefProcessList;//代办流程列表
            Post["/flowBefProcess/saveFlowBefProcess"] = SaveBefProcess;//代办流程审核
            Post["/flowAftProcess/aftprocessList"] = AftProcessList;//已办流程审核
            Post["/flowDelegate/delegateList"] = DelegateList;//工作流程审核
        }

        /// <summary>
        /// 发起流程列表
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Negotiator DesignList(dynamic _)
        {
            try
            {
                var res = "";
                var recdata = this.GetModule<ReceiveModule<PaginationModule>>();
                bool resValidation = this.DataValidation(recdata.userid, recdata.token);
                if (!resValidation)
                {
                    return this.SendData(ResponseType.Fail, "后台无登录信息");
                }
                else
                {
                    var watch = CommonHelper.TimerStart();
                    Pagination pagination = new Pagination
                    {
                        page = recdata.data.page,
                        rows = recdata.data.rows,
                        sidx = recdata.data.sidx,
                        sord = recdata.data.sord
                    };
                    var data = wfFlowInfoBLL.GetPageList(pagination, recdata.data.queryData);

                    DataPageList<DataTable> dataPageList = new DataPageList<DataTable>
                    {
                        rows = data,
                        total = pagination.total,
                        page = pagination.page,
                        records = pagination.records,
                        costtime = CommonHelper.TimerEnd(watch)
                    };
                    res = JsonConvert.SerializeObject(dataPageList, new DataTableConverter());
                    return this.SendData(res, recdata.userid, recdata.token, ResponseType.Success);
                }
            }
            catch (Exception ex)
            {
                return this.SendData(ResponseType.Fail, ex.ToString());
            }
        }
        /// <summary>
        /// 发起流程保存
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Negotiator SaveFlowDesign(dynamic _)
        {
            try
            {

                var schemeinfo = this.GetModule<ReceiveModule<recData>>();

                WFProcessInstanceEntity wfProcessInstanceEntity = schemeinfo.data.data.ToObject<WFProcessInstanceEntity>();

                var schemeinfo1 = wfFlowInfoBLL.GetEntity(schemeinfo.data.keyValue);
                //var schemecontent1 = wfFlowInfoBLL.GetSchemeEntity(schemeinfo1.F_Id, schemeinfo1.F_SchemeVersion);
                //var authorize1 = wfFlowInfoBLL.GetAuthorizeEntityList(schemeinfo1.F_Id);

                //WFSchemeContentEntity wfSchemeContentEntity = schemeinfo.data.data.ToObject<WFSchemeContentEntity>();
                //WFSchemeContentEntity wfSchemeContentEntity = wfFlowInfoBLL.GetSchemeEntity(schemeinfo.data.keyValue, schemeinfo1.F_SchemeVersion);
                
                //wfSchemeContentEntity.F_WFSchemeInfoId = wfProcessInstanceEntity.F_Id;

                bool resValidation = this.DataValidation(schemeinfo.userid, schemeinfo.token);

                if (!resValidation)
                {
                    return this.SendData(ResponseType.Fail, "后台无登录信息");
                }
                else
                {
                    
                    //wfProcessBll.CreateProcess(schemeinfo.data.keyValue, wfProcessInstanceEntity, schemeinfo.data.data.ToString());
                    return this.SendData(ResponseType.Success);
                }
            }
            catch
            {
                return this.SendData(ResponseType.Fail, "异常");
            }
        }

        /// <summary>
        /// 草稿流程列表
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Negotiator RoughdraftList(dynamic _)
        {
            try
            {
                var res = "";
                var recdata = this.GetModule<ReceiveModule<PaginationModule>>();
                bool resValidation = this.DataValidation(recdata.userid, recdata.token);
                if (!resValidation)
                {
                    return this.SendData(ResponseType.Fail, "后台无登录信息");
                }
                else
                {
                    var watch = CommonHelper.TimerStart();
                    Pagination pagination = new Pagination
                    {
                        page = recdata.data.page,
                        rows = recdata.data.rows,
                        sidx = recdata.data.sidx,
                        sord = recdata.data.sord
                    };
                    //var data = wfProcessBll.GetPageList(pagination, recdata.data.queryData, "3");

                    DataPageList<DataTable> dataPageList = new DataPageList<DataTable>
                    {
                        //rows = data,
                        total = pagination.total,
                        page = pagination.page,
                        records = pagination.records,
                        costtime = CommonHelper.TimerEnd(watch)
                    };
                    res = JsonConvert.SerializeObject(dataPageList, new DataTableConverter());
                    return this.SendData(res, recdata.userid, recdata.token, ResponseType.Success);
                }
            }
            catch (Exception ex)
            {
                return this.SendData(ResponseType.Fail, ex.ToString());
            }
        }
        /// <summary>
        /// 草稿流程删除
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Negotiator DeleteFlowRoughdraft(dynamic _)
        {
            var recdata = this.GetModule<ReceiveModule<WFSchemeInfoEntity>>();
            try
            {
                bool resValidation = this.DataValidation(recdata.userid, recdata.token);
                if (!resValidation)
                {
                    return this.SendData(ResponseType.Fail, "后台无登录信息");
                }
                else
                {
                    wfFlowInfoBLL.RemoveForm(recdata.data.F_Id);
                    return this.SendData(ResponseType.Success);
                }
            }
            catch (System.Exception)
            {
                return this.SendData(ResponseType.Fail, "异常");
            }
        }
        /// <summary>
        /// 获取我的流程列表
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Negotiator ProcessList(dynamic _)
        {
            try
            {
                var res = "";
                var recdata = this.GetModule<ReceiveModule<PaginationModule>>();
                bool resValidation = this.DataValidation(recdata.userid, recdata.token);
                if (!resValidation)
                {
                    return this.SendData(ResponseType.Fail, "后台无登录信息");
                }
                else
                {
                    var watch = CommonHelper.TimerStart();
                    Pagination pagination = new Pagination
                    {
                        page = recdata.data.page,
                        rows = recdata.data.rows,
                        sidx = recdata.data.sidx,
                        sord = recdata.data.sord
                    };
                    var data = wfProcessBll.GetPageList(pagination, recdata.data.queryData);

                    DataPageList<DataTable> dataPageList = new DataPageList<DataTable>
                    {
                        rows = data,
                        total = pagination.total,
                        page = pagination.page,
                        records = pagination.records,
                        costtime = CommonHelper.TimerEnd(watch)
                    };
                    res = JsonConvert.SerializeObject(dataPageList, new DataTableConverter());
                    return this.SendData(res, recdata.userid, recdata.token, ResponseType.Success);
                }
            }
            catch (Exception ex)
            {
                return this.SendData(ResponseType.Fail, ex.ToString());
            }
        }
        /// <summary>
        /// 待办流程
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Negotiator BefProcessList(dynamic _)
        {
            try
            {
                var recdata = this.GetModule<ReceiveModule<PaginationModule>>();
                bool resValidation = this.DataValidation(recdata.userid, recdata.token);
                if (!resValidation)
                {
                    return this.SendData(ResponseType.Fail, "后台无登录信息");
                }
                else
                {
                    var watch = CommonHelper.TimerStart();
                    Pagination pagination = new Pagination
                    {
                        page = recdata.data.page,
                        rows = recdata.data.rows,
                        sidx = recdata.data.sidx,
                        sord = recdata.data.sord
                    };
                    var data = wfProcessBll.GetToMeBeforePageList(pagination, recdata.data.queryData);
                    var JsonData = new
                    {
                        rows = data,
                        total = pagination.total,
                        page = pagination.page,
                        records = pagination.records,
                        costtime = CommonHelper.TimerEnd(watch)
                    };
                    return this.SendData(JsonData.ToJson(), recdata.userid, recdata.token, ResponseType.Success);
                }
            }
            catch (Exception ex)
            {
                return this.SendData(ResponseType.Fail, ex.ToString());
            }
        }
        /// <summary>
        /// 已办流程
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Negotiator AftProcessList(dynamic _)
        {
            try
            {
                var recdata = this.GetModule<ReceiveModule<PaginationModule>>();
                bool resValidation = this.DataValidation(recdata.userid, recdata.token);
                if (!resValidation)
                {
                    return this.SendData(ResponseType.Fail, "后台无登录信息");
                }
                else
                {
                    var watch = CommonHelper.TimerStart();
                    Pagination pagination = new Pagination
                    {
                        page = recdata.data.page,
                        rows = recdata.data.rows,
                        sidx = recdata.data.sidx,
                        sord = recdata.data.sord
                    };
                    var data = wfProcessBll.GetToMeAfterPageList(pagination, recdata.data.queryData);
                    var JsonData = new
                    {
                        rows = data,
                        total = pagination.total,
                        page = pagination.page,
                        records = pagination.records,
                        costtime = CommonHelper.TimerEnd(watch)
                    };
                    return this.SendData(JsonData.ToJson(), recdata.userid, recdata.token, ResponseType.Success);
                }
            }
            catch (Exception ex)
            {
                return this.SendData(ResponseType.Fail, ex.ToString());
            }
        }
        /// <summary>
        /// 代办流程审核
        /// </summary>
        /// <returns></returns>
        private Negotiator SaveBefProcess(dynamic _)
        {
            try
            {
                var schemeinfo = this.GetModule<ReceiveModule<WFSchemeInfoEntity>>();

                bool resValidation = this.DataValidation(schemeinfo.userid, schemeinfo.token);
                if (!resValidation)
                {
                    return this.SendData(ResponseType.Fail, "后台无登录信息");
                }
                else
                {

                    return this.SendData(ResponseType.Success);
                }
            }
            catch
            {
                return this.SendData(ResponseType.Fail, "异常");
            }
        }
        /// <summary>
        /// 工作委托列表
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Negotiator DelegateList(dynamic _)
        {
            try
            {
                var recdata = this.GetModule<ReceiveModule<PaginationModule>>();
                bool resValidation = this.DataValidation(recdata.userid, recdata.token);
                if (!resValidation)
                {
                    return this.SendData(ResponseType.Fail, "后台无登录信息");
                }
                else
                {
                    var watch = CommonHelper.TimerStart();
                    Pagination pagination = new Pagination
                    {
                        page = recdata.data.page,
                        rows = recdata.data.rows,
                        sidx = recdata.data.sidx,
                        sord = recdata.data.sord
                    };
                    var data = wfDelegate.GetRulePageList(pagination, recdata.data.queryData, recdata.userid);
                    var JsonData = new
                    {
                        rows = data,
                        total = pagination.total,
                        page = pagination.page,
                        records = pagination.records,
                        costtime = CommonHelper.TimerEnd(watch)
                    };
                    return this.SendData(JsonData.ToJson(), recdata.userid, recdata.token, ResponseType.Success);
                }
            }
            catch (Exception ex)
            {
                return this.SendData(ResponseType.Fail, ex.ToString());
            }
        }
        public class schemecontent
        {
            public string Id { get; set; }

            public string WFSchemeInfoId { get; set; }

            public string SchemeVersion { get; set; }

            public string SchemeContent { get; set; }

            public string FrmId { get; set; }

            public string FrmName { get; set; }

            public string FrmContent { get; set; }


        }
    }
    
    public class recData {
        public string data { get; set; }

        public string keyValue { get; set; }
    }
}