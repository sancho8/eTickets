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
            using (Ticket_DBEntities1 t = new Ticket_DBEntities1())
            {
                ViewBag.Cards = t.Cards.ToList<Card>();
                ViewBag.Validators = t.Validators.ToList<eTickets.Validator>();
                return View("Admin", "_Layout");
            }
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

        [HttpPost]
        public ActionResult changeCardStatus(int cardId, int status)
        {
            using (var db = new Ticket_DBEntities1())
            {
                var card = db.Cards.SingleOrDefault(c => c.Id == cardId);
                card.Status = status;
                db.SaveChanges();
                var cards = db.Cards.ToList<Card>();
                ViewBag.cards = cards;
                return PartialView("AdminCardTableView");
            }
        }

        [HttpPost]
        public ActionResult changeValidatorData(int validatorId, int status, int payment)
        {
            using (var db = new Ticket_DBEntities1())
            {
                var validator = db.Validators.SingleOrDefault(v => v.Id == validatorId);
                validator.Status = status;
                validator.Payment = payment;
                db.SaveChanges();
                var validators = db.Validators.ToList<Validator>();
                ViewBag.validators = validators;
                return PartialView("AdminValidatorTableView");
            }
        }

        public ActionResult AddCard(int cardNumber)
        {
            using (var db = new Ticket_DBEntities1())
            {
                if (cardNumber < 1000 || cardNumber > 9999)
                {
                    ViewBag.ErrorMessage = "Неверный формат номера карты";
                }
                else if (db.Cards.Any(c => c.Number == cardNumber))
                {
                    ViewBag.ErrorMessage = "Карта с таким номером уже существует";
                }
                else { 
                    var card = new Card();
                    card.Id = db.Cards.Count() + 1;
                    card.Number = cardNumber;
                    card.Status = 0;
                    card.Balance = 0;
                    db.Cards.Add(card);
                    db.SaveChanges();
                }
                var cards = db.Cards.ToList<Card>();
                ViewBag.cards = cards;
                return PartialView("AdminCardTableView");
            }
        }

    }
}