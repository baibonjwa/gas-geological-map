using System;

namespace LibSocket
{
    /// <summary>
    /// Socket消息基类
    /// 每条Socket消息均包含Socket命令（SOCKET_COMMAND）、预警数据更新时间（注意：不一定是系统当前时间）
    /// </summary>
    public class SocketMessage
    {
        private COMMAND_ID _commandId = COMMAND_ID.UNDEFINED;//默认未定义

        //巷道ID
        protected int _tunnelID;

        /// <summary>
        /// 巷道ID
        /// </summary>
        public int TunnelId
        {
            get { return _tunnelID; }
            set { _tunnelID = value; }
        }

        //工作面ID
        protected int _workFaceID;

        /// <summary>
        /// 工作面ID
        /// </summary>
        public int WorkFaceId
        {
            get { return _workFaceID; }
            set { _workFaceID = value; }
        }

        //private DateTime _dTimeInternal = DateTime.Now;
        //Json解析使用的字段,为了控制其格式，因此使用string类型
        private string _dTime = "";

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="cmdId">Socket命令ID</param>
        /// <param name="dt">日期时间</param>
        public SocketMessage(COMMAND_ID cmdId, DateTime dt)
        {
            _commandId = cmdId;
            _dTime = dt.ToString(ConstSocketStr.DATE_FORMART_YYYY_MM_DD);
        }

        /// <summary>
        /// Socket命令，用于内部解析判别
        /// 外部调用时不要使用该方法，仅能通过构造函数进行设置，以免遗漏赋值;此处开发writer属性是为了兼容Json解析功能，否则Json不能正常解析！
        /// </summary>
        public COMMAND_ID CommandId
        {
            get { return _commandId; }
            //外部调用时不要使用该方法，仅能通过构造函数进行设置，以免遗漏赋值;此处开发writer属性是为了兼容Json解析功能，否则Json不能正常解析！
            set { _commandId = value; }
        }

        /// <summary>
        /// 预警数据更新（添加/修改/删除）时间或系统当前时间（预警数据没有时间的使用系统当前时间 时间格式：2014-03-08 11:29:55
        /// 转换为Json数据时必须使用字符串以统一格式
        /// 外部调用时不要使用该方法，仅能通过构造函数进行设置，以免遗漏赋值;此处开发writer属性是为了兼容Json解析功能，否则Json不能正常解析！
        /// </summary>
        public string DTime
        {
            get { return _dTime; }
            //外部调用时不要使用该方法，仅能通过构造函数进行设置，以免遗漏赋值;此处开发writer属性是为了兼容Json解析功能，否则Json不能正常解析！
            set { _dTime = value; }
        }

        /// <summary>
        /// 将时间字符串转换为DateTime
        /// </summary>
        /// <returns></returns>
        public DateTime ConvertTimeStr2DateTime()
        {
            return Convert.ToDateTime(_dTime);
        }

        public override string ToString()
        {
            string ret = "";
            ret += CommandId.ToString();
            ret += "; ";
            ret += DTime;
            ret += "; ";
            return ret;
        }
    }

    #region 服务器发送给客户端的消息
    /// <summary>
    /// 更新预警结果消息类
    /// </summary>
    public class UpdateWarningResultMessage : SocketMessage
    {
        // 预警类型---Ourburst/Overlimit
        private string _warningType;

        /// <summary>
        /// 预警类型
        /// </summary>
        public string WarningType
        {
            get { return _warningType; }
            set { _warningType = value; }
        }

        // 预警级别--red/yellow
        private string _warningLevel;

        /// <summary>
        /// 预警级别
        /// </summary>
        public string WarningLevel
        {
            get { return _warningLevel; }
            set { _warningLevel = value; }
        }

        // 预警原因
        private string _sReason;

        /// <summary>
        /// 预警原因
        /// </summary>
        public string WarningReason
        {
            get { return _sReason; }
            set { _sReason = value; }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="tunnelID">巷道ID</param>
        /// <param name="dt">更新时间</param>
        public UpdateWarningResultMessage(int tunnelID, DateTime dt)
            : base(COMMAND_ID.NTFY_WARNING_RESULT, dt)
        {
            _tunnelID = tunnelID;
        }
    }
    #endregion

    #region 客户端发送给服务器的数据
    /// <summary>
    /// 更新预警数据消息类
    /// </summary>
    public class UpdateWarningDataMsg : SocketMessage
    {
        //数据表名
        private string _tableName;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }
        //操作类型
        private OPERATION_TYPE _operationType;

        /// <summary>
        /// 操作类型
        /// </summary>
        public OPERATION_TYPE OperationType
        {
            get { return _operationType; }
            set { _operationType = value; }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="tunnelID">更新数据的巷道ID</param>
        /// <param name="tableName">数据对应的表名</param>
        /// <param name="operationType">操作类型</param>
        /// <param name="dt">数据更新时间</param>
        public UpdateWarningDataMsg(int workFaceId, int tunnelID, string tableName, OPERATION_TYPE operationType, DateTime dt)
            : base(COMMAND_ID.UPDATE_WARNING_DATA, dt)
        {
            this._workFaceID = workFaceId;
            this._tunnelID = tunnelID;
            this._tableName = tableName;
            this._operationType = operationType;
        }

        public override string ToString()
        {
            string ret = base.ToString();

            ret += WorkFaceId.ToString();
            ret += ";";
            ret += TunnelId.ToString();
            ret += "; ";
            ret += TableName;
            ret += "; ";
            ret += OperationType.ToString();
            ret += "; ";
            return ret;
        }
    }

