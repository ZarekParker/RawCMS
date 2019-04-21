﻿//******************************************************************************
// <copyright file="license.md" company="RawCMS project  (https://github.com/arduosoft/RawCMS)">
// Copyright (c) 2019 RawCMS project  (https://github.com/arduosoft/RawCMS)
// RawCMS project is released under GPL3 terms, see LICENSE file on repository root at  https://github.com/arduosoft/RawCMS .
// </copyright>
// <author>Daniele Fontani, Emanuele Bucarelli, Francesco Minà</author>
// <autogenerated>true</autogenerated>
//******************************************************************************
namespace RawCMS.Client.BLL.Model
{
    public class BaseRequest
    {
        public string Token { get; set; }
        public string Collection { get; set; }
        public string RawQuery { get; set; }
        public string Url { get; set; }
        public bool Unsafe { get; set; }
    }
}