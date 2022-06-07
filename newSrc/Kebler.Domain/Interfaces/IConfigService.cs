﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kebler.Domain.Models;

namespace Kebler.Domain.Interfaces
{
    public interface IConfigService
    {
        public bool IsInited { get; }

        public static DefaultSettings DefaultSettingsInstanse;

        public void Save();

        public void LoadConfig();

    }
}
