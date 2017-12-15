using AccountsContext;
using AccountsFunction;
using AccountsWebAuthentication.Helper;
using AIMS.Models;
using InventoryContext;
using InventoryEntity;
using MvcRazorToPdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace AIMS.Controllers
{
    public class ReviewerController : BaseController
    {
        private IFUser _iFUser;
        
        public ReviewerController()
        {
            _iFUser = new FUser();
        }
        // GET: Reviewer/InitialReview

        [CustomAuthorize(AllowedRoles = new string[] { "PurchasingOfficer" })]
        public ActionResult Requisition()
        {
            return View();
        }

        // GET: Reviewer/DeclinedInitialReview
        [CustomAuthorize(AllowedRoles = new string[] { "PurchasingOfficer" })]
        public ActionResult DeclinedInitialReview()
        {
            return View();
        }

        // GET: Reviewer/DeliveredRequisition
        [CustomAuthorize(AllowedRoles = new string[] { "PurchasingOfficer" })]
        public ActionResult DeliveryMonitoring()
        {
            return View();
        }

        // GET: Reviewer/SupplierInformation
        [CustomAuthorize(AllowedRoles = new string[] { "PurchasingOfficer" })]
        public ActionResult ManageSupplier()
        {
            return View();
        }

        // GET: Reviewer/ApprovalReview
        [CustomAuthorize(AllowedRoles = new string[] { "Approver" })]
        public ActionResult ApprovalReview()
        {
            return View();
        }

        // GET: Reviewer/OrderPurchasing
        [CustomAuthorize(AllowedRoles = new string[] { "PurchasingOfficer" })]
        public ActionResult OrderProcessing()
        {
            return View();
        }

        public ActionResult PO()
        {
            return View();
        }


        //public ActionResult PurchaseOrderPDF(PurchaseOrder purchaseOrder)
        //{
        //    var purOrder = new PurchaseOrder();
        //    purOrder.SupplierID = 1;
        //    purOrder.SupplierName = "Makoy Supplies";
        //    purOrder.SupplierAddress = "Miss Teresita C. Metrillo 7114 Kundiman Street, Sampaloc 1008 Manila";
        //    purOrder.ContactPerson = "Mark Atienza";
        //    purOrder.ContactNo = "0917 5684 322";
        //    purOrder.SupplierEmail = "marka@gmail.com";
        //    purOrder.Vatable = true;

        //    purOrder.RequiredDate = DateTime.Now;
        //    purOrder.RequisitionID = 1;
        //    purOrder.RequisitionItems = new List<RequisitionItem>
        //    {
        //           new RequisitionItem
        //            {
        //                Quantity = 20,
        //                UnitOfMeasurement = "each",
        //                Description = "A4",
        //                ItemName = "Bond Paper",
        //                UnitPrice = 90,
        //            },   new RequisitionItem
        //            {
        //                Quantity = 20,
        //                UnitOfMeasurement = "each",
        //                Description = "A4",
        //                ItemName = "Bond Paper",
        //                UnitPrice = 90,
        //            },   new RequisitionItem
        //            {
        //                Quantity = 20,
        //                UnitOfMeasurement = "each",
        //                Description = "A4",
        //                ItemName = "Bond Paper",
        //                UnitPrice = 90,
        //            }
        //    };
        //    InvoiceInfo model = (InvoiceInfo)Session["invoiceData"];
        //    var date = String.Format("{0:yyyyMMdd-hhmm}", DateTime.Now);
        //    return new PdfActionResult(purOrder);
        //    //{
        //    //    FileDownloadName = date + "PurchaseOrder.pdf"
        //    //};

        //}

        //// GET: Reviewer/FinalReview
        //[System.Web.Mvc.Helper.CustomAuthorizeAttribute(UserRole = "FinalReviewer")]
        //public ActionResult FinalReview()
        //{
        //    return View();
        //}

        // GET: Reviewer/Approval
        //// GET: Reviewer/Requestor
        //[System.Web.Mvc.Helper.CustomAuthorizeAttribute(UserRole = "Requestor")]
        //public ActionResult Requestor()
        //{
        //    return View();
        //}

        //// GET: Reviewer/DeclinedRequest
        //[System.Web.Mvc.Helper.CustomAuthorizeAttribute(UserRole = "Requestor")]
        //public ActionResult DeclinedRequest()
        //{
        //    return View();
        //}

        //DISPLAY ALL REQUISITION

        public ActionResult PurchaseOrderPDF()
        {
            PurchaseOrder model = (PurchaseOrder)Session["purchaseOrderData"];
            var date = String.Format("{0:yyyyMMdd}", DateTime.Now);

            return new PdfActionResult(model)
            {
                FileDownloadName = date + "-PurchaseOrder" + ((model.RequisitionID + 1).ToString("D4")) + ".pdf"
            };

        }
        //public JsonResult ViewAllRequisition(string Status)
        //{
        //    try
        //    {
        //        List<Account> account = new List<Account>();//account = Account model
        //        List<Requisition> requisition = new List<Requisition>();//requisitions = Requisitions model

        //        using (var context = new InventoryDbContext())//Use dbInventory
        //        {
        //            requisition = (from req in context.Requisition
        //                           join loc in context.Location
        //                             on req.LocationId equals loc.LocationId
        //                           where req.Status == Status
        //                           select new Requisition
        //                           {
        //                               RequisitionID = req.RequisitionId,
        //                               SpecialInstruction = req.SpecialInstruction,
        //                               RequisitionDate = req.RequisitionDate,
        //                               RequiredDate = req.RequiredDate,
        //                               RequisitionType = req.RequisitionType,
        //                               Status = req.Status,
        //                               SupplierID = req.SupplierId,
        //                               Reason = req.ReasonForDeclined,
        //                               LocationID = loc.LocationId,
        //                               LocationName = loc.LocationName,
        //                               UserID = req.UserId.Value
        //                           }).ToList();
        //        }


        //        using (var context = new AccountDbContext())//Use dbAccount
        //        {
        //            var userIDs = requisition.Select(b => b.UserID);
        //            //SELECT ALL USER FROM DbAccount
        //            account = (from user in context.Users
        //                       where userIDs.Contains(user.UserId)
        //                       select new Account
        //                       {
        //                           UserID = user.UserId,
        //                           Firstname = user.Firstname,
        //                           Middlename = user.Middlename,
        //                           Lastname = user.Lastname,
        //                           Department = user.Department,
        //                           Contact = user.Contact,
        //                           Email = user.Email,
        //                       }).ToList();
        //        }

        //        //Join all data (account and requisition)

        //        requisition = (from req in requisition
        //                       join acc in account
        //                            on req.UserID equals acc.UserID
        //                       select new Requisition
        //                       {
        //                           RequisitionID = req.RequisitionID,

        //                           Firstname = acc.Firstname,
        //                           Middlename = acc.Middlename,
        //                           Lastname = acc.Lastname,
        //                           Department = acc.Department,
        //                           Contact = acc.Contact,
        //                           Email = acc.Email,

        //                           SpecialInstruction = req.SpecialInstruction,
        //                           RequisitionDateString = String.Format("{0: MMMM dd, yyyy}", req.RequisitionDate),
        //                           RequiredDateString = String.Format("{0: MMMM dd, yyyy}", req.RequiredDate),
        //                           RequisitionType = req.RequisitionType,
        //                           Status = req.Status,
        //                           SupplierID = req.SupplierID,
        //                           Reason = req.Reason,
        //                           LocationID = req.LocationID,
        //                           LocationName = req.LocationName
        //                       }).ToList();
        //        return Json(requisition);//Return data as json
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex);
        //    }
        //}

        public JsonResult LoadPages(Page page, string Status)
        {
            int totalpages = 0;
            var totalpositions = 0;

            using (var context = new InventoryDbContext())
            {
                totalpositions = context.Requisition.Where(req => req.Status == Status).Count();
            }

            if (totalpositions % page.itemPerPage != 0)
            {
                totalpages = (totalpositions / page.itemPerPage) + 1;
            }
            else
            {
                totalpages = (totalpositions / page.itemPerPage);
            }
            List<Page> pages = new List<Page>();
            for (int x = 1; x <= totalpages; x++)
            {
                if (x == page.PageNumber)
                {
                    pages.Add(
                    new Page
                    {
                        PageNumber = x,
                        PageStatus = true
                    });
                }
                else
                {
                    pages.Add(
                    new Page
                    {
                        PageNumber = x,
                        PageStatus = false
                    });
                }
            }
            return Json(pages);
        }

        //=====LOAD DATA=====//
        public JsonResult LoadPageData(Page page, string Status)
        {
            int beginning = page.itemPerPage * (page.PageNumber - 1);
            //List<Account> account = new List<Account>();//account = Account model
            List<Requisition> requisition = new List<Requisition>();//requisitions = Requisitions model
            try
            {
                using (var context = new InventoryDbContext())//Use dbInventory
                {
                    requisition = (from req in context.Requisition
                                   join loc in context.Location
                                     on req.LocationId equals loc.LocationId
                                   where req.Status == Status
                                   select new Requisition
                                   {
                                       RequisitionID = req.RequisitionId,
                                       SpecialInstruction = req.SpecialInstruction,
                                       RequisitionDate = req.RequisitionDate,
                                       RequiredDate = req.RequiredDate,
                                       RequisitionType = req.RequisitionType,
                                       Status = req.Status,
                                       SupplierID = req.SupplierId,
                                       Reason = req.ReasonForDeclined,
                                       LocationID = loc.LocationId,
                                       LocationName = loc.LocationName,
                                       LocationAddress = loc.LocationAddress,
                                       UserID = req.UserId.Value
                                   }).ToList();
                }
                var users = _iFUser.Read();
                //using (var context = new AccountDbContext())//Use dbAccount
                //{
                //    var userIDs = requisition.Select(b => b.UserID);
                //    //SELECT ALL USER FROM DbAccount
                //    account = (from user in context.Users
                //               where userIDs.Contains(user.UserId)
                //               select new Account
                //               {
                //                   UserID = user.UserId,
                //                   Firstname = user.Firstname,
                //                   Middlename = user.Middlename,
                //                   Lastname = user.Lastname,
                //                   Department = user.Department,
                //                   Contact = user.Contact,
                //                   Email = user.Email,
                //               }).ToList();
                //}

                using (var context = new AccountDbContext())//Use dbAccount
                {
                    var userIDs = requisition.Select(b => b.UserID);
                    //SELECT ALL USER FROM DbAccount
                    account = (from user in context.Users
                               where userIDs.Contains(user.UserId)
                               select new Account
                               {
                                   UserID = user.UserId,
                                   Firstname = user.Firstname,
                                   Middlename = user.Middlename,
                                   Lastname = user.Lastname,
                                   Department = user.Department,
                                   Contact = user.Contact,
                                   Email = user.Email,
                               }).ToList();
                }

                //Join all data (account and requisition)

                requisition = (from req in requisition
                               join acc in users
                                    on req.UserID equals acc.UserId
                               select new Requisition
                               {
                                   RequisitionID = req.RequisitionID,

                                   Firstname = acc.Username,
                                   //Middlename = acc.Middlename,
                                   //Lastname = acc.Lastname,
                                   //Department = acc.Department,
                                   //Contact = acc.Contact,
                                   //Email = acc.Email,

                                   SpecialInstruction = req.SpecialInstruction,
                                   RequisitionDateString = String.Format("{0: MMMM dd, yyyy}", req.RequisitionDate),
                                   RequiredDateString = String.Format("{0: MMMM dd, yyyy}", req.RequiredDate),
                                   RequisitionType = req.RequisitionType,
                                   Status = req.Status,
                                   SupplierID = req.SupplierID,
                                   Reason = req.Reason,
                                   LocationID = req.LocationID,
                                   LocationName = req.LocationName,
                                   LocationAddress = req.LocationAddress,
                               }).OrderBy(e => e.RequisitionID).Skip(beginning).Take(page.itemPerPage).ToList().ToList();
                return Json(requisition);//Return data as json
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }


        public JsonResult DeliveryMonitoringPages(Page page)
        {
            int totalpages = 0;
            var totalpositions = 0;

            using (var context = new InventoryDbContext())
            {
                totalpositions = context.Requisition.Where(req => (req.Status == "Partial Delivery" && req.Status == "Delivered")).Count();
            }

            if (totalpositions % page.itemPerPage != 0)
            {
                totalpages = (totalpositions / page.itemPerPage) + 1;
            }
            else
            {
                totalpages = (totalpositions / page.itemPerPage);
            }
            List<Page> pages = new List<Page>();
            for (int x = 1; x <= totalpages; x++)
            {
                if (x == page.PageNumber)
                {
                    pages.Add(
                    new Page
                    {
                        PageNumber = x,
                        PageStatus = true
                    });
                }
                else
                {
                    pages.Add(
                    new Page
                    {
                        PageNumber = x,
                        PageStatus = false
                    });
                }
            }
            return Json(pages);
        }

        //=====LOAD DATA=====//
        public JsonResult DeliveryMonitoringPageData(Page page)
        {
            int beginning = page.itemPerPage * (page.PageNumber - 1);
            List<Account> account = new List<Account>();//account = Account model
            List<Requisition> requisition = new List<Requisition>();//requisitions = Requisitions model
            try
            {
                using (var context = new InventoryDbContext())//Use dbInventory
                {
                    requisition = (from pd in context.PartialDelivery
                                   join req in context.Requisition on pd.RequisitionId equals req.RequisitionId
                                   join loc in context.Location on req.LocationId equals loc.LocationId
                                   where req.Status == "Partial Delivery" || req.Status == "Delivered"
                                   select new Requisition
                                   {
                                       PartialDeliveryID = pd.PartialDeliveryId,
                                       DeliveryDate = pd.DeliveryDate,
                                       RequisitionID = req.RequisitionId,
                                       SpecialInstruction = req.SpecialInstruction,
                                       RequisitionDate = req.RequisitionDate,
                                       RequiredDate = req.RequiredDate,
                                       RequisitionType = req.RequisitionType,
                                       Status = req.Status,
                                       SupplierID = pd.SupplierId,
                                       Reason = req.ReasonForDeclined,
                                       LocationID = loc.LocationId,
                                       LocationName = loc.LocationName,
                                       LocationAddress = loc.LocationAddress,
                                       UserID = req.UserId.Value,
                                       SupplierInvoiceNo = pd.SupplierInvoiceNo,
                                       DeliveryReceiptNo = pd.DeliveryReceiptNo
                                   }).ToList();
                }

                using (var context = new AccountDbContext())//Use dbAccount
                {
                    var userIDs = requisition.Select(b => b.UserID);
                    //SELECT ALL USER FROM DbAccount
                    account = (from user in context.Users
                               where userIDs.Contains(user.UserId)
                               select new Account
                               {
                                   UserID = user.UserId,
                                   Firstname = user.Firstname,
                                   Middlename = user.Middlename,
                                   Lastname = user.Lastname,
                                   Department = user.Department,
                                   Contact = user.Contact,
                                   Email = user.Email,
                               }).ToList();
                }

                //Join all data (account and requisition)

                requisition = (from req in requisition
                               join acc in account
                                    on req.UserID equals acc.UserID
                               select new Requisition
                               {
                                   RequisitionID = req.RequisitionID,

                                   Firstname = acc.Firstname,
                                   Middlename = acc.Middlename,
                                   Lastname = acc.Lastname,
                                   Department = acc.Department,
                                   Contact = acc.Contact,
                                   Email = acc.Email,

                                   PartialDeliveryID = req.PartialDeliveryID,
                                   DeliveryDateString = String.Format("{0: MMMM dd, yyyy}", req.DeliveryDate),
                                   SpecialInstruction = req.SpecialInstruction,
                                   RequisitionDateString = String.Format("{0: MMMM dd, yyyy}", req.RequisitionDate),
                                   RequiredDateString = String.Format("{0: MMMM dd, yyyy}", req.RequiredDate),
                                   RequisitionType = req.RequisitionType,
                                   Status = req.Status,
                                   SupplierID = req.SupplierID,
                                   Reason = req.Reason,
                                   LocationID = req.LocationID,
                                   LocationName = req.LocationName,
                                   LocationAddress = req.LocationAddress,
                                   SupplierInvoiceNo = req.SupplierInvoiceNo,
                                   DeliveryReceiptNo = req.DeliveryReceiptNo
                               }).OrderBy(e => e.RequisitionID).Skip(beginning).Take(page.itemPerPage).ToList().ToList();
                return Json(requisition);//Return data as json
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }


        //DISPLAY ALL DECLINED REQUISITION
        public JsonResult ViewAllDeclinedRequisition(string Status)
        {
            try
            {
                List<Account> account = new List<Account>();//account = Account model
                List<Requisition> requisition = new List<Requisition>();//requisitions = Requisitions model

                using (var context = new InventoryDbContext())//Use dbInventory
                {
                    //Select all declined request
                    requisition = (from req in context.Requisition
                                   where (req.Status == "Declined by Final Reviewer" || req.Status == "Declined by Approver")
                                   join loc in context.Location
                                     on req.LocationId equals loc.LocationId
                                   select new Requisition
                                   {
                                       RequisitionID = req.RequisitionId,
                                       SpecialInstruction = req.SpecialInstruction,
                                       RequisitionDate = req.RequisitionDate,
                                       RequiredDate = req.RequiredDate,
                                       RequisitionType = req.RequisitionType,
                                       Status = req.Status,
                                       SupplierID = req.SupplierId,
                                       Reason = req.ReasonForDeclined,
                                       LocationID = loc.LocationId,
                                       LocationName = loc.LocationName,
                                       UserID = req.UserId.Value
                                   }).ToList();
                }

                var users = _iFUser.Read();
                //using (var context = new AccountDbContext())//Use dbAccount
                //{
                //    var userIDs = requisition.Select(b => b.UserID);
                //    //SELECT ALL USER FROM DbAccount
                //    account = (from user in context.Users
                //               where userIDs.Contains(user.UserId)
                //               select new Account
                //               {
                //                   UserID = user.UserId,
                //                   Firstname = user.Firstname,
                //                   Middlename = user.Middlename,
                //                   Lastname = user.Lastname,
                //                   Department = user.Department,
                //                   Contact = user.Contact,
                //                   Email = user.Email,
                //               }).ToList();
                //}


                //Join all data (account and requisition)
                requisition = (from req in requisition
                               join acc in account
                                  on req.UserID equals acc.UserID

                               select new Requisition
                               {
                                   Firstname = acc.Firstname,
                                   Middlename = acc.Middlename,
                                   Lastname = acc.Lastname,
                                   Department = acc.Department,
                                   Contact = acc.Contact,
                                   Email = acc.Email,
                                   RequisitionID = req.RequisitionID,
                                   SpecialInstruction = req.SpecialInstruction,
                                   RequisitionDateString = req.RequisitionDate.ToString(),
                                   RequiredDateString = req.RequiredDate.ToString(),
                                   RequisitionType = req.RequisitionType,
                                   Status = req.Status,
                                   Reason = req.Reason,
                                   SupplierID = req.SupplierID,
                                   LocationID = req.LocationID,
                                   LocationName = req.LocationName
                               }).ToList();


                return Json(requisition);//Return data as json
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        //VIEW REQUISITION ITEM
        public JsonResult RequisitionItem(Requisition requisition)
        {
            try
            {
                List<RequisitionItem> requisitionItem = new List<RequisitionItem>();//Initialize Requisition model
                List<Supplier> supplier = new List<Supplier>();//Initialize Requisition model
                using (var context = new InventoryDbContext())
                {
                    //Query for getting requisitionItem
                    var requisitionItems = from req in context.Requisition
                                           join reqItem in context.RequisitionItem
                                             on req.RequisitionId equals reqItem.RequisitionId
                                           join inv in context.InventoryItem
                                             on reqItem.InventoryItemId equals inv.InventoryItemId
                                           join uom in context.UnitOfMeasurement
                                             on inv.UnitOfMeasurementId equals uom.UnitOfMeasurementId
                                           where reqItem.RequisitionId == requisition.RequisitionID
                                           select new
                                           {
                                               RequisitionItemID = reqItem.RequisitionItemId,
                                               InventoryItemID = inv.InventoryItemId,
                                               ItemName = inv.ItemName,
                                               UnitDescription = uom.Description,
                                               Quantity = reqItem.Quantity,
                                               ItemDescription = reqItem.Description,
                                               UnitPrice = reqItem.UnitPrice,
                                               PurchaseOrderId = reqItem.PurchaseOrderId,
                                               SupplierID = req.SupplierId
                                           };
                    var supplierInfo = from supp in context.Supplier
                                       where supp.SupplierId == requisition.SupplierID
                                       select supp;

                    var reqItemAndSupp = from reqItems in requisitionItems
                                         join suppInfo in supplierInfo
                                            on reqItems.SupplierID equals suppInfo.SupplierId into g
                                         from supp in g.DefaultIfEmpty()
                                         select new
                                         {
                                             RequisitionItemID = reqItems.RequisitionItemID,
                                             InventoryItemID = reqItems.InventoryItemID,
                                             ItemName = reqItems.ItemName,
                                             UnitDescription = reqItems.UnitDescription,
                                             Quantity = reqItems.Quantity,
                                             ItemDescription = reqItems.ItemDescription,
                                             UnitPrice = reqItems.UnitPrice,
                                             PurchaseOrderId = reqItems.PurchaseOrderId,

                                             TinNumber = supp.TinNumber,

                                             SupplierID = (int?)supp.SupplierId,
                                             SupplierName = supp.SupplierName,
                                             Address = supp.Address,
                                             ContactPerson = supp.ContactPerson,
                                             ContactNo = supp.ContactNo,
                                             Email = supp.Email
                                         };


                    //Put all data to model requisitionItem as list
                    requisitionItem = reqItemAndSupp.Select(
                    ri => new RequisitionItem
                    {
                        RequisitionItemID = ri.RequisitionItemID,
                        InventoryItemID = ri.InventoryItemID,
                        ItemName = ri.ItemName,
                        UnitOfMeasurement = ri.UnitDescription,
                        Quantity = ri.Quantity,
                        Description = ri.ItemDescription,
                        UnitPrice = ri.UnitPrice,
                        PurchaseOrderId = ri.PurchaseOrderId,
                        TinNumber = ri.TinNumber,

                        SupplierID = ri.SupplierID == null ? 0 : ri.SupplierID,
                        SupplierName = ri.SupplierName,
                        Address = ri.Address,
                        ContactPerson = ri.ContactPerson,
                        ContactNo = ri.ContactNo,
                        Email = ri.Email

                    }).ToList();
                }

                return Json(requisitionItem);//send data as Json
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        //VIEW REQUISITION ITEM
        public JsonResult DeliveryMonitoringRequisitionItem(Requisition requisition)
        {
            try
            {
                List<RequisitionItem> requisitionItem = new List<RequisitionItem>();//Initialize Requisition model
                List<Supplier> supplier = new List<Supplier>();//Initialize Requisition model
                using (var context = new InventoryDbContext())
                {
                    //Query for getting requisitionItem
                    //var requisitionItems = from req in context.Requisition
                    //                       join reqItem in context.RequisitionItem
                    //                         on req.RequisitionId equals reqItem.RequisitionId
                    //                       join inv in context.InventoryItem
                    //                         on reqItem.InventoryItemId equals inv.InventoryItemId
                    //                       join uom in context.UnitOfMeasurement
                    //                         on inv.UnitOfMeasurementId equals uom.UnitOfMeasurementId
                    //                       where reqItem.RequisitionId == requisition.RequisitionID
                    //                       select new
                    //                       {
                    //                           RequisitionItemID = reqItem.RequisitionItemId,
                    //                           InventoryItemID = inv.InventoryItemId,
                    //                           ItemName = inv.ItemName,
                    //                           UnitDescription = uom.Description,
                    //                           Quantity = reqItem.Quantity,
                    //                           ItemDescription = reqItem.Description,
                    //                           UnitPrice = reqItem.UnitPrice,
                    //                           PurchaseOrderId = reqItem.PurchaseOrderId,
                    //                           SupplierID = req.SupplierId
                    //                       };
                    supplier = (from supp in context.Supplier
                                where supp.SupplierId == requisition.SupplierID
                                select new Supplier
                                {
                                    TinNumber = supp.TinNumber,
                                    SupplierID = (int?)supp.SupplierId,
                                    SupplierName = supp.SupplierName,
                                    Address = supp.Address,
                                    ContactPerson = supp.ContactPerson,
                                    ContactNo = supp.ContactNo,
                                    Email = supp.Email,
                                    RequisitionItem = (from pd in context.PartialDelivery
                                                       join pdi in context.PartialDeliveryItem on pd.PartialDeliveryId equals pdi.PartialDeliveryId
                                                       join ri in context.RequisitionItem on pdi.RequisitionItemId equals ri.RequisitionItemId
                                                       join ii in context.InventoryItem on ri.InventoryItemId equals ii.InventoryItemId
                                                       join uom in context.UnitOfMeasurement on ii.UnitOfMeasurementId equals uom.UnitOfMeasurementId
                                                       where pdi.PartialDeliveryId == requisition.PartialDeliveryID && pdi.DeliveredQuantity != 0
                                                       select new RequisitionItem
                                                       {
                                                           InventoryItemID = ii.InventoryItemId,
                                                           UnitOfMeasurement = uom.Description,
                                                           Quantity = pdi.DeliveredQuantity,
                                                           UnitPrice = ri.UnitPrice,
                                                           ItemName = ii.ItemName,
                                                           RequisitionItemID = ri.RequisitionItemId
                                                       }).ToList()
                                }).ToList();

                    //var reqItemAndSupp = from reqItems in requisitionItems
                    //                     join suppInfo in supplierInfo
                    //                        on reqItems.SupplierID equals suppInfo.SupplierId into g
                    //                     from supp in g.DefaultIfEmpty()
                    //                     select new
                    //                     {
                    //                         RequisitionItemID = reqItems.RequisitionItemID,
                    //                         InventoryItemID = reqItems.InventoryItemID,
                    //                         ItemName = reqItems.ItemName,
                    //                         UnitDescription = reqItems.UnitDescription,
                    //                         Quantity = reqItems.Quantity,
                    //                         ItemDescription = reqItems.ItemDescription,
                    //                         UnitPrice = reqItems.UnitPrice,
                    //                         PurchaseOrderId = reqItems.PurchaseOrderId,

                    //                         TinNumber = supp.TinNumber,
                    //                         SupplierID = (int?)supp.SupplierId,
                    //                         SupplierName = supp.SupplierName,
                    //                         Address = supp.Address,
                    //                         ContactPerson = supp.ContactPerson,
                    //                         ContactNo = supp.ContactNo,
                    //                         Email = supp.Email
                    //                     };


                    ////Put all data to model requisitionItem as list
                    //requisitionItem = reqItemAndSupp.Select(
                    //ri => new RequisitionItem
                    //{
                    //    RequisitionItemID = ri.RequisitionItemID,
                    //    InventoryItemID = ri.InventoryItemID,
                    //    ItemName = ri.ItemName,
                    //    UnitOfMeasurement = ri.UnitDescription,
                    //    Quantity = ri.Quantity,
                    //    Description = ri.ItemDescription,
                    //    UnitPrice = ri.UnitPrice,
                    //    PurchaseOrderId = ri.PurchaseOrderId,
                    //    TinNumber = ri.TinNumber,
                    //    SupplierID = ri.SupplierID == null ? 0 : ri.SupplierID,
                    //    SupplierName = ri.SupplierName,
                    //    Address = ri.Address,
                    //    ContactPerson = ri.ContactPerson,
                    //    ContactNo = ri.ContactNo,
                    //    Email = ri.Email

                    //}).ToList();
                }

                return Json(supplier);//send data as Json
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }


        //DISPLAY TOTAL PAGES OF SUPPLIER
        public JsonResult LoadSupplierPage(Page page)
        {
            int totalpages = 0;
            var totalpositions = 0;

            using (var context = new InventoryDbContext())
            {
                totalpositions = context.Supplier.Count();
            }

            if (totalpositions % page.itemPerPage != 0)
            {
                totalpages = (totalpositions / page.itemPerPage) + 1;
            }
            else
            {
                totalpages = (totalpositions / page.itemPerPage);
            }
            List<Page> pages = new List<Page>();
            for (int x = 1; x <= totalpages; x++)
            {
                if (x == page.PageNumber)
                {
                    pages.Add(
                    new Page
                    {
                        PageNumber = x,
                        PageStatus = true
                    });
                }
                else
                {
                    pages.Add(
                    new Page
                    {
                        PageNumber = x,
                        PageStatus = false
                    });
                }
            }
            return Json(pages);
        }

        //DISPLAY ALL SUPPLIERS
        public JsonResult LoadAllSupplierPageData(Page page)
        {
            try
            {
                int beginning = page.itemPerPage * (page.PageNumber - 1);
                List<Supplier> supplier = new List<Supplier>();
                using (var context = new InventoryDbContext())
                {
                    supplier = (from supp in context.Supplier
                                select new Supplier
                                {
                                    SupplierID = supp.SupplierId,
                                    SupplierName = supp.SupplierName,
                                    Address = supp.Address,
                                    ContactPerson = supp.ContactPerson,
                                    ContactNo = supp.ContactNo,
                                    Email = supp.Email,
                                    TinNumber = supp.TinNumber,
                                    Vatable = supp.Vatable
                                }).OrderBy(e => e.SupplierID).Skip(beginning).Take(page.itemPerPage).ToList();
                    return Json(supplier);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
        }

        //DISPLAY ALL COMPANY NAME
        public JsonResult DisplayCompanyName()
        {
            try
            {
                List<Supplier> supplier = new List<Supplier>();//Exam = Exam model

                using (var context = new InventoryDbContext())
                {
                    //Get all existing supplier
                    var displaySupplier = from supp in context.Supplier
                                          select new
                                          {
                                              SupplierID = supp.SupplierId,
                                              SupplierName = supp.SupplierName,
                                              Address = supp.Address,
                                              ContactPerson = supp.ContactPerson,
                                              ContactNo = supp.ContactNo,
                                              Email = supp.Email,
                                          };
                    //Put all data to Supplier List 
                    supplier = displaySupplier.Select(
                        displaySupp => new Supplier
                        {
                            SupplierID = displaySupp.SupplierID,
                            SupplierName = displaySupp.SupplierName,
                            Address = displaySupp.Address,
                            ContactPerson = displaySupp.ContactPerson,
                            ContactNo = displaySupp.ContactNo,
                            Email = displaySupp.Email
                        }).ToList();
                }
                return Json(supplier);//return as json
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        //DISPLAY SUPPLIER INFORMATION AND ITEMS
        public JsonResult DisplaySupplierItem(Supplier supplier)
        {
            try
            {
                List<SupplierInventoryItem> supplierInventoryItem = new List<SupplierInventoryItem>();
                using (var context = new InventoryDbContext())
                {
                    supplierInventoryItem = (from supp in context.Supplier
                                             join suppInv in context.SupplierInventoryItem
                                                 on supp.SupplierId equals suppInv.SupplierId
                                             join inv in context.InventoryItem
                                                 on suppInv.InventoryId equals inv.InventoryItemId
                                             join uom in context.UnitOfMeasurement
                                                 on inv.UnitOfMeasurementId equals uom.UnitOfMeasurementId
                                             where suppInv.SupplierId == supplier.SupplierID
                                             select new SupplierInventoryItem
                                             {
                                                 InventoryItemID = inv.InventoryItemId,
                                                 ItemName = inv.ItemName,
                                                 UomDescription = uom.Description,
                                                 UnitPrice = suppInv.UnitPrice
                                             }).ToList();
                    return Json(supplierInventoryItem);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
        }

        //SELECT UNIT PRICE VIA SUPPLIER
        public JsonResult SelectUnitPriceViaSupplier(Supplier supplier)
        {
            try
            {
                List<Supplier> supplierInformation = new List<Supplier>();
                using (var context = new InventoryDbContext())
                {
                    supplierInformation = (from supp in context.Supplier
                                           where supp.SupplierId == supplier.SupplierID
                                           select new Supplier
                                           {
                                               SupplierID = supp.SupplierId,
                                               SupplierName = supp.SupplierName,
                                               Address = supp.Address,
                                               ContactPerson = supp.ContactPerson,
                                               ContactNo = supp.ContactNo,
                                               Email = supp.Email,
                                               supplierItemList = context.SupplierInventoryItem
                                                               .Where(a => a.SupplierId == supplier.SupplierID)
                                                               .Select(a => new SupplierInventoryItem
                                                               {
                                                                   UnitPrice = a.UnitPrice,
                                                                   InventoryItemID = a.InventoryId
                                                               }).ToList(),
                                               Vatable = supp.Vatable
                                           }).ToList();
                }
                return Json(supplierInformation);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
        }

        //REJECT REQUISITION
        [HttpPost]
        public JsonResult DeclineRequisition(int requisitionID, string Status, string ReasonForDeclining)
        {
            try
            {
                using (var context = new InventoryDbContext())//use DbInventory
                {
                    var eRequisition = context.Requisition.FirstOrDefault(x => x.RequisitionId == requisitionID);
                    eRequisition.Status = Status;
                    eRequisition.ReasonForDeclined = ReasonForDeclining;
                    context.SaveChanges();//Save all changes
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
            return Json(string.Empty);
        }
        //ACCEPT REQUISITION
        [HttpPost]
        public JsonResult AcceptRequisition(int requisitionID, Requisition requisition)
        {
            try
            {
                string isSuccess = "";
                using (var context = new InventoryDbContext())//Use DbInventory
                {

                    if (requisition.Status == "Approved")
                    {
                        var tblPurchasingOrder = context.PurchasingOrder.FirstOrDefault(a => (a.RequisitionId == requisitionID && a.SupplierId == requisition.SupplierID));
                        if (tblPurchasingOrder == null)
                        {
                            EPurchasingOrder purchasingOrder = new EPurchasingOrder()
                            {
                                SupplierId = requisition.SupplierID,
                                RequisitionId = requisitionID,
                                DeliveryCharges = requisition.DeliveryCharges,
                                DeliveryDate = DateTime.Now
                            };
                            context.PurchasingOrder.Add(purchasingOrder);
                            context.SaveChanges();

                            foreach (var items in requisition.RequisitionItems)
                            {
                                if (items.isItemSelected == true)
                                {
                                    foreach (var requisitionItem in context.RequisitionItem
                                   .Where(x => x.RequisitionId == requisitionID && x.InventoryItemId == items.InventoryItemID)
                                   .ToList())
                                    {
                                        requisitionItem.UnitPrice = items.UnitPrice;
                                        requisitionItem.PurchaseOrderId = purchasingOrder.PurchaseOrderId;
                                    }
                                    context.SaveChanges();//Update all item from tblRequisitonItem
                                }
                            }
                            bool isRequisitionCompleted = false;
                            var purchaseOrderIds = context.RequisitionItem.Where(x => x.RequisitionId == requisitionID).Select(a => new RequisitionItem { PurchaseOrderId = a.PurchaseOrderId }).ToList();
                            foreach (var item in purchaseOrderIds)
                            {
                                isRequisitionCompleted = item.PurchaseOrderId != 0 ? isRequisitionCompleted = true : isRequisitionCompleted = false;
                                if (!isRequisitionCompleted) break;
                            }
                            if (isRequisitionCompleted)
                            {
                                var eRequisition = context.Requisition.FirstOrDefault(x => x.RequisitionId == requisitionID);
                                eRequisition.SupplierId = requisition.SupplierID;
                                eRequisition.Status = requisition.Status;
                                context.SaveChanges();//Save status
                            }
                            isSuccess = "success";
                        }
                        else
                        {
                            isSuccess = "not";
                        }
                    }
                    else
                    {
                        var eRequisition = context.Requisition.FirstOrDefault(x => x.RequisitionId == requisitionID);
                        eRequisition.SupplierId = requisition.SupplierID;
                        eRequisition.Status = requisition.Status;
                        context.SaveChanges();//Save status
                    }
                }
                return Json(isSuccess);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        //ADD SUPPLIER
        [HttpPost]
        public JsonResult AddSupplier(Supplier supplier)
        {
            try
            {
                using (var context = new InventoryDbContext())//use dbInventory
                {
                    var tblSupplier = context.Supplier.FirstOrDefault(a => a.SupplierName == supplier.SupplierName);
                    if (tblSupplier == null)
                    {

                        if (context.Supplier.Any(o => o.SupplierId == supplier.SupplierID))
                        {
                            return Json("SupplierExist");
                        }
                        else
                        {
                            ESupplier addSupplier = new ESupplier()
                            {
                                TinNumber = supplier.TinNumber,
                                SupplierName = supplier.SupplierName,
                                Address = supplier.Address,
                                ContactPerson = supplier.ContactPerson,
                                ContactNo = supplier.ContactNo,
                                Email = supplier.Email,
                                Vatable = supplier.Vatable
                            };
                            context.Supplier.Add(addSupplier);
                            context.SaveChanges();

                            tblSupplier = addSupplier;
                        }

                    }
                    //if (tblSupplier != null)
                    //{
                    //    foreach (var req in context.Requisition.Where(x => x.RequisitionId == requisitionID).ToList())
                    //    {
                    //        req.SupplierId = tblSupplier.SupplierId;
                    //    }
                    //    context.SaveChanges();//Save status
                    //}
                    return Json(tblSupplier);
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }

        }

        //ADD NEW SUPPLIER ITEM
        [HttpPost]
        public JsonResult AddSupplierItem(Supplier supplier)
        {
            try
            {
                using (var context = new InventoryDbContext())//use dbInventory
                {
                    var isItemExists = true;
                    foreach (var supp in supplier.supplierItemList)
                    {
                        var tblSupplierItem = context.SupplierInventoryItem.Any(s => s.SupplierId == supplier.SupplierID && s.InventoryId == supp.InventoryItemID);
                        isItemExists = tblSupplierItem;
                        break;

                    }
                    if (isItemExists == false)
                    {
                        foreach (var supp in supplier.supplierItemList)
                        {
                            ESupplierInventoryItem eSupplierItem = new ESupplierInventoryItem()
                            {
                                SupplierId = supplier.SupplierID.Value,
                                InventoryId = supp.InventoryItemID,
                                UnitPrice = supp.UnitPrice
                            };
                            context.SupplierInventoryItem.Add(eSupplierItem);
                            context.SaveChanges();
                        }
                        isItemExists = false;
                    }

                    return Json(isItemExists);
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }

        }

        //UPDATE SUPPLIER ITEM
        [HttpPost]
        public JsonResult UpdateSupplierItems(Supplier supplier)
        {
            try
            {
                using (var context = new InventoryDbContext())//use dbInventory
                {
                    foreach (var items in supplier.supplierItemList)
                    {
                        foreach (var supplierItem in context.SupplierInventoryItem
                            .Where(x => x.SupplierId == supplier.SupplierID && x.InventoryId == items.InventoryItemID)
                            .ToList())
                        {
                            supplierItem.UnitPrice = items.UnitPrice;
                        }
                        context.SaveChanges();//Update all item from tblRequisitonItem
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
            return Json(string.Empty);

        }

        //UPDATE REQUISITION
        [HttpPost]
        public JsonResult UpdateRequisition(Requisition requisition)
        {
            try
            {
                using (var context = new InventoryDbContext())//use dbInventory
                {
                    //loop all elements of requisition
                    var tblRequisition = context.Requisition.FirstOrDefault(x => x.RequisitionId == requisition.RequisitionID);
                    tblRequisition.RequiredDate = requisition.RequiredDate;
                    context.SaveChanges();//update requiredDate from tblRequisition
                    //loop all elements of requistionitem
                    foreach (var items in requisition.RequisitionItems)
                    {
                        foreach (var some in context.RequisitionItem
                            .Where(x => x.RequisitionId == requisition.RequisitionID && x.InventoryItemId == items.InventoryItemID)
                            .ToList())
                        {
                            some.Description = items.Description;
                            some.UnitPrice = items.UnitPrice;
                            some.Quantity = items.Quantity;
                        }
                        context.SaveChanges();//Update all item from tblRequisitonItem
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
            return Json(string.Empty);
        }

        //UPDATE UNIT PRICE
        [HttpPost]
        public JsonResult UpdateUnitPrice(Requisition requisition)
        {
            try
            {
                using (var context = new InventoryDbContext())//use dbInventory
                {
                    //loop all elements of requisition
                    //var tblRequisition = context.Requisition.FirstOrDefault(x => x.RequisitionId == requisition.RequisitionID);
                    //tblRequisition.RequiredDate = requisition.RequiredDate;
                    //context.SaveChanges();//update requiredDate from tblRequisition
                    //loop all elements of requistionitem
                    foreach (var items in requisition.RequisitionItems)
                    {
                        foreach (var some in context.RequisitionItem
                            .Where(x => x.RequisitionId == requisition.RequisitionID && x.RequisitionItemId == items.RequisitionItemID)
                            .ToList())
                        {
                            //some.Description = items.Description;
                            some.UnitPrice = items.UnitPrice;
                            //some.Quantity = items.Quantity;
                        }
                        context.SaveChanges();//Update all item from tblRequisitonItem
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
            return Json(string.Empty);
        }

        //UPDATE UNIT PRICE
        [HttpPost]
        public JsonResult UpdateSupplierDetails(Supplier supplier)
        {
            try
            {
                using (var context = new InventoryDbContext())//use dbInventory
                {
                    //loop all elements of requisition
                    var tblSupplier = context.Supplier.FirstOrDefault(x => x.SupplierId == supplier.SupplierID);
                    tblSupplier.TinNumber = supplier.TinNumber;
                    tblSupplier.SupplierName = supplier.SupplierName;
                    tblSupplier.Address = supplier.Address;
                    tblSupplier.ContactPerson = supplier.ContactPerson;
                    tblSupplier.Email = supplier.Email;
                    tblSupplier.Vatable = supplier.Vatable;
                    context.SaveChanges();//update requiredDate from tblRequisition
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
            return Json(string.Empty);
        }

        //DOWNLOAD PDF
        [HttpPost]
        public ActionResult DownloadPdf(PurchaseOrder purchaseOrder)
        {
            Session["purchaseOrderData"] = purchaseOrder;
            return RedirectToAction("PurchaseOrderPDF", "Reviewer");
        }
    }
}

