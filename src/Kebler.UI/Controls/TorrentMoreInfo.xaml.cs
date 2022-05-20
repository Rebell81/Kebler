﻿using System;
using System.Threading.Tasks;
using Kebler.Core.Models.Arguments;

namespace Kebler.UI.Controls
{
    /// <summary>
    ///     Interaction logic for TorrentMoreInfo.xaml
    /// </summary>
    public partial class TorrentMoreInfo
    {
        public Func<TorrentSettings, Task> UpdateTorrent;

        public TorrentMoreInfo()
        {
            InitializeComponent();
        }
    }
}