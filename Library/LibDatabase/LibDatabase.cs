// ******************************************************************
// 概  述：
// 作  者：
// 创建日期：
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
#define RELEASE_GWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using LibEncryptDecrypt;
using System.IO;
using LibConfig;

namespace LibDatabase
{
    /// <summary>
    /// 带特殊字符SQL
    /// </summary>
    public class PValue
    {
        private string proc;

        public string Proc
        {
            get { return proc; }
            set { proc = value; }
        }
        private string paraValue;

        public string ParaValue
        {
            get { return paraValue; }
            set { paraValue = value; }
        }
        /// <summary>
        /// 带特殊字符SQL参数
        /// </summary>
        /// <param name="proc">列名</param>
        /// <param name="pValue">参数</param>
        public PValue(string proc, string pValue)
        {
            this.proc = proc;
            this.paraValue = pValue;
        }
    }


    public enum DATABASE_TYPE
    {
        GasEmissionDB,//1.工作面瓦斯涌出动态特征管理系统
        MiningSchedulingDB,//2.工作面采掘进度管理系统数据库
        GeologyMeasureDB,//3.工作面地质测量管理系统
        OutburstPreventionDB,//4.工作面动态防突管理系统
        WarningManagementDB,//5.工作面预警管理系统
    }
    public class ManageDataBase
    {

