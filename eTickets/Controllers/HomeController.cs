using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eTickets.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            using (Ticket_DBEntities1 t = new Ticket_DBEntities1())
            {
                var cards = t.Cards.ToList<Card>();
                return View(cards);
            }
        }

        public ActionResult GetUserPage()
        {
            return RedirectToAction("Index");
        }

        public ActionResult GetAdminPage()
        {
            return RedirectToAction("Index");
        }

        public ActionResult GetOperationPage()
        {
            using (Ticket_DBEntities1 t = new Ticket_DBEntities1())
            {
                var cards = t.Cards.ToList<Card>();
                ViewBag.cards = cards;
                return View("OperationPage", "_Layout");
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

        public ActionResult GetOperations(string cardNumber)
        {
            using (var db = new Ticket_DBEntities1())
            {
                var operations = (from e in db.Operations
                                  where e.Card.Number.ToString().Equals(cardNumber)
                                  select e).ToList<Operation>().OrderByDescending(op => op.Date);
                ViewBag.balance = (from e in db.Cards
                                   where e.Number.ToString().Equals(cardNumber)
                                   select e).Single().Balance;
                return PartialView("OperationsTableView", operations);
            }
        }
        
    }
}