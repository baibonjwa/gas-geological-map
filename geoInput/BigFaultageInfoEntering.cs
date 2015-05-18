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

namespace geoInput
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
        }

        public BigFaultageInfoEntering(InferFaultage inferFaultage)
        {
            InitializeComponent();
            // 主键
            using (new SessionScope())
            {
                // 设置窗体默认属性
                inferFaultage = InferFaultage.Find(inferFaultage.id);
                tbFaultageName.Text = inferFaultage.big_faultage_name;
                tbGap.Text = inferFaultage.gap;
                tbAngle.Text = inferFaultage.angle;
                tbTrend.Text = inferFaultage.trend;

                if (inferFaultage.type == "正断层")
                {
                    rbtnFrontFaultage.Checked = true;
                    rbtnOppositeFaultage.Checked = false;
                }
                else
                {
                    rbtnFrontFaultage.Checked = false;
                    rbtnOppositeFaultage.Checked = true;
                }

                foreach (var i in inferFaultage.big_faultage_points)
                {
                    if (i.up_or_down == "上盘")
                    {
                        dgrdvUp.Rows.Add(i.coordinate_x, i.coordinate_y, i.coordinate_z);
                    }
                    else
                    {
                        dgrdvDown.Rows.Add(i.coordinate_x, i.coordinate_y, i.coordinate_z);
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
            var bigFaultage = InferFaultage.find_one_by_big_faultage_name(tbFaultageName.Text);
            var bigFaultagePoingList = new List<InferFaultagePoint>();
            if (bigFaultage == null)
            {
                bigFaultage = new InferFaultage
                {
                    big_faultage_name = tbFaultageName.Text,
                    gap = tbGap.Text,
                    angle = tbAngle.Text,
                    trend = tbTrend.Text,
                    type = rbtnFrontFaultage.Checked ? "正断层" : "逆断层",
                    bid = IdGenerator.NewBindingId()
                };
                for (var i = 0; i < dgrdvUp.Rows.Count; i++)
                {
                    var point = new InferFaultagePoint { up_or_down = "上盘" };
                    if (dgrdvUp.Rows[i].Cells[0].Value == null) continue;
                    point.coordinate_x = Convert.ToDouble(dgrdvUp.Rows[i].Cells[0].Value);
                    point.coordinate_y = Convert.ToDouble(dgrdvUp.Rows[i].Cells[1].Value);
                    point.coordinate_z = Convert.ToDouble(dgrdvUp.Rows[i].Cells[2].Value);
                    point.bid = IdGenerator.NewBindingId();
                    bigFaultagePoingList.Add(point);
                }
                for (var i = 0; i < dgrdvDown.Rows.Count; i++)
                {
                    var point = new InferFaultagePoint();
                    if (dgrdvDown.Rows[i].Cells[0].Value == null) continue;
                    point.up_or_down = "下盘";
                    point.coordinate_x = Convert.ToDouble(dgrdvDown.Rows[i].Cells[0].Value);
                    point.coordinate_y = Convert.ToDouble(dgrdvDown.Rows[i].Cells[1].Value);
                    point.coordinate_z = Convert.ToDouble(dgrdvDown.Rows[i].Cells[2].Value);
                    point.bid = IdGenerator.NewBindingId();
                    bigFaultagePoingList.Add(point);
                }
                bigFaultage.Save();
                var title = bigFaultage.big_faultage_name + "  " + bigFaultage.angle + "  " +
                            bigFaultage.gap;
                DrawBigFaultageInfo.DrawTddc(title, bigFaultagePoingList, bigFaultage.bid);
            }
            else
            {
                bigFaultage.big_faultage_name = tbFaultageName.Text;
                bigFaultage.gap = tbGap.Text;
                bigFaultage.angle = tbAngle.Text;
                bigFaultage.trend = tbTrend.Text;
                bigFaultage.type = rbtnFrontFaultage.Checked ? "正断层" : "逆断层";
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
                    var bigFaultage = InferFaultage.find_one_by_big_faultage_name(split[0]);
                    var bigFaultagePoints = new List<InferFaultagePoint>();
                    if (bigFaultage == null)
                    {
                        bigFaultage = new InferFaultage
                        {
                            big_faultage_name = split[0],
                            gap = split[1],
                            type = split[2],
                            angle = split[3],
                            bid = IdGenerator.NewBindingId()
                        };
                    }
                    else
                    {
                        bigFaultage.big_faultage_name = split[0];
                        bigFaultage.gap = split[1];
                        bigFaultage.type = split[2];
                        bigFaultage.angle = split[3];
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
                            bigFaultagePoints.Add(new InferFaultagePoint
                            {
                                bid = IdGenerator.NewBindingId(),
                                infer_faultage = bigFaultage,
                                coordinate_x = Convert.ToDouble(strs[i].Split(',')[0]),
                                coordinate_y = Convert.ToDouble(strs[i].Split(',')[1]),
                                coordinate_z = 0.0,
                                up_or_down = "上盘"
                            });
                        }
                        if (type == "下盘")
                        {
                            bigFaultagePoints.Add(new InferFaultagePoint
                            {
                                bid = IdGenerator.NewBindingId(),
                                infer_faultage = bigFaultage,
                                coordinate_x = Convert.ToDouble(strs[i].Split(',')[0]),
                                coordinate_y = Convert.ToDouble(strs[i].Split(',')[1]),
                                coordinate_z = 0.0,
                                up_or_down = "下盘"
                            });
                        }
                    }
                    bigFaultage.big_faultage_points = bigFaultagePoints;
                    var title = bigFaultage.big_faultage_name + "  " + bigFaultage.angle + "  " +
                                bigFaultage.gap;
                    DrawBigFaultageInfo.DrawTddc(title, bigFaultagePoints, bigFaultage.bid);
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
            Alert.AlertMsg("导入成功！");
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            Alert.AlertMsg(_errorMsg);
        }
    }
}