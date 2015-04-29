using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace GIS.Common
{
    /// <summary>
    /// MDB数据库基本操作
    /// </summary>
    public class MDBOperation
    {
        private static System.Data.OleDb.OleDbConnection m_ODbConn;
        public static System.Data.OleDb.OleDbConnection ODbConn
        {
            get { return m_ODbConn; }
            set { m_ODbConn = value; }
        }
        /// <summary>
        /// 获得数据库连接
        /// </summary>
        /// <param name="strConnString"></param>连接字符串
        /// <returns></returns>
        public static OleDbConnection GetODbConnection(string strConnString)
        {
            if (m_ODbConn != null)
                return m_ODbConn;
            try
            {
                if (!string.IsNullOrEmpty(strConnString))
                {
                    m_ODbConn = new OleDbConnection(strConnString);
                    if (m_ODbConn.State == System.Data.ConnectionState.Closed)
                    {
                        m_ODbConn.Open();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return m_ODbConn;
        }

        public static OleDbDataReader GetDataReader(string strSql)
        {
            try
            {
                OleDbCommand orclCmd = new OleDbCommand(strSql, m_ODbConn);
                OleDbDataReader dataReader = orclCmd.ExecuteReader();
                return dataReader;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        public static IDataAdapter GetDataAdapter(string strSql)
        {
            OleDbDataAdapter orclDA = default(OleDbDataAdapter);
            try
            {
                orclDA = new OleDbDataAdapter(strSql, m_ODbConn);
                return orclDA;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }

        }
        public static void ExecuteSQL(string strSql)
        {
            OleDbCommand dbCommand = default(OleDbCommand);
            try
            {
                dbCommand = new OleDbCommand(strSql, m_ODbConn);
                dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbCommand = null;
            }

        }
        /// <summary>
        /// 根据查询语句获取内存表(OleDb)
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static DataTable GetDataTableFromSql(string strSql)
        {
            OleDbCommand dbCommand = default(OleDbCommand);
            DataTable dt = new DataTable();
            OleDbDataAdapter dbDataAdapter = default(OleDbDataAdapter);
            try
            {
                dbDataAdapter = new OleDbDataAdapter();
                dbCommand = new OleDbCommand(strSql, m_ODbConn);
                dbDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                dbDataAdapter.SelectCommand = dbCommand;
                dbDataAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbCommand = null;
                dbDataAdapter = null;
            }
            return dt;
        }
        /// <summary>
        /// 利用内存表更新数据库(OleDb)
        /// </summary>
        /// <param name="dtNewData"></param>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static bool UpDateDataBaseFromTable(DataTable dtNewData, string strSql)
        {
            OleDbDataAdapter dbDataAdapter = default(OleDbDataAdapter);
            OleDbCommandBuilder dbCmdBuilder = default(OleDbCommandBuilder);
            try
            {
                dbDataAdapter = new OleDbDataAdapter(strSql, m_ODbConn);
                dbCmdBuilder = new OleDbCommandBuilder(dbDataAdapter);
                dbDataAdapter.Update(dtNewData);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
            finally
            {
                dbDataAdapter = null;
                dbCmdBuilder = null;
            }
        }
        /// <summary>
        /// 将图片，文档等文件保存到数据库
        /// </summary>
        /// <param name="strFileName"></param>文件路径
        /// <param name="strID"></param>该表的主键
        public static void WriteFileInOleDb(string strFileName, int strID)
        {
            try
            {
                FileStream fileStream = new FileStream(strFileName, FileMode.Open, FileAccess.ReadWrite);
                BinaryReader filerd = new BinaryReader(fileStream, Encoding.Default);
                byte[] fileByte = new byte[fileStream.Length + 1];
                filerd.Read(fileByte, 0, (int)fileStream.Length);
                string strSql = "update t_pro_filedoc set FILE_CONTENT=:P1 Where ID=" + strID.ToString();
                UpdateBlobField(strSql, fileByte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 参数与二进制字节数组互换
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="byteBlob"></param>
        /// <returns></returns>
        public static bool UpdateBlobField(string strSQL, byte[] byteBlob)
        {
            OleDbCommand pDBCommand = default(OleDbCommand);
            OleDbParameter pPara = default(OleDbParameter);
            pDBCommand = new OleDbCommand(strSQL, m_ODbConn);
            pPara = new OleDbParameter(":P1", OleDbType.Binary, byteBlob.Length);
            pPara.Value = byteBlob;
            pDBCommand.Parameters.Add(pPara);
            if (pDBCommand.ExecuteNonQuery() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
