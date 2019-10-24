﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.ApiGateway.Classes.Settings
{
    public class CacheOption
    {
        /// <summary>
        /// Enable cache Middleware
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        /// Cache duration time in second, default 60 seconds
        /// </summary>
        public int Duration { get; set; } = 60;
        /// <summary>
        /// The size limit for the response cache middleware in bytes. The default is set to 100 MB.
        /// </summary>
        public long SizeLimit { get; set; } = 100 * 1024 * 1024;

        /// <summary>
        /// The largest cacheable size for the response body in bytes. The default is set to 64 MB.
        /// </summary>
        public long MaximumBodySize { get; set; } = 64 * 1024 * 1024;

        /// <summary>
        /// <c>true</c> if request paths are case-sensitive; otherwise <c>false</c>. The default is to treat paths as case-insensitive.
        /// </summary>
        public bool UseCaseSensitivePaths { get; set; } = false;
    }
}
