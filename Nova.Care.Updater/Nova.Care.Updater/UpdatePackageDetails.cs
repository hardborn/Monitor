using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Care.Updater
{
    internal class UpdatePackageDetails
    {
        public TargetType Type { get; set; }
        public string UpdateSource { get; set; }
        public string UpdateTarget { get; set; }
    }

    internal enum TargetType
    {
        Folder,
        File
    }
}
