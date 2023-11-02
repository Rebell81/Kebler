﻿using System.Globalization;
using System.Windows;

namespace Kebler.Domain.Models
{
    public class DefaultSettings
    {
        public const double MoreInfoColumnMaxHeight = 282D;
        public int SortType { get; set; } = 0;
        //public string SortVal { get; set; } = nameof(TorrentInfo.UploadedEver);
        public CultureInfo Language { get; set; } = new("en");
        public bool IsAddTorrentWindowShow { get; set; }
        public int UpdateTime { get; set; } = 5000;
        public int TorrentPeerLimit { get; set; } = 25;
        public double CategoriesWidth { get; set; } = 150;
        public double MoreInfoHeight { get; set; } = 202;
        public double MainWindowWidth { get; set; } = 600;
        public double MainWindowHeight { get; set; } = 800;
        public string ColumnSizes { get; set; } = string.Empty;
        public bool AllowPreRelease { get; set; }
        public bool MoreInfoShow { get; set; } = true;
        public bool CanSearchForSimilatTorrentsWhenAddingNewOne { get; set; } = true;
        public bool ShowCategoryCount { get; set; } = true;
        public bool TraceEnabled { get; set; } = false;
        public bool TraceRequestsEnabled { get; set; } = false;


    }
}