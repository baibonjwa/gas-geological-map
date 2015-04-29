// ******************************************************************
// 概  述：自动调整Farpoint行高和列宽
// 注意：该函数对多行文字及合并单元格的情况尚未详细测试！
// 作  者：杨小颖
// 创建日期：2014/01/26
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;

namespace LibCommon
{
    public static class LibAutoResizeFarpoint
    {
        /// <summary>
        /// 自动调整Farpoint行高。
        /// </summary>
        /// <param name="fp"></param>
        /// <param name="startColIdx"></param>起始列，farpoint中某些列（如：选择列)不需要自动调整高度，通过该参数进行控制;如果全部自动调整，则将该参数设置为0</param>
        public static void AutoFitHeight(FarPoint.Win.Spread.FpSpread fp,int startColIdx)
        {
            int rowCnt = fp.ActiveSheet.Rows.Count;
            for (int i = startColIdx; i < rowCnt; i++)
            {
                fp.ActiveSheet.Rows[i].Height = fp.ActiveSheet.Rows[i].GetPreferredHeight();
            }
        }

        /// <summary>
        /// 自动调整Farpoint列的宽度。
        /// 注：该函数需要在设置完单元格的内容之后调。
        /// </summary>
        /// <param name="fp"></param>
        /// <param name="startRowIdx">起始行，farpoint中某些标题行不需要自动调整宽度，通过该参数进行控制;如果全部自动调整，则将该参数设置为0</param>
        public static void AutoFitWidth(FarPoint.Win.Spread.FpSpread fp, int startRowIdx)
        {
            int colCnt = fp.ActiveSheet.Columns.Count;
            for (int i = startRowIdx; i < colCnt; i++)
            {
                fp.ActiveSheet.Columns[i].Width = fp.ActiveSheet.Columns[i].GetPreferredWidth();
            }
        }

        /// <summary>
        /// 自动调整Farpoint行高和列宽
        /// </summary>
        /// <param name="fp"></param>
        public static void AutoFitWidthAndHeight(FarPoint.Win.Spread.FpSpread fp, int startRowIdx, int startColIdx)
        {
            AutoFitWidth(fp, startRowIdx);
            AutoFitHeight(fp, startColIdx);
        }
    }
}
