using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDash.Models
{
    public class Chart
    {
        public string title { get; set; }
        public string type { get; set; }
        public String data { get; set; }
        public String datatype { get; set; }
        public String labels { get; set; }
        public List<List<String>> data2 { get; set; }
    }
}
