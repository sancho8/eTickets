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
            return View("Admin", "_Layout");
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
                var card = db.Cards.SingleOrDefault(c => c.Id == cardId);
                if (card != null)
                {
                    card.Balance += amount;

                    Operation op = new Operation();
                    op.Id = db.Operations.Count() + 1;
                    op.Card_Id = cardId;
                    op.Card = card;
                    op.Date = DateTime.Now;
                    if (amount >= 0)
                    {
                        op.Description = "Пополнение баланса";
                    }
                    else
                    { 
                        op.Description = "Списание средств";
                    }
                    op.Amount = amount;
                    db.Operations.Add(op);

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