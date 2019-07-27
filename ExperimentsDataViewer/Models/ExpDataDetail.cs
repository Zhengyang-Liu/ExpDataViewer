using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExperimentsDataViewer.Models
{
    public class ExpDataDetail
    {
        public int Id { get; set; }

        // Indicates the experiment number.
        public int ExpNo { get; set; }

        public DateTime CollectedTime { get; set; }

        public double Acceleration { get; set; }
    }
}