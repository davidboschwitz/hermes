using System;
using System.Collections.Generic;
using System.Text;

namespace Hermes.Models
{
    public class ReportItem
    {
        public DateTime TimeStamp { get; set; }
        public Guid Guid;
        public string From;
        public string Title { get; set; }
        public string Text { get; set; }
        public Boolean LocationBased { get; set; }
        public Tuple<Double,Double> LatLong { get; set; }

    }
}
