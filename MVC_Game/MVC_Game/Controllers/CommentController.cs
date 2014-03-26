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
            GästebuchDAL gbDAL = new GästebuchDAL();
            // alle Einträge ermitteln
            List<Gast> gbEntries = new List<Gast>();

            return View(gbDAL.GetAllEntries());
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
                else
                {


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
            }
            return View();
        }
        public ActionResult Delete(int delID) {

            GästebuchDAL gbDAL = new GästebuchDAL();
            MessageView mv;

            if (gbDAL.Delete(delID))
            {
                mv = new MessageView
                {
                    Title = "Gästebuch",
                    Message = "Löschen war erfolgreich"

                };
                return View("ViewMessage", mv);
            }
            else
            {
                mv = new MessageView
                {
                    Title = "Gästebuch",
                    Message = "Löschen war leider nicht erfolgreich!",
                    AdditonalInfos = "Bitte probieren Sie es später ernet!"
                };
                return View("ViewMessage", mv);
            }
        
        }
        
        public ActionResult Update(int updateID)
        {
            GästebuchDAL gbDal = new GästebuchDAL();
            MessageView mv;

            // den gewünschten Eintrag aus der DB - Tabelle ermitteln
             Gast entry = gbDal.GetEntry(updateID);

            // diesen Eintrag im Formular anzeigen
            if (entry != null)
            {
                // diesen Eintrag im Formular anzeigen
                return View("Add", entry);

            }
            else
            {
                // Fehlermeldung anzeigen
                mv = new MessageView
                {
                    Title = "Gästebuch",
                    Message = "Eintrag wurde nicht gefunden!",
                    AdditonalInfos = "Bitte probieren Sie es später ernet!"
                };
                return View("ViewMessage", mv);
            }
        }
        [HttpPost]
        public ActionResult Update(int updateID, Gast entry) {

            GästebuchDAL gbDal = new GästebuchDAL();
            MessageView mv;

            if (ModelState.IsValid)
            {
                if (gbDal.Update(updateID, entry))
                {
                    // es wird zur Action Index (GuestbookController) weitergeleitet
                    return RedirectToAction("index", "guestbook");
                }
                else
                {
                    // Fehlermeldung anzeigen
                    mv = new MessageView
                    {
                        Title = "Gästebuch",
                        Message = "Eintrag konnte nicht geändert werden!",
                        AdditonalInfos = "Bitte probieren Sie es später ernet!"
                    };
                    // Fehlermeldungs - View angezeigt
                    return View("ViewMessage", mv);
                }
            }
            else
            {
                return View("Add");
            }

       
        
        
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