using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bakery.Models;

namespace Bakery.Controllers
{
    public class SalesController : Controller
    {
        BakeryEntities db = new BakeryEntities();
        // GET: Sales
        public ActionResult Index()
        {
            ViewBag.Employees = new SelectList(db.Employees, "EmployeeKey", "EmployeeTitle");
            ViewBag.Customers = new SelectList(db.People, "PersonKey", "PersonEmail");
            ViewBag.Products = new SelectList(db.Products, "ProductKey", "ProductName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "ProductKey, SaleDetailQuantity, SaleDetailDiscount, SaleDetailSaleTaxPercent, SaleDetailEatInTax")]SaleDetail sd)
        {
            //sale table
            Sale sale = new Sale();
            sale.SaleDate = DateTime.Now;
            sale.CustomerKey = Int32.Parse(Request.Form["PersonKey"]);
            sale.EmployeeKey = Int32.Parse(Request.Form["EmployeeKey"]);
            db.Sales.Add(sale);
            db.SaveChanges();

            //saledetail table
            sd.SaleKey = sale.SaleKey;
            sd.SaleDetailPriceCharged = db.Products.Find(sd.ProductKey).ProductPrice;
            db.SaleDetails.Add(sd);
            db.SaveChanges();

            //employee
            Person em = db.People.Find(db.Employees.Find(sale.EmployeeKey).PersonKey);
            //customer
            Person cust = db.People.Find(sale.CustomerKey);

            //get receipt
            Receipt receipt = new Receipt();
            receipt.date = sale.SaleDate.ToString();
            receipt.item = db.Products.Find(sd.ProductKey).ProductName;
            receipt.quantity = sd.SaleDetailQuantity;
            receipt.employee = em.PersonFirstName + " " + em.PersonLastName;
            receipt.customer = cust.PersonFirstName + " " + cust.PersonLastName;
            receipt.price = sd.SaleDetailPriceCharged;
            receipt.salesTax = (decimal)sd.SaleDetailSaleTaxPercent;
            receipt.eatInTax = (decimal)sd.SaleDetailEatInTax;

            return View("Receipt", receipt);
        }

        public ActionResult Receipt(Receipt rcpt)
        {
            return View(rcpt);
        }

        public ActionResult All()
        {
            return View(db.SaleDetails.ToList());
        }
    }
}
