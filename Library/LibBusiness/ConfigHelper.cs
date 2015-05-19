using YamlDotNet.RepresentationModel;

namespace LibBusiness
{
    public class ConfigHelper
    {
        private static YamlStream _yaml;

        public static dynamic config { get; set; }

        public ConfigHelper()
        {
            load();
            config.mxd_path = get_attribute("mxd_path");
            config.gdb_path = get_attribute("gdb_path");
            config.coal_name = get_attribute("coal_name");
            config.lithology = get_attribute("lithology");
        }

        public static void load()
        {
            if (_yaml == null)
                _yaml = YamlHelper.ReadFromFile("config.yaml");

        }

        public static string get_attribute(string attr)
        {
            load();
            var mapping =
                (YamlMappingNode)_yaml.Documents[0].RootNode;
            return mapping.Children[new YamlScalarNode(attr)].ToString();
        }
    }

}

