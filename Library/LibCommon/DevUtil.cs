using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Utils.CodedUISupport;
using DevExpress.XtraBars.Ribbon.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;

namespace LibCommon
{
    public class DevUtil
    {
        public static void DevPrint(GridControl gc,String printHeader)
        {
            var ps = new PrintingSystem();

            var link = new PrintableComponentLink(ps);
            ps.Links.Add(link);

            link.Component = gc;//这里可以是可打印的部件
            var phf = link.PageHeaderFooter as PageHeaderFooter;
            if (phf != null)
            {
                phf.Header.Content.Clear();
                phf.Header.Content.AddRange(new[] { "", printHeader, "" });
                phf.Header.Font = new System.Drawing.Font("宋体", 14, System.Drawing.FontStyle.Bold);
                phf.Header.LineAlignment = BrickAlignment.Center;
            }
            link.CreateDocument(); //建立文档

            ps.PreviewFormEx.Show();
        }
    }
}
