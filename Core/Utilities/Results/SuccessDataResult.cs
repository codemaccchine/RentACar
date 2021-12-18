﻿using Core.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data) : base(data, true)
        {
        }

        public SuccessDataResult(T data, string message) : base(data, true)
        {
        }

        /**************************************************************
         * 
         * Genelde bu şekilde kullanılmaz
         * 
         */
         

        public SuccessDataResult() : base(default, true)
        {
        }

        public SuccessDataResult(string message) : base(default, true, message)
        {
        }

    }
}
