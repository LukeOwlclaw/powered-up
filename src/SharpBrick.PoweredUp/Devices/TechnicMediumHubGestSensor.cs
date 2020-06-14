// 100

using System;
using System.Collections.Generic;
using System.Linq;
using SharpBrick.PoweredUp.Protocol;
using SharpBrick.PoweredUp.Utils;

namespace SharpBrick.PoweredUp
{
    public class TechnicMediumHubGestSensor : Device, IPoweredUpDevice
    {
        public TechnicMediumHubGestSensor()
        { }

        public TechnicMediumHubGestSensor(IPoweredUpProtocol protocol, byte hubId, byte portId)
            : base(protocol, hubId, portId)
        { }

        public IEnumerable<byte[]> GetStaticPortInfoMessages(Version softwareVersion, Version hardwareVersion)
            => @"
0B-00-43-64-01-02-01-01-00-00-00
05-00-43-64-02
11-00-44-64-00-00-47-45-53-54-00-00-00-00-00-00-00
0E-00-44-64-00-01-00-00-00-00-00-00-80-40
0E-00-44-64-00-02-00-00-00-00-00-00-C8-42
0E-00-44-64-00-03-00-00-00-00-00-00-80-40
0A-00-44-64-00-04-00-00-00-00
08-00-44-64-00-05-44-00
0A-00-44-64-00-80-01-00-01-00
".Trim().Split("\n").Select(s => BytesStringUtil.StringToData(s));
    }
}