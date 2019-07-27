using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExperimentsDataViewer.Models
{
    public class ExpNumberConfig
    {
        // The key of this table
        [Key]
        public int Id { set; get; }

        // The current experiment number.
        public int CurrentExpNo { set; get; }
    }
}