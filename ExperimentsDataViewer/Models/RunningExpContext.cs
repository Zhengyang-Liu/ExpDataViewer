using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ExperimentsDataViewer.Models
{
    public class RunningExpContext : DbContext
    {
        public RunningExpContext() : base("name=RunningExpContext")
        {
        }

        public System.Data.Entity.DbSet<ExperimentsDataViewer.Models.RunningExp> RunningExp { get; set; }
    }
}