using Nova.LCT.GigabitSystem.Common;
using Nova.LCT.GigabitSystem.HWPointDetect;
using Nova.Monitoring.ColudSupport;
using Nova.Monitoring.Common;
using Quartz;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace Nova.Monitoring.MarsDataAcquisition
{
    public partial class DataAcquisition
    {
        private Dictionary<string, IJobDetail> _spotInspectionJobTable = new Dictionary<string, IJobDetail>();
        private Dictionary<string, ParameterInspectionCycleConfig> _spotInspectionConfigTable = new Dictionary<string, ParameterInspectionCycleConfig>();
        private Dictionary<string, SpotInspectionResult> _spotInspectionResultTable = new Dictionary<string, SpotInspectionResult>();
        private IScheduler _spotInspectionScheduler;
        private static object _syncObj = new object();

        private void ProcessSpotInspection(object state)
        {
            string ledSn = state as string;
            var screenInfo = _screenInfos.Find(l => l.LedSN == ledSn);
            if (_moniDatareader == null)
            {
                return;
            }

            _moniDatareader.DetectPoint(screenInfo.Commport, screenInfo.LedInfo as ILEDDisplayInfo, _detectConfigParamList[ledSn], ledSn);
            _moniDatareader.DetectPointCompletedEvent -= DetectPointCompleted;
            _moniDatareader.DetectPointCompletedEvent += DetectPointCompleted;
        }
        private void DetectPointCompleted(SpotInspectionResult res, object userToken)
        {
            res.Id = userToken as string;            
            SendData("SpotInspectionResult", CommandTextParser.GetJsonSerialization<SpotInspectionResult>(res));
        }

    }

    public class SpotInspectionJob:IJob
    {
        private DataAcquisition _dataAcquisition;
        private string _sn;
        private static object m_obj = new object();

        public void Execute(IJobExecutionContext context)
        {
            lock (m_obj)
            {
                JobDataMap data = context.JobDetail.JobDataMap;
                string sn = data.GetString("SpotInspectionJob_SN");
                _dataAcquisition = data.Get("Sender") as DataAcquisition;
                _sn = sn;

                var screenInfo = _dataAcquisition.ScreenInfos.Find(l => l.LedSN == sn);
                if (_dataAcquisition.MonitorDataReader == null || !_dataAcquisition.DetectConfigParamList.Keys.Contains(sn))
                {
                    return;
                }
                _dataAcquisition.MonitorDataReader.DetectPointCompletedEvent -= DetectPointCompleted;
                _dataAcquisition.MonitorDataReader.DetectPointCompletedEvent += DetectPointCompleted;
                _dataAcquisition.MonitorDataReader.DetectPoint(screenInfo.Commport, screenInfo.LedInfo as ILEDDisplayInfo, _dataAcquisition.DetectConfigParamList[sn], sn);
            }           
        }

        private void DetectPointCompleted(SpotInspectionResult res, object userToken)
        {
            res.Id = userToken as string;
            if (_sn == res.Id)
            {
                _dataAcquisition.MonitorDataReader.DetectPointCompletedEvent -= DetectPointCompleted;
                _dataAcquisition.SendData("SpotInspectionResult", CommandTextParser.GetJsonSerialization<SpotInspectionResult>(res));                
            }           
        }
    }
}
