using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Castle.ActiveRecord;
using GIS;
using LibCommon;
using LibEntity;

namespace sys3
{
    public partial class BigFaultageInfoEntering : Form
    {
        private string _errorMsg;

        /// <summary>
        ///     构造方法
        /// </summary>
        public BigFaultageInfoEntering()
        {
            InitializeComponent();
            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.INSERT_BIG_FAULTAGE_INFO);
        }

        public BigFaultageInfoEntering(BigFaultage bigFaultage)
        {
            InitializeComponent();
            // 主键
            using (new SessionScope())
            {
                // 设置窗体默认属性
                bigFaultage = BigFaultage.Find(bigFaultage.BigFaultageId);
                FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.INSERT_BIG_FAULTAGE_INFO);

                tbFaultageName.Text = bigFaultage.BigFaultageName;
                tbGap.Text = bigFaultage.Gap;
                tbAngle.Text = bigFaultage.Angle;
                tbTrend.Text = bigFaultage.Trend;

                if (bigFaultage.Type == "正断层")
                {
                    rbtnFrontFaultage.Checked = true;
                    rbtnOppositeFaultage.Checked = false;
                }
                else
                {
                    rbtnFrontFaultage.Checked = false;
                    rbtnOppositeFaultage.Checked = true;
                }

                foreach (var i in bigFaultage.BigFaultagePoints)
                {
                    if (i.UpOrDown == "上盘")
                    {
                        dgrdvUp.Rows.Add(i.CoordinateX, i.CoordinateY, i.CoordinateZ);
                    }
                    else
                    {
                        dgrdvDown.Rows.Add(i.CoordinateX, i.CoordinateY, i.CoordinateZ);
                    }
                }
            }
        }

        private void btnReadTxt_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                RestoreDirectory = true,
                Filter = @"文本文件(*.txt)|*.txt|所有文件(*.*)|*.*",
                Multiselect = true
            };
            //ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var aa = ofd.FileName;
                var strs = File.ReadAllLines(aa, Encoding.GetEncoding("GB2312"));
                var type = "";
                for (var i = 0; i < strs.Length; i++)
                {
                    if (i == 0)
                    {
                        var line1 = strs[i].Split('|');
                        tbFaultageName.Text = line1[0];
                        tbGap.Text = line1[1];
                        if (line1[2] == "正断层")
                        {
                            rbtnFrontFaultage.Checked = true;
                            rbtnOppositeFaultage.Checked = false;
                        }
                        else if (line1[2] == "逆断层")
                        {
                            rbtnFrontFaultage.Checked = false;
                            rbtnOppositeFaultage.Checked = true;
                        }
                        tbAngle.Text = line1[3];
                    }
                    if (strs[i] == "上盘")
                    {
                        type = "上盘";
                        continue;
                    }
                    if (strs[i] == "下盘")
                    {
                        type = "下盘";
                        continue;
                    }
                    if (strs[i].Equals(""))
                    {
                        continue;
                    }
                    if (type == "上盘")
                    {
                        dgrdvUp.Rows.Add(strs[i].Split(',')[0], strs[i].Split(',')[1], "0");
                    }
                    if (type == "下盘")
                    {
                        dgrdvDown.Rows.Add(strs[i].Split(',')[0], strs[i].Split(',')[1], "0");
                    }
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var bigFaultage = BigFaultage.FindOneByBigFaultageName(tbFaultageName.Text);
            var bigFaultagePoingList = new List<BigFaultagePoint>();
            if (bigFaultage == null)
            {
                bigFaultage = new BigFaultage
                {
                    BigFaultageName = tbFaultageName.Text,
                    Gap = tbGap.Text,
                    Angle = tbAngle.Text,
                    Trend = tbTrend.Text,
                    Type = rbtnFrontFaultage.Checked ? "正断层" : "逆断层",
                    BindingId = IDGenerator.NewBindingID()
                };
                for (var i = 0; i < dgrdvUp.Rows.Count; i++)
                {
                    var point = new BigFaultagePoint {UpOrDown = "上盘"};
                    if (dgrdvUp.Rows[i].Cells[0].Value == null) continue;
                    point.CoordinateX = Convert.ToDouble(dgrdvUp.Rows[i].Cells[0].Value);
                    point.CoordinateY = Convert.ToDouble(dgrdvUp.Rows[i].Cells[1].Value);
                    point.CoordinateZ = Convert.ToDouble(dgrdvUp.Rows[i].Cells[2].Value);
                    point.BindingId = IDGenerator.NewBindingID();
                    bigFaultagePoingList.Add(point);
                }
                for (var i = 0; i < dgrdvDown.Rows.Count; i++)
                {
                    var point = new BigFaultagePoint();
                    if (dgrdvDown.Rows[i].Cells[0].Value == null) continue;
                    point.UpOrDown = "下盘";
                    point.CoordinateX = Convert.ToDouble(dgrdvDown.Rows[i].Cells[0].Value);
                    point.CoordinateY = Convert.ToDouble(dgrdvDown.Rows[i].Cells[1].Value);
                    point.CoordinateZ = Convert.ToDouble(dgrdvDown.Rows[i].Cells[2].Value);
                    point.BindingId = IDGenerator.NewBindingID();
                    bigFaultagePoingList.Add(point);
                }
                bigFaultage.Save();
                var title = bigFaultage.BigFaultageName + "  " + bigFaultage.Angle + "  " +
                            bigFaultage.Gap;
                DrawBigFaultageInfo.DrawTddc(title, bigFaultagePoingList, bigFaultage.BindingId);
            }
            else
            {
                bigFaultage.BigFaultageName = tbFaultageName.Text;
                bigFaultage.Gap = tbGap.Text;
                bigFaultage.Angle = tbAngle.Text;
                bigFaultage.Trend = tbTrend.Text;
                bigFaultage.Type = rbtnFrontFaultage.Checked ? "正断层" : "逆断层";
                foreach (var bigFaultagePoint in bigFaultagePoingList)
                {
                    bigFaultagePoint.Save();
                }
                bigFaultage.Save();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReadMultTxt_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                RestoreDirectory = true,
                Filter = @"文本文件(*.txt)|*.txt|所有文件(*.*)|*.*",
                Multiselect = true
            };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            _errorMsg = @"失败文件名：";
            pbCount.Maximum = ofd.FileNames.Length;
            pbCount.Value = 0;
            lblTotal.Text = ofd.FileNames.Length.ToString(CultureInfo.InvariantCulture);
            foreach (var fileName in ofd.FileNames)
            {
                try
                {
                    var strs = File.ReadAllLines(fileName, Encoding.GetEncoding("GB2312"));
                    var type = "";
                    var split = strs[0].Split('|');
                    var bigFaultage = BigFaultage.FindOneByBigFaultageName(split[0]);
                    var bigFaultagePoints = new List<BigFaultagePoint>();
                    if (bigFaultage == null)
                    {
                        bigFaultage = new BigFaultage
                        {
                            BigFaultageName = split[0],
                            Gap = split[1],
                            Type = split[2],
                            Angle = split[3],
                            BindingId = IDGenerator.NewBindingID()
                        };
                    }
                    else
                    {
                        bigFaultage.BigFaultageName = split[0];
                        bigFaultage.Gap = split[1];
                        bigFaultage.Type = split[2];
                        bigFaultage.Angle = split[3];
                    }


                    for (var i = 1; i < strs.Length; i++)
                    {
                        if (strs[i] == "上盘")
                        {
                            type = "上盘";
                            continue;
                        }
                        if (strs[i] == "下盘")
                        {
                            type = "下盘";
                            continue;
                        }
                        if (strs[i].Equals(""))
                        {
                            continue;
                        }
                        if (type == "上盘")
                        {
                            bigFaultagePoints.Add(new BigFaultagePoint
                            {
                                BindingId = IDGenerator.NewBindingID(),
                                BigFaultage = bigFaultage,
                                CoordinateX = Convert.ToDouble(strs[i].Split(',')[0]),
                                CoordinateY = Convert.ToDouble(strs[i].Split(',')[1]),
                                CoordinateZ = 0.0,
                                UpOrDown = "上盘"
                            });
                        }
                        if (type == "下盘")
                        {
                            bigFaultagePoints.Add(new BigFaultagePoint
                            {
                                BindingId = IDGenerator.NewBindingID(),
                                BigFaultage = bigFaultage,
                                CoordinateX = Convert.ToDouble(strs[i].Split(',')[0]),
                                CoordinateY = Convert.ToDouble(strs[i].Split(',')[1]),
                                CoordinateZ = 0.0,
                                UpOrDown = "下盘"
                            });
                        }
                    }
                    bigFaultage.BigFaultagePoints = bigFaultagePoints;
                    var title = bigFaultage.BigFaultageName + "  " + bigFaultage.Angle + "  " +
                                bigFaultage.Gap;
                    DrawBigFaultageInfo.DrawTddc(title, bigFaultagePoints, bigFaultage.BindingId);
                    bigFaultage.Save();
                    lblSuccessed.Text =
                        (Convert.ToInt32(lblSuccessed.Text) + 1).ToString(CultureInfo.InvariantCulture);
                    pbCount.Value++;
                }
                catch (Exception)
                {
                    lblError.Text =
                        (Convert.ToInt32(lblError.Text) + 1).ToString(CultureInfo.InvariantCulture);
                    lblSuccessed.Text =
                        (Convert.ToInt32(lblSuccessed.Text) - 1).ToString(CultureInfo.InvariantCulture);
                    _errorMsg += fileName.Substring(fileName.LastIndexOf(@"\", StringComparison.Ordinal) + 1) + "\n";
                    btnDetails.Enabled = true;
                }
            }
            Alert.alert("导入成功！");
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            Alert.alert(_errorMsg);
        }
    }
}