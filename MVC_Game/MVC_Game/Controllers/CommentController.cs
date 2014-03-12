using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Game.Models;
using MVC_Game.Classes;


namespace MVC_Game.Controllers
{
    public class CommentController : Controller
    {
        //
        // GET: /Gästebuch/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Gast entry)
        {
            if (ModelState.IsValid)
            {
                MessageView mv;
                //DB Teil
                GästebuchDAL gbDAL = new GästebuchDAL();

                if (gbDAL.Insert(entry))
                {
                    mv = new MessageView
                    {
                        Title = "Gästebuch",
                        Message = "Eintrag war erfolgreich"
                    };

                    return View("ViewMessage", mv);
                    // return //alles OK
                }

                mv = new MessageView
                {
                    Title = "Gästebuch",
                    Message = "Eintrag war nicht erfolgreich",
                    AdditonalInfos = "xxxxxxx"
                };
                return View("ViewMessage", mv);
                //  return //Eintrag hat nicht funktioniert

                //+ entsprechende Meldung ausgeben 


            }
            return View();
        }
    }
}
/*
 //ALTER TEIL
        // GET: /Commant/
        [HttpGet]
        public ActionResult Index()
        {
            Gast gast = new Gast();

            return View(gast);
        }
        [HttpPost]
        public ActionResult Index(Gast gast)
        {   
            // Diese Methode wird aufgerufen per Post (http-Methode)
            // und zwar wenn das Formular abgesendet (submit) wird
            if (ModelState.IsValid)
            {
                return View("CommentOK");
            }
            else
            {
                return View();
            }
        }
    }

*/