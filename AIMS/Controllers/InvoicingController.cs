using AIMS.Models;
using AIMS.Models.Context;
using AIMS.Models.Tables;
using MvcRazorToPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIMS.Controllers
{
    public class InvoicingController : Controller
    {
        // GET: Invoicing
        public ActionResult Index()
        {
            return View();
        }
        // GET: Invoicing
        public ActionResult AddInvoice()
        {
            return View();
        }
        // GET: Invoicing
        [HttpGet]
        public ActionResult InvoicePdf()
        {
            //InvoiceInfo model = (InvoiceInfo)TempData["invoiceData"];
            InvoiceInfo model = (InvoiceInfo)Session["invoiceData"];
            var date = String.Format("{0:yyyymmdd}", DateTime.Now);
            return new PdfActionResult(model)
            {
                FileDownloadName = date + "AndersonInvoice.pdf"
            };
        }
        [HttpPost]
        public ActionResult DownloadPdf(InvoiceInfo invoiceInfo)
        {
            InsertInvoice(invoiceInfo);
            Session["invoiceData"] = invoiceInfo;
            return RedirectToAction("InvoicePdf", "Invoicing");
        }

        [HttpPost]
        public ActionResult DownloadPdfFromView(InvoiceInfo invoiceInfo)
        {
            //TempData["invoiceData"] = invoiceInfo;
            Session["invoiceData"] = invoiceInfo;
            return RedirectToAction("InvoicePdf", "Invoicing");
        }

        [HttpPost]
        public JsonResult SavePdf(InvoiceInfo invoiceInfo)
        {
            InsertInvoice(invoiceInfo);
            return Json(String.Empty);
        }

        public void InsertInvoice(InvoiceInfo invoiceInfo)
        {
            try
            {
                using (var context = new InventoryDbContext())
                {
                    var tempTblClient = new TableClient();
                    TableClient tblClient = new TableClient()
                    {
                        ClientID = invoiceInfo.ClientID,
                        ClientName = invoiceInfo.ClientName,
                        Address = invoiceInfo.Address,
                        ContactNo = invoiceInfo.ContactNo,
                        TinNo = invoiceInfo.TinNo
                    };
                    context.TblClient.Add(tblClient);
                    context.SaveChanges();

                    tempTblClient = tblClient;

                    if (tempTblClient != null)
                    {
                        foreach (var item in invoiceInfo.ClientItemList)
                        {
                            TableClientItem tblClientItem = new TableClientItem()
                            {
                                Quantity = item.Quantity,
                                ItemNo = item.ItemNo,
                                Description = item.Description,
                                UnitPrice = item.UnitPrice,
                                Discount = item.Discount,
                                ClientBaseID = tempTblClient.ClientBaseID
                            };
                            context.TblClientItem.Add(tblClientItem);
                            context.SaveChanges();

                        }

                        TableInvoice tblInvoice = new TableInvoice()
                        {
                            InvoiceDate = invoiceInfo.InvoiceDate,
                            ClientBaseID = tempTblClient.ClientBaseID,
                            DueDate = invoiceInfo.DueDate,
                            InvoicePeriod = invoiceInfo.InvoicePeriod,
                            AccountName = invoiceInfo.AccountName,
                            USDAccountNo = invoiceInfo.USDAccountNo,
                            BankName = invoiceInfo.BankName,
                            BankAddress = invoiceInfo.BankAddress,
                            SwiftCode = invoiceInfo.SwiftCode
                        };
                        context.TblInvoice.Add(tblInvoice);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public JsonResult DisplayInvoice()
        {
            try
            {
                List<InvoiceInfo> invoiceInfo = new List<InvoiceInfo>();
                using (var context = new InventoryDbContext())
                {
                    invoiceInfo = (from invoice in context.TblInvoice
                                   join client in context.TblClient
                                    on invoice.ClientBaseID equals client.ClientBaseID
                                   select new InvoiceInfo
                                   {
                                       InvoiceID = invoice.InvoiceID,
                                       ClientName = client.ClientName,
                                       ClientID = client.ClientID,
                                       Address = client.Address,
                                       ContactNo = client.ContactNo,
                                       TinNo = client.TinNo,

                                       ClientItemList = context.TblClientItem.Where(a => a.ClientBaseID == client.ClientBaseID).Select(
                                           a => new ClientItem
                                           {
                                               Quantity = a.Quantity,
                                               ItemNo = a.ItemNo,
                                               Description = a.Description,
                                               UnitPrice = a.UnitPrice,
                                               Discount = a.Discount,
                                               LineTotal = (a.Quantity * a.UnitPrice) - a.Discount
                                           }).ToList(),

                                       InvoiceDate = invoice.InvoiceDate,
                                       DueDate = invoice.DueDate,
                                       InvoicePeriod = invoice.InvoicePeriod,
                                       AccountName = invoice.AccountName,
                                       USDAccountNo = invoice.USDAccountNo,
                                       BankName = invoice.BankName,
                                       BankAddress = invoice.BankAddress,
                                       SwiftCode = invoice.SwiftCode
                                   }).ToList();
                }
                return Json(invoiceInfo);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
        }

    }
}