using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace VerbaCompareScraper
{
    [DebuggerDisplay("{name}")]
    public class TermInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool ordering { get; set; }
        public bool inquiry { get; set; }
    }
}
