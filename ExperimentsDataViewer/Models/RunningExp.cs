using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExperimentsDataViewer.Models
{
    public class RunningExp
    {
        [Key]
        // The experiment number.
        public int ExpNo { set; get; }

        // The experiment current status: 0-Inprogress; 1-Finished
        public int Status { set; get; }
    }
}