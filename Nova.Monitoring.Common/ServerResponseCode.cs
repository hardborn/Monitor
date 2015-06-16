using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    /// <summary>
    /// （NovaCare）服务器响应码
    /// </summary>
    public enum ServerResponseCode
    {
        ProtocolError = 0,
        AccountNotExist = 1,
        ScreenAlreadyExists = 2,
        ScreenRegisteredSuccessfully = 3,
        ScreenRegistrationFailed = 4,
        ScreenNotExist = 5,
        HeartbeatSuccessfully = 6,
        ScreenBasicInfoUpdateSuccessfully = 7,
        ScreenBasicInfoUpdateFailed = 8,
        MacEmpty = 9,
        SnEmpty = 10,
        ScreenReregister = 11,
        ExceptionResult = 99
    }
}
