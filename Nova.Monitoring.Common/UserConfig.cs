using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public class UserConfig : ICloneable
    {

        public TemperatureType TemperatureUnit { get; set; }


        public UserConfig()
        {

        }

        private UserConfig(UserConfig config)
        {
            this.TemperatureUnit = config.TemperatureUnit;
        }

        public object Clone()
        {
            var config = new UserConfig(this);
            return config;
        }
    }

    [Serializable]
    /// <summary>
    /// 温度显示类型
    /// </summary>
    public enum TemperatureType
    {
        /// <summary>
        /// 摄氏
        /// </summary>
        Celsius = 0,
        /// <summary>
        /// 华氏
        /// </summary>
        Fahrenheit
    }
}
