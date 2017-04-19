using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eTickets.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (Ticket_DBEntities1 t = new Ticket_DBEntities1())
            {
                var cards = t.Cards.ToList<Card>();
                return View(cards);
            }
        }

        public ActionResult AddCurrency(int cardId, int amount)
        {
            using (var db = new Ticket_DBEntities1())
            {
                var result = db.Cards.SingleOrDefault(c => c.Id == cardId);
                if (result != null)
                {
                    result.Balance += amount;
                    db.SaveChanges();
                }
                return PartialView("CardTableView", db.Cards.ToList<Card>());
            }
        }
        
    }
}