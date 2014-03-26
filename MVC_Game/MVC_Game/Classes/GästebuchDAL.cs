using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MVC_Game.Classes;
using MVC_Game.Models;

namespace MVC_Game.Classes
{
    public class GästebuchDAL : I_DB<int,Gast>
    {
        public bool Insert(Gast entry)
        {
            using (GästebuchContext gbContext = new GästebuchContext())
            {
                gbContext.Gäste.Add(entry);
                return gbContext.SaveChanges() == 1;
            }
        }

        public bool Update(int key, Gast updatedEntry)
        {
            using (GästebuchContext gbContext = new GästebuchContext()){
                var entryToUpdate = (from entry in gbContext.Gäste
                                     where entry.EntryId == key
                                     select entry).FirstOrDefault();
                // falls != null
                if (entryToUpdate != null)
                {
                    // dann mit den aktuellen Daten (updatedEntry) aktualisieren ( SaveChanges() )
                    entryToUpdate.Name = updatedEntry.Name;
                    entryToUpdate.Stars = updatedEntry.Stars;
                    entryToUpdate.Comment= updatedEntry.Comment;
                    // Datum und EntryID werden nicht geändert
                }

                return gbContext.SaveChanges() == 1;
            }
        }

        public Gast GetEntry(int id)
        {
            using (GästebuchContext gbContext = new GästebuchContext()){
                var gbEntries = (from entry in gbContext.Gäste
                                       where entry.EntryId== id
                                       select entry)
                                       .FirstOrDefault();

                return gbEntries;

            }
        }
        
        public List<Gast> GetAllEntries()
        {
            using (GästebuchContext gbContext = new GästebuchContext())
            {
                var gbEntries = from g in gbContext.Gäste
                                orderby g.Datum descending
                                select g;

                return gbEntries.ToList();
            }
        }

        public bool Delete(int id)
        {
            // LINQ - Abfrage (Entry mit der ID id)
            using (GästebuchContext gbContext = new GästebuchContext())
            {
                var entryToDelete = (from entry in gbContext.Gäste
                                     where entry.EntryId == id
                                     select entry)
                                    .FirstOrDefault();

                // auf dem Kontext Remove() aufrufen
                if (entryToDelete != null)
                {
                    gbContext.Gäste.Remove(entryToDelete);
                }

                return gbContext.SaveChanges() == 1;
            }
        }
    }
}