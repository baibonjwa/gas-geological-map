// ******************************************************************
// 概  述：停采线数据实体
// 作  者：宋英杰
// 日  期：2014/3/12
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class GeologySpaceEntityNew
    {
        // 主键
        private int iD;

        /// <summary>
        /// 设置或获取主键
        /// </summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        // 工作面ID
        private int workFaceId;

        /// <summary>
        /// 
        /// </summary>
        public int WorkSpaceID
        {
            get { return workFaceId; }
            set { workFaceId = value; }
        }

        // 距离
        private int tectonicID;

        /// <summary>
        /// 
        /// </summary>
        public int TectonicID
        {
            get { return tectonicID; }
            set { tectonicID = value; }
        }

        // 起点坐标Y
        private double distance;

        /// <summary>
        /// 设置或获取起点坐标Y
        /// </summary>
        public double Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        // 类型
        private int tectonicType;

        /// <summary>
        /// 类型
        /// </summary>
        public int TectonicType
        {
            get { return tectonicType; }
            set { tectonicType = value; }
        }

        //时间 

        private string ondatetime;

        /// <summary>
        /// 
        /// </summary>
        public string onDateTime
        {
            get { return ondatetime; }
            set { ondatetime = value; }
        }
    }
}
