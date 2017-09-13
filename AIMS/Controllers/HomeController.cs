using AIMS.Models;
using InventoryContext;
using InventoryEntity;
using MvcRazorToPdf;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AIMS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //EInventoryItem inventortItem = new sInventoryItem()
            //{
            //    ItemName = "Bond Paper",
            //    UnitOfMeasurementId = 1

            //};

            //using (var ctx = new InventoryDbContext())
            //{
            //    ctx.InventoryItem.Add(inventortItem);
            //    ctx.SaveChanges();
            //}


            //EUser user = new EUser()
            //{
            //    Username = "isdom\\MarkA",
            //    Lastname = "Atienza",
            //    Firstname = "Mark",
            //    Middlename = "DL",
            //    Department = "IT Department",
            //    Email = "markatienza@gmail.com",
            //    Contact = "0933665541",

            //};
            //using (var ctx = new AccountContext.AccountDbContext())
            //{
            //    ctx.Users.Add(user);
            //    ctx.SaveChanges();
            //}


            return View();
        }

        //ABOUT US        
        public ActionResult Aboutus()
        {
            return View();
        }

        //LANDING PAGE
        public ActionResult LandingPage()
        {
            return View();
        }

        public ActionResult Pdf(Requisition requisition)
        {
            var date = String.Format("{0:yyyymmdd}", DateTime.Now);
            var req = new Requisition();
            req.RequisitionID = 1;
            req.RequisitionDate = DateTime.Now;
            req.Firstname = "Mark";
            req.Lastname = "Atienza";
            req.Roles = "Admin Assistant";
            req.Contact = "0917 5684 322";
            req.Email = "marka@andersongroup.uk.com";
            req.RequisitionItems = new List<RequisitionItem>
            {
                   new RequisitionItem
                    {
                        Quantity = 20,
                        UnitOfMeasurement = "each",
                        Description = "A4",
                        ItemName = "Bond Paper",
                        UnitPrice = 90,
                    }
            };
            //byte[] pdfOutput = ControllerContext.GeneratePdf(client, "Pdf");
            //string fullPath = Server.MapPath("~/App_Data/FreshlyMade.pdf");
            //if (System.IO.File.Exists(fullPath))
            //{
            //    System.IO.File.Delete(fullPath);
            //}
            //System.IO.File.WriteAllBytes(fullPath, pdfOutput);
            //return View("Pdf");
            return new PdfActionResult(req);
            //{
            //    FileDownloadName = date + "AndersonInvoice.pdf"
            //};

            //return new PdfActionResult(client)
            //{
            //    FileDownloadName = "ElanWasHere.pdf"
            //};
            //}
        }
    }
}