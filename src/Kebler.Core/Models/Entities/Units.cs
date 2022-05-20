﻿using System.ComponentModel;
using Newtonsoft.Json;

namespace Kebler.Core.Models.Entities
{
    public class Units : INotifyPropertyChanged
    {
        [JsonProperty("speed-units")]
        public string[] SpeedUnits { get; set; }

        [JsonProperty("speed-bytes")]
        public int SpeedBytes { get; set; }

        [JsonProperty("size-units")]
        public string[] SizeUnits { get; set; }

        [JsonProperty("size-bytes")]
        public int SizeBytes { get; set; }

        [JsonProperty("memory-units")]
        public string[] MemoryUnits { get; set; }

        [JsonProperty("memory-bytes")]
        public int MemoryBytes { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}