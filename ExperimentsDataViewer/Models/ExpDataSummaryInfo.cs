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
    public class ExpDataSummaryInfo
    {
        // The key of this table
        [Key]
        public int Id { set; get; }

        // Indicates the current experiment number.
        public int ExpNo { set; get; }

        // The experiment start time
        [DataType(DataType.DateTime)]
        public DateTime StartTime { set; get; }

        // The experiment end time
        [DataType(DataType.DateTime)]
        public DateTime EndTime { set; get; }

        // The number of this experiment results collected
        public int ExpResultCount { set; get; }

        // The experiment current status: 0-Inprogress; 1-Finished
        public int Status { set; get; }
    }
}