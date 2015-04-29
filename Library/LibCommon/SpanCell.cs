using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCommon
{
    /// <summary>
    /// 合并单元格信息
    /// </summary>
    public class SpanCell
    {
        private FarPoint.Win.Spread.FpSpread fp;
        //行索引
        private int _rowIdx;
        public int RowIdx
        {
            get { return _rowIdx; }
            set { _rowIdx = value; }
        }

        //列索引
        private int _colIdx;
        public int ColIdx
        {
            get { return _colIdx; }
            set { _colIdx = value; }
        }

        //合并行数
        private int _spanRowCnt;
        public int SpanRowCnt
        {
            get { return _spanRowCnt; }
            set { _spanRowCnt = value; }
        }

        //合并列数
        private int _spanColCnt;
        public int SpanColCnt
        {
            get { return _spanColCnt; }
            set { _spanColCnt = value; }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SpanCell(FarPoint.Win.Spread.FpSpread fp)
        {
            this.fp = fp;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SpanCell(int rowIndex, int colIndex, int rowCount, int colCount)
        {
            _rowIdx = rowIndex;
            _colIdx = colIndex;
            _spanRowCnt = rowCount;
            _spanColCnt = colCount;
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="fp"></param>
        /// <param name="info"></param>
        public void applyCellsSpan()
        {
            fp.ActiveSheet.AddSpanCell(RowIdx, ColIdx, SpanRowCnt, SpanColCnt);
            fp.ActiveSheet.Cells[RowIdx, ColIdx].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            fp.ActiveSheet.Cells[RowIdx, ColIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }

        public void setCellType(FarPoint.Win.Spread.CellType.ICellType cellType)
        {
            this.fp.ActiveSheet.Cells[RowIdx, ColIdx].CellType = cellType;
        }

        public void setCellLocked(bool locked)
        {
            this.fp.ActiveSheet.Cells[RowIdx, ColIdx].Locked = locked;
        }

        public void setText(string txt)
        {
            this.fp.ActiveSheet.Cells[RowIdx, ColIdx].Text = txt;
        }
    }
}
