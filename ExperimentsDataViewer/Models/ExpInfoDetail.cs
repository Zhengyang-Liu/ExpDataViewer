using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExperimentsDataViewer.Models
{
    public class ExpInfoDetail
    {
        public int Id { get; set; }

        // Indicates the experiment number.
        public int ExpNo { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CollectedTime { get; set; }

        public double Acceleration { get; set; }
    }
}