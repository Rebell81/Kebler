﻿using System.ComponentModel;
using Newtonsoft.Json;

namespace Kebler.Transmission.Models
{
    public class TransmissionTorrentPeersFrom
    {
        [JsonProperty("fromDht")]
        public int FromDHT { get; set; }

        [JsonProperty("fromIncoming")]
        public int FromIncoming { get; set; }

        [JsonProperty("fromLpd")]
        public int FromLPD { get; set; }

        [JsonProperty("fromLtep")]
        public int FromLTEP { get; set; }

        [JsonProperty("fromPex")]
        public int FromPEX { get; set; }

        [JsonProperty("fromTracker")]
        public int FromTracker { get; set; }


        //~TransmissionTorrentPeersFrom()
        //{
        //    PropertyChanged = null;
        //}
    }
}