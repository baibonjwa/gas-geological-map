using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;

namespace GIS.MapPrint
{
    public partial class TextSetUp : Form
    {
        private FontDialog m_fontDialog = null;
        private IPoint m_point = null;
        private ITextElement m_textElement = null;
        private bool m_Flag = false;//fals  添加，true 修改

        public TextSetUp()
        {
            InitializeComponent();
            m_fontDialog = new FontDialog();
            m_fontDialog.ShowColor = true;
            InitFontDialog();
        }

        public void NewTextElement(IPoint point)
        {
            //添加
            m_point = point;
            m_Flag = false;
        }
        public void UpdateTextElement(ITextElement textElement)
        {
            //修改
            m_textElement = textElement;
            m_Flag = true;
            InitTextElementStyle(textElement);
        }

        private void InitTextElementStyle(ITextElement textElement)
        {
            txtContent.Text = textElement.Text;

            Font font = new Font(textElement.Symbol.Font.Name, (float)textElement.Symbol.Font.Size);
            Font fontOld = font;
            if (textElement.Symbol.Font.Bold)
            {
                fontOld = font;
                font = new Font(fontOld, FontStyle.Bold);
            }
            if (textElement.Symbol.Font.Italic)
            {
                fontOld = font;
                font = new Font(fontOld, FontStyle.Italic);
            }
            if (textElement.Symbol.Font.Strikethrough)
            {
                fontOld = font;
                font = new Font(fontOld, FontStyle.Strikeout);
            }
            if (textElement.Symbol.Font.Underline)
            {
                fontOld = font;
                font = new Font(fontOld, FontStyle.Underline);
            }
            m_fontDialog.Font = font;
            lblExample.Font = font;

            Color color = ColorTranslator.FromOle(textElement.Symbol.Color.RGB);
            m_fontDialog.Color = color;
            lblExample.ForeColor = color;
        }



        private void InitFontDialog()
        {
            m_fontDialog.Font = lblExample.Font;

        }

        private void btnFontSet_Click(object sender, EventArgs e)
        {
            if (m_fontDialog.ShowDialog() == DialogResult.OK)
            {
                lblExample.Font = m_fontDialog.Font;
                lblExample.ForeColor = m_fontDialog.Color;
            }
        }

        private void btnConform_Click(object sender, EventArgs e)
        {
            if (m_Flag)
            {
                Common.MapPrintCommon.TextElementUpdate(m_textElement, txtContent.Text, lblExample.Font, lblExample.ForeColor);
            }
            else
            {
                Common.MapPrintCommon.TextElementAdd(m_point, txtContent.Text, lblExample.Font, lblExample.ForeColor);
            }

            this.Close();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCalcel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.No;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (m_Flag)
            {
                Common.MapPrintCommon.TextElementUpdate(m_textElement, txtContent.Text, lblExample.Font, lblExample.ForeColor);
            }
            else
            {
                Common.MapPrintCommon.TextElementAdd(m_point, txtContent.Text, lblExample.Font, lblExample.ForeColor,ref m_textElement);
                m_Flag = true;
            }
        }

    }
}
