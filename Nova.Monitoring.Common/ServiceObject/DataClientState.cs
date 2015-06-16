using System;
using System.Collections.Generic;
using System.Linq;

namespace Nova.Monitoring.Common
{
    public enum DataClientState
    {
        /// <summary>
        /// 通讯管道已经关闭，无法发送和接收数据。
        /// </summary>
        Closed,
        /// <summary>
        /// 通讯管道正在关闭。
        /// </summary>
        Closing,
        /// <summary>
        /// 该通讯管道已经连接，等待打开。
        /// </summary>
        Created,
        /// <summary>
        /// 通讯通道出错状态。
        /// </summary>
        Faulted,
        /// <summary>
        /// 初始化了客户端，该通讯管道还未连接。
        /// </summary>
        Initialize,
        /// <summary>
        /// 通讯管道已经打开，可以发送和接收数据。
        /// </summary>
        Opened,
        /// <summary>
        /// 通讯管道正在打开。
        /// </summary>
        Opening,
        /// <summary>
        /// 未知状态.
        /// </summary>
        Unknown
    }
}
