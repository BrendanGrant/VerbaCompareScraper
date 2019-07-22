using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace VerbaCompareScraper
{
    [DebuggerDisplay("{name}")]
    public class Section
    {
        public string id { get; set; }
        public string name { get; set; }
        public string instructor { get; set; }
    }

    [DebuggerDisplay("{name}")]
    public class CourseSectionInfo
    {
        public string name { get; set; }
        public string id { get; set; }
        public Section[] sections { get; set; }
    }
}
