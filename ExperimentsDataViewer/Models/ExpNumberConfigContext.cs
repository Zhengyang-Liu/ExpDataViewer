using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ExperimentsDataViewer.Models
{
    public class ExpNumberConfigContext : DbContext
    {
        public ExpNumberConfigContext() : base("name=ExpNumberConfigContext")
        {
        }

        public System.Data.Entity.DbSet<ExperimentsDataViewer.Models.ExpNumberConfig> ExpNumberConfig { get; set; }
    }
}