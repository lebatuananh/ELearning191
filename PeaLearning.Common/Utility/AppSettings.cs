using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace PeaLearning.Common.Utility
{
    public class AppSettings
    {
        private static AppSettings _instance;
        private static readonly object ObjLocked = new object();

        protected AppSettings()
        {
        }

        public IConfiguration Configuration { get; private set; }

        public static AppSettings Instance
        {
            get
            {
                if (null == _instance)
                    lock (ObjLocked)
                    {
                        if (null == _instance)
                            _instance = new AppSettings();
                    }

                return _instance;
            }
        }

        public void SetConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public bool GetBool(string key, bool defaultValue = false)
        {
            try
            {
                return Configuration.GetSection("StringValue").GetChildren().FirstOrDefault(x => x.Key == key).Value
                    .ToBool();
            }
            catch
            {
                return defaultValue;
            }
        }

        public string GetConnection(string key, string defaultValue = "")
        {
            try
            {
                return Configuration.GetConnectionString(key);
            }
            catch
            {
                return defaultValue;
            }
        }

        public int GetInt32(string key, int defaultValue = 0)
        {
            try
            {
                return Configuration.GetSection("StringValue").GetChildren().FirstOrDefault(x => x.Key == key).Value
                    .ToInt();
                //return (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[key]) ? ConfigurationManager.AppSettings[key].ToInt() : defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        public long GetInt64(string key, long defaultValue = 0L)
        {
            try
            {
                return !string.IsNullOrEmpty(ConfigurationManager.AppSettings[key])
                    ? ConfigurationManager.AppSettings[key].ToLong()
                    : defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public string GetString(string key, string defaultValue = "")
        {
            try
            {
                var value = Configuration.GetSection("StringValue").GetChildren().FirstOrDefault(x => x.Key == key)
                    ?.Value;
                return string.IsNullOrEmpty(value) ? defaultValue : value;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static T Get<T>(string key = null)
        {
            if (string.IsNullOrWhiteSpace(key)) return Instance.Configuration.Get<T>();

            var section = Instance.Configuration.GetSection(key);
            return section.Get<T>();
        }

        public static T Get<T>(string key, T defaultValue)
        {
            if (Instance.Configuration.GetSection(key) == null)
                return defaultValue;

            if (string.IsNullOrWhiteSpace(key))
                return Instance.Configuration.Get<T>();
            var value = Instance.Configuration.GetSection(key).Get<T>();
            return (value == null || EqualityComparer<T>.Default.Equals(value, default)) ? defaultValue : value;
        }
    }
}
