using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExperimentsDataViewer.Models
{
    public class RunningExp
    {
        [Key]
        // The experiment number.
        public int ExpNo { set; get; }

        [Column(TypeName = "datetime2")]
        public DateTime StartTime { get; set; }
    }
}