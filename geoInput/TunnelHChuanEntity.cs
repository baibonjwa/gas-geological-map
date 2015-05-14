// ******************************************************************
// 概  述：
// 作  者：
// 日  期：2014-8-15
// 版本号：
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class TunnelHChuanEntity
    {
        //主键
        private int Id;

        /// <summary>
        /// 设置或获取主键
        /// </summary>
        public int ID
        {
            get { return Id; }
            set { Id = value; }
        }

        // 关联巷道1ID
        private int tunnelID1;

        /// <summary>
        /// 设置或获取巷道ID
        /// </summary>
        public int TunnelID1
        {
            get { return tunnelID1; }
            set { tunnelID1 = value; }
        }

        // 关联巷道1ID
        private int tunnelID2;

        /// <summary>
        /// 设置或获取巷道ID
        /// </summary>
        public int TunnelID2
        {
            get { return tunnelID2; }
            set { tunnelID2 = value; }
        }

        //导线点X1
        private double x_1;

        /// <summary>
        /// 导线点X1
        /// </summary>
        public double X_1
        {
            get { return x_1; }
            set { x_1 = value; }
        }

        //导线点Y1
        private double y_1;

        /// <summary>
        /// 导线点X1
        /// </summary>
        public double Y_1
        {
            get { return y_1; }
            set { y_1 = value; }
        }

        //导线点Z1
        private double z_1;

        /// <summary>
        /// 导线点Z1
        /// </summary>
        public double Z_1
        {
            get { return z_1; }
            set { z_1 = value; }
        }

        //导线点X2
        private double x_2;

        /// <summary>
        /// 导线点X1
        /// </summary>
        public double X_2
        {
            get { return x_2; }
            set { x_2 = value; }
        }

        //导线点Y1
        private double y_2;

        /// <summary>
        /// 导线点X1
        /// </summary>
        public double Y_2
        {
            get { return y_2; }
            set { y_2 = value; }
        }

        //导线点Z1
        private double z_2;

        /// <summary>
        /// 导线点Z1
        /// </summary>
        public double Z_2
        {
            get { return z_2; }
            set { z_2 = value; }
        }

        //方位角
        private double azimuth;

        /// <summary>
        /// 设置或获取方位角
        /// </summary>
        public double Azimuth
        {
            get { return azimuth; }
            set { azimuth = value; }
        }

        // 队别编号
        private int teamNameID;

        /// <summary>
        /// 设置或获取队别编号
        /// </summary>
        public int TeamNameID
        {
            get { return teamNameID; }
            set { teamNameID = value; }
        }

        // 开工日期
        private DateTime startDate;

        /// <summary>
        /// 设置或获取开工日期
        /// </summary>
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        // 是否施工完毕
        private int isFinish;

        /// <summary>
        /// 设置或获取是否掘进完毕
        /// </summary>
        public int IsFinish
        {
            get { return isFinish; }
            set { isFinish = value; }
        }

        // 停工日期
        private DateTime stopDate;

        /// <summary>
        /// 设置或获取停工日期
        /// </summary>
        public DateTime StopDate
        {
            get { return stopDate; }
            set { stopDate = value; }
        }

        // 工作制式
        private string workStyle;

        /// <summary>
        /// 设置或获取工作制式
        /// </summary>
        public string WorkStyle
        {
            get { return workStyle; }
            set { workStyle = value; }
        }

        // 班次
        private string workTime;

        /// <summary>
        /// 设置或获取班次
        /// </summary>
        public string WorkTime
        {
            get { return workTime; }
            set { workTime = value; }
        }

        // 班次
        private string state;

        /// <summary>
        /// 设置或获取巷道状态
        /// </summary>
        public string State
        {
            get { return state; }
            set { state = value; }
        }
    }
}
