using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Nova.LCT.GigabitSystem.Common;

namespace Nova.Monitoring.UI.SenderStatusDisplay
{
    public class SenderInfo
    {
        public string CommPort = "";
        public byte SenderIndex = 0;
        public Rectangle SenderRect = Rectangle.Empty;
        public WorkStatusType Status = WorkStatusType.Unknown;
        public ValueInfo RefreshRateInfo = new ValueInfo();
        public Dictionary<int, RedundantStateType> RedundantStateTypeList = new Dictionary<int, RedundantStateType>();
        /// <summary>
        /// 格子风格的clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            SenderInfo newObj = new SenderInfo();
            CopyTo(newObj);
            return newObj;
        }
        /// <summary>
        /// 格子风格拷贝
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CopyTo(object obj)
        {
            SenderInfo gridInfo = (SenderInfo)obj;
            if (gridInfo == null)
            {
                return false;
            }
            gridInfo.CommPort = CommPort;
            gridInfo.SenderIndex = SenderIndex;
            gridInfo.SenderRect = SenderRect;
            gridInfo.Status = Status;

            gridInfo.RefreshRateInfo = new ValueInfo();
            gridInfo.RefreshRateInfo.IsValid = RefreshRateInfo.IsValid;
            gridInfo.RefreshRateInfo.Value = RefreshRateInfo.Value;
            gridInfo.RedundantStateTypeList = new Dictionary<int, RedundantStateType>();
            foreach (int index in RedundantStateTypeList.Keys)
            {
                gridInfo.RedundantStateTypeList.Add(index, RedundantStateTypeList[index]);
            }
            return true;
        }
    }

    public class MouseOperateInGridEventArgs : EventArgs
    {
        public SenderInfo SenderGridInfo
        {
            get { return _senderGridInfo; }
        }
        public MouseEventArgs BaseArgs
        {
            get{ return _baseArgs; }
        }

        private SenderInfo _senderGridInfo = null;
        private MouseEventArgs _baseArgs = null;
        public MouseOperateInGridEventArgs(MouseEventArgs baseArgs, SenderInfo senderGridInfo)
        {
            _baseArgs = baseArgs;
            if (senderGridInfo == null)
            {
                _senderGridInfo = null;
            }
            else
            {
                _senderGridInfo = (SenderInfo)senderGridInfo.Clone();
            }
        }
    }
    public delegate void MouseOperateInGridEventHandler(object sender, MouseOperateInGridEventArgs e);
}
