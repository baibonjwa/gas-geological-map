// ******************************************************************
// 概  述：excel操作
// 作  者：杨小颖
// 创建日期：2014/1/8
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace LibCommon
{
    public class LibExcelHelper
    {
        /// <summary>
        /// 导入excel中指定sheet的内容
        /// </summary>
        /// <returns>sheet内容</returns>
        public static DataSet importExcelSheetToDataSet(string excelFilePath, string sheetName)
        {
            string strConn;
            strConn = "Provider=Microsoft.Ace.OleDb.12.0;Data Source=" + excelFilePath + ";;Extended Properties='Excel 12.0 Xml; HDR=YES; IMEX=1'";
            OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT *FROM [" + sheetName + "$]", strConn);
            DataSet myDataSet = new DataSet();
            try
            {
                myCommand.Fill(myDataSet);
            }
            catch (Exception ex)
            {
                Alert.alert(ex.Message);
                return null;
            }
            return myDataSet;
        }

    }
}
