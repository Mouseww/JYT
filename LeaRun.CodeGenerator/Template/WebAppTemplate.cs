using LeaRun.Application.Entity.AppManage;
using LeaRun.CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaRun.Util;

namespace LeaRun.CodeGenerator.Template
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：LR215
    /// 日 期：2016.09.06 14:47
    /// 描 述：代码生成模板（WebApp）
    /// </summary>
    public class WebAppTemplate
    {
        /// <summary>
        /// 产生html页面
        /// </summary>
        /// <param name="lists"></param>
        /// <returns></returns>
        public string AppBuilder(List<AppTemplatesEntity> lists, AppProjectEntity projectEntity, string outputDirectory)
        {
            string path = "";
            Dictionary<string, AppTemplatesEntity> pageEntitys = new Dictionary<string, AppTemplatesEntity>();
            Dictionary<string, List<AppTemplatesEntity>> componentEntitys = new Dictionary<string, List<AppTemplatesEntity>>();
            List<AppTemplatesEntity> tabsEntitys = new List<AppTemplatesEntity>();
            AppTemplatesEntity tabEntity = null;
            try
            {
                //将数据做分类处理
                foreach (var item in lists)
                {
                    if (item.F_Value == "lrTabs")
                    {
                        tabEntity = item;
                    }
                    else if (item.F_Value == "lrTab")
                    {
                        tabsEntitys.Add(item);
                    }
                    if (item.F_Type == "Page" && item.F_Value != "lrTabs")
                    {
                        pageEntitys.Add(item.F_Id, item);
                    }
                    else if (item.F_Type == "Component")
                    {
                        if (componentEntitys.ContainsKey(item.F_Parent))
                        {
                            componentEntitys[item.F_Parent].Add(item);
                        }
                        else
                        {
                            List<AppTemplatesEntity> componentlists = new List<AppTemplatesEntity>();
                            componentlists.Add(item);
                            componentEntitys.Add(item.F_Parent, componentlists);
                        }
                    }
                }
                //JS文件
                string jsPath = outputDirectory + "\\js";

                #region 创建appJs文件
                string appJsPath = jsPath + "\\learun-app.js";
                if (!System.IO.File.Exists(appJsPath))
                {
                    DirFileHelper.CreateFileContent(appJsPath, JSApp());
                }
                #endregion

                #region 创建路由文件
                string routerJsPath = jsPath + "\\learun-uirouter.js";
                if (!System.IO.File.Exists(routerJsPath))
                {
                    DirFileHelper.CreateFileContent(routerJsPath, JSRouter(pageEntitys, tabsEntitys, projectEntity));
                }
                #endregion

                #region 创建controllers文件
                string controllersJsPath = jsPath + "\\learun-controllers.js";
                if (!System.IO.File.Exists(controllersJsPath))
                {
                    DirFileHelper.CreateFileContent(controllersJsPath, JSControllers(pageEntitys));
                }
                #endregion

                //html文件
                string htmlPath = outputDirectory + "\\templates";
                #region 创建页面
                foreach (var item in pageEntitys){
                    string htmlPagePath = htmlPath + "\\page" + item.Value.F_Id.Replace("-","") + ".html";
                    if (!System.IO.File.Exists(htmlPagePath))
                    {
                        DirFileHelper.CreateFileContent(htmlPagePath, PageBuilder(item.Value, componentEntitys[item.Key]));
                    }
                }
                #endregion

                #region 创建tabs页
                string htmlTabsPath = htmlPath + "\\tabs.html";
                if (!System.IO.File.Exists(htmlTabsPath))
                {
                    DirFileHelper.CreateFileContent(htmlTabsPath, TabBuilder(tabEntity, tabsEntitys));
                }
                #endregion

                ZipHelper.CreateZip(outputDirectory, outputDirectory+".zip");

                return outputDirectory + ".zip";
            }
            catch {
                throw;
            }
        }

        #region 创建appJs文件
        public string JSApp() {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("var app = angular.module('starter', ['ionic', 'starter.uirouter','starter.controllers'])\r\n");
                sb.Append(".run(['$ionicPlatform', '$rootScope', '$state',\r\n");
                sb.Append("function ($ionicPlatform, $rootScope, $state) {\r\n");
                sb.Append("$ionicPlatform.ready(function() {\r\n");
                sb.Append("if (window.cordova && window.cordova.plugins && window.cordova.plugins.Keyboard){\r\n");
                sb.Append("cordova.plugins.Keyboard.hideKeyboardAccessoryBar(false);\r\n");
                sb.Append("cordova.plugins.Keyboard.disableScroll(true);\r\n");
                sb.Append("}\r\n");
                sb.Append("});\r\n");
                sb.Append("}])\r\n");
                sb.Append(".config(['$ionicConfigProvider',function ($ionicConfigProvider) {\r\n");
                sb.Append("$ionicConfigProvider.backButton.previousTitleText(false);\r\n");
                sb.Append("$ionicConfigProvider.backButton.text('').icon('ion-ios-arrow-left');\r\n");
                sb.Append("$ionicConfigProvider.scrolling.jsScrolling(true);\r\n");
                sb.Append("$ionicConfigProvider.platform.ios.tabs.style('standard');\r\n");
                sb.Append("$ionicConfigProvider.platform.ios.tabs.position('bottom');\r\n");
                sb.Append("$ionicConfigProvider.platform.android.tabs.style('standard');\r\n");
                sb.Append("$ionicConfigProvider.platform.android.tabs.position('bottom');\r\n");
                sb.Append("$ionicConfigProvider.platform.ios.navBar.alignTitle('center');\r\n");
                sb.Append("$ionicConfigProvider.platform.android.navBar.alignTitle('center');\r\n");
                sb.Append("$ionicConfigProvider.platform.ios.views.transition('ios');\r\n");
                sb.Append("$ionicConfigProvider.platform.android.views.transition('android');\r\n");
                sb.Append("$ionicConfigProvider.tabs.position('bottom');\r\n");
                sb.Append("}]);\r\n");
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 创建路由文件
        public string JSRouter(Dictionary<string, AppTemplatesEntity> lists, List<AppTemplatesEntity> tablists, AppProjectEntity projectEntity)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("angular.module('starter.uirouter', [])\r\n");
                sb.Append(".config(function ($stateProvider, $urlRouterProvider) {\r\n");
                sb.Append("$stateProvider\r\n");

                if (projectEntity.F_IsTabed == 1)
                {
                    sb.Append(".state('tab', {\r\n");
                    sb.Append("url: '/tab',\r\n");
                    sb.Append("abstract: true,\r\n");
                    sb.Append("templateUrl: 'templates/tabs.html',\r\n");
                    sb.Append("controller: 'lrTabsCtrl'\r\n");
                    sb.Append("})");

                    foreach (var item in tablists)
                    {
                        AppPageAttrModel attr = item.F_Content.ToObject<AppPageAttrModel>();
                        sb.Append(".state('tab" + item.F_Id.Replace("-", "") + "', {\r\n");
                        sb.Append("url: '/" + attr.routing + "',\r\n");
                        sb.Append("views: {\r\n");
                        sb.Append("'tab-home': {\r\n");
                        sb.Append("templateUrl: 'templates/" + attr.routing + ".html'\r\n");
                        sb.Append("}\r\n");
                        sb.Append("}\r\n");
                        sb.Append("})\r\n");
                    }
                }
                foreach (var item in lists)
                {
                    AppPageAttrModel attr = item.Value.F_Content.ToObject<AppPageAttrModel>();
                    if(attr.isFirst == "true" )
                    {
                        string pageName = "page" + item.Value.F_Id.Replace("-", "");
                        sb.Append(".state('" + pageName + "', {\r\n");
                        sb.Append("url: '/" + pageName + "',\r\n");
                        sb.Append("templateUrl: 'templates/" + pageName + ".html',\r\n");
                        sb.Append("controller:'" + pageName + "Ctrl'\r\n");
                        sb.Append("});\r\n");
                        break;    
                    }
                }
                sb.Append("$urlRouterProvider.otherwise('/')\r\n");
                return sb.ToString();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 创建controllers文件
        public string JSControllers(Dictionary<string, AppTemplatesEntity> lists)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("angular.module('starter.controllers', [])\r\n");
                foreach (var item in lists)
                {
                    AppPageAttrModel attr = item.Value.F_Content.ToObject<AppPageAttrModel>();
                    string pageName = "page" + item.Value.F_Id.Replace("-", "");
                    sb.Append(".controller(" + pageName + "'Ctrl',['$scope',\r\n");
                    sb.Append("function ($scope,) {};\r\n");
                    sb.Append("}])\r\n");
                    break;
                }

                sb.Append(";\r\n");
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 页面
        public string PageBuilder(AppTemplatesEntity page, List<AppTemplatesEntity> component)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                AppPageAttrModel attr = page.F_Content.ToObject<AppPageAttrModel>();
                string pageName = "page" + page.F_Id.Replace("-", "");
                if (attr.isTabed == "true")
                {
                    sb.Append("<ion-view hide-nav-bar=\"" + attr.isHeadHide + "\" view-title=\"" + page.F_Name + "\">\r\n");
                    sb.Append("<ion-content padding=\"" + attr.isPadding + "\" style=\"background-color:" + attr.bgColor + ";\">\r\n");
                    sb.Append(ComponentBuilder(component));
                    sb.Append("</ion-content>\r\n");
                    sb.Append("</ion-view>\r\n");
                }
                else
                {
                    sb.Append("<ion-modal-view ng-controller=\"" + pageName + "Ctrl\">\r\n");
                    sb.Append("<ion-header-bar class=\"bar-stable nav-bar-block nav-title-slide-ios7\">\r\n");
                    sb.Append("<button class=\"button button-clear\" ng-click=\"closePageModel()\"><i class=\"icon ion-ios-arrow-left\"></i></button>\r\n");
                    sb.Append("<h1 class=\"title\">" + page.F_Name + "</h1>\r\n");
                    sb.Append("/ion-header-bar>\r\n");
                    sb.Append("<ion-content>\r\n");
                    sb.Append(ComponentBuilder(component));
                    sb.Append("</ion-content>\r\n");
                    sb.Append("</ion-modal-view>\r\n");
                }


                return sb.ToString();
            }
            catch{
                throw;
            }
        }
        #endregion

        #region 页面组件
        public string ComponentBuilder(List<AppTemplatesEntity> component)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                foreach (var item in component)
                {
                    switch (item.F_Value)
                    {
                        case "lrHeader":
                            AppHeadAttrModel attra = item.F_Content.ToObject<AppHeadAttrModel>();
                            sb.Append("<" + attra.size + " style=\"color:" + attra.color + ";");
                            sb.Append("font-weight:" + attra.weight.size + ";");
                            if (attra.weight.isItalic == "true")
                            {
                                sb.Append("font-style:italic;");
                            }
                            sb.Append("text-align:" + attra.align + ";");
                            sb.Append("\">" + attra.text + "</" + attra.size + ">");
                            break;
                        case "lrParagraph":
                            AppParagraphAttrModel attrb = item.F_Content.ToObject<AppParagraphAttrModel>();
                            sb.Append("<p style=\"color:" + attrb.color + ";");
                            sb.Append("font-size:" + attrb.size + ";");
                            sb.Append("text-align:" + attrb.align + ";");
                            sb.Append("\">" + attrb.content + "</p>");
                            break;
                        case "lrInput":
                            AppInputAttrModel attrc = item.F_Content.ToObject<AppInputAttrModel>();
                            sb.Append("<label class=\"item item-input\">");
                            sb.Append("<input type=\"" + attrc.inputType + "\" placeholder=\"" + attrc.placeholder + "\">");
                            sb.Append("</label>");
                            break;
                        case "lrBtn":
                            AppBtnAttrModel attrd = item.F_Content.ToObject<AppBtnAttrModel>();
                            sb.Append("<button style=\"color:" + attrd.color + ";");
                            sb.Append("font-size:" + attrd.size + ";");
                            sb.Append("text-align:" + attrd.align + ";");
                            sb.Append("font-weight:" + attrd.weight.size + ";");
                            if (attrd.weight.isItalic == "true")
                            {
                                sb.Append("font-style:italic;");
                            }
                            sb.Append("\" class=\"button button-" + attrd.btnType + "button-" + attrd.btnSize + "button-" + attrd.btnTheme + "\"");
                            sb.Append(">" + attrd.text);
                            sb.Append("</button>");
                            break;
                        case "lrList3":
                            ApplrList3AttrModel attre = item.F_Content.ToObject<ApplrList3AttrModel>();
                            sb.Append("<div class=\"list lr-list-type3\"><ion-item class=\"item item-icon-left\">");
                            sb.Append("<i class=\"icon " + attre.icon + " " + attre.color + "\" ></i>");
                            sb.Append(" <span>" + attre.name + "</span></ion-item></div>");
                            break;
                        case "lrList4":
                            ApplrList4AttrModel attrf = item.F_Content.ToObject<ApplrList4AttrModel>();
                            sb.Append("<ion-item class=\"item-icon-right lr-iconitem item \">");
                            sb.Append("<i class=\"icon " + attrf.icon + " " + attrf.color + "\" ></i>");
                            sb.Append(" <span>" + attrf.name + "</span><i class=\"icon ion-chevron-right icon-accessory \"></i></ion-item>");
                            break;
                    }
                }
                return sb.ToString();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        #endregion

        #region 创建tab页面
        public string TabBuilder(AppTemplatesEntity tab, List<AppTemplatesEntity> tabs)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                AppTabsAttrModel attrTabs = tab.F_Content.ToObject<AppTabsAttrModel>();
                sb.Append("<ion-tabs class=\"tabs-icon-" + attrTabs.iconType + " tabs-background-" + attrTabs.bgColor + " tabs-color-" + attrTabs.iconColor + "\" >\r\n");
                foreach (var item in tabs)
                {
                    AppTabAttrModel attr = item.F_Content.ToObject<AppTabAttrModel>();
                    sb.Append("<ion-tab title=\"" + item.F_Name + "\" icon-on=\"" + attr.iconOn + "\" icon-off=\"" + attr.iconOff + "\" href=\"#/tab/" + attr.innerTabPage + "\">\r\n");
                    sb.Append("<ion-nav-view name=\"tab-" + item.F_Id.Replace("-","") + "\"></ion-nav-view>\r\n");
                    sb.Append("</ion-tab>\r\n");
                }
                sb.Append("</ion-tabs>\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion
    }
}
