using IniParser;
using IniParser.Model;

namespace Agar.io_sfml.Engine.Utils
{
    public class ConfigLoader
    {
        private string _configPath;

        /// <summary>
        /// Initializes a new instance of the ConfigLoader class
        /// </summary>
        public ConfigLoader(string configPath)
        {
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
            _configPath = Path.Combine(projectRoot, configPath);
        }

        /// <summary>
        /// Loads configuration data from an INI file
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> Load()
        {
            var parser = new FileIniDataParser();
            IniData iniData = parser.ReadFile(_configPath);

            var configData = new Dictionary<string, Dictionary<string, string>>();

            foreach (var section in iniData.Sections)
            {
                var sectionData = new Dictionary<string, string>();
                foreach (var key in section.Keys)
                {
                    sectionData[key.KeyName] = key.Value;
                }
                configData[section.SectionName] = sectionData;
            }

            return configData;
        }
    }
}