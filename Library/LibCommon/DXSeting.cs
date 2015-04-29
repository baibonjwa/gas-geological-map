// ******************************************************************
// 概  述：
// 作  者：
// 创建日期：
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars.Localization;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraPrinting.Localization;

namespace LibCommon
{
    public class DXSeting
    {
        /// <summary>
        /// 浮动工具条中文设置
        /// </summary>
        public static void floatToolsLoadSet()
        {
            BarLocalizer.Active = new BarLocalizer(); //DevExpress.LocalizationCHS.DevExpressXtraBarsLocalizationCHS();
            Localizer.Active = new EditResLocalizer(); //DevExpress.LocalizationCHS.DevExpressXtraEditorsLocalizationCHS();
            PreviewLocalizer.Active = new PreviewLocalizer();// DevExpress.LocalizationCHS.DevExpressXtraPrintingLocalizationCHS();
        }

        /// <summary>
        /// 窗体皮肤设置
        /// </summary>
        public static void formSkinsSet()
        {
            //DX皮肤
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.UserSkins.BonusSkins.Register();
            //UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
            UserLookAndFeel.Default.SetSkinStyle("Office 2007 Blue");
        }

        public static void SetFormSkin(string skinName)
        {
            //DX皮肤
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.UserSkins.BonusSkins.Register();
            UserLookAndFeel.Default.SetSkinStyle(skinName);
        }
    }

    static public class DXSkineNames
    {
        public const string DEV_EXPRESS_STYLE = "DevExpress Style";
        public const string OFFICE_2007_BLUE = "Office 2007 Blue";
        public const string CARAMEL = "Caramel";
        public const string MONEY_TWINS = "Money Twins";
        public const string LILIAN = "Lilian";
        public const string DEV_EXPRESS_DARK_STYLE = "DevExpress Dark Style";
        public const string I_MAGINARY = "iMaginary";
        public const string BLACK = "Black";
        public const string BLUE = "Blue";

        static public string[] GetDXSkinNames()
        {
            string[] ret = new string[]
            {
                DEV_EXPRESS_STYLE,
                OFFICE_2007_BLUE,
                CARAMEL,
                MONEY_TWINS,
                LILIAN,
                DEV_EXPRESS_DARK_STYLE,
                I_MAGINARY,
                BLACK,
                BLUE,
            };
            return ret;
        }
    }

}

