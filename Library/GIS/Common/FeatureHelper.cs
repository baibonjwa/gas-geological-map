using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using ESRI.ArcGIS.Geometry;

namespace GIS.Common
{
    public class FeatureHelper
    {
        public static void CopyFeature(IFeature pSrcFea, IFeature pDestFea)
        {
            CopyFeature(pSrcFea, pDestFea, true);
        }
        public static void CopyFeature(IFeature pSrcFea, IFeature pDestFea, bool pOverwrite)
        {
            try
            {
                IFeatureClass class2 = pSrcFea.Table as IFeatureClass;
                IFeatureClass class3 = pDestFea.Table as IFeatureClass;
                int num = pSrcFea.Fields.FieldCount;
                for (int i = 0; i < num; i++)
                {
                    IField field = class2.Fields.get_Field(i);
                    if ((((field.Type != esriFieldType.esriFieldTypeOID) &&
                        (field.Type != esriFieldType.esriFieldTypeGeometry))
                        && (field != class2.LengthField)) && (field != class2.AreaField))
                    {
                        string str = field.Name.ToUpper();
                        int num3 = class3.Fields.FindField(str);
                        if (num3 >= 0)
                        {
                            IField field2 = class3.Fields.get_Field(num3);
                            if ((((field2.Type != esriFieldType.esriFieldTypeOID) &&
                        (field2.Type != esriFieldType.esriFieldTypeGeometry))
                        && (field2 != class3.LengthField)) && (field2 != class3.AreaField))
                            {
                                object obj2 = pSrcFea.get_Value(i);
                                if (pOverwrite)
                                {
                                    if ((obj2 == null) || (obj2 is DBNull))
                                    {
                                        obj2 = null;
                                    }
                                    if (field2.CheckValue(obj2))
                                    {
                                        try
                                        {
                                            pDestFea.set_Value(num3, obj2);
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                else
                                {
                                    object obj3 = pDestFea.get_Value(num3);
                                    if ((obj3 == null) || (obj3 is DBNull))
                                    {
                                        if ((obj2 == null) || (obj2 is DBNull))
                                        {
                                            obj2 = null;
                                        }
                                        if (field2.CheckValue(obj2))
                                        {
                                            try
                                            {
                                                pDestFea.set_Value(num3, obj2);
                                            }
                                            catch
                                            {
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                pDestFea.Store();
            }
            catch (Exception)
            {
            }
        }
        public static void CopyRow(IRow pSrcFea, IRow pDestFea)
        {
            try
            {
                ITable table = pSrcFea.Table;
                ITable table2 = pDestFea.Table;
                IField field = null;
                IField field2 = null;
                IField field3 = null;
                IField field4 = null;
                if (table is IFeatureClass)
                {
                    field = (table as IFeatureClass).LengthField;
                    field2 = (table as IFeatureClass).AreaField;
                }
                if (table2 is IFeatureClass)
                {
                    field3 = (table2 as IFeatureClass).LengthField;
                    field4 = (table2 as IFeatureClass).AreaField;
                }
                int num = pSrcFea.Fields.FieldCount;
                for (int i = 0; i < num; i++)
                {
                    IField field5 = table.Fields.get_Field(i);
                    if ((((field5.Type != esriFieldType.esriFieldTypeOID)
                        && (field5.Type != esriFieldType.esriFieldTypeGeometry)) && (field5 != field)) && (field5 != field2))
                    {
                        string str = field5.Name.ToUpper();
                        object obj2 = pSrcFea.get_Value(i);
                        if ((obj2 != null) || (obj2 is DBNull))
                        {
                            int num3 = table2.Fields.FindField(str);
                            if (num3 >= 0)
                            {
                                IField field6 = table2.Fields.get_Field(num3);
                                if (((((field6.Type != esriFieldType.esriFieldTypeOID) &&
                                    (field6.Type != esriFieldType.esriFieldTypeGeometry)) && (field6 != field3)) && (field6 != field4)) && (((obj2 != null) && !(obj2 is DBNull)) && field6.CheckValue(obj2)))
                                {
                                    try
                                    {
                                        pDestFea.set_Value(num3, obj2);
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                }
                pDestFea.Store();
            }
            catch (Exception)
            {
            }
        }
        public static void CopyRow(IRow pSrcFea, IRow pDestFea, List<int> pSrcField, List<int> pDestField)
        {
            for (int i = 0; i < pSrcField.Count; i++)
            {
                int num2 = pSrcField[i];
                int num3 = pDestField[i];
                if ((num2 >= 0) && (num3 >= 0))
                {
                    try
                    {
                        object obj2 = pSrcFea.get_Value(num2);
                        IField field = pDestFea.Fields.get_Field(num3);
                        if (((obj2 != null) && !(obj2 is DBNull)) && field.CheckValue(obj2))
                        {
                            try
                            {
                                pDestFea.set_Value(num3, obj2);
                            }
                            catch
                            {
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        string message = exception.Message;
                    }
                }
            }
        }
        public static void CopyTable(ITable pSrcTable, ITable pDestTable)
        {
            Exception exception;
            try
            {
                if ((pSrcTable != null) && (pDestTable != null))
                {
                    List<int> pSrcField = null;
                    List<int> pDestField = null;
                    CreateFieldMap(pSrcTable, pDestTable, out pSrcField, out pDestField);
                    ICursor o = pSrcTable.Search(null, true);
                    int num = pSrcTable.RowCount(null);
                    if (num > 0)
                    {
                        int num2 = (num / 10) + 1;
                        IWorkspaceEdit edit = (pDestTable as IDataset).Workspace as IWorkspaceEdit;
                        edit.StartEditing(false);
                        edit.StartEditOperation();
                        IRow pSrcFea = o.NextRow();
                        int num3 = 1;
                        while (pSrcFea != null)
                        {
                            try
                            {
                                if ((num2 >= 0x3e8) && ((num3++ % num2) == 0))
                                {
                                    edit.StopEditOperation();
                                    edit.StopEditing(true);
                                    edit.StartEditing(false);
                                    edit.StartEditOperation();
                                }
                                IRow pDestFea = pDestTable.CreateRow();
                                CopyRow(pSrcFea, pDestFea, pSrcField, pDestField);
                                if ((pSrcFea is IFeature) && (pDestFea is IFeature))
                                {
                                    (pDestFea as IFeature).Shape = (pSrcFea as IFeature).ShapeCopy;
                                }
                                pDestFea.Store();
                            }
                            catch (Exception exception1)
                            {
                                exception = exception1;
                            }
                            pSrcFea = o.NextRow();
                            edit.StopEditOperation();
                            edit.StopEditing(true);
                        }
                        Marshal.ReleaseComObject(o);
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
            }
        }
        public static void CreateFieldMap(ITable pSrcTable, ITable pDestTable, out List<int> pSrcField, out List<int> pDestField)
        {
            Exception exception;
            pSrcField = new List<int>();
            pDestField = new List<int>();
            int num = pSrcTable.Fields.FieldCount;
            IField field = null;
            try
            {
                if (pSrcTable is IFeatureClass)
                {
                    field = (pSrcTable as IFeatureClass).LengthField;
                }
            }
            catch (Exception exception1)
            {
                exception = exception1;
            }
            IField field2 = null;
            try
            {
                if (pSrcTable is IFeatureClass)
                {
                    field2 = (pSrcTable as IFeatureClass).AreaField;
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
            }
            IField field3 = null;
            try
            {
                if (pDestTable is IFeatureClass)
                {
                    field3 = (pDestTable as IFeatureClass).LengthField;
                }
            }
            catch (Exception exception3)
            {
                exception = exception3;
            }
            IField field4 = null;
            try
            {
                if (pDestTable is IFeatureClass)
                {
                    field4 = (pDestTable as IFeatureClass).AreaField;
                }
            }
            catch (Exception exception4)
            {
                exception = exception4;
            }
            for (int i = 0; i < num; i++)
            {
                IField field5 = pSrcTable.Fields.get_Field(i);
                if ((((field5.Type != esriFieldType.esriFieldTypeOID) &&
                    (field5.Type != esriFieldType.esriFieldTypeGeometry)) && (field5 != field)) && (field5 != field2))
                {
                    string str = field5.Name.ToUpper();
                    int item = pDestTable.Fields.FindField(str);
                    if (item >= 0)
                    {
                        IField field6 = pDestTable.Fields.get_Field(item);
                        if ((((field6.Type != esriFieldType.esriFieldTypeOID) &&
                            (field6.Type != esriFieldType.esriFieldTypeGeometry)) && (field6 != field3)) && (field6 != field4))
                        {
                            pSrcField.Add(i);
                            pDestField.Add(item);
                        }
                    }
                }
            }
        }
        public static double GetFeatureDoubleValue(IFeature pFea, string pField)
        {
            return LSCommonHelper.ConvertHelper.ObjectToDouble(GetRowValue(pFea, pField));
        }
        public static int GetFeatureIntValue(IFeature pFea, string pField)
        {
            return LSCommonHelper.ConvertHelper.ObjectToInt(GetRowValue(pFea, pField));
        }
        public static string GetFeatureStringValue(IFeature pFea, string pField)
        {
            return GetRowValue(pFea, pField).ToString();
        }
        public static object GetFeatureValue(IFeature pFea, string pField)
        {
            return GetRowValue(pFea, pField);
        }
        public static object GetRowValue(IRow pRow, string pField)
        {
            if ((pRow != null) && (pField != null))
            {
                int num = pRow.Fields.FindField(pField);
                if (num >= 0)
                {
                    object obj2 = pRow.get_Value(num);
                    if ((obj2 == null) || (obj2 is DBNull))
                    {
                        obj2 = "";
                    }
                    return obj2;
                }
            }
            return "";
        }
        public static void SetFeatureValue(IFeature pFea, string pField, object pValue)
        {
            SetRowValue(pFea, pField, pValue);
        }
        public static void SetFeatureValue(IFeatureBuffer pFea, string pField, object pValue)
        {
            SetRowValue(pFea as IRow, pField, pValue);
        }
        public static void SetRowValue(IRow pRow, string pField, object pValue)
        {
            if ((pRow != null) && (pField != null))
            {
                int num = pRow.Fields.FindField(pField);
                if (num >= 0)
                {
                    if ((pValue == null) || (pValue is DBNull))
                    {
                        pValue = "";
                    }
                    IField field = pRow.Fields.get_Field(num);
                    if (field.CheckValue(pValue))
                    {
                        if ((field.Type == esriFieldType.esriFieldTypeInteger) && ((pValue == null) || (pValue.ToString() == "")))
                        {
                            pRow.set_Value(num, 0);
                        }
                        else if ((field.Type == esriFieldType.esriFieldTypeDouble) && ((pValue == null) || (pValue.ToString() == "")))
                        {
                            pRow.set_Value(num, 0.0);
                        }
                        else
                        {
                            pRow.set_Value(num, pValue);
                        }
                    }
                    else
                    {
                        try
                        {
                            pRow.set_Value(num, pValue);
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

    }
}
