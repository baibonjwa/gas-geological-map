using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

// Includes Nini namespace (don't forget to add the reference in your project)
using Nini.Config;

namespace LibConfig
{
    public sealed class ConfigManager
    {
        private static volatile ConfigManager instance;
        private static object syncRoot = new Object();

        private Hashtable ht = new Hashtable();

        private ConfigManager() { }

        public static ConfigManager Instance
        {
            get
            {
                if (null == instance)
                {
                    lock (syncRoot)
                    {
                        if (null == instance)
                        {
                            instance = new ConfigManager();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Loads all configuration values into the UI controls.
        /// </summary>
        public string init(string path)
        {
            string configPath = path + "\\" + ConfigConst.CONFIG_FILE_NAME;
            if (!File.Exists(configPath))
            {
                return "无法找到配置文件:" + ConfigConst.CONFIG_FILE_NAME;
            }

            // Load the configuration source file
            IConfigSource source = new IniConfigSource(configPath);

            // Set the config to the Logging section of the INI file.
            IConfig config = source.Configs[ConfigConst.CONFIG_NETWORK];

            // Load up some normal configuration values
            string serverIp = config.Get(ConfigConst.CONFIG_SERVER_IP);
            string port = config.Get(ConfigConst.CONFIG_PORT);
            string restPort = config.Get(ConfigConst.CONFIG_REST_PORT);

            ConfigManager cfgMgr = ConfigManager.Instance;

            cfgMgr.add(ConfigConst.CONFIG_SERVER_IP, serverIp);
            cfgMgr.add(ConfigConst.CONFIG_PORT, port);
            cfgMgr.add(ConfigConst.CONFIG_REST_PORT, restPort);

            config = source.Configs[ConfigConst.CONFIG_DATABASE];
            string dataSource = config.Get(ConfigConst.CONFIG_DATASOURCE);
            string databaseMain = config.Get(ConfigConst.CONFIG_DATABASE_MAIN);
            string databaseGIS = config.Get(ConfigConst.CONFIG_DATABASE_GIS);
            string databaseUID = config.Get(ConfigConst.CONFIG_DATABASE_UID);
            string databasePwd = config.Get(ConfigConst.CONFIG_DATABASE_PASSWD);
            string mxdFile = config.Get(ConfigConst.CONFIG_MXD_FILE);

            cfgMgr.add(ConfigConst.CONFIG_DATASOURCE, dataSource);
            cfgMgr.add(ConfigConst.CONFIG_DATABASE_MAIN, databaseMain);
            cfgMgr.add(ConfigConst.CONFIG_DATABASE_GIS, databaseGIS);
            cfgMgr.add(ConfigConst.CONFIG_DATABASE_UID, databaseUID);
            cfgMgr.add(ConfigConst.CONFIG_DATABASE_PASSWD, databasePwd);
            cfgMgr.add(ConfigConst.CONFIG_MXD_FILE, mxdFile);

            return string.Empty;
        }

        public void add(string key, string value)
        {
            ht.Add(key, value);
        }

        public string getValueByKey(string key)
        {
            return ht[key].ToString();
        }
    }
}
