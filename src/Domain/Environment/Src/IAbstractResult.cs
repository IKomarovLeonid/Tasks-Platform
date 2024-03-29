﻿using Objects.Common;

namespace Environment
{
    public interface IAbstractResult
    {
        public ErrorCode Code { get; }

        public string Message { get; }
    }
}
