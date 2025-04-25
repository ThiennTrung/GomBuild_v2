using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GomBuild_v2.Model
{
    public class JsonString
    {
        public string DEV { get; set; }
        public string SITE { get; set; }
        public List<BUILDS> BUILDS { get; set; }

    }

    public class BUILDS
    {
        public int STT { get; set; }
        public string VERSION { get; set; }
        public string PROJECT { get; set; }
        public bool ISCURRENT { get; set; }
        public string NOTE { get; set; }

    }
}
