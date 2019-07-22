using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace VerbaCompareScraper
{
    [DebuggerDisplay("{name}")]
    class CourseTypeInfo
    {
        public string id { get; set; } //Was int
        public string name { get; set; }

        //public string code { get; set; }
    }
}
