using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.RepresentationModel;

namespace LibBusiness
{
    public class YamlHelper
    {
        public static YamlStream ReadFromFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            string content = sr.ReadToEnd();
            var input = new StringReader(content);
            var yaml = new YamlStream();
            yaml.Load(input);
            return yaml;
        }
    }
}
