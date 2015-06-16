using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Nova.Monitoring.Common
{
    [DataContract]
    public class Command : ICloneable
    {
        private string _id;
        private CommandCode _code;
        private string _source;
        private TargetType _target;
        private string _commandText;
        private string _description;

        [DataMember]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        [DataMember]
        public CommandCode Code
        {
            get { return _code; }
            set
            {
                _code = value;
            }
        }
        [DataMember]
        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }
        [DataMember]
        public TargetType Target
        {
            get { return _target; }
            set { _target = value; }
        }
        [DataMember]
        public string CommandText
        {
            get { return _commandText; }
            set
            {
                _commandText = value;
            }
        }
        [DataMember]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Command()
        {
            Guid guid = Guid.NewGuid(); ;            
            _id = guid.ToString();
        }
        private Command(Command cmd)
        {
            this.Id = cmd.Id;
            this.Code = cmd.Code;
            this.Source = cmd.Source.Clone() as string;
            this.Target = cmd.Target;
            this.CommandText = cmd.CommandText.Clone() as string;
            this.Description = cmd.Description.Clone() as string;
        }

        public object Clone()
        {
            Command command = new Command(this);
            return command;
        }
    }

    [DataContract]
    public enum CommandCode
    {
        [EnumMember]
        SetLedMonitoringConfigInfo = 1,
        [EnumMember]
        RefreshLedScreenConfigInfo,
        [EnumMember]
        SetLedAcquisitionConfigInfo,
        [EnumMember]
        SetLedAlarmConfigInfo,
        [EnumMember]
        UpdateStrategy,
        [EnumMember]
        RefreshOpticalProbeInfo,
        [EnumMember]
        SetBrightness,
        [EnumMember]
        OpenDevice,
        [EnumMember]
        CloseDevice,
        [EnumMember]
        MonitoringData = 10,
        [EnumMember]
        RefreshFunctionCardInfo,
        [EnumMember]
        RefreshMonitoringData,
        [EnumMember]
        SetSmartBrightEasyConfigInfo,
        [EnumMember]
        RefreshSmartBrightEasyConfigInfo,
        [EnumMember]
        StartSmartBrightness,
        [EnumMember]
        StopSmartBrightness,
        [EnumMember]
        SetFromCOMToSN,
        [EnumMember]
        SetSpotInspectionConfigInfo,
        [EnumMember]
        SetPeriodicInspectionConfigInfo,
        [EnumMember]
        MonitorDataToLCT = 20,
        [EnumMember]
        BrightnessConfigToLCT,
        [EnumMember]
        GetBrightnessConfig,
        [EnumMember]
        ResumeDataAcquisition,
        [EnumMember]
        StopDataAcquisition,
        [EnumMember]
        LCTGetScreenInfo,
        [EnumMember]
        LCTGetMonitorData,
        [EnumMember]
        ScreenInfoToLCT,
              
        [EnumMember]
        UpdateSystem = 40,
         [EnumMember]
        ViewMonitoringUI

    }

    public enum TargetType
    {
        ToAll,
        ToDataSource,
        ToClient,
        ToRuleEngine
    }
}
