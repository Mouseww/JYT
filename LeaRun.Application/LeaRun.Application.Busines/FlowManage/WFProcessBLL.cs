using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.Entity.FormManage;
using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.BaseManage;
using LeaRun.Application.IService.FlowManage;
using LeaRun.Application.IService.FormManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Application.Service.BaseManage;
using LeaRun.Application.Service.FlowManage;
using LeaRun.Application.Service.FormManage;
using LeaRun.Application.Service.SystemManage;
using LeaRun.Util.Ioc;
using LeaRun.Util.WebControl;
using LeaRun.WorkFlow;
using System;
using System.Collections.Generic;
using System.Data;

namespace LeaRun.Application.Busines.FormManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈彬彬
    /// 编辑人：LR0101 2106.10.25 14:10
    /// 日 期：2016.03.19 13:57
    /// 描 述：工作流流程模板操作
    /// </summary>
    public class WFProcessBLL
    {
        private IWFSchemeInfoService wfSchemeInfoService = new WFSchemeInfoService();
        private IWFProcessSchemeService wfProcessSchemeService = new WFProcessSchemeService();
        private IWFDelegateRuleService wfDelegateRuleService = new WFDelegateRuleService();
        private IWFProcessInstanceService wfProcessInstanceService = new WFProcessInstanceService();
        private IWFProcessOperationHistoryService wfProcessOperationHistoryService = new WFProcessOperationHistoryService();
        private IWFProcessTransitionHistoryService wfProcessTransitionHistoryService = new WFProcessTransitionHistoryService();
        private IFormModuleService wfFrmMainService = new FormModuleService();


        private IUserService userService = new UserService();//用户数据库操作类
        private IDepartmentService departmentService = new DepartmentService();
        private IOrganizeService organizeService = new OrganizeService();

        //表单相关操作
        private IFormModuleService server = new FormModuleService();
        private IFormModuleInstanceService serverInstance = new FormModuleInstanceService();
        private Form_ModuleContentIService serverContent = new Form_ModuleContentService();

        //数据库相关操作
        private IDataBaseLinkService dataBaseServer = new DataBaseLinkService();

        private string delegateUserList = "";
        #region 流程实例API
        /// <summary>
        /// 创建一个流程实例
        /// </summary>
        /// <param name="wfCPParameterModel"></param>
        /// <returns></returns>
        public void CreateProcessInstance(WFCPParameterModel wfCPParameterModel)
        {
            try
            {
                if (string.IsNullOrEmpty(wfCPParameterModel.processId))
                {
                    throw (new Exception("没有设置processId值!"));
                }

                #region 获取流程模板
                string processSchemeId = "";
                WFSchemeInfoEntity wfSchemeInfoEntity = null;
                WFProcessSchemeEntity wfProcessSchemeEntity = null;
                if (!string.IsNullOrEmpty(wfCPParameterModel.schemeId))
                {
                    wfSchemeInfoEntity = wfSchemeInfoService.GetEntity(wfCPParameterModel.schemeId);
                    wfProcessSchemeEntity = wfProcessSchemeService.GetEntityBySchemeId(wfCPParameterModel.schemeId);
                }
                else
                {
                    wfSchemeInfoEntity = wfSchemeInfoService.GetEntityByModuleId(wfCPParameterModel.moduleId);
                    wfProcessSchemeEntity = wfProcessSchemeService.GetEntityByModuleId(wfCPParameterModel.moduleId);
                    
                }
                if (wfSchemeInfoEntity == null)
                {
                    wfSchemeInfoEntity = wfSchemeInfoService.GetEntityByModuleId(wfCPParameterModel.moduleId);
                    wfProcessSchemeEntity = wfProcessSchemeService.GetEntityByModuleId(wfCPParameterModel.moduleId);
                }
                
                if (wfSchemeInfoEntity == null){
                    throw (new Exception("没有流程模板值!"));
                }
                wfCPParameterModel.schemeId = wfSchemeInfoEntity.F_Id;

                if (wfProcessSchemeEntity == null || wfSchemeInfoEntity.F_ModifyDate != wfProcessSchemeEntity.F_SchemeVersion)
                {
                    wfProcessSchemeEntity = new WFProcessSchemeEntity();
                    wfProcessSchemeEntity.F_SchemeInfoId = wfSchemeInfoEntity.F_Id;
                    wfProcessSchemeEntity.F_ModuleId = wfSchemeInfoEntity.F_ModuleId;
                    wfProcessSchemeEntity.F_SchemeVersion = wfSchemeInfoEntity.F_ModifyDate;
                    wfProcessSchemeEntity.F_SchemeContent = wfSchemeInfoEntity.F_SchemeContent;
                    processSchemeId = wfProcessSchemeService.SaveEntity("", wfProcessSchemeEntity);
                }
                else
                {
                    processSchemeId = wfProcessSchemeEntity.F_Id;
                }
                #endregion

                #region 初始化流程模板内容数据
                IWfRuntime wfRuntime = new WfRuntime(wfSchemeInfoEntity.F_SchemeContent);
                WFNodeModel startNode = wfRuntime.getStartNode();
                List<WFNodeModel> nextNodes = wfRuntime.getNextNodes(startNode.id, wfCPParameterModel.formData);
                if (nextNodes.Count == 0)
                {
                    throw (new Exception("无法寻找到下一个节点!"));
                }
                #endregion

                #region 实例信息
                WFProcessInstanceEntity wfProcessInstanceEntity = new WFProcessInstanceEntity();

                wfProcessInstanceEntity.F_Id = wfCPParameterModel.processId;
                wfProcessInstanceEntity.F_ProcessSchemeId = processSchemeId;
                wfProcessInstanceEntity.F_Code = wfSchemeInfoEntity.F_SchemeName;
                wfProcessInstanceEntity.F_SchemeType = wfSchemeInfoEntity.F_SchemeType;
                wfProcessInstanceEntity.F_EnabledMark = 1;//正式运行

                wfProcessInstanceEntity.F_Name = wfCPParameterModel.processName;
                wfProcessInstanceEntity.F_WfLevel = wfCPParameterModel.level;
                wfProcessInstanceEntity.F_Description = wfCPParameterModel.description;
                
                //流转的节点信息
                List<WFProcessNodesEntity> wfProcessNodeList = new List<WFProcessNodesEntity>();
                List<WFProcessTransitionHistoryEntity> processTransitionHistoryList = new List<WFProcessTransitionHistoryEntity>();
                List<WFDelegateRecordEntity> delegateRecordEntitylist = new List<WFDelegateRecordEntity>();

                WFProcessNodesEntity wfProcessNodesEntity1 = new WFProcessNodesEntity();
                wfProcessNodesEntity1.F_ProcessId = wfCPParameterModel.processId;
                wfProcessNodesEntity1.F_NodeId = startNode.id;
                wfProcessNodesEntity1.F_NodeName = startNode.name;
                wfProcessNodesEntity1.F_NodeType = startNode.type;
                wfProcessNodesEntity1.F_IsActivtyId = 0;
                wfProcessNodesEntity1.F_NodeSate = 1;
                wfProcessNodeList.Add(wfProcessNodesEntity1);

                foreach (var nodeOne in nextNodes)
                {
                    WFProcessNodesEntity wfProcessNodesEntity = new WFProcessNodesEntity();
                    wfProcessNodesEntity.F_ProcessId = wfCPParameterModel.processId;
                    wfProcessNodesEntity.F_NodeId = nodeOne.id;
                    wfProcessNodesEntity.F_NodeName = nodeOne.name;
                    wfProcessNodesEntity.F_NodeType = nodeOne.type;
                    wfProcessNodesEntity.F_IsActivtyId = 1;
                    wfProcessNodesEntity.F_NodeSate = 0;
                    wfProcessNodesEntity.F_PreviousId = startNode.id;
                    wfProcessNodesEntity.F_PreviousName = startNode.name;
                    wfProcessNodesEntity.F_UserList = GetMakerList(nodeOne);
                    #region 委托记录
                    List<WFDelegateRecordEntity> delegateRecordEntitylistOne = GetDelegateRecordList(wfCPParameterModel, wfProcessInstanceEntity.F_Code, wfProcessNodesEntity.F_UserList);
                    delegateRecordEntitylist.AddRange(delegateRecordEntitylistOne);
                    wfProcessNodesEntity.F_UserList += delegateUserList;
                    #endregion

                    wfProcessNodeList.Add(wfProcessNodesEntity);

                    #region 流转记录
                    WFProcessTransitionHistoryEntity processTransitionHistoryEntity = new WFProcessTransitionHistoryEntity();
                    processTransitionHistoryEntity.F_ProcessId = wfCPParameterModel.processId;
                    processTransitionHistoryEntity.F_FromNodeId = startNode.id;
                    processTransitionHistoryEntity.F_FromNodeName = startNode.name;
                    processTransitionHistoryEntity.F_FromNodeType = startNode.type;
                    processTransitionHistoryEntity.F_ToNodeId = nodeOne.id;
                    processTransitionHistoryEntity.F_ToNodeName = nodeOne.name;
                    processTransitionHistoryEntity.F_ToNodeType = nodeOne.type;
                    processTransitionHistoryEntity.F_TransitionSate = 0;
                    processTransitionHistoryList.Add(processTransitionHistoryEntity);
                    #endregion
                }
                #endregion

                #region 流程操作记录
                WFProcessOperationHistoryEntity processOperationHistoryEntity = new WFProcessOperationHistoryEntity();
                processOperationHistoryEntity.F_ProcessId = wfCPParameterModel.processId;
                processOperationHistoryEntity.F_Type = 0;
                processOperationHistoryEntity.F_Content = "【创建】" + OperatorProvider.Provider.Current().UserName + "创建了一个流程进程【" + wfProcessInstanceEntity.F_Code + "/" + wfProcessInstanceEntity.F_Name + "】";
                #endregion

                #region 表单模板的保存和表单数据
                FormModuleEntity formModuleEntity = server.GetEntity(wfCPParameterModel.formId);
                Form_ModuleContentEntity contentEntity = serverContent.GetEntity(wfCPParameterModel.formId, formModuleEntity.F_Version);
                if (contentEntity == null)//此表单当前模板还没有保存
                {
                    Form_ModuleContentEntity formModuleContentEntity = new Form_ModuleContentEntity()
                    {
                        F_FrmContent = formModuleEntity.F_FrmContent,
                        F_FrmId = formModuleEntity.F_FrmId,
                        F_FrmVersion = formModuleEntity.F_Version
                    };
                    serverContent.SaveEntity("", formModuleContentEntity);
                    contentEntity = formModuleContentEntity;
                }
                FormModuleInstanceEntity instanceEntity = new FormModuleInstanceEntity();
                instanceEntity.F_FrmContentId = contentEntity.F_Id;
                instanceEntity.F_ObjectId = wfCPParameterModel.processId;
                instanceEntity.F_FrmInstanceJson = wfCPParameterModel.formData;
                serverInstance.SaveEntity("", instanceEntity, contentEntity);

                #endregion

                WFCreateProcessModel wfCreateProcessModel = new WFCreateProcessModel()
                {
                    processInstanceEntity = wfProcessInstanceEntity,
                    processNodeList = wfProcessNodeList,
                    processTransitionHistoryList = processTransitionHistoryList,
                    delegateRecordEntitylist = delegateRecordEntitylist,
                    processOperationHistoryEntity = processOperationHistoryEntity
                };
                wfProcessInstanceService.CreateProcess(wfCreateProcessModel);

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 编辑流程实例
        /// </summary>
        /// <param name="wfCPParameterModel"></param>
        /// <returns></returns>
        public void EditProcessInstance(WFCPParameterModel wfCPParameterModel)
        {
            try
            {
                if (string.IsNullOrEmpty(wfCPParameterModel.processId))
                {
                    throw (new Exception("没有设置processId值!"));
                }
                #region 获取实例
                WFProcessInstanceEntity wfProcessInstanceEntityOld = wfProcessInstanceService.GetEntity(wfCPParameterModel.processId);
                WFProcessSchemeEntity wfProcessSchemeEntity = wfProcessSchemeService.GetEntity(wfProcessInstanceEntityOld.F_ProcessSchemeId);
                wfCPParameterModel.schemeId = wfProcessSchemeEntity.F_SchemeInfoId;
                #endregion

                #region 初始化流程模板内容数据
                IWfRuntime wfRuntime = new WfRuntime(wfProcessSchemeEntity.F_SchemeContent);
                WFNodeModel startNode = wfRuntime.getStartNode();
                List<WFNodeModel> nextNodes = wfRuntime.getNextNodes(startNode.id, wfCPParameterModel.formData);
                if (nextNodes.Count == 0)
                {
                    throw (new Exception("无法寻找到下一个节点!"));
                }
                #endregion

                #region 实例信息
                WFProcessInstanceEntity wfProcessInstanceEntity = new WFProcessInstanceEntity();

                wfProcessInstanceEntity.F_Id = wfCPParameterModel.processId;
                wfProcessInstanceEntity.F_EnabledMark = 1;//正式运行
                wfProcessInstanceEntity.F_IsFinished = 1;//表示正常运行
                wfProcessInstanceEntity.F_Name = wfCPParameterModel.processName;
                wfProcessInstanceEntity.F_WfLevel = wfCPParameterModel.level;
                wfProcessInstanceEntity.F_Description = wfCPParameterModel.description;

                //流转的节点信息
                List<WFProcessNodesEntity> wfProcessNodeList = new List<WFProcessNodesEntity>();
                List<WFProcessTransitionHistoryEntity> processTransitionHistoryList = new List<WFProcessTransitionHistoryEntity>();
                List<WFDelegateRecordEntity> delegateRecordEntitylist = new List<WFDelegateRecordEntity>();
                foreach (var nodeOne in nextNodes)
                {
                    WFProcessNodesEntity wfProcessNodesEntity = new WFProcessNodesEntity();
                    wfProcessNodesEntity.F_ProcessId = wfCPParameterModel.processId;
                    wfProcessNodesEntity.F_NodeId = nodeOne.id;
                    wfProcessNodesEntity.F_NodeName = nodeOne.name;
                    wfProcessNodesEntity.F_NodeType = nodeOne.type;
                    wfProcessNodesEntity.F_IsActivtyId = 1;
                    wfProcessNodesEntity.F_NodeSate = 0;
                    wfProcessNodesEntity.F_PreviousId = startNode.id;
                    wfProcessNodesEntity.F_PreviousName = startNode.name;
                    wfProcessNodesEntity.F_UserList = GetMakerList(nodeOne);
                    #region 委托记录
                    List<WFDelegateRecordEntity> delegateRecordEntitylistOne = GetDelegateRecordList(wfCPParameterModel, wfProcessInstanceEntity.F_Code, wfProcessNodesEntity.F_UserList);
                    delegateRecordEntitylist.AddRange(delegateRecordEntitylistOne);
                    wfProcessNodesEntity.F_UserList += delegateUserList;
                    #endregion

                    wfProcessNodeList.Add(wfProcessNodesEntity);

                    #region 流转记录
                    WFProcessTransitionHistoryEntity processTransitionHistoryEntity = new WFProcessTransitionHistoryEntity();
                    processTransitionHistoryEntity.F_ProcessId = wfCPParameterModel.processId;
                    processTransitionHistoryEntity.F_FromNodeId = startNode.id;
                    processTransitionHistoryEntity.F_FromNodeName = startNode.name;
                    processTransitionHistoryEntity.F_FromNodeType = startNode.type;
                    processTransitionHistoryEntity.F_ToNodeId = nodeOne.id;
                    processTransitionHistoryEntity.F_ToNodeName = nodeOne.name;
                    processTransitionHistoryEntity.F_ToNodeType = nodeOne.type;
                    processTransitionHistoryEntity.F_TransitionSate = 0;
                    processTransitionHistoryList.Add(processTransitionHistoryEntity);
                    #endregion
                }
                #endregion

                #region 流程操作记录
                WFProcessOperationHistoryEntity processOperationHistoryEntity = new WFProcessOperationHistoryEntity();
                processOperationHistoryEntity.F_ProcessId = wfCPParameterModel.processId;
                processOperationHistoryEntity.F_Type = 0;
                processOperationHistoryEntity.F_Content = "【重新发起】" + OperatorProvider.Provider.Current().UserName + "创建了一个流程进程【" + wfProcessInstanceEntity.F_Code + "/" + wfProcessInstanceEntity.F_Name + "】";
                #endregion

                #region 表单模板的保存和表单数据
                FormModuleEntity formModuleEntity = server.GetEntity(wfCPParameterModel.formId);
                Form_ModuleContentEntity contentEntity = serverContent.GetEntity(wfCPParameterModel.formId, formModuleEntity.F_Version);
                if (contentEntity == null)//此表单当前模板还没有保存
                {
                    Form_ModuleContentEntity formModuleContentEntity = new Form_ModuleContentEntity()
                    {
                        F_FrmContent = formModuleEntity.F_FrmContent,
                        F_FrmId = formModuleEntity.F_FrmId,
                        F_FrmVersion = formModuleEntity.F_Version
                    };
                    serverContent.SaveEntity(wfCPParameterModel.formInstanceId, formModuleContentEntity);
                    contentEntity = formModuleContentEntity;
                }
                FormModuleInstanceEntity instanceEntity = new FormModuleInstanceEntity();
                instanceEntity.F_FrmContentId = contentEntity.F_Id;
                instanceEntity.F_ObjectId = wfCPParameterModel.processId;
                instanceEntity.F_FrmInstanceJson = wfCPParameterModel.formData;
                serverInstance.SaveEntity("", instanceEntity, contentEntity);
                #endregion

                WFCreateProcessModel wfCreateProcessModel = new WFCreateProcessModel()
                {
                    processInstanceEntity = wfProcessInstanceEntity,
                    processNodeList = wfProcessNodeList,
                    processTransitionHistoryList = processTransitionHistoryList,
                    delegateRecordEntitylist = delegateRecordEntitylist,
                    processOperationHistoryEntity = processOperationHistoryEntity
                };
                wfProcessInstanceService.EditProcess(wfCreateProcessModel);

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 节点审核
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="flag"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public void NodeVerification(WFVerificationParameterModel wfVerificationParameterModel)
        {
            try
            {
                WFProcessNodesEntity wfProcessNodesEntity = wfProcessInstanceService.GetProcessNode(wfVerificationParameterModel.nodeId);
                WFProcessNodesEntity wfProcessNode = new WFProcessNodesEntity();
                WFVerificationProcessModel wfVerificationProcessModel = new WFVerificationProcessModel();

                WFProcessInstanceEntity wfProcessInstanceEntity = wfProcessInstanceService.GetEntity(wfVerificationParameterModel.processId);
                WFProcessSchemeEntity wfProcessSchemeEntity = wfProcessSchemeService.GetEntity(wfProcessInstanceEntity.F_ProcessSchemeId);
                IWfRuntime wfRuntime = new WfRuntime(wfProcessSchemeEntity.F_SchemeContent);
                WFNodeModel currentNode = wfRuntime.getCurrentNode(wfProcessNodesEntity.F_NodeId);
                List<WFNodeModel> nextNodes = wfRuntime.getNextNodes(wfProcessNodesEntity.F_NodeId, wfVerificationParameterModel.formDataList);

                ILeaWorkFlow iLeaWorkFlow = null;
                if (!string.IsNullOrEmpty(currentNode.setInfo.nodeSysFn))
                {
                    iLeaWorkFlow = UnityIocHelper.wfDBwinstance.GetService<ILeaWorkFlow>(currentNode.setInfo.nodeSysFn);
                }
                else
                {
                    iLeaWorkFlow = UnityIocHelper.wfDBwinstance.GetService<ILeaWorkFlow>();
                }
                List<WFProcessNodesEntity> wfProcessNodeList = new List<WFProcessNodesEntity>();
                List<WFProcessTransitionHistoryEntity> processTransitionHistoryList = new List<WFProcessTransitionHistoryEntity>();
                List<WFDelegateRecordEntity> delegateRecordEntitylist = new List<WFDelegateRecordEntity>();

                if (wfVerificationParameterModel.isOk)//通过
                {
                   
                    #region 获取流转节点
                    foreach (var nodeOne in nextNodes)
                    {
                        if (nodeOne.type == "confluencenode")//如果是会签的节点
                        {
                            WFProcessNodesEntity confluencenode = wfProcessInstanceService.GetProcessNode(nodeOne.id, wfVerificationParameterModel.processId);
                            if (confluencenode == null)
                            {
                                confluencenode = new WFProcessNodesEntity();
                                confluencenode.F_NodeSate = 10;//会签节点
                                confluencenode.F_ConfluenceOkNum = 0;
                                confluencenode.F_ProcessId = wfVerificationParameterModel.processId;
                                confluencenode.F_NodeId = nodeOne.id;
                                confluencenode.F_NodeName = nodeOne.name;
                                confluencenode.F_NodeType = nodeOne.type;
                                confluencenode.F_IsActivtyId = 1;
                            }

                            int confluencenodeOkNum = wfRuntime.getToNodeNum(nodeOne.id);
                            List<WFNodeModel> nextNodes2 = null;
                            bool isconfluencenodeOk = false;
                            switch (nodeOne.setInfo.nodeConfluenceType)
                            {
                                case "0"://所有步骤通过
                                    if (confluencenodeOkNum == (confluencenode.F_ConfluenceOkNum + 1))
                                    {
                                        isconfluencenodeOk = true;
                                    }
                                    break;
                                case "1"://一个步骤通过即可
                                    isconfluencenodeOk = true;
                                    break;
                                case "2"://按百分比计算
                                    if ((confluencenode.F_ConfluenceOkNum + 1) >= (confluencenodeOkNum * int.Parse(nodeOne.setInfo.nodeConfluenceRate) / 100))
                                    {
                                        isconfluencenodeOk = true;
                                    }
                                    break;
                            }
                            #region 会签通过后
                            WFProcessNodesEntity confluencenode1 = new WFProcessNodesEntity();
                            if (isconfluencenodeOk)
                            {
                                confluencenode1.F_NodeSate = 1;

                                nextNodes2 = wfRuntime.getNextNodes(nodeOne.id, wfVerificationParameterModel.formDataList);
                                foreach (var nodeOne2 in nextNodes2)
                                {
                                    WFProcessNodesEntity wfProcessNodesOne = new WFProcessNodesEntity();
                                    wfProcessNodesOne.F_ProcessId = wfVerificationParameterModel.processId;
                                    wfProcessNodesOne.F_NodeId = nodeOne2.id;
                                    wfProcessNodesOne.F_NodeName = nodeOne2.name;
                                    wfProcessNodesOne.F_NodeType = nodeOne2.type;
                                    wfProcessNodesOne.F_IsActivtyId = 1;
                                    wfProcessNodesOne.F_NodeSate = 0;
                                    wfProcessNodesOne.F_PreviousId = wfProcessNodesEntity.F_NodeId;
                                    wfProcessNodesOne.F_PreviousName = wfProcessNodesEntity.F_NodeName;
                                    wfProcessNodesOne.F_UserList = GetMakerList(nodeOne2, wfProcessInstanceEntity.F_CreateUserId);
                                    #region 委托记录
                                    List<WFDelegateRecordEntity> delegateRecordEntitylistOne = GetDelegateRecordList(wfProcessInstanceEntity.F_Id, wfProcessInstanceEntity.F_Name, wfProcessSchemeEntity.F_SchemeInfoId, wfProcessInstanceEntity.F_Code, wfProcessNodesEntity.F_UserList);
                                    delegateRecordEntitylist.AddRange(delegateRecordEntitylistOne);
                                    wfProcessNodesEntity.F_UserList += delegateUserList;
                                    #endregion

                                    wfProcessNodeList.Add(wfProcessNodesOne);

                                    #region 流转记录
                                    WFProcessTransitionHistoryEntity processTransitionHistoryEntity = new WFProcessTransitionHistoryEntity();
                                    processTransitionHistoryEntity.F_ProcessId = wfVerificationParameterModel.processId;
                                    processTransitionHistoryEntity.F_FromNodeId = wfProcessNodesEntity.F_NodeId;
                                    processTransitionHistoryEntity.F_FromNodeName = wfProcessNodesEntity.F_NodeName;
                                    processTransitionHistoryEntity.F_FromNodeType = wfProcessNodesEntity.F_NodeType;
                                    processTransitionHistoryEntity.F_ToNodeId = nodeOne2.id;
                                    processTransitionHistoryEntity.F_ToNodeName = nodeOne2.name;
                                    processTransitionHistoryEntity.F_ToNodeType = nodeOne2.type;
                                    processTransitionHistoryEntity.F_TransitionSate = 0;
                                    processTransitionHistoryList.Add(processTransitionHistoryEntity);
                                    #endregion
                                }
                            }
                            confluencenode.F_ConfluenceOkNum++;
                            
                            confluencenode1.F_Id = confluencenode.F_Id;
                            confluencenode1.F_NodeId = confluencenode.F_NodeId;
                            confluencenode1.F_ProcessId = confluencenode.F_ProcessId;
                            confluencenode1.F_ConfluenceOkNum = confluencenode.F_ConfluenceOkNum;
                            wfProcessNodeList.Add(confluencenode1);
                            #endregion
                        }
                        else
                        {
                            WFProcessNodesEntity wfProcessNodesOne = new WFProcessNodesEntity();
                            wfProcessNodesOne.F_ProcessId = wfVerificationParameterModel.processId;
                            wfProcessNodesOne.F_NodeId = nodeOne.id;
                            wfProcessNodesOne.F_NodeName = nodeOne.name;
                            wfProcessNodesOne.F_NodeType = nodeOne.type;
                            wfProcessNodesOne.F_IsActivtyId = 1;
                            wfProcessNodesOne.F_NodeSate = 0;
                            wfProcessNodesOne.F_PreviousId = wfProcessNodesEntity.F_NodeId;
                            wfProcessNodesOne.F_PreviousName = wfProcessNodesEntity.F_NodeName;
                            wfProcessNodesOne.F_UserList = GetMakerList(nodeOne, wfProcessInstanceEntity.F_CreateUserId);
                            #region 委托记录
                            List<WFDelegateRecordEntity> delegateRecordEntitylistOne = GetDelegateRecordList(wfProcessInstanceEntity.F_Id, wfProcessInstanceEntity.F_Name, wfProcessSchemeEntity.F_SchemeInfoId, wfProcessInstanceEntity.F_Code, wfProcessNodesEntity.F_UserList);
                            delegateRecordEntitylist.AddRange(delegateRecordEntitylistOne);
                            wfProcessNodesEntity.F_UserList += delegateUserList;
                            #endregion

                            wfProcessNodeList.Add(wfProcessNodesOne);

                            #region 流转记录
                            WFProcessTransitionHistoryEntity processTransitionHistoryEntity = new WFProcessTransitionHistoryEntity();
                            processTransitionHistoryEntity.F_ProcessId = wfVerificationParameterModel.processId;
                            processTransitionHistoryEntity.F_FromNodeId = wfProcessNodesEntity.F_NodeId;
                            processTransitionHistoryEntity.F_FromNodeName = wfProcessNodesEntity.F_NodeName;
                            processTransitionHistoryEntity.F_FromNodeType = wfProcessNodesEntity.F_NodeType;
                            processTransitionHistoryEntity.F_ToNodeId = nodeOne.id;
                            processTransitionHistoryEntity.F_ToNodeName = nodeOne.name;
                            processTransitionHistoryEntity.F_ToNodeType = nodeOne.type;
                            processTransitionHistoryEntity.F_TransitionSate = 0;
                            processTransitionHistoryList.Add(processTransitionHistoryEntity);
                            #endregion
                        }
                    }
                    #endregion

                    #region 操作记录
                    WFProcessOperationHistoryEntity processOperationHistoryEntity = new WFProcessOperationHistoryEntity();
                    processOperationHistoryEntity.F_ProcessId = wfVerificationParameterModel.processId;
                    processOperationHistoryEntity.F_Type = 2;
                    processOperationHistoryEntity.F_Content = "【审核】【" + OperatorProvider.Provider.Current().UserName + "】【" + wfProcessNodesEntity.F_NodeName + "】【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "】同意,备注：" + wfVerificationParameterModel.description;
                    #endregion

                    wfProcessNode.F_Id = wfProcessNodesEntity.F_Id;
                    wfProcessNode.F_NodeSate = 1;
                    wfProcessNode.F_IsActivtyId = 0;
                    wfProcessNode.F_Description = wfVerificationParameterModel.description;
                    wfVerificationProcessModel.processNodeList = wfProcessNodeList;
                    wfVerificationProcessModel.processTransitionHistoryList = processTransitionHistoryList;
                    wfVerificationProcessModel.delegateRecordEntitylist = delegateRecordEntitylist;
                    wfVerificationProcessModel.node = wfProcessNode;
                    wfVerificationProcessModel.processOperationHistoryEntity = processOperationHistoryEntity;

                    wfProcessInstanceService.SaveProcessVerification(wfVerificationProcessModel);

                    #region 触发SQL语句
                    if (!string.IsNullOrEmpty(currentNode.setInfo.nodeSQL))
                    {
                        string sql = string.Format(currentNode.setInfo.nodeSQL, wfVerificationParameterModel.processId);
                        dataBaseServer.ExecuteBySql(currentNode.setInfo.nodeDbId, sql);
                    }
                    #endregion

                    #region 触发一个事件
                    iLeaWorkFlow.Sucess(wfVerificationParameterModel.processId);
                    #endregion

                    #region 保存下表单数据
                    foreach (var formObj in wfVerificationParameterModel.formDataList)
                    {
                        FormModuleEntity formModuleEntity = server.GetEntity(formObj.formId);
                        Form_ModuleContentEntity contentEntity = serverContent.GetEntity(formObj.formId, formModuleEntity.F_Version);
                        if (contentEntity == null)//此表单当前模板还没有保存
                        {
                            Form_ModuleContentEntity formModuleContentEntity = new Form_ModuleContentEntity()
                            {
                                F_FrmContent = formModuleEntity.F_FrmContent,
                                F_FrmId = formModuleEntity.F_FrmId,
                                F_FrmVersion = formModuleEntity.F_Version
                            };
                            serverContent.SaveEntity("", formModuleContentEntity);
                            contentEntity = formModuleContentEntity;
                        }
                        FormModuleInstanceEntity instanceEntity = new FormModuleInstanceEntity();
                        instanceEntity.F_FrmContentId = contentEntity.F_Id;
                        instanceEntity.F_ObjectId = wfVerificationParameterModel.processId;
                        instanceEntity.F_FrmInstanceJson = formObj.formData;
                        serverInstance.SaveEntity(instanceEntity, contentEntity);
                    }
                    #endregion

                }
                else//不通过
                {
                    #region 节点更新
                    wfProcessNode.F_Id = wfProcessNodesEntity.F_Id;
                    wfProcessNode.F_NodeSate = 2;
                    wfProcessNode.F_Description = wfVerificationParameterModel.description;
                    #endregion

                    #region 操作记录
                    WFProcessOperationHistoryEntity processOperationHistoryEntity = new WFProcessOperationHistoryEntity();
                    processOperationHistoryEntity.F_ProcessId = wfVerificationParameterModel.processId;
                    processOperationHistoryEntity.F_Type = 2;
                    processOperationHistoryEntity.F_Content = "【审核】【" + OperatorProvider.Provider.Current().UserName + "】【" + wfProcessNodesEntity.F_NodeName + "】【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "】不同意,备注：" + wfVerificationParameterModel.description;
                    #endregion

                    #region 跳转到驳回的节点
                    if (currentNode.setInfo.nodeRejectType != "3")
                    {
                        WFNodeModel turnDownNode = getTurnDownNode(wfVerificationParameterModel.processId, currentNode, wfRuntime);
                        if (turnDownNode == null)
                        {
                            throw (new Exception("驳回找不到节点!"));
                        }

                        WFProcessNodesEntity wfProcessNodesOne = new WFProcessNodesEntity();
                        wfProcessNodesOne.F_ProcessId = wfVerificationParameterModel.processId;
                        wfProcessNodesOne.F_NodeId = turnDownNode.id;
                        wfProcessNodesOne.F_NodeName = turnDownNode.name;
                        wfProcessNodesOne.F_NodeType = turnDownNode.type;
                        wfProcessNodesOne.F_IsActivtyId = 1;
                        wfProcessNodesOne.F_NodeSate = 0;
                        wfProcessNodesOne.F_PreviousId = wfProcessNodesEntity.F_NodeId;
                        wfProcessNodesOne.F_PreviousName = wfProcessNodesEntity.F_NodeName;

                        if (turnDownNode.type == "startround")//如果是开始节点，需要重新提交
                        {
                            wfProcessNodesOne.F_UserList = wfProcessInstanceEntity.F_CreateUserId;
                            wfVerificationProcessModel.wfProcessInstanceEntity = new WFProcessInstanceEntity() { 
                                F_IsFinished = 0,
                                F_Id = wfProcessInstanceEntity.F_Id
                            };//表示流程需要重新发起
                        }
                        else
                        {
                            if (turnDownNode.setInfo.nodeDesignate == "4")//前一步骤领导
                            {
                                wfProcessNodesOne.F_UserList = wfProcessNodesEntity.F_CreateUserId;
                            }
                            else
                            {
                                wfProcessNodesOne.F_UserList = GetMakerList(turnDownNode, wfProcessInstanceEntity.F_CreateUserId);
                            }
                        }

                        #region 委托记录
                        List<WFDelegateRecordEntity> delegateRecordEntitylistOne = GetDelegateRecordList(wfProcessInstanceEntity.F_Id, wfProcessInstanceEntity.F_Name, wfProcessSchemeEntity.F_SchemeInfoId, wfProcessInstanceEntity.F_Code, wfProcessNodesEntity.F_UserList);
                        delegateRecordEntitylist.AddRange(delegateRecordEntitylistOne);
                        wfProcessNodesEntity.F_UserList += delegateUserList;
                        #endregion

                        wfProcessNodeList.Add(wfProcessNodesOne);

                        #region 流转记录
                        WFProcessTransitionHistoryEntity processTransitionHistoryEntity = new WFProcessTransitionHistoryEntity();
                        processTransitionHistoryEntity.F_ProcessId = wfVerificationParameterModel.processId;
                        processTransitionHistoryEntity.F_FromNodeId = wfProcessNodesEntity.F_NodeId;
                        processTransitionHistoryEntity.F_FromNodeName = wfProcessNodesEntity.F_NodeName;
                        processTransitionHistoryEntity.F_FromNodeType = wfProcessNodesEntity.F_NodeType;
                        processTransitionHistoryEntity.F_ToNodeId = turnDownNode.id;
                        processTransitionHistoryEntity.F_ToNodeName = turnDownNode.name;
                        processTransitionHistoryEntity.F_ToNodeType = turnDownNode.type;
                        processTransitionHistoryEntity.F_TransitionSate = 0;
                        processTransitionHistoryList.Add(processTransitionHistoryEntity);
                        #endregion
                    }
                    #endregion

                    #region 保存下表单数据
                    foreach (var formObj in wfVerificationParameterModel.formDataList)
                    {
                        FormModuleEntity formModuleEntity = server.GetEntity(formObj.formId);
                        Form_ModuleContentEntity contentEntity = serverContent.GetEntity(formObj.formId, formModuleEntity.F_Version);
                        if (contentEntity == null)//此表单当前模板还没有保存
                        {
                            Form_ModuleContentEntity formModuleContentEntity = new Form_ModuleContentEntity()
                            {
                                F_FrmContent = formModuleEntity.F_FrmContent,
                                F_FrmId = formModuleEntity.F_FrmId,
                                F_FrmVersion = formModuleEntity.F_Version
                            };
                            serverContent.SaveEntity("", formModuleContentEntity);
                            contentEntity = formModuleContentEntity;
                        }
                        FormModuleInstanceEntity instanceEntity = new FormModuleInstanceEntity();
                        instanceEntity.F_FrmContentId = contentEntity.F_Id;
                        instanceEntity.F_ObjectId = wfVerificationParameterModel.processId;
                        instanceEntity.F_FrmInstanceJson = formObj.formData;
                        serverInstance.SaveEntity(instanceEntity, contentEntity);
                    }
                    #endregion

                    wfVerificationProcessModel.node = wfProcessNode;
                    wfVerificationProcessModel.processOperationHistoryEntity = processOperationHistoryEntity;
                    wfVerificationProcessModel.processNodeList = wfProcessNodeList;
                    wfVerificationProcessModel.processTransitionHistoryList = processTransitionHistoryList;
                    wfVerificationProcessModel.delegateRecordEntitylist = delegateRecordEntitylist;

                    wfProcessInstanceService.SaveProcessVerification(wfVerificationProcessModel);

                    #region 触发一个事件
                    iLeaWorkFlow.Fail(wfVerificationParameterModel.processId);
                    #endregion
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取我的流程数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public DataTable GetMyProcessPageList(Pagination pagination, string queryJson)
        {
            return wfProcessInstanceService.GetMyProcessPageList(pagination, queryJson);
        }

        /// <summary>
        /// 获取实例进程模板的实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public WFProcessInstanceEntity GetProcessEntity(string keyValue)
        {
            return wfProcessInstanceService.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取工作流实例里所有流转的节点
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public IEnumerable<WFProcessNodesEntity> GetProcessNodeList(string processId)
        {
            return wfProcessInstanceService.GetProcessNodeList(processId);
        }
        /// <summary>
        /// 获取实例进程模板的实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public WFProcessSchemeEntity GetProcessSchemeEntity(string keyValue)
        {
            return wfProcessSchemeService.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取登录者需要处理的流程
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetToMeBeforePageList(Pagination pagination, string queryJson)
        {
            return wfProcessInstanceService.GetToMeBeforePageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取登录者已经处理的流程
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetToMeAfterPageList(Pagination pagination, string queryJson)
        {
            return wfProcessInstanceService.GetToMeAfterPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取流程实例分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson)
        {
            return wfProcessInstanceService.GetPageList(pagination, queryJson);
        }

        #region 提交数据
        /// <summary>
        /// 流程指派
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="makeLists"></param>
        public void DesignateProcess(string processId, string makeLists)
        {
            wfProcessInstanceService.DesignateProcess(processId, makeLists);
        }
        /// <summary>
        /// 删除工作流实例进程
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public int DeleteProcess(string keyValue)
        {
            return wfProcessInstanceService.DeleteProcess(keyValue);
        }
        /// <summary>
        /// 虚拟操作实例
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="state">0暂停,1启用,2取消（召回）</param>
        /// <returns></returns>
        public int OperateVirtualProcess(string keyValue, int state)
        {
            return wfProcessInstanceService.OperateVirtualProcess(keyValue, state);
        }
        #endregion

        #endregion

        #region 流程实例处理公共方法
        /// <summary>
        /// 寻找该节点执行人
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private string GetMakerList(WFNodeModel node,string createUserId = null)
        {
            try
            {
                string makerlsit = "";
                string userId = OperatorProvider.Provider.Current().UserId;
                if (!string.IsNullOrEmpty(createUserId))
                {
                    userId = createUserId;
                }
                UserEntity userEntity = null;
                if (node.setInfo == null)
                {
                    makerlsit = "1";
                }
                else
                {
                    switch (node.setInfo.nodeDesignate)
                    {
                        case "1"://所有成员
                            makerlsit = "1";
                            break;
                        case "2"://指定成员
                            makerlsit = ArrwyToString(node.setInfo.nodeDesignateData.role, makerlsit);
                            makerlsit = ArrwyToString(node.setInfo.nodeDesignateData.post, makerlsit);
                            makerlsit = ArrwyToString(node.setInfo.nodeDesignateData.usergroup, makerlsit);
                            makerlsit = ArrwyToString(node.setInfo.nodeDesignateData.user, makerlsit);
                            if (string.IsNullOrEmpty(makerlsit))
                            {
                                makerlsit = "-1";
                            }
                            break;
                        case "3"://发起者领导
                            userEntity = userService.GetEntity(userId);
                            if (string.IsNullOrEmpty(userEntity.F_ManagerId))
                            {
                                makerlsit = "-1";
                            }
                            else
                            {
                                makerlsit = userEntity.F_ManagerId;
                            }
                            break;
                        case "4"://前一步骤领导
                            userEntity = userService.GetEntity(OperatorProvider.Provider.Current().UserId);
                            if (string.IsNullOrEmpty(userEntity.F_ManagerId))
                            {
                                makerlsit = "-1";
                            }
                            else
                            {
                                makerlsit = userEntity.F_ManagerId;
                            }
                            break;
                        case "5"://发起者部门领导
                            userEntity = userService.GetEntity(userId);
                            DepartmentEntity departmentEntity = departmentService.GetEntity(userEntity.F_DepartmentId);
                            if (string.IsNullOrEmpty(departmentEntity.F_ManagerId))
                            {
                                makerlsit = "-1";
                            }
                            else
                            {
                                makerlsit = departmentEntity.F_ManagerId;
                            }
                            break;
                        case "6"://发起者公司领导
                            userEntity = userService.GetEntity(userId);
                            OrganizeEntity organizeEntity = organizeService.GetEntity(userEntity.F_OrganizeId);

                            if (string.IsNullOrEmpty(organizeEntity.F_ManagerId))
                            {
                                makerlsit = "-1";
                            }
                            else
                            {
                                makerlsit = organizeEntity.F_ManagerId;
                            }
                            break;
                    }
                }
                if (makerlsit == "-1")
                {
                    throw (new Exception("无法寻找到节点的审核者,请查看流程设计是否有问题!"));
                }
                return makerlsit;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 将数组转化成逗号相隔的字串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Str"></param>
        /// <returns></returns>
        private string ArrwyToString(List<string> data, string Str)
        {
            string resStr = Str;
            foreach (var item in data)
            {
                if (resStr != "")
                {
                    resStr += ",";
                }
                resStr += item;
            }
            return resStr;
        }
        /// <summary>
        /// 获取委托记录列表
        /// </summary>
        /// <param name="wfCPParameterModel"></param>
        /// <param name="schemeCode"></param>
        /// <param name="makerList"></param>
        /// <returns></returns>
        private List<WFDelegateRecordEntity> GetDelegateRecordList(WFCPParameterModel wfCPParameterModel,string schemeCode, string makerList)
        {
            try
            {
                return GetDelegateRecordList(wfCPParameterModel.processId, wfCPParameterModel.processName, wfCPParameterModel.schemeId,schemeCode, makerList);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取委托记录列表
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="processName"></param>
        /// <param name="schemeId"></param>
        /// <param name="schemeCode"></param>
        /// <param name="makerList"></param>
        /// <returns></returns>
        private List<WFDelegateRecordEntity> GetDelegateRecordList(string processId, string processName, string schemeId, string schemeCode, string makerList)
        {
            try
            {
                delegateUserList = "";
                WFDelegateRecordEntity delegateRecordEntity = null;
                List<WFDelegateRecordEntity> delegateRecordEntitylist = new List<WFDelegateRecordEntity>();
                DataTable dt = wfDelegateRuleService.GetEntityBySchemeInfoId(schemeId, makerList.Split(','));
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["F_Id"].ToString()))
                    {
                        delegateRecordEntity = new WFDelegateRecordEntity();
                        delegateRecordEntity.F_WFDelegateRuleId = dr["F_Id"].ToString();
                        delegateRecordEntity.F_FromUserId = dr["F_CreateUserId"].ToString();
                        delegateRecordEntity.F_FromUserName = dr["F_CreateUserName"].ToString();
                        delegateRecordEntity.F_ToUserId = dr["F_ToUserId"].ToString();
                        delegateRecordEntity.F_ToUserName = dr["F_ToUserName"].ToString();

                        delegateRecordEntity.F_ProcessId = processId;
                        delegateRecordEntity.F_ProcessCode = schemeCode;
                        delegateRecordEntity.F_ProcessName = processName;

                        delegateRecordEntitylist.Add(delegateRecordEntity);

                        delegateUserList += "," + dr["F_ToUserId"].ToString();
                    }
                }

                return delegateRecordEntitylist;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取驳回的节点
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="node"></param>
        /// <param name="wfRuntime"></param>
        /// <returns></returns>
        private WFNodeModel getTurnDownNode(string processId, WFNodeModel node, IWfRuntime wfRuntime)
        {
            try
            {
                WFNodeModel toNode = new WFNodeModel();
                switch (node.setInfo.nodeRejectType)
                {
                    case "0"://前一步

                        IEnumerable<WFProcessTransitionHistoryEntity> entitylist = wfProcessTransitionHistoryService.GetEntityLast(processId, node.id);
                        foreach (var item in entitylist)
                        {
                            return wfRuntime.getCurrentNode(item.F_FromNodeId);
                        }
                        break;
                    case "1"://第一步
                        return wfRuntime.getStartNode();
                    case "2"://某一步
                        return wfRuntime.getCurrentNode(node.setInfo.nodeRejectStep);
                    case "3"://不处理
                        return null;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion
    }
}
