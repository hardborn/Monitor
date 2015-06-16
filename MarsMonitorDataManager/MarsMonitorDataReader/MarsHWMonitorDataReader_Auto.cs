using Nova.LCT.GigabitSystem.Common;
using Nova.LCT.GigabitSystem.HWConfigAccessor;
using Nova.Monitoring.ColudSupport;
using Nova.Monitoring.HardwareMonitorInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Nova.Monitoring.MarsMonitorDataReader
{
    public partial class MarsHWMonitorDataReader
    {
        private class SN_ScannerPropertyReader
        {
            public string SN { get; set; }
            public ScannerPropertyReader ScannerProperty { get; set; }
            public ScanBoardPosition ScanPosition { get; set; }
            public bool IsUserRedundancy { get; set; }
            public ScanBoardPosition ScanRedundancyPosition { get; set; }
        }

        private System.Threading.Timer _autoTimer;
        private List<SN_ScannerPropertyReader> _sn_ScannerInfos = null;
        private Dictionary<string, List<PeripheralsLocation>> _sn_BrightSensors = null;
        private List<AutoReadResultData> _autoReadResultDatas = null;
        private SN_ScannerPropertyReader _current_ScannerInfo = null;
        private object _lockObj = new object();
        private AutoResetEvent _autoReadEvent = new AutoResetEvent(false);
        private int _autolocked = 1;

        private int _interval = 60000;
        public int Interval
        {
            get
            {
                return _interval / 1000;
            }
            set
            {
                _interval = value * 1000;
                _autoTimer.Change(_interval, _interval);
            }
        }

        private void ClearScannerDic()
        {
            lock (_lockObj)
            {
                _sn_ScannerInfos.Clear();
                _sn_BrightSensors.Clear();
                _autoReadResultDatas.Clear();
                Interlocked.Exchange(ref _autolocked, 1);
            }
            _autoTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }

        public List<AutoReadResultData> AutoReadResultDatas()
        {
            List<AutoReadResultData> autoData = new List<AutoReadResultData>();
            lock (_lockObj)
            {
                foreach (AutoReadResultData auto in _autoReadResultDatas)
                {
                    autoData.Add((AutoReadResultData)auto.Clone());
                }
            }
            return autoData;
        }

        private void AutoTimerCallBack(object state)
        {
            _fLogService.Debug("AutoTimer Start");
            if (Interlocked.Exchange(ref _autolocked, 0) == 1)
            {
                _autoReadEvent.Reset();
                int count = _sn_ScannerInfos.Count;
                for (int i = 0; i < count; i++)
                {
                    if (i >= _sn_ScannerInfos.Count)
                    {
                        return;
                    }
                    _sn_ScannerInfos[i].IsUserRedundancy = false;
                    _current_ScannerInfo = _sn_ScannerInfos[i];
                    _sn_ScannerInfos[i].ScannerProperty.ReadScanBd200ParasInfo(_sn_ScannerInfos[i].ScanPosition, CompletedReadScanBdPropertyCallBack);
                    _autoReadEvent.WaitOne(2000, false);
                }

                ExecuteCommandCallBack(
                    new TransferParams() { TranType = TransferType.M3_PeripheralsInfo },
                    SensorCallBack, null);
                _autoReadEvent.WaitOne(2000, false);
                Interlocked.Exchange(ref _autolocked, 1);
            }
        }

        #region 亮度相关

        private void CompletedReadScanBdPropertyCallBack(CompletedReadScanBdPropInfo info)
        {
            if (info.ReadRes == ReadConfigRes.Succeed)
            {
                ScanBoardProperty scanBdProperty = new ScanBoardProperty();
                info.ScanBdProperty.CopyTo(scanBdProperty);
                SetBright(true, scanBdProperty.Brightness);
                _autoReadEvent.Set();
            }
            else
            {
                if (_current_ScannerInfo.IsUserRedundancy)
                {
                    SetBright(false, 0);
                    _autoReadEvent.Set();
                }
                else
                {
                    _current_ScannerInfo.IsUserRedundancy = true;
                    _current_ScannerInfo.ScannerProperty.ReadScanBd200ParasInfo(_current_ScannerInfo.ScanRedundancyPosition, CompletedReadScanBdPropertyCallBack);
                }
            }
        }

        private void SetBright(bool isSucess, int value)
        {
            lock (_lockObj)
            {
                AutoReadResultData auto = _autoReadResultDatas.Find(a => a.SN == _current_ScannerInfo.SN);
                if (auto != null)
                {
                    auto.AutoBrightInfo.IsSucess = isSucess;
                    auto.AutoBrightInfo.BrightValue = value;
                }
                else
                {
                    _autoReadResultDatas.Add(new AutoReadResultData()
                    {
                        SN = _current_ScannerInfo.SN,
                        AutoBrightInfo = new AutoGetBrightInfo() { IsSucess = isSucess, BrightValue = value }
                    });
                }
            }
        }

        private void SetAutoReader(string sn, string comName, ILEDDisplayInfo scrInfo, List<SenderRedundancyInfo> reduInfoList)
        {
            if (string.IsNullOrEmpty(comName) || string.IsNullOrEmpty(sn) || scrInfo == null)
            {
                return;
            }
            ScannerPropertyReader scan = new ScannerPropertyReader(_serverProxy, comName);
            ScanBoardRegionInfo scanBoard = null;
            scrInfo.GetFirstScanBoardInList(out scanBoard);
            if (scanBoard != null)
            {
                ScanBoardPosition pos = new ScanBoardPosition();
                pos.SenderIndex = scanBoard.SenderIndex;
                pos.PortIndex = scanBoard.PortIndex;
                pos.ScanBdIndex = scanBoard.ConnectIndex;
                ScanBoardPosition posSlave = new ScanBoardPosition();
                if (reduInfoList != null)
                {
                    for (int j = 0; j < reduInfoList.Count; j++)
                    {
                        if (reduInfoList[j].MasterSenderIndex == scanBoard.SenderIndex
                            && reduInfoList[j].MasterPortIndex == scanBoard.PortIndex)
                        {
                            posSlave.SenderIndex = reduInfoList[j].SlaveSenderIndex;
                            posSlave.PortIndex = reduInfoList[j].SlavePortIndex;
                            posSlave.ScanBdIndex = scanBoard.ConnectIndex;
                        }
                    }
                }
                lock (_lockObj)
                {
                    _sn_ScannerInfos.Add(new SN_ScannerPropertyReader()
                    {
                        SN = sn,
                        ScannerProperty = scan,
                        ScanPosition = pos,
                        ScanRedundancyPosition = posSlave
                    });
                }
            }
            if (_sn_ScannerInfos != null && _sn_ScannerInfos.Count == 1)
            {
                _autoTimer.Change(_interval, _interval);
            }
        }
        #endregion

        #region 光探头读取

        private void SetSensorData(string sn, DisplaySmartBrightEasyConfig dispaySoftWareConfig, bool isAdd)
        {
            if (string.IsNullOrEmpty(sn) || (isAdd && dispaySoftWareConfig == null))
            {
                return;
            }
            if (isAdd)
            {
                if (dispaySoftWareConfig.AutoBrightSetting != null && dispaySoftWareConfig.AutoBrightSetting.UseLightSensorList != null)
                {
                    if (_sn_BrightSensors.ContainsKey(sn))
                    {
                        _sn_BrightSensors.Remove(sn);
                    }
                    _sn_BrightSensors.Add(sn, new List<PeripheralsLocation>());
                    foreach (PeripheralsLocation per in dispaySoftWareConfig.AutoBrightSetting.UseLightSensorList)
                    {
                        _sn_BrightSensors[sn].Add((PeripheralsLocation)per.Clone());
                    }
                }
            }
            else
            {
                if (_sn_BrightSensors.ContainsKey(sn))
                {
                    _sn_BrightSensors.Remove(sn);
                }
            }
        }
        private void SensorCallBack(TransferParams param, object userToken)
        {
            Action action = new Action(() =>
            {
                try
                {
                    ExecSensorData(CommandTextParser.GetDeJsonSerialization<PeripheralsLocateInfo>(param.Content));
                }
                catch (Exception ex)
                {
                    _fLogService.Error("ExistCatch：Auto Sensor CallBack Error:" + ex.ToString());
                }
                finally
                {
                    _autoReadEvent.Set();
                }
            });
            action.BeginInvoke(null, null);
        }

        private void ExecSensorData(PeripheralsLocateInfo perInfos)
        {
            lock (_lockObj)
            {
                foreach (KeyValuePair<string, List<PeripheralsLocation>> keyValue in _sn_BrightSensors)
                {
                    if (perInfos.UseablePeripheralList == null || perInfos.UseablePeripheralList.Count == 0)
                    {
                        AutoReadResultData auto = _autoReadResultDatas.Find(a => a.SN == keyValue.Key);
                        if (auto != null)
                        {
                            auto.AutoSensorInfo.IsSucess = false;
                            auto.AutoSensorInfo.SensorValue = 0;
                        }
                        else
                        {
                            _autoReadResultDatas.Add(new AutoReadResultData()
                            {
                                SN = keyValue.Key,
                                AutoSensorInfo = new AutoGetSensorInfo() { IsSucess = false, SensorValue = 0 }
                            });
                        }
                        continue;
                    }
                    int value = 0;
                    int count = 0;
                    foreach (PeripheralsLocation per in keyValue.Value)
                    {
                        var resultItem = perInfos.UseablePeripheralList.FirstOrDefault(o => o.Equals(per));
                        if (resultItem != null)
                        {
                            value += (int)resultItem.SensorValue;
                            count++;
                        }
                    }
                    if (count > 0)
                    {
                        AutoReadResultData auto = _autoReadResultDatas.Find(a => a.SN == keyValue.Key);
                        if (auto != null)
                        {
                            auto.AutoSensorInfo.IsSucess = true;
                            auto.AutoSensorInfo.SensorValue = (int)(value / count);
                        }
                        else
                        {
                            _autoReadResultDatas.Add(new AutoReadResultData()
                            {
                                SN = keyValue.Key,
                                AutoSensorInfo = new AutoGetSensorInfo() { IsSucess = true, SensorValue = (int)(value / count) }
                            });
                        }
                    }
                }
            }
        }
        #endregion
    }
}