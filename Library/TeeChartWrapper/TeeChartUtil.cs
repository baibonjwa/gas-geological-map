using System;

namespace TeeChartWrapper
{
    public class TeeChartUtil
    {
        public static void resetTeeChart(Steema.TeeChart.TChart tChart)
        {
            // 初始化
            tChart.Series[0].Clear();

            // 设置X轴最小值和最大值
            tChart.Series[0].GetHorizAxis.SetMinMax(DateTime.Now, DateTime.Now.AddSeconds(1200));

            // 设置Y轴的最小值和最大值
            tChart.Series[0].GetVertAxis.SetMinMax(0, 10);

            // 设置Y轴间距
            tChart.Axes.Left.Increment = 0.1;

            //tChart.Axes.Bottom.MinorTickCount = this.updateFrequency;
        }

        /// <summary>
        /// 给TeeChart增加一个数据，并且重新绘制图形。
        /// </summary>
        /// <param name="tChart">Tea Chart</param>
        /// <param name="time">X Axes</param>
        /// <param name="value">Y Axes</param>
        public static void addSingleData2TeeChart(Steema.TeeChart.TChart tChart, int dataCountPerFrame, DateTime time, double value)
        {
            //// 重绘
            tChart.AutoRepaint = false;
            double maxVertValue = 0;
            double minVertValue = 0;

            // 绘画坐标点超过20个时将实时更新X时间坐标
            //while (tChart.Series[0].Count > 0 && tChart.Series[0].Count >= dataCountPerFrame - 1)
            //{
            // 删除第一个点
            //tChart.Series[0].Delete(0);
            // 重新设置X轴的最大值和最小值---x轴的时间间隔为20min.
            tChart.Series[0].GetHorizAxis.SetMinMax(DateTime.Now.AddSeconds(dataCountPerFrame * -1 * 10), DateTime.Now);
            //}

            tChart.Series[0].Add(time, value);

            // 更新最大值和最小值。
            double[] yValues = tChart.Series[0].YValues.Value;
            for (int i = 0; i < yValues.Length; i++)
            {
                if (yValues[i] > maxVertValue)
                {
                    maxVertValue = yValues[i];
                }
                if (yValues[i] < minVertValue)
                {
                    minVertValue = yValues[i];
                }
            }

            // update vertical coordinate, -+2 in order to make the curve align middle.
            if (tChart.Name == "tChartN")
            {
                tChart.Series[0].GetVertAxis.SetMinMax(minVertValue - 1, maxVertValue + 1);
            }
            //if (maxVertValue < 1)
            //{
            //    tChart.Series[0].GetVertAxis.SetMinMax(0, 1);
            //}
            //else
            //{
            //    tChart.Series[0].GetVertAxis.SetMinMax(minVertValue - minVertValue * 0.1, maxVertValue + maxVertValue * 0.1);
            //}
            // 重绘
            tChart.AutoRepaint = true;
            tChart.Refresh();
        }
    }
}