        //声明api
        [DllImport("kernel32")]
        private static extern bool GetPrivateProfileString(string lpApplicationName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        //public StringBuilder DataSource = new StringBuilder(256);
        //public StringBuilder UID = new StringBuilder(256);
        //public StringBuilder PWD = new StringBuilder(256);
        //public StringBuilder DATABASE = new StringBuilder(256);

        public string DataSource = string.Empty;
        public string DataBase = string.Empty;
        public string strID = string.Empty;
        public string strPW = string.Empty;

        public string title;
        public string configPath;

        private SqlConnection _sqlCon = null;

        private string GetConnectString()
        {

            try
            {
                ConfigManager cfgMgr = ConfigManager.Instance;

                DataSource = cfgMgr.getValueByKey(ConfigConst.CONFIG_DATASOURCE);
                DataBase = cfgMgr.getValueByKey(ConfigConst.CONFIG_DATABASE_MAIN);

                strID = cfgMgr.getValueByKey(ConfigConst.CONFIG_DATABASE_UID);
                strPW = cfgMgr.getValueByKey(ConfigConst.CONFIG_DATABASE_PASSWD);

#if RELEASE_GWS
                strID = DWEncryptDecryptClass.DecryptString(strID);
                strPW = DWEncryptDecryptClass.DecryptString(strPW);
#endif
                string connectString = "Data Source=" + DataSource +
                    ";Initial Catalog=" + DataBase +
                    ";Persist Security Info=True;User ID=" + strID +
                    ";Password=" + strPW + ";Pooling=True";//应启用连接池 Pooling=True
                return connectString;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return String.Empty;
        }

        public void SetConnectString()
        {
            try
            {
#if RELEASE_GWS
                string strDataSource = DWEncryptDecryptClass.EncryptString(DataSource.ToString());
                string strDATABASE = DWEncryptDecryptClass.EncryptString(DataBase.ToString());
                string UID = DWEncryptDecryptClass.EncryptString(strID.ToString());
                string PWD = DWEncryptDecryptClass.EncryptString(strPW.ToString());
                WritePrivateProfileString(title, "DataSource", strDataSource, configPath);
                WritePrivateProfileString(title, "DATABASE", strDATABASE, configPath);
                WritePrivateProfileString(title, "UID", strID, configPath);
                WritePrivateProfileString(title, "PWD", strPW, configPath);
#endif
                //WritePrivateProfileString(title, "DataSource", DataSource.ToString(), configPath);
                //WritePrivateProfileString(title, "DATABASE", DATABASE.ToString(), configPath);
                //WritePrivateProfileString(title, "UID", UID.ToString(), configPath);
                //WritePrivateProfileString(title, "PWD", PWD.ToString(), configPath);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        string _conStr = "";
        public ManageDataBase(DATABASE_TYPE dt)
        {
            _conStr = GetConnectString();
            _sqlCon = new SqlConnection(_conStr);
        }

        /// <summary>
        /// 打开数据库连接.
        /// </summary>
        public void Open()
        {
            if (_sqlCon == null)
            {
                _sqlCon = new SqlConnection(_conStr);
                _sqlCon.Open();
            }
            else
            {
                if (_sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    _sqlCon.Open();
                }
                else if (_sqlCon.State == System.Data.ConnectionState.Broken)
                {
                    _sqlCon.Close();
                    _sqlCon.Open();
                }
            }

        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            if (_sqlCon != null)
            {
                _sqlCon.Close();
                _sqlCon.Dispose();
                _sqlCon = null;
            }
        }

        /// <summary>
        /// 根据SQL查询返回DataSet对象，如果没有查询到则返回NULL
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet ReturnDS(string sql)
        {
            DataSet ds = new DataSet();
            try
            {
                if (_sqlCon == null)
                {
                    _sqlCon = new SqlConnection(_conStr);
                    _sqlCon.Open();
                }
                SqlCommand cmd = new SqlCommand(sql, _sqlCon);

                cmd.CommandTimeout = 2000;
                this.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                //throw (e);
            }
            finally
            {
                this.Close();
            }
            return ds;
        }

        /**  暂定 未使用  ↓↓↓↓↓↓↓↓↓↓↓↓↓↓ **/
        /// <summary>
        /// 执行存储过程返回DataSet
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="prams">参数列表</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(string procName, SqlParameter[] prams)
        {
            DataSet ds = new DataSet();

            try
            {
                if (_sqlCon == null)
                {
                    _sqlCon = new SqlConnection(_conStr);
                    _sqlCon.Open();
                }
                //SqlCommand cmd = new SqlCommand(sql, _sqlCon)
                SqlCommand cmd = new SqlCommand(procName, _sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;

                if (prams != null)
                {
                    foreach (SqlParameter parameter in prams)
                        cmd.Parameters.Add(parameter);
                }

                //cmd.CommandTimeout = 2000;
                this.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);

            }
            catch //(Exception e)
            {
                //MessageBox.Show(e.Message);
                //throw (e);
            }
            finally
            {
                this.Close();
            }
            return ds;

        }
        /**  暂定 未使用 ↑↑↑↑↑↑↑↑↑↑↑↑↑ **/

        public bool ExecuteDataSet(string sql, PValue[] pValue)
        {
            bool succeed = false;
            int cnt = 0;

            try
            {
                if (_sqlCon == null)
                {
                    _sqlCon = new SqlConnection(_conStr);
                    _sqlCon.Open();
                }
                //SqlCommand cmd = new SqlCommand(sql, _sqlCon)
                SqlCommand cmd = _sqlCon.CreateCommand();
                cmd.CommandText = sql;

                for (int i = 0; i < pValue.Length; i++)
                {
                    cmd.Parameters.Add(new SqlParameter(pValue[i].Proc, pValue[i].ParaValue));
                }

                this.Open();
                cmd.CommandTimeout = 2000;
                cnt = cmd.ExecuteNonQuery();

            }
            catch// (Exception e)
            {
                //MessageBox.Show(e.Message);
                //throw (e);
            }
            finally
            {
                if (cnt > 0)
                {
                    succeed = true;
                }
                this.Close();
            }
            return succeed;
        }

        public DataSet ReturnDSNotOpenAndClose(string sql)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, _sqlCon);
                cmd.CommandTimeout = 2000;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch// (Exception e)
            {
                //throw (e);

            }
            return ds;
        }

        /// <summary>
        /// 对数据库的增，删，改的操作
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="[] p1">byte 类型的图片</param>
        /// <param name="par1">sql参数</param>
        /// <returns>是否成功</returns>
        public bool OperateDB(string sql, byte[] p1, string par1)
        {
            bool succeed = false;
            int cnt = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(sql, _sqlCon);
                cmd.CommandTimeout = 100;
                this.Open();

                if (p1 == null)
                    p1 = new byte[0];

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(par1, SqlDbType.Image).Value = p1;

                cnt = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                //MessageBox.Show(e.ToString());
                if (e.ToString().Contains("违反了 PRIMARY KEY 约束 "))
                {
                    MessageBox.Show("此信息在数据库中已经存在,无法将此信息再次保存");
                }
                else
                    throw (e);
            }
            finally
            {
                if (cnt > 0)
                {
                    succeed = true;
                }
                this.Close();
            }

            return succeed;
        }

        /// <summary>
        /// 对数据库的增，删，改的操作
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>是否成功</returns>
        public bool OperateDB(string sql)
        {
            bool succeed = false;
            int cnt = 0;
            try
            {
                this.Open();
                SqlCommand cmd = new SqlCommand(sql, _sqlCon);
                cmd.CommandTimeout = 2000;
                cnt = cmd.ExecuteNonQuery();
            }
            catch //(Exception e)
            {
                // throw (e);
            }
            finally
            {
                if (cnt > 0)
                {
                    succeed = true;
                }
                this.Close();
            }
            return succeed;
        }


        public string OperateDBReturnValue(string sql)
        {
            bool succeed = false;
            string cnt = "";
            try
            {
                this.Open();
                SqlCommand cmd = new SqlCommand(sql, _sqlCon);
                cmd.CommandTimeout = 2000;
                cnt = cmd.ExecuteScalar().ToString();
            }
            catch //(Exception e)
            {
                // throw (e);
            }
            finally
            {
                this.Close();
            }
            return cnt;
        }

        public bool OperateDBNotOpenAndClose(string sql)
        {
            bool succeed = false;
            int cnt = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(sql, _sqlCon);
                cmd.CommandTimeout = 2000;
                cnt = cmd.ExecuteNonQuery();
            }
            catch// (Exception e)
            {
                //throw (e);
            }
            finally
            {
                if (cnt > 0)
                {
                    succeed = true;
                }
            }
            return succeed;
        }

