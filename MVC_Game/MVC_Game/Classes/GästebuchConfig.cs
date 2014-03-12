using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MVC_Game.Models;
using MVC_Game.Classes;
using System.Data.Entity.ModelConfiguration;

namespace MVC_Game.Classes
{
    public class GästebuchConfig : EntityTypeConfiguration<Gast>
    {

         public GästebuchConfig() {
            this.ToTable("gästebucheineträge");
            this.HasKey(x=> x.EntryId);
            this.Property(x => x.EntryId).HasColumnName("entry_id");
            this.Property(x => x.Name).HasColumnName("name").IsRequired();
            this.Property(x => x.Comment).HasColumnName("text");
            this.Property(x => x.Stars).HasColumnName("stars").IsRequired();
            this.Property(x => x.Sex).HasColumnName("geschlecht").IsRequired();
            this.Property(x => x.Email).HasColumnName("email").IsRequired();
            this.Property(x => x.Datum).HasColumnName("datum").IsRequired(); 

        }

    }
}