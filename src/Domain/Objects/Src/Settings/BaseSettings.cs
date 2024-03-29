﻿using System;

namespace Objects.Settings
{
    public class BaseSettings : ISettings
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public DateTime UpdatedUtc { get; set; }
    }

    public enum SettingsType
    {
        Jobs
    }
}
