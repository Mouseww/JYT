using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using LeaRun.Util.Offices;
using System.Web;
using System.Threading;
using System;
using System.IO;
using LeaRun.Application.Code;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Cache;
using System.Linq;
using LeaRun.Application.Entity.PublicInfoManage;
using LeaRun.Application.Busines.PublicInfoManage;
using LeaRun.Application.Entity.SystemManage;
using System.Data.Common;

namespace LeaRun.Application.Web.Controllers
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 编辑人：LR0101 2016:11:14 22:08
    /// 日 期：2016.2.03 10:58
    /// 描 述：公共控制器
    /// </summary>
    public class UtilityController : Controller
    {
        #region 验证对象值不能重复
        #endregion

        #region 导入Excel
        System_SetExcelImprotBLL templatebll = new System_SetExcelImprotBLL();
        System_SetExcelImportFiledBLL filedBll = new System_SetExcelImportFiledBLL();
        /// <summary>
        /// Excel导入弹层
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExcelImportForm()
        {
            return View();
        }
        /// <summary>
        /// 下载excel模板文件
        /// </summary>
        /// <param name="templateId"></param>
        [HttpPost]
        public void DownExcelTemplate(string templateId)
        {
            var templateInfo = templatebll.GetEntity(templateId);
            var filedsInfo = filedBll.GetList("{\"F_ImportTemplateId\":\"" + templateInfo.F_Id + "\"}");

            //设置导出格式
            ExcelConfig excelconfig = new ExcelConfig();
            excelconfig.FileName = Server.UrlDecode(templateInfo.F_Name) + ".xls";
            excelconfig.IsAllSizeColumn = true;
            excelconfig.ColumnEntity = new List<ColumnEntity>();
            //表头
            List<GridColumnModel> columnData = new List<GridColumnModel>();
            DataTable dt = new DataTable();
            foreach (var col in filedsInfo)
            {
                if (col.F_RelationType != 1 && col.F_RelationType != 4 && col.F_RelationType != 5 && col.F_RelationType != 6 && col.F_RelationType != 7)
                {
                    excelconfig.ColumnEntity.Add(new ColumnEntity()
                    {
                        Column = col.F_FliedName,
                        ExcelColumn = col.F_ColName,
                        Alignment = "center",
                    });
                    dt.Columns.Add(col.F_FliedName, typeof(string));
                }
            }
            ExcelHelper.ExcelDownload(dt, excelconfig);
        }
        /// <summary>
        /// excel文件导入（通用）
        /// </summary>
        /// <param name="templateId"></param>
        /// <param name="Filedata"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExecuteImportExcel(string templateId, HttpPostedFileBase Filedata)
        {
            try
            {
                Thread.Sleep(500);////延迟500毫秒
                //没有文件上传，直接返回
                if (Filedata == null || string.IsNullOrEmpty(Filedata.FileName) || Filedata.ContentLength == 0)
                {
                    if (Request.Files.Count > 0)
                    {
                        Filedata = Request.Files[0];
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                string FileEextension = Path.GetExtension(Filedata.FileName);
                DataTable dt = ExcelHelper.ExcelImport(Filedata.InputStream, FileEextension);//excel导入数据
                var data = templatebll.ExcelImport(templateId, dt);

                var jsonData = new
                {
                    fileId = templateId,
                    Rows = data
                };

                return Content(jsonData.ToJson());
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        #endregion

        #region 导出Excel
        /// <summary>
        /// 请选择要导出的字段页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ExcelExportForm()
        {
            return View();
        }
        /// <summary>
        /// 执行导出Excel
        /// </summary>
        /// <param name="columnJson">表头</param>
        /// <param name="rowJson">数据</param>
        /// <param name="exportField">导出字段</param>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        [ValidateInput(false)]
        public void ExecuteExportExcel(string columnJson, string rowJson, string exportField, string filename)
        {
            //设置导出格式
            ExcelConfig excelconfig = new ExcelConfig();
            excelconfig.Title = Server.UrlDecode(filename);
            excelconfig.TitleFont = "微软雅黑";
            excelconfig.TitlePoint = 15;
            excelconfig.FileName = Server.UrlDecode(filename) + ".xls";
            excelconfig.IsAllSizeColumn = true;
            excelconfig.ColumnEntity = new List<ColumnEntity>();
            //表头
            List<GridColumnModel> columnData = columnJson.ToList<GridColumnModel>();
            //行数据
            DataTable rowData = rowJson.ToTable();
            //写入Excel表头
            string[] fieldInfo = exportField.Split(',');
            foreach (string item in fieldInfo)
            {
                var list = columnData.FindAll(t => t.name == item);
                foreach (GridColumnModel gridcolumnmodel in list)
                {
                    if (gridcolumnmodel.hidden.ToLower() == "false" && gridcolumnmodel.label != null)
                    {
                        string align = gridcolumnmodel.align;
                        excelconfig.ColumnEntity.Add(new ColumnEntity()
                        {
                            Column = gridcolumnmodel.name,
                            ExcelColumn = gridcolumnmodel.label,
                            //Width = gridcolumnmodel.width,
                            Alignment = gridcolumnmodel.align,
                        });
                    }
                }
            }
            ExcelHelper.ExcelDownload(rowData, excelconfig);
        }
        #endregion

        #region 上传文件
        private FileAnnexesBLL fileAnnexesBLL = new FileAnnexesBLL();
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="DataItemEncode"></param>
        /// <param name="DataItemName"></param>
        /// <param name="Filedata"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadifyFile(string DataItemEncode, string DataItemName, HttpPostedFileBase Filedata)
        {
            try
            {
                Thread.Sleep(500);////延迟500毫秒
                //没有文件上传，直接返回
                if (Filedata == null || string.IsNullOrEmpty(Filedata.FileName) || Filedata.ContentLength == 0)
                {
                    if (Request.Files.Count > 0)
                    {
                        Filedata = Request.Files[0];
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                //获取文件完整文件名(包含绝对路径)
                //文件存放路径格式：/Resource/ResourceFile/{userId}{data}/{guid}.{后缀名}
                string userId = OperatorProvider.Provider.Current().UserId;
                string fileGuid = Guid.NewGuid().ToString();
                long filesize = Filedata.ContentLength;
                string FileEextension = Path.GetExtension(Filedata.FileName);
                string uploadDate = DateTime.Now.ToString("yyyyMMdd");//
                string dataItemFilePath = new DataItemDetailBLL().GetDataItemList().Where(t => t.F_EnCode == DataItemEncode).First(t => t.F_ItemName == DataItemName).F_ItemValue;
                string virtualPath = string.Format("{0}/{1}/{2}/{3}{4}", dataItemFilePath, userId, uploadDate, fileGuid, FileEextension);
                //创建文件夹
                string path = Path.GetDirectoryName(virtualPath);
                Directory.CreateDirectory(path);
                FileAnnexesEntity fileAnnexesEntity = new FileAnnexesEntity();
                if (!System.IO.File.Exists(virtualPath))
                {
                    //保存文件
                    Filedata.SaveAs(virtualPath);
                    //文件信息写入数据库
                    fileAnnexesEntity.F_Id = fileGuid;
                    fileAnnexesEntity.F_FileName = Filedata.FileName;
                    fileAnnexesEntity.F_FilePath = virtualPath;
                    fileAnnexesEntity.F_FileSize = filesize.ToString();
                    fileAnnexesEntity.F_FileExtensions = FileEextension;
                    fileAnnexesEntity.F_FileType = FileEextension.Replace(".", "");
                    fileAnnexesBLL.SaveEntity(null, fileAnnexesEntity);
                }
                var data = new
                {
                    fileId = fileAnnexesEntity.F_Id
                };
                return Content(data.ToJson());
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public void DownFile(string fileId)
        {
            FileAnnexesEntity data = fileAnnexesBLL.GetEntity(fileId);
            string filename = Server.UrlDecode(data.F_FileName);//返回客户端文件名称
            string filepath = data.F_FilePath;
            if (FileDownHelper.FileExists(filepath))
            {
                FileDownHelper.DownLoadold(filepath, filename);
            }
        }
        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="fileIdList"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFiles(string fileIdList)
        {
            try
            {
                var data = fileAnnexesBLL.GetEntityList(fileIdList);
                return Content(data.ToJson());

            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RemoveFile(string fileId)
        {
            try
            {
                FileAnnexesEntity fileInfoEntity = fileAnnexesBLL.GetEntity(fileId);
                fileAnnexesBLL.DeleteEntity(fileId);
                //删除文件
                if (System.IO.File.Exists(fileInfoEntity.F_FilePath))
                {
                    System.IO.File.Delete(fileInfoEntity.F_FilePath);
                }
                var data = new
                {
                    code = "1",
                };
                return Content(data.ToJson());
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }
        #endregion

        #region 上传Ckeditor图片
        /// <summary>
        /// 上传Ckeditor图片
        /// </summary>
        /// <param name="upload"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadImage(string DataItemEncode, string DataItemName, HttpPostedFileBase Filedata)
        {
            try
            {
                Thread.Sleep(500);////延迟500毫秒
                //没有文件上传，直接返回
                if (Filedata == null || string.IsNullOrEmpty(Filedata.FileName) || Filedata.ContentLength == 0)
                {
                    if (Request.Files.Count > 0)
                    {
                        Filedata = Request.Files[0];
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                //获取文件完整文件名(包含绝对路径)
                //文件存放路径格式：/Resource/ResourceFile/{userId}{data}/{guid}.{后缀名}
                string userId = OperatorProvider.Provider.Current().UserId;
                string fileGuid = Guid.NewGuid().ToString();
                long filesize = Filedata.ContentLength;
                string FileEextension = Path.GetExtension(Filedata.FileName);
                string uploadDate = DateTime.Now.ToString("yyyyMMdd");//
                string dataItemFilePath = new DataItemDetailBLL().GetDataItemList().Where(t => t.F_EnCode == DataItemEncode).First(t => t.F_ItemName == DataItemName).F_ItemValue;
                string virtualPath = string.Format("{0}/{1}/{2}/{3}{4}", dataItemFilePath, userId, uploadDate, fileGuid, FileEextension);
                //创建文件夹
                string path = Path.GetDirectoryName(virtualPath);
                Directory.CreateDirectory(path);
                FileAnnexesEntity fileAnnexesEntity = new FileAnnexesEntity();
                if (!System.IO.File.Exists(virtualPath))
                {
                    //保存文件
                    Filedata.SaveAs(virtualPath);
                    //文件信息写入数据库
                    fileAnnexesEntity.F_Id = fileGuid;
                    fileAnnexesEntity.F_FileName = Filedata.FileName;
                    fileAnnexesEntity.F_FilePath = virtualPath;
                    fileAnnexesEntity.F_FileSize = filesize.ToString();
                    fileAnnexesEntity.F_FileExtensions = FileEextension;
                    fileAnnexesEntity.F_FileType = FileEextension.Replace(".", "");
                    fileAnnexesBLL.SaveEntity(null, fileAnnexesEntity);
                }
                string url = string.Format("http://{0}/Utility/DownFile?fileId={1}", Request.UrlReferrer.Authority, fileAnnexesEntity.F_Id);
                string CKEditorFuncNum = System.Web.HttpContext.Current.Request["CKEditorFuncNum"];
                //上传成功后，我们还需要通过以下的一个脚本把图片返回到第一个tab选项
                
                return Content("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            
        }
        #endregion

        #region 解决打印插件跨域问题
        /// <summary>
        /// 打印页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult printIndex()
        {
            return View();
        }
        #endregion

        #region 展示系统图标
        /// <summary>
        /// 图标选择展示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IconPreviewIndex()
        {
            return View();
        }
        #endregion

        #region 获取当前信息
        /// <summary>
        /// 图标选择展示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult getCurrentInfo()
        {
            var data = new
            {
                userId = OperatorProvider.Provider.Current().UserId,
                userName = OperatorProvider.Provider.Current().UserName,
                companyId = OperatorProvider.Provider.Current().CompanyId,
                departmentId = OperatorProvider.Provider.Current().DepartmentId,
                time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            return Content(data.ToJson());
        }
        #endregion

        [HttpGet]
        public ActionResult GetLearunData()
        {
            var data = new
            {
                qty = 100,
                total = 1000

            };

            return Content(data.ToJson());
        }
    }
}
