﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaRun.CodeGenerator.Comm
{
    /// <summary>
    /// 公共帮助类
    /// </summary>
    public class CommHelper
    {
        /// <summary>
        /// C#实体数据类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string FindModelsType(string name)
        {
            name = name.ToLower();
            if (name == "int" || name == "number" || name == "integer" || name == "smallint")
            {
                return "int?";
            }
            else if (name == "tinyint")
            {
                return "byte?";
            }
            else if (name == "numeric" || name == "real" || name == "float")
            {
                return "Single?";
            }
            else if (name == "float")
            {
                return "float?";
            }
            else if (name == "decimal" || name == "number(8,2)")
            {
                return "decimal?";
            }
            else if (name == "char" || name == "varchar" || name == "nvarchar2" || name == "text" || name == "nchar" || name == "nvarchar" || name == "ntext")
            {
                return "string";
            }
            else if (name == "bit")
            {
                return "bool?";
            }
            else if (name == "datetime" || name == "date" || name == "smalldatetime")
            {
                return "DateTime?";
            }
            else if (name == "money" || name == "smallmoney")
            {
                return "double?";
            }
            else
            {
                return "string";
            }
        }
        /// <summary>
        ///  C#实体数据类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string FindModelsTypeEx(string name)
        {
            name = name.ToLower();
            if (name == "int" || name == "number" || name == "integer" || name == "smallint")
            {
                return "数字";
            }
            else if (name == "tinyint")
            {
                return "数字";
            }
            else if (name == "numeric" || name == "real" || name == "float")
            {
                return "数字";
            }
            else if (name == "float")
            {
                return "数字";
            }
            else if (name == "decimal" || name == "number(8,2)")
            {
                return "数字";
            }
            else if (name == "char" || name == "varchar" || name == "nvarchar2" || name == "text" || name == "nchar" || name == "nvarchar" || name == "ntext")
            {
                return "字符串";
            }
            else if (name == "bit")
            {
                return "bool";
            }
            else if (name == "datetime" || name == "date" || name == "smalldatetime")
            {
                return "日期";
            }
            else if (name == "money" || name == "smallmoney")
            {
                return "数字";
            }
            else
            {
                return "字符串";
            }
        }
    }
}