using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [DataType(DataType.DateTime)]
        public DateTime StartTime { set; get; }

        // The experiment end time
        [DataType(DataType.DateTime)]
        public DateTime EndTime { set; get; }
    }
}