    /// <summary>
    /// 重设巷道预警规则消息类
    /// </summary>
    public class ResetTunnelRulesMsg : SocketMessage
    {
        //数据表名
        private string _tableName;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        //操作类型
        private OPERATION_TYPE _operationType;

        /// <summary>
        /// 操作类型
        /// </summary>
        public OPERATION_TYPE OperationType
        {
            get { return _operationType; }
        }

        public ResetTunnelRulesMsg(int workFaceId, int tunnelID, string tableName, DateTime dt)
            : base(COMMAND_ID.RESET_TUNNEL_RULES, dt)
        {
            this._workFaceID = workFaceId;
            this._tunnelID = tunnelID;
            this._tableName = tableName;
            this._operationType = OPERATION_TYPE.UPDATE;
        }

        public override string ToString()
        {
            string ret = base.ToString();

            ret += TunnelId.ToString();
            ret += TableName;
            ret += OperationType.ToString();
            return ret;
        }
    }

    #endregion

    /// <summary>
    /// 地质构造消息类
    /// </summary>
    public class GeologyMsg : SocketMessage
    {
        private COMMAND_ID _commandId = COMMAND_ID.UNDEFINED;//默认未定义
        private string _dTime = "";

        public GeologyMsg(int workFaceId, int tunnelID, string tableName, DateTime dt, COMMAND_ID commandId)
            : base(COMMAND_ID.RESET_TUNNEL_RULES, dt)
        {
            this._workFaceID = workFaceId;
            this._tunnelID = tunnelID;
            this._tableName = tableName;
            this._operationType = OPERATION_TYPE.UPDATE;
            this._commandId = commandId;
        }

        public COMMAND_ID CommandId
        {
            get { return _commandId; }
            set { _commandId = value; }
        }

        public string DTime
        {
            get { return _dTime; }
            set { _dTime = value; }
        }

        //数据表名
        private string _tableName;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        //操作类型
        private OPERATION_TYPE _operationType;

        /// <summary>
        /// 操作类型
        /// </summary>
        public OPERATION_TYPE OperationType
        {
            get { return _operationType; }
        }
    }

    public enum OPERATION_TYPE
    {
        //添加
        ADD = 0,
        //修改
        UPDATE = 1,
        //删除
        DELETE = 2,
    }

    public static class ConstIndexs
    {
        //通用Socket命令ID起始索引
        public const int COMMON_SOCKET_ID_START_IDX = 0;

        //Server-->Client命令ID起始索引
        public const int S2C_SOCKET_ID_START_IDX = 10;

        //Client-->Server命令ID起始索引
        public const int C2S_SOCKET_ID_START_IDX = 20;

    }
    /// <summary>
    /// Socket命令
    /// </summary>
    public enum COMMAND_ID
    {
        //未定义命令，未对该命令进行处理
        UNDEFINED = 0,//ConstIndexs.COMMON_SOCKET_ID_START_IDX + 0,//0
        //心跳检测，检验客户端与服务器是否保持连接
        KEEP_ALIVE = 1,//ConstIndexs.COMMON_SOCKET_ID_START_IDX + 1,

        #region Server-->Client
        //通知客户端预警结果已更新
        NTFY_WARNING_RESULT = ConstIndexs.S2C_SOCKET_ID_START_IDX + 0,//10
        UPDATE_INFO_OK = ConstIndexs.S2C_SOCKET_ID_START_IDX + 1,
        // 通知客户端单个巷道预警信息
        WARNING_RESULT_SINGLE = ConstIndexs.S2C_SOCKET_ID_START_IDX + 2,
        #endregion

        #region Client-->Server
        // 通知服务端预警数据已更新
        UPDATE_WARNING_DATA = ConstIndexs.C2S_SOCKET_ID_START_IDX + 0,//20
        // 重设巷道预警规则
        RESET_TUNNEL_RULES = ConstIndexs.C2S_SOCKET_ID_START_IDX + 1,
        // 与服务器同步当前时间（优先级靠后）
        SYNC_DATETIME = ConstIndexs.C2S_SOCKET_ID_START_IDX + 2,
        // 更新班次信息
        UPDATE_WORK_TIME = ConstIndexs.C2S_SOCKET_ID_START_IDX + 3,
        // 注册预警结果通知----关心所有预警信息
        REGISTER_WARNING_RESULT_NOTIFICATION_ALL = ConstIndexs.C2S_SOCKET_ID_START_IDX + 4,
        // 注册预警结果通知----只关心单条巷道的预警信息
        REGISTER_WARNING_RESULT_NOTIFICATION_SINGLE = ConstIndexs.C2S_SOCKET_ID_START_IDX + 5,

        // 更新地质构造信息 
        UPDATE_GEOLOG_DATA = ConstIndexs.C2S_SOCKET_ID_START_IDX + 6,

        UPDATE_WARNING_RESULT = ConstIndexs.C2S_SOCKET_ID_START_IDX + 7

        // 更新传感器
        // UPDATE_TRANSDUCER = ConstIndexs.C2S_SOCKET_ID_START_IDX + 7


        #endregion
    }
}

