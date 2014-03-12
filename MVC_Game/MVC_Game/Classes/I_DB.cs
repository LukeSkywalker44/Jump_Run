using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Game.Classes
{
   interface I_DB<Key,Type>
    {
        bool Insert(Type entry);
        bool Update(Key key, Type updatedEntry);
        Type GetEntry(Key id);
        List<Type> GetAllEntries();
        bool Delete(Key id);
    }
}