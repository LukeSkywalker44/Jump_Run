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
            throw new NotImplementedException();
        }
/*
        public Gast GetEntry(int id)
        {
            using (GästebuchContext gbContext = new GästebuchContext()){
                List<Gast> gbEntries = from g in gbContext.Gäste
                                       select g;



            }
        }
        */
        public List<Gast> GetAllEntries()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}