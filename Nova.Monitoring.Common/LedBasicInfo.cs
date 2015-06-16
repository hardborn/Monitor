using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Nova.Monitoring.Common
{
    [Serializable]
    public class LedBasicInfo : ICloneable
    {
        private string _sn;
        private string _aliaName;
        private int _width;
        private int _height;
        private bool _bSupportPointDetect;
        private int _pointCount;
        private int _ledIndexOfCom;
        private string _commport;
        private List<PartInfo> _partInfos;

        [XmlAttribute()]
        public string Sn
        {
            get { return _sn; }
            set
            {
                _sn = value;
            }
        }
        [XmlAttribute()]
        public string AliaName
        {
            get { return _aliaName; }
            set
            {
                _aliaName = value;
            }
        }
        [XmlAttribute()]
        public int Width
        {
            get { return _width; }
            set
            {
                _width = value;
            }
        }

        [XmlAttribute()]
        public int Height
        {
            get { return _height; }
            set
            {
                _height = value;
            }
        }

        [XmlAttribute()]
        public bool IsSupportPointDetect
        {
            get { return _bSupportPointDetect; }
            set { _bSupportPointDetect = value; }
        }

        [XmlAttribute()]
        public int PointCount
        {
            get { return _pointCount; }
            set { _pointCount = value; }
        }

        [XmlAttribute()]
        public int LedIndexOfCom
        {
            get { return _ledIndexOfCom; }
            set { _ledIndexOfCom = value; }
        }
        [XmlAttribute()]
        public string Commport
        {
            get { return _commport; }
            set { _commport = value; }
        }

        [XmlArray("PartInfos")]
        public List<PartInfo> PartInfos
        {
            get { return _partInfos; }
            set
            {
                _partInfos = value;
            }
        }

        public LedBasicInfo() { }
        private LedBasicInfo(LedBasicInfo info)
        {
            _sn = info.Sn.Clone() as string;
            _width = info.Width;
            _height = info.Height;
            _partInfos = info.PartInfos.Select(item => (PartInfo)item.Clone()).ToList();
        }

        public object Clone()
        {
            LedBasicInfo basicInfo = new LedBasicInfo(this);
            return basicInfo;
        }
    }

    [Serializable]
    public class PartInfo : ICloneable
    {
        private int _amount;
        private DeviceType _type;

        [XmlAttribute()]
        public DeviceType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        [XmlAttribute()]
        public int Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
            }
        }
        public PartInfo() { }
        private PartInfo(PartInfo info)
        {
            _type = info.Type;
            _amount = info.Amount;
        }

        public object Clone()
        {
            PartInfo partinfo = new PartInfo(this);
            return partinfo;
        }

    }
}
