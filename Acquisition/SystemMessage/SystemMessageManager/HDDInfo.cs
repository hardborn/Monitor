using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.SystemMessageManager
{
    /// <summary>
    /// disk
    /// </summary>
    public class HDDInfo
    {
        public HDDInfo(string DiskName, long Size, long FreeSpace)
        {
            this.DiskName = DiskName;
            this.TotalSize = Size;
            this.FreeSpace = FreeSpace;
        }

        private String _diskName;
        public String DiskName
        {
            get { return _diskName; }
            set { _diskName = value; }
        }

        private long _totalSize;
        public long TotalSize
        {
            get { return _totalSize; }
            set { _totalSize = value; }
        }

        private long _freeSpace;
        public long FreeSpace
        {
            get { return _freeSpace; }
            set { _freeSpace = value; }
        }
    }
}
