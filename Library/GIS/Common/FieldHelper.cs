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
    public class FieldHelper
    {
        // Methods
        public static IField CreateDoubleField(string pFieldName, int pFieldLen, int pFieldScale)
        {
            FieldClass class2 = new FieldClass();
            IFieldEdit edit = class2;
            edit.Name_2 = pFieldName;
            edit.Type_2 = esriFieldType.esriFieldTypeDouble;
            edit.Precision_2 = pFieldLen;
            edit.Scale_2 = pFieldScale;
            return class2;
        }

        public static IField CreateGeometryField(esriGeometryType pGT, ISpatialReference pSR)
        {
            return CreateGeometryField(pGT, pSR, false);
        }

        public static IField CreateGeometryField(esriGeometryType pGT, ISpatialReference pSR, bool pHasZ)
        {
            FieldClass class2 = new FieldClass();
            IFieldEdit edit = class2;
            edit.Name_2 = "SHAPE";
            edit.AliasName_2 = "图形对象";
            edit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            IGeometryDef pGeoDef = new GeometryDefClass();
            IGeometryDefEdit class3 = (IGeometryDefEdit)pGeoDef;

            class3.GeometryType_2 = pGT;
            class3.HasM_2 = false;
            class3.HasZ_2 = pHasZ;
            class3.GridCount_2 = 1;
            class3.set_GridSize(0, 1000.0);
            class3.SpatialReference_2 = pSR;
            edit.GeometryDef_2 = class3;
            return class2;
        }

        public static IField CreateIntField(string pFieldName, int pFieldLen)
        {
            FieldClass class2 = new FieldClass();
            IFieldEdit edit = class2;
            edit.Name_2 = pFieldName;
            edit.Precision_2 = pFieldLen;
            edit.Type_2 = esriFieldType.esriFieldTypeInteger;
            return class2;
        }

        public static IField CreateOIDField()
        {
            FieldClass class2 = new FieldClass();
            IFieldEdit edit = class2;
            edit.Name_2 = "OBJECTID";
            edit.AliasName_2 = "表内唯一编号";
            edit.Type_2 = esriFieldType.esriFieldTypeOID;
            return class2;
        }

        public static IField CreateRasterField(string pName, string pAliasName, ISpatialReference pSR)
        {
            FieldClass class2 = new FieldClass();
            IFieldEdit edit = class2;
            edit.Name_2 = pName;
            edit.AliasName_2 = pAliasName;
            edit.Type_2 = esriFieldType.esriFieldTypeRaster;
            IRasterDef def = new RasterDefClass();
            def.IsRasterDataset = false;
            def.SpatialReference = pSR;
            (edit as IFieldEdit2).RasterDef = def;
            return class2;
        }

        public static IField CreateTextField(string pName, string pAliasName, int aWidth)
        {
            FieldClass class2 = new FieldClass();
            IFieldEdit edit = class2;
            edit.Name_2 = pName.ToUpper();
            edit.AliasName_2 = pAliasName;
            edit.Type_2 = esriFieldType.esriFieldTypeString;
            edit.Length_2 = aWidth;
            return class2;
        }

        public static int QueryFieldLength(IField paramFld)
        {
            if ((paramFld.Type == esriFieldType.esriFieldTypeSingle) || (paramFld.Type == esriFieldType.esriFieldTypeDouble))
            {
                return paramFld.Precision;
            }
            if (paramFld.Type == esriFieldType.esriFieldTypeInteger)
            {
                return paramFld.Length;
            }
            return paramFld.Length;
        }

        public static int QueryFieldPrecision(IField paramFld)
        {
            if ((paramFld.Type == esriFieldType.esriFieldTypeSingle) || (paramFld.Type == esriFieldType.esriFieldTypeDouble))
            {
                return paramFld.Scale;
            }
            return 0;
        }

        public static string QueryFieldTypeName(esriFieldType paramFT)
        {
            string str = "";
            if (esriFieldType.esriFieldTypeBlob == paramFT)
            {
                return "二进制";
            }
            if (esriFieldType.esriFieldTypeDate == paramFT)
            {
                return "日期";
            }
            if (esriFieldType.esriFieldTypeDouble == paramFT)
            {
                return "浮点数";
            }
            if (esriFieldType.esriFieldTypeGeometry == paramFT)
            {
                return "图形对象";
            }
            if (esriFieldType.esriFieldTypeGlobalID == paramFT)
            {
                return "全局编号";
            }
            if (esriFieldType.esriFieldTypeGUID == paramFT)
            {
                return "永久唯一编号";
            }
            if (esriFieldType.esriFieldTypeInteger == paramFT)
            {
                return "整型";
            }
            if (esriFieldType.esriFieldTypeOID == paramFT)
            {
                return "表内编号";
            }
            if (esriFieldType.esriFieldTypeRaster == paramFT)
            {
                return "影像图";
            }
            if (esriFieldType.esriFieldTypeSingle == paramFT)
            {
                return "浮点数";
            }
            if (esriFieldType.esriFieldTypeSmallInteger == paramFT)
            {
                return "短整型";
            }
            if (esriFieldType.esriFieldTypeString == paramFT)
            {
                str = "字符串";
            }
            return str;
        }

        public static IField QueryField(IFields pFields, string sName)
        {
            IField pField = null;
            int n = pFields.FindField(sName);
            if (n > -1)
            {
                pField = pFields.get_Field(n);
                return pField;
            }
            return pField;
        }

        public static IField AlterGeometryFieldSR(IField pField, ISpatialReference sr)
        {
            IFieldEdit pEdit = pField as IFieldEdit;
            IGeometryDef pGeoDef = pField.GeometryDef;
            IGeometryDefEdit pDEdit = pGeoDef as IGeometryDefEdit;
            pDEdit.SpatialReference_2 = sr;
            pEdit.GeometryDef_2 = pGeoDef;
            return pField;
        }
        public static IField AlterRasterFieldSR(IField pField, ISpatialReference sr)
        {
            IFieldEdit2 pEdit = pField as IFieldEdit2;
            IRasterDef pRDef = pEdit.RasterDef;
            pRDef.SpatialReference = sr;
            return pField;
        }
    }
}
