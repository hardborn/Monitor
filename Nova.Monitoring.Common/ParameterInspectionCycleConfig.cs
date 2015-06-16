using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
   public class ParameterInspectionCycleConfig
    {
       /// <summary>
       /// PeriodType
       /// {0-每天,1-每周,2-每月，3-不启用}
       /// </summary>
        public PeriodType Cycle { get; set; } 
       /// <summary>
       /// {Cycle：0 - 无效}
       /// {Cycle：1 - 周几}
       /// {Cycle：2 - 每月多少号}
       /// {Cycle：3 - 无效}
       /// </summary>
        public int Sign { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }

   public enum PeriodType
   {
       Daily,
       Weekly,
       Monthly,
       Disable
   }
}
