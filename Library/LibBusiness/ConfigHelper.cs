using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace LibBusiness
{
    public class ConfigHelper
    {
        private static YamlStream _yaml;

        public static List<CoalSeam> coal_seams { get; set; }

        public static CoalSeam current_seam { get; set; }

        public static string server_ip { get; set; }

        public static string db_instance { get; set; }

        public static string uid { get; set; }

        public static string password { get; set; }

        public static string update_url { get; set; }

        public static string update_file { get; set; }

        public ConfigHelper()
        {
            load();
        }

        public static void load()
        {
            coal_seams = new List<CoalSeam>();
            if (_yaml == null)
                _yaml = YamlHelper.ReadFromFile("config.yaml");


            server_ip = get_attribute("server_ip");
            db_instance = get_attribute("db_instance");
            uid = get_attribute("uid");
            password = get_attribute("password");
            update_url = get_attribute("update_url");
            update_file = get_attribute("update_file");

            var mapping = (YamlMappingNode)_yaml.Documents[0].RootNode;
            var items = (YamlSequenceNode)mapping.Children[new YamlScalarNode("coal_seams")];
            foreach (var item in items.Cast<YamlMappingNode>())
            {
                var coalSeam = new CoalSeam
                {
                    name = item.Children[new YamlScalarNode("name")].ToString(),
                    db_name = item.Children[new YamlScalarNode("db_name")].ToString(),
                    gis_name = item.Children[new YamlScalarNode("gis_name")].ToString(),
                    mxd_name = item.Children[new YamlScalarNode("mxd_name")].ToString(),
                    port = item.Children[new YamlScalarNode("port")].ToString(),
                    rest_port = item.Children[new YamlScalarNode("rest_port")].ToString()
                };
                coal_seams.Add(coalSeam);
            }
        }

        public static string get_attribute(string attr)
        {
            var mapping =
                (YamlMappingNode)_yaml.Documents[0].RootNode;
            return mapping.Children[new YamlScalarNode(attr)].ToString();
        }
    }

}

