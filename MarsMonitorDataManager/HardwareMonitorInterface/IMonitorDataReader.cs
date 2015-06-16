using Nova.LCT.GigabitSystem.Common;
using Nova.LCT.GigabitSystem.HWPointDetect;
using Nova.LCT.Message.Client;
using Nova.Monitoring.ColudSupport;
using Nova.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Monitoring.HardwareMonitorInterface
{
    public interface IMonitorDataReader
    {
        #region 属性
        /// <summary>
        /// 系统类型
        /// </summary>
        HWSystemType SysType
        {
            get;
        }
        #endregion

        #region 接口
        /// <summary>
        /// 初始化系统
        /// </summary>
        /// <returns></returns>
        InitialErryType Initialize();
        /// <summary>
        /// 开始读取监控数据
        /// </summary>
        /// <param name="callBack"></param>
        /// <param name="userToken"></param>
        /// <returns></returns>
        ReadMonitorDataErrType BeginReadData(CompletedMonitorCallback callBack, object userToken);
        /// <summary>
        /// 开始读取指定的发送卡数据
        /// </summary>
        /// <param name="senderInfos"></param>
        /// <param name="callBack"></param>
        /// <param name="userToken"></param>
        /// <returns></returns>
        ReadMonitorDataErrType BeginRetryReadSenderData(SerializableDictionary<string, List<byte>> senderInfos, CompletedMonitorCallback callBack, object userToken);
        /// <summary>
        /// 开始读取指定的接收卡数据
        /// </summary>
        /// <param name="scanBordInfos"></param>
        /// <param name="callBack"></param>
        /// <param name="userToken"></param>
        /// <returns></returns>
        ReadMonitorDataErrType BeginRetryReadScannerData(Dictionary<string, List<ScanBoardRegionInfo>> scanBordInfos, CompletedMonitorCallback callBack, object userToken);
        /// <summary>
        /// 释放监控对象所占用的内存
        /// </summary>
        void Unitialize();
        /// <summary>
        /// 更新配置文件
        /// </summary>
        /// <param name="commandText"></param>
        void UpdateConfigMessage(string commandText);
        /// <summary>
        /// 带回调的硬件调用方法
        /// </summary>
        /// <param name="param">硬件的类型及参数</param>
        /// <param name="callback">回调</param>
        /// <param name="userToken"></param>
        void ExecuteCommandCallBack(TransferParams param, TransFerParamsDataHandler callback, object userToken);

        HWSettingResult SetScreenBright(string screenUDID, byte brightness);

        List<ScreenInfo> GetAllScreenInfo();
        void DetectPoint(string comName, ILEDDisplayInfo ledDisplayInfo, DetectConfigParams detectParams, object usertoken);
        Dictionary<string, List<ILEDDisplayInfo>> GetAllCommPortLedDisplay();
        event DetectPointCompletedEventHandler DetectPointCompletedEvent;
        List<AutoReadResultData> AutoReadResultDatas();
        #endregion

        #region 事件
        event NotifyUpdateCfgFileResEventHandler NotifyUpdateCfgFileResEvent;

        event EventHandler NotifyScreenCfgChangedEvent;

        event NotifySettingCompletedEventHandler NotifySettingCompletedEvent;

        event NotifyRegisterErrEventHandler NotifyRegisterErrEvent;
        #endregion
    }
}
