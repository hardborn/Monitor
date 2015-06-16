using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public class SpotInspectionResult
    {
        /// <summary>
        /// 唯一标识点检屏体信息（Mac+Sn）
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 点检完成时间戳
        /// </summary>
        public string Timestamp { get; set; }
        /// <summary>
        /// 点检结果(True:成功 False:失败)
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// 是否支持虚拟红
        /// </summary>
        public bool IsSupportVirtualRed { get; set; }
        /// <summary>
        /// 屏体总灯点数
        /// </summary>
        public int TotalPoint { get; set; }
        /// <summary>
        /// 屏体总灯板单元数
        /// </summary>
        public int UnitCount { get; set; }
        /// <summary>
        /// 有坏灯点的箱体列表
        /// </summary>
        public List<SpotInspectionBox> SpotInspectionBoxList { get; set; }

        public SpotInspectionResult()
        {
            SpotInspectionBoxList = new List<SpotInspectionBox>();
        }
    }

    public class SpotInspectionBox
    {
        /// <summary>
        /// 箱体位置信息
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 箱体总灯点数
        /// </summary>
        public int BoxTotalPoint { get; set; }
        /// <summary>
        /// 有坏灯点的灯板单元列表
        /// </summary>
        public List<SpotInspectionUnit> SpotInspectionUnitList { get; set; }

        public SpotInspectionBox()
        {
            SpotInspectionUnitList = new List<SpotInspectionUnit>();
        }
    }

    public class SpotInspectionUnit
    {
        /// <summary>
        /// 灯板总的灯点数
        /// </summary>
        public int UnitTotalPoint { get; set; }
        /// <summary>
        /// 灯板总的坏灯点数
        /// </summary>
        public int AllErrorPointNumner { get; set; }
        /// <summary>
        /// 红灯的坏灯点数
        /// </summary>
        public int RedPointErrorNumber { get; set; }
        /// <summary>
        /// 蓝灯的坏灯点数
        /// </summary>
        public int BluePointErrorNumber { get; set; }
        /// <summary>
        /// 绿灯的坏灯点数
        /// </summary>
        public int GreenPointErrorNumber { get; set; }
        /// <summary>
        /// 虚拟红灯的坏灯点数
        /// </summary>
        public int VirtualRedPointErrorNumber { get; set; }
    }
}