        /// <summary>
        /// 测试联接
        /// </summary>
        /// <returns></returns>
        public bool TestLink()
        {
            bool boolReturn = false;

            string connectString = GetConnectString();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = 60;

                _sqlCon = new SqlConnection(connectString);
                _sqlCon.Open();

                boolReturn = true;
            }
            catch //(Exception e)
            {
                boolReturn = false;
            }
            finally
            {
                this.Close();
            }
            return boolReturn;
        }

        /// <summary>
        /// 返回Datareader对象
        /// </summary>
        /// <param name="Sqlstr">查询字符串</param>
        /// <returns>返回值</returns>
        public SqlDataReader GetDataReader(string Sqlstr)
        {
            Open();
            SqlCommand cmd = new SqlCommand(Sqlstr, _sqlCon);
            SqlDataReader sdr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            return sdr;
        }



        /*以下代码暂不使用
        public bool OperateDB(string sql, byte[] pb1, byte[] pb2, byte[] pb3, string par1, string par2, string par3)
        {
            bool succeed = false;
            int cnt = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandTimeout = 100;
                this.Open();
                //cmd.Parameters.Add("@Cover", SqlDbType.VarBinary).Value = picbyte;
                if (pb1 == null)
                    pb1 = new byte[0];
                if (pb2 == null)
                    pb2 = new byte[0];
                if (pb3 == null)
                    pb3 = new byte[0];
                cmd.CommandType = CommandType.Text;
                //@PicEmployeePersonnelArchives,@PicNewJoinInsuranceIDCard,@PicFirstExamSalaryTable,@PicSalaryDispenceTable,@PicSetDownOriginalCertificate,@PicSpecialCertificate,@PicPhotoPictute
                cmd.Parameters.Add(par1, SqlDbType.Image).Value = pb1;
                cmd.Parameters.Add(par2, SqlDbType.Image).Value = pb2;
                cmd.Parameters.Add(par3, SqlDbType.Image).Value = pb3;
                cnt = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                //MessageBox.Show(e.ToString());
                if (e.ToString().Contains("违反了 PRIMARY KEY 约束 "))
                {
                    MessageBox.Show("此信息在数据库中已经存在,无法将此信息再次保存");
                }
                else
                    throw (e);
            }
            finally
            {
                if (cnt > 0)
                {
                    succeed = true;
                }
                this.Close();
            }

            return succeed;
        }

        public bool OperateDB(string sql, byte[] pb1, byte[] pb2, byte[] pb3, byte[] pb4, byte[] pb5, byte[] pb6, byte[] pb7, string par1, string par2, string par3, string par4, string par5, string par6, string par7)
        {
            bool succeed = false;
            int cnt = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandTimeout = 100;
                this.Open();
                //cmd.Parameters.Add("@Cover", SqlDbType.VarBinary).Value = picbyte;
                if (pb1 == null)
                    pb1 = new byte[0];
                if (pb2 == null)
                    pb2 = new byte[0];
                if (pb3 == null)
                    pb3 = new byte[0];
                if (pb4 == null)
                    pb4 = new byte[0];
                if (pb5 == null)
                    pb5 = new byte[0];
                if (pb6 == null)
                    pb6 = new byte[0];
                if (pb7 == null)
                    pb7 = new byte[0];
                cmd.CommandType = CommandType.Text;
                //@PicEmployeePersonnelArchives,@PicNewJoinInsuranceIDCard,@PicFirstExamSalaryTable,@PicSalaryDispenceTable,@PicSetDownOriginalCertificate,@PicSpecialCertificate,@PicPhotoPictute
                cmd.Parameters.Add(par1, SqlDbType.Image).Value = pb1;
                cmd.Parameters.Add(par2, SqlDbType.Image).Value = pb2;
                cmd.Parameters.Add(par3, SqlDbType.Image).Value = pb3;
                cmd.Parameters.Add(par4, SqlDbType.Image).Value = pb4;
                cmd.Parameters.Add(par5, SqlDbType.Image).Value = pb5;
                cmd.Parameters.Add(par6, SqlDbType.Image).Value = pb6;
                cmd.Parameters.Add(par7, SqlDbType.Image).Value = pb7;
                cnt = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                //MessageBox.Show(e.ToString());
                if (e.ToString().Contains("违反了 PRIMARY KEY 约束 "))
                {
                    MessageBox.Show("此信息在数据库中已经存在,无法将此信息再次保存");
                }
                else
                    throw (e);
            }
            finally
            {
                if (cnt > 0)
                {
                    succeed = true;
                }
                this.Close();
            }

            return succeed;
        }

        public bool OperateDB(string sql, byte[] pb1, byte[] pb2, byte[] pb3, byte[] pb4, byte[] pb5, byte[] pb6, byte[] pb7, byte[] pb8, string par1, string par2, string par3, string par4, string par5, string par6, string par7, string par8)
        {
            bool succeed = false;
            int cnt = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandTimeout = 100;
                this.Open();
                //cmd.Parameters.Add("@Cover", SqlDbType.VarBinary).Value = picbyte;
                if (pb1 == null)
                    pb1 = new byte[0];
                if (pb2 == null)
                    pb2 = new byte[0];
                if (pb3 == null)
                    pb3 = new byte[0];
                if (pb4 == null)
                    pb4 = new byte[0];
                if (pb5 == null)
                    pb5 = new byte[0];
                if (pb6 == null)
                    pb6 = new byte[0];
                if (pb7 == null)
                    pb7 = new byte[0];
                if (pb8 == null)
                    pb8 = new byte[0];
                cmd.CommandType = CommandType.Text;
                //@PicEmployeePersonnelArchives,@PicNewJoinInsuranceIDCard,@PicFirstExamSalaryTable,@PicSalaryDispenceTable,@PicSetDownOriginalCertificate,@PicSpecialCertificate,@PicPhotoPictute
                cmd.Parameters.Add(par1, SqlDbType.Image).Value = pb1;
                cmd.Parameters.Add(par2, SqlDbType.Image).Value = pb2;
                cmd.Parameters.Add(par3, SqlDbType.Image).Value = pb3;
                cmd.Parameters.Add(par4, SqlDbType.Image).Value = pb4;
                cmd.Parameters.Add(par5, SqlDbType.Image).Value = pb5;
                cmd.Parameters.Add(par6, SqlDbType.Image).Value = pb6;
                cmd.Parameters.Add(par7, SqlDbType.Image).Value = pb7;
                cmd.Parameters.Add(par8, SqlDbType.Image).Value = pb8;
                cnt = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                //MessageBox.Show(e.ToString());
                if (e.ToString().Contains("违反了 PRIMARY KEY 约束 "))
                {
                    MessageBox.Show("此信息在数据库中已经存在,无法将此信息再次保存");
                }
                else
                    throw (e);
            }
            finally
            {
                if (cnt > 0)
                {
                    succeed = true;
                }
                this.Close();
            }

            return succeed;
        }

        public bool OperateDB(string sql, byte[] pb1, byte[] pb2, byte[] pb3, byte[] pb4, byte[] pb5, string par1, string par2, string par3, string par4, string par5)
        {
            bool succeed = false;
            int cnt = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandTimeout = 100;
                this.Open();
                //cmd.Parameters.Add("@Cover", SqlDbType.VarBinary).Value = picbyte;
                if (pb1 == null)
                    pb1 = new byte[0];
                if (pb2 == null)
                    pb2 = new byte[0];
                if (pb3 == null)
                    pb3 = new byte[0];
                if (pb4 == null)
                    pb4 = new byte[0];
                if (pb5 == null)
                    pb5 = new byte[0];
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(par1, SqlDbType.Image).Value = pb1;
                cmd.Parameters.Add(par2, SqlDbType.Image).Value = pb2;
                cmd.Parameters.Add(par3, SqlDbType.Image).Value = pb3;
                cmd.Parameters.Add(par4, SqlDbType.Image).Value = pb4;
                cmd.Parameters.Add(par5, SqlDbType.Image).Value = pb5;
                cnt = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                //MessageBox.Show(e.ToString());
                if (e.ToString().Contains("违反了 PRIMARY KEY 约束 "))
                {
                    MessageBox.Show("此信息在数据库中已经存在,无法将此信息再次保存");
                }
                else
                    throw (e);
            }
            finally
            {
                if (cnt > 0)
                {
                    succeed = true;
                }
                this.Close();
            }

            return succeed;
        }

        public bool OperateDB(string sql, byte[] p1, string par1)
        {
            bool succeed = false;
            int cnt = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandTimeout = 100;
                this.Open();

                if (p1 == null)
                    p1 = new byte[0];

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(par1, SqlDbType.Image).Value = p1;

                cnt = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                //MessageBox.Show(e.ToString());
                if (e.ToString().Contains("违反了 PRIMARY KEY 约束 "))
                {
                    MessageBox.Show("此信息在数据库中已经存在,无法将此信息再次保存");
                }
                else
                    throw (e);
            }
            finally
            {
                if (cnt > 0)
                {
                    succeed = true;
                }
                this.Close();
            }

            return succeed;
        }

        public bool OperateDB(string sql, byte[] p1, byte[] p2, string par1, string par2)
        {
            bool succeed = false;
            int cnt = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandTimeout = 100;
                this.Open();

                if (p1 == null)
                    p1 = new byte[0];
                if (p2 == null)
                    p2 = new byte[0];

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(par1, SqlDbType.Image).Value = p1;
                cmd.Parameters.Add(par2, SqlDbType.Image).Value = p2;

                cnt = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                //MessageBox.Show(e.ToString());
                if (e.ToString().Contains("违反了 PRIMARY KEY 约束 "))
                {
                    MessageBox.Show("此信息在数据库中已经存在,无法将此信息再次保存");
                }
                else
                    throw (e);
            }
            finally
            {
                if (cnt > 0)
                {
                    succeed = true;
                }
                this.Close();
            }

            return succeed;
        }
        */
        //插入一条记录
        public void OperateDBWithPropertys(String[] DBPropertys, String DBTable, int length, SqlDbType[] sqlTypes, Object[] contents)
        {
            _sqlCon = new SqlConnection(_conStr);
            _sqlCon.Open();

            string sql_Porperty = " ";
            string sql_Porperty2 = " ";

            for (int i = 0; i < length; i++)
            {
                sql_Porperty += DBPropertys[i] + ",";
                sql_Porperty2 += "@" + DBPropertys[i] + ",";
            }

            string tempSQL = sql_Porperty.Substring(0, sql_Porperty.Length - 1);
            string tempSQL2 = sql_Porperty2.Substring(0, sql_Porperty2.Length - 1);
            String SQLstr = "insert into " + DBTable + " (" + tempSQL + ") values (" + tempSQL2 + ")";

            SqlCommand mycomm = new SqlCommand(SQLstr, _sqlCon);
            for (int i = 0; i < length; i++)
            {
                mycomm.Parameters.Add(new SqlParameter("@" + DBPropertys[i], sqlTypes[i]));
                mycomm.Parameters["@" + DBPropertys[i]].Value = contents[i];
            }

            mycomm.ExecuteNonQuery();
            _sqlCon.Close();
        }

    }
}
