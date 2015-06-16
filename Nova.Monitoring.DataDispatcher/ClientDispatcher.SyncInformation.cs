using Log4NetLibrary;
using Nova.Monitoring.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.DataDispatcher
{
    public partial class ClientDispatcher
    {
        // private Dictionary<string, SyncInformation> _syncInfoTable = new Dictionary<string, SyncInformation>();
        private SyncInformationManager _syncInformationManager;

        public void UpdateSyncData(string sn, SyncData syncData)
        {
            _logService.Debug(string.Format("<-{0}->:ThreadID={1}", "UpdateSyncData", System.Threading.Thread.GetDomainID().ToString()));
            _syncInformationManager.UpdateSyncData(GetScreenId(AppDataConfig.CurrentMAC, sn), syncData);
        }
    }

    public class SyncInformationManager
    {
        private Dictionary<string, SyncInformation> _syncInfoTable = new Dictionary<string, SyncInformation>();
        private ILogService _logService;

        public SyncInformationManager()
        {
            _logService = new FileLogService(typeof(ClientDispatcher));
        }

        public bool ContainsScreen(string id)
        {
            return _syncInfoTable.ContainsKey(id) ? true : false;
        }

        public void AddSyncData(string id, SyncData syncData)
        {
            if (ContainsScreen(id))
            {
                var data = _syncInfoTable[id].SyncDatas.Find(s => s.SyncType == syncData.SyncType);
                if (data == null)
                {
                    _syncInfoTable[id].SyncDatas.Add(syncData);
                }
                else
                {
                    _logService.Debug(string.Format("<-{0}->:ThreadID={1}", "AddSyncData", System.Threading.Thread.GetDomainID().ToString()));
                    UpdateSyncData(id, syncData);
                }
            }
            else
            {
                var syncInformation = new SyncInformation();
                syncInformation.Sn = id;
                syncInformation.SyncDatas = new List<SyncData>() { syncData };
                _syncInfoTable.Add(id, syncInformation);
            }
        }

        public void RemoveSyncData(string id, SyncData syncData)
        {
            if (ContainsScreen(id))
            {
                _syncInfoTable[id].SyncDatas.Remove(syncData);
            }
        }

        public bool UpdateSyncData(string id, SyncData syncData)
        {
            _logService.Debug(string.Format("<-{0}->:ThreadID={1}", "UpdateSyncData", System.Threading.Thread.GetDomainID().ToString()));
            if (ContainsScreen(id))
            {
                SyncData configSyncData = null;
                try
                {
                    configSyncData = _syncInfoTable[id].SyncDatas.FirstOrDefault(d => d.SyncType == syncData.SyncType);
                }
                catch (Exception ex)
                {
                    _logService.Error(string.Format("ExistCatch:<-{0}->:{1},\nThreadID={2}", "UpdateSyncData", "_syncInfoTable[id].SyncDatas.FirstOrDefault error,info:" + ex.ToString(), System.Threading.Thread.GetDomainID().ToString()));
                    return false;
                }
                if (configSyncData == null)
                {
                    _logService.Error(string.Format("<-{0}->:{1}", "UpdateSyncData", "无指定的更新项."));
                    return false;
                }

                _syncInfoTable[id].SyncDatas.Remove(configSyncData);
                _syncInfoTable[id].SyncDatas.Add(syncData);
                return true;
            }
            else
            {
                _logService.Error(string.Format("<-{0}->:{1}", "UpdateSyncData", "无指定的更新项."));
                return false;
            }
        }

        public SyncInformation GetSyncInformation(string id)
        {
            return _syncInfoTable[id];
        }

        public SyncData GetSyncData(string id, SyncType syncType)
        {
            if (ContainsScreen(id))
            {
                return _syncInfoTable[id].SyncDatas.FirstOrDefault(s => s.SyncType == syncType);
            }
            else
            {
                return null;
            }
        }

        public void InitializeSyncDataBy(string id)
        {
            foreach (int type in Enum.GetValues(typeof(SyncType)))
            {
                var summary = new SyncSummary();
                summary.SyncMark = "-1";
                summary.Type = (SyncType)Enum.Parse(typeof(SyncType), type.ToString());
                var syncData = new SyncData()
                {
                    SyncType = (SyncType)Enum.Parse(typeof(SyncType), type.ToString()),
                    SyncParam = SyncFlag.Synchronized,
                    SyncContent = string.Empty,
                    Datestamp = "-1"
                };
                AddSyncData(id, syncData);
            }
        }

        public void SynchronizeWithServer()
        {

        }
    }
}
