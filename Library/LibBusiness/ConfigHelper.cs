using YamlDotNet.RepresentationModel;

namespace LibBusiness
{
    public class ConfigHelper
    {
        private static YamlStream _yaml;

        public ConfigHelper()
        {
            Load();
        }

        public static void Load()
        {
            if (_yaml == null)
                _yaml = YamlHelper.ReadFromFile("config.yaml");
        }

        public static string GetAttribute(string attr)
        {
            Load();
            var mapping =
                (YamlMappingNode)_yaml.Documents[0].RootNode;
            return mapping.Children[new YamlScalarNode(attr)].ToString();
        }
    }

}

