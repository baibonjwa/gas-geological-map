using GIS.HdProc;

namespace GIS
{
    public class GisHelper
    {
        public static void DelHdByHdId(string hdId)
        {
            //清除巷道信息
            string sql = "\"" + GIS_Const.FIELD_HDID + "\"='" + hdId + "'";
            //string sql = "\"" + GIS_Const.FIELD_HDID + "\"<>'" + HdId + "'";
            Global.commonclss.DelFeatures(Global.pntlyr, sql);
            Global.commonclss.DelFeatures(Global.centerlyr, sql);
            Global.commonclss.DelFeatures(Global.centerfdlyr, sql);
            Global.commonclss.DelFeatures(Global.hdfdfulllyr, sql);
            Global.commonclss.DelFeatures(Global.hdfdlyr, sql);
            Global.commonclss.DelFeatures(Global.pntlinlyr, sql);
            //删除峒室信息
            Global.commonclss.DelFeatures(Global.dslyr, sql);
        }
    }
}
