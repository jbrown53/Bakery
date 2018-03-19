using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bakery.Models;

namespace Bakery.Controllers
{
    public class CustomerController : Controller
    {
        BakeryEntities db = new BakeryEntities();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "PersonLastName, PersonFirstName, PersonEmail, PersonPhone")]Person p)
        {
            p.PersonDateAdded = DateTime.Now;
            db.People.Add(p);

            return View("Result", new Message("New customer has been registered successfully"));
        }

        public ActionResult Result(Message message)
        {
            return View(message);
        }
    }
}