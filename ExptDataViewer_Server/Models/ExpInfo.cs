using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExperimentsDataViewer.Models
{
    /*
     This class records the summary info for every experiment.
         */
    public class ExpInfo
    {
        // Indicates the current experiment number.
        [Key]
        public int ExpNo { set; get; }

        // The experiment start time
        [Column(TypeName = "datetime2")]
        public DateTime StartTime { set; get; }

        // The experiment end time
        [Column(TypeName = "datetime2")]
        public DateTime EndTime { set; get; }
    }
}