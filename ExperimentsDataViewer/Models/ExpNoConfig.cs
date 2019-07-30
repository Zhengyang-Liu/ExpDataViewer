namespace ExperimentsDataViewer.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public class ExpNoConfig : DbContext
    {
        // Your context has been configured to use a 'ExpNoConfig' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ExperimentsDataViewer.Models.ExpNoConfig' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ExpNoConfig' 
        // connection string in the application configuration file.
        public ExpNoConfig()
            : base("name=ExpNoConfig")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public DbSet<ExpNo> expNo { get; set; }
    }

    public class ExpNo
    {
        [Key]
        public int Id { get; set; }
        public int NextNo { get; set; }
    }
}