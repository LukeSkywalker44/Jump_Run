using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MVC_Game.Classes;
using MVC_Game.Models;

namespace MVC_Game.Classes
{
    public class GästebuchContext:DbContext
     {
        //Ctor... es wird vom EF die DB db_hotel2 erzeugt
        public GästebuchContext () : base("db_Game2") { }
        public DbSet<Gast> Gäste { get; set; }

        //wir könnten die Klasse Gast annotieren 
        //hier verwenden wir allerdings die Fluent-API

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GästebuchkConfig());
        }
    }
}