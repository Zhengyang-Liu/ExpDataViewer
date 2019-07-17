using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ExperimentsDataViewer.Models
{
    public class ExpData
    {
        public string ExpID { get; set; }
        public DateTime CollectedTime { get; set; }
        public double AccelerationValue { get; set; }
    }
}