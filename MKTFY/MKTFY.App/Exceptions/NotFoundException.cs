﻿using System;

namespace MKTFY.App.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() { }

        public NotFoundException(string message)
            : base(message)
        { }

    }
}
