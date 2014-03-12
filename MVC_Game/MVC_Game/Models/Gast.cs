using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Game.Models
{

    public enum Geschlecht
    {
        männlich, weiblich, keineAngabe

    }

    public enum Sterne
    {
        eins, zwei, drei, vier, fünf, keineAngabe
    }

    public class Gast
    {
        public int EntryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]

        public Geschlecht Sex { get; set; }
        public Sterne Stars { get; set; }

        [UIHint("EmailAddress")]
        public string Email { get; set; }


        [UIHint("MultilineText")]
        public string Comment { get; set; }

        public DateTime Datum { get; set; }

        public Gast() : this("",Geschlecht.keineAngabe,Sterne.keineAngabe,"","",DateTime.Now) { }
        public Gast(string n, Geschlecht sex, Sterne star ,string email,string com,DateTime datum) {
            this.Name = n;
            this.Sex = sex;
            this.Stars = star;
            this.Email = email;
            this.Comment = com;
            this.Datum = datum;
        }




    }
}