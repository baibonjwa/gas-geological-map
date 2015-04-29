// ******************************************************************
// 概  述：PropertyGrid封装
// 作  者：杨小颖  
// 创建日期：2013/12/13
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace LibXPorperty
{
    public class XProp
    {
        private string theId = ""; //属性Id,可以忽略
        private string theCategory = ""; //属性所属类别
        private string theName = "";     //属性名称
        private bool theReadOnly = false;  //属性的只读性，true为只读
        private string theDescription = ""; //属性的描述内容
        private object theValue = null;    //值
        private System.Type theType = null; //类型
        private bool theVisible = true;  //显示或隐藏，true为显示
        TypeConverter theConverter = null;  //类型转换
        public string Id
        {
            get { return theId; }
            set { theId = value; }
        }
        public string Category
        {
            get { return theCategory; }
            set { theCategory = value; }
        }
        public bool ReadOnly
        {
            get { return theReadOnly; }
            set { theReadOnly = value; }
        }
        public string Name
        {
            get { return this.theName; }
            set { this.theName = value; }
        }
        public object Value
        {
            get { return this.theValue; }
            set { this.theValue = value; }
        }
        public string Description
        {
            get { return theDescription; }
            set { theDescription = value; }
        }
        public System.Type ProType
        {
            get { return theType; }
            set { theType = value; }
        }
        public bool Visible
        {
            get { return theVisible; }
            set { theVisible = value; }
        }
        public virtual TypeConverter Converter
        {
            get { return theConverter; }
            set { theConverter = value; }
        }
    }


    public class XProps : List<XProp>, ICustomTypeDescriptor
    {
        #region ICustomTypeDescriptor 成员

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(System.Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(System.Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public PropertyDescriptorCollection GetProperties(System.Attribute[] attributes)
        {
            ArrayList props = new ArrayList();
            for (int i = 0; i < this.Count; i++)
            {  //判断属性是否显示
                if (this[i].Visible == true)
                {
                    XPropDescriptor psd = new XPropDescriptor(this[i], attributes);
                    props.Add(psd);
                }
            }
            PropertyDescriptor[] propArray = (PropertyDescriptor[])props.ToArray(typeof(PropertyDescriptor));
            return new PropertyDescriptorCollection(propArray);
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return TypeDescriptor.GetProperties(this, true);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.Count; i++)
            {
                sb.Append("[" + i + "] " + this[i].ToString() + System.Environment.NewLine);
            }
            return sb.ToString();
        }
    }

    public class XPropDescriptor : PropertyDescriptor
    {
        XProp theProp;
        public XPropDescriptor(XProp prop, Attribute[] attrs)
            : base(prop.Name, attrs)
        {
            theProp = prop;
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }
        public override string Category
        {
            get { return theProp.Category; }
        }

        public override string Description
        {
            get { return theProp.Description; }
        }

        public override TypeConverter Converter
        {
            get { return theProp.Converter; }
        }

        public override System.Type ComponentType
        {
            get { return this.GetType(); }
        }

        public override object GetValue(object component)
        {
            return theProp.Value;
        }

        public override bool IsReadOnly
        {
            get { return theProp.ReadOnly; }
        }

        public override System.Type PropertyType
        {
            get { return theProp.ProType; }
        }

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
            theProp.Value = value;
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
    }


    //重写下拉菜单中的项，使之与属性页的项关联
    public abstract class ComboBoxItemTypeConvert : TypeConverter
    {
        public Hashtable myhash = null;
        public ComboBoxItemTypeConvert()
        {
            myhash = new Hashtable();
            GetConvertHash();
        }
        public abstract void GetConvertHash();

        //是否支持选择列表的编辑
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        //重写combobox的选择列表
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            int[] ids = new int[myhash.Values.Count];
            int i = 0;
            foreach (DictionaryEntry myDE in myhash)
            {
                ids[i++] = (int)(myDE.Key);
            }
            return new StandardValuesCollection(ids);
        }
        //判断转换器是否可以工作
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }
        //重写转换器，将选项列表（即下拉菜单）中的值转换到该类型的值
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object obj)
        {
            if (obj is string)
            {
                foreach (DictionaryEntry myDE in myhash)
                {
                    if (myDE.Value.Equals((obj.ToString())))
                        return myDE.Key;
                }
            }
            return base.ConvertFrom(context, culture, obj);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }
        //重写转换器将该类型的值转换到选择列表中
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object obj, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                foreach (DictionaryEntry myDE in myhash)
                {
                    if (myDE.Key.Equals(obj))
                        return myDE.Value.ToString();
                }
                return "";
            }
            return base.ConvertTo(context, culture, obj, destinationType);
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }
    }
    //重写下拉菜单，在这里实现定义下拉菜单内的项
    public class MyComboItemConvert : ComboBoxItemTypeConvert
    {
        private Hashtable hash;
        public override void GetConvertHash()
        {
            try
            {
                myhash = hash;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }
        public MyComboItemConvert(string str)
        {
            hash = new Hashtable();
            string[] stest = str.Split(',');
            for (int i = 0; i < stest.Length; i++)
            {
                hash.Add(i, stest[i]);
            }
            GetConvertHash();
            value = 0;
        }
        public int value { get; set; }
        public MyComboItemConvert(string str, int s)
        {
            hash = new Hashtable();
            string[] stest = str.Split(',');
            for (int i = 0; i < stest.Length; i++)
            {
                hash.Add(i, stest[i]);
            }
            GetConvertHash();
            value = s;
        }
    }
}
