﻿using System;

namespace Objects
{
    public interface ISettings
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public DateTime UpdatedUtc { get; set; }
    }
}
