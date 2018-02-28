using AccountsContext;
using AccountsFunction;
using AccountsWebAuthentication.Helper;
using AIMS.Classes;
using AIMS.Models;
using ElectronicMailNotification.Models;
using InventoryContext;
using InventoryEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace AIMS.Controllers
{
    public class RequisitionController : BaseController
    {
        private IFUser _iFUser;

        // GET: Requisition/AddRequisition
        public RequisitionController()
        {
            _iFUser = new FUser();
        }
        [CustomAuthorize(AllowedRoles = new string[] { "Receptionist" })]
        public ActionResult AddRequisition()
        {
            return View();
        }

        // GET: Requisition/ApprovedRequisition
        [CustomAuthorize(AllowedRoles = new string[] { "Receptionist" })]
        public ActionResult DeliveryAcceptance()
        {
            return View();
        }

        // GET: Requisition/DeclinedRequisition
        [CustomAuthorize(AllowedRoles = new string[] { "Receptionist" })]
        public ActionResult DeclinedRequisition()
        {
            return View();
        }

        // GET: Requisition/CurrentRequisitionStatus
        [CustomAuthorize(AllowedRoles = new string[] { "Receptionist" })]
        public ActionResult AllRequisition()
        {
            return View();
        }

        // GET: Requisition/CurrentRequisitionStatus
        [CustomAuthorize(AllowedRoles = new string[] { "Receptionist" })]
        public ActionResult PartialDelivery()
        {
            return View();
        }

        //DISPLAY ALL ITEMS FROM INVENTORY
        public JsonResult AllItem()
        {
            try
            {
                List<InventoryItem> item = new List<InventoryItem>();//requisitions = Requisitions model
                using (var context = new InventoryDbContext())
                {
                    //Get all existing item
                    var displayAllItem = from invItem in context.InventoryItem
                                         join uom in context.UnitOfMeasurement
                                            on invItem.UnitOfMeasurementId equals uom.UnitOfMeasurementId
                                         select new
                                         {
                                             InventoryItemID = invItem.InventoryItemId,
                                             ItemName = invItem.ItemName,
                                             ItemCode = invItem.ItemCode,
                                             UnitOfMeasurementID = uom.UnitOfMeasurementId,
                                             UnitDescription = uom.Description
                                         };
                    //Put all data to InventoryItem List
                    item = displayAllItem.Select(
                        allItem => new InventoryItem
                        {
                            InventoryItemID = allItem.InventoryItemID,
                            ItemName = allItem.ItemName,
                            ItemCode = allItem.ItemCode,
                            UnitOfMeasurement = new UnitOfMeasurement
                            {
                                UnitOfMeasurementID = allItem.UnitOfMeasurementID,
                                Description = allItem.UnitDescription
                            }
                        }).ToList();
                }
                return Json(item);//return data as json
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        //DISPLAY ALL EXISTING UNIT OF MEASUREMENT
        public JsonResult AllUnitOfMeasurement()
        {
            try
            {
                List<UnitOfMeasurement> unitOfMeasurement = new List<UnitOfMeasurement>();//requisitions = Requisitions model
                using (var context = new InventoryDbContext())//Use to select database(DbInventory)
                {
                    //Get all existing UnitOfMeasurement
                    var displayAllUOM = from uom in context.UnitOfMeasurement
                                        select uom;
                    //Put all data to InventoryItem
                    unitOfMeasurement = displayAllUOM.Select(
                        uom => new UnitOfMeasurement
                        {
                            UnitOfMeasurementID = uom.UnitOfMeasurementId,
                            Description = uom.Description
                        }).ToList();
                }
                return Json(unitOfMeasurement);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        //DISPLAY ALL ITEMS VIA SUPPLIER NAME
        public JsonResult DisplayViaSupplier(Supplier supplier)
        {
            try
            {
                List<InventoryItem> supplierItem = new List<InventoryItem>();//Initialize the model
                using (var context = new InventoryDbContext())
                {
                    //Display all item depends on supplier
                    var displayBySupplier = from invItem in context.InventoryItem
                                            join uom in context.UnitOfMeasurement
                                                on invItem.UnitOfMeasurementId equals uom.UnitOfMeasurementId
                                            join suppInvItem in context.SupplierInventoryItem
                                                on invItem.InventoryItemId equals suppInvItem.InventoryId into isi
                                            from suppInvItem in isi.DefaultIfEmpty()
                                            where (suppInvItem.SupplierId == supplier.SupplierID)
                                            select new
                                            {
                                                InventoryItemID = invItem.InventoryItemId,
                                                ItemName = invItem.ItemName,
                                                ItemCode = invItem.ItemCode,
                                                UnitOfMeasurementID = uom.UnitOfMeasurementId,
                                                UnitDescription = uom.Description,
                                                SupplierID = suppInvItem.SupplierId
                                            };
                    //Put all data to Inventory Item List 
                    supplierItem = displayBySupplier.Select(
                        dbs => new InventoryItem
                        {
                            InventoryItemID = dbs.InventoryItemID,
                            ItemName = dbs.ItemName,
                            ItemCode = dbs.ItemCode,
                            UnitOfMeasurement = new UnitOfMeasurement
                            {
                                UnitOfMeasurementID = dbs.UnitOfMeasurementID,
                                Description = dbs.UnitDescription
                            }

                        }).ToList();
                }
                return Json(supplierItem); //return as json data
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        //DISPLAY ALL LOCATION
        public JsonResult DisplayLocation()
        {
            try
            {
                List<Location> location = new List<Location>();//Exam = Exam model

                using (var context = new InventoryDbContext())
                {
                    //Get all existing supplier
                    var displayLocation = from loc in context.Location
                                          select new
                                          {
                                              LocationID = loc.LocationId,
                                              LocationName = loc.LocationName
                                          };
                    //Put all data to Supplier List 
                    location = displayLocation.Select(
                        displayLoc => new Location
                        {
                            LocationID = displayLoc.LocationID,
                            LocationName = displayLoc.LocationName
                        }).ToList();
                }
                return Json(location);//return as json
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        //=====LOAD PAGE=====//
        public JsonResult LoadPages(Page page)
        {
            int totalpages = 0;
            var totalpositions = 0;

            using (var context = new InventoryDbContext())
            {
                totalpositions = context.Requisition.Where(req => (req.Status != "Approved" && req.Status != "Declined")).Count();
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

        public JsonResult LoadDeliveredPages(Page page)
            //For loading Delivery Section of Requesition
        {
            int totalpages = 0;
            var totalpositions = 0;

            using (var context = new InventoryDbContext())
            {
                totalpositions = context.Requisition.Where(req => (req.Status == "Delivered" )).Count();
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
        public JsonResult LoadPageData(Page page)
        {
            int beginning = page.itemPerPage * (page.PageNumber - 1);
            //List<Account> account = new List<Account>();//account = Account model
            List<Requisition> requisition = new List<Requisition>();//requisitions = Requisitions model
            try
            {
                using (var context = new InventoryDbContext())//Use dbInventory
                {
                    requisition = (from req in context.Requisition
                                   join loc in context.Location on req.LocationId equals loc.LocationId
                                   
                                   select new Requisition
                                   {
                                       RequisitionID = req.RequisitionId,
                                       SpecialInstruction = req.SpecialInstruction,
                                       RequisitionDate = req.RequisitionDate,
                                       RequiredDate = req.RequiredDate,
                                       RequisitionType = req.RequisitionType,
                                       Status = req.Status,
                                       Reason = req.ReasonForDeclined,
                                       SupplierID = req.SupplierId,

                                       LocationID = loc.LocationId,
                                       LocationName = loc.LocationName,

                                       UserID = req.UserId.Value,

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
                                   LocationName = req.LocationName
                               }).OrderBy(e => e.RequisitionID).Skip(beginning).Take(page.itemPerPage).ToList().ToList();
                return Json(requisition);//Return data as json
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        public JsonResult LoadPageDeliveredData(Page page)
        {
            int beginning = page.itemPerPage * (page.PageNumber - 1);
            //List<Account> account = new List<Account>();//account = Account model
            List<Requisition> requisition = new List<Requisition>();//requisitions = Requisitions model
            try
            {
                using (var context = new InventoryDbContext())//Use dbInventory
                {
                    requisition = (from req in context.Requisition
                                   join loc in context.Location on req.LocationId equals loc.LocationId
                                   //where (req.Status != "Approved" && req.Status != "Declined")
                                   where (req.Status == "Delivered")
                                   select new Requisition
                                   {
                                       RequisitionID = req.RequisitionId,
                                       SpecialInstruction = req.SpecialInstruction,
                                       RequisitionDate = req.RequisitionDate,
                                       RequiredDate = req.RequiredDate,
                                       RequisitionType = req.RequisitionType,
                                       Status = req.Status,
                                       Reason = req.ReasonForDeclined,
                                       SupplierID = req.SupplierId,

                                       LocationID = loc.LocationId,
                                       LocationName = loc.LocationName,

                                       UserID = req.UserId.Value,

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
                                   LocationName = req.LocationName
                               }).OrderBy(e => e.RequisitionID).Skip(beginning).Take(page.itemPerPage).ToList().ToList();
                return Json(requisition);//Return data as json
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }
        //VIEW PURCHASE ORDER
        public JsonResult ViewPurchaseOrder(Requisition requisition)

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


        //public JsonResult LoadApprovedRequisitionPages(Page page, string Status)
        //{
        //    int totalpages = 0;
        //    var totalpositions = 0;

        //    using (var context = new InventoryDbContext())
        //    {
        //        totalpositions = context.Requisition.Where(req => req.Status == Status).Count();
        //    }

        //    if (totalpositions % page.itemPerPage != 0)
        //    {
        //        totalpages = (totalpositions / page.itemPerPage) + 1;
        //    }
        //    else
        //    {
        //        totalpages = (totalpositions / page.itemPerPage);
        //    }
        //    List<Page> pages = new List<Page>();
        //    for (int x = 1; x <= totalpages; x++)
        //    {
        //        if (x == page.PageNumber)
        //        {
        //            pages.Add(
        //            new Page
        //            {
        //                PageNumber = x,
        //                PageStatus = true
        //            });
        //        }
        //        else
        //        {
        //            pages.Add(
        //            new Page
        //            {
        //                PageNumber = x,
        //                PageStatus = false
        //            });
        //        }
        //    }
        //    return Json(pages);
        //}

        ////=====LOAD DATA=====//
        //public JsonResult LoadApprovedRequisitionPageData(Page page, string Status)
        //{
        //    int beginning = page.itemPerPage * (page.PageNumber - 1);
        //    List<Account> account = new List<Account>();//account = Account model
        //    List<Requisition> requisition = new List<Requisition>();//requisitions = Requisitions model
        //    try
        //    {
        //        using (var context = new InventoryDbContext())//Use dbInventory
        //        {
        //            requisition = (from req in context.Requisition
        //                           join po in context.PurchasingOrder on req.RequisitionId equals po.RequisitionId
        //                           join loc in context.Location on req.LocationId equals loc.LocationId
        //                           where req.Status == Status
        //                           select new Requisition
        //                           {
        //                               RequisitionID = req.RequisitionId,
        //                               SpecialInstruction = req.SpecialInstruction,
        //                               RequisitionDate = req.RequisitionDate,
        //                               RequiredDate = req.RequiredDate,
        //                               RequisitionType = req.RequisitionType,
        //                               Status = req.Status,
        //                               SupplierID = po.SupplierId,
        //                               PurchaseOrderID = po.PurchaseOrderId,
        //                               Reason = req.ReasonForDeclined,
        //                               LocationID = loc.LocationId,
        //                               LocationName = loc.LocationName,
        //                               LocationAddress = loc.LocationAddress,
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
        //                           PurchaseOrderID = req.PurchaseOrderID,
        //                           Reason = req.Reason,
        //                           LocationID = req.LocationID,
        //                           LocationName = req.LocationName,
        //                           LocationAddress = req.LocationAddress,
        //                       }).OrderBy(e => e.RequisitionID).Skip(beginning).Take(page.itemPerPage).ToList().ToList();
        //        return Json(requisition);//Return data as json
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex);
        //    }
        //}

        //VIEW REQUISITION ITEM
        public JsonResult DisplayRequisitionItem(Requisition requisition)
        {
            try
            {
                List<RequisitionItem> requisitionItem = new List<RequisitionItem>();//Initialize Requisition model
                List<Supplier> supplier = new List<Supplier>();//Initialize Requisition model
                List<PurchaseOrder> purchaseOrder = new List<PurchaseOrder>();//Initialize Requisition model
                using (var context = new InventoryDbContext())
                {
                    ////Query for getting requisitionItem
                    //requisitionItem = from req in context.Requisition
                    //                  join reqItem in context.RequisitionItem on req.RequisitionId equals reqItem.RequisitionId
                    //                  join inv in context.InventoryItem on reqItem.InventoryItemId equals inv.InventoryItemId
                    //                  join uom in context.UnitOfMeasurement on inv.UnitOfMeasurementId equals uom.UnitOfMeasurementId
                    //                  where reqItem.RequisitionId == requisition.RequisitionID && reqItem.PurchaseOrderId == requisition.PurchaseOrderID
                    //                  select new
                    //                  {
                    //                      RequisitionItemID = reqItem.RequisitionItemId,
                    //                      InventoryItemID = inv.InventoryItemId,
                    //                      ItemName = inv.ItemName,
                    //                      UnitOfMeasurement = uom.Description,
                    //                      Quantity = reqItem.Quantity,
                    //                      Description = reqItem.Description,
                    //                      UnitPrice = reqItem.UnitPrice,
                    //                      PurchaseOrderId = reqItem.PurchaseOrderId,
                    //                      SupplierID = req.SupplierId,
                    //                  };

                    purchaseOrder = (from supp in context.Supplier
                                     where supp.SupplierId == requisition.SupplierID
                                     select new PurchaseOrder
                                     {
                                         TinNumber = supp.TinNumber,
                                         SupplierID = supp.SupplierId,
                                         SupplierName = supp.SupplierName,
                                         SupplierAddress = supp.Address,
                                         ContactPerson = supp.ContactPerson,
                                         ContactNo = supp.ContactNo,
                                         Email = supp.Email, 
                                         RequisitionItems = (from req in context.Requisition
                                                             join reqItem in context.RequisitionItem on req.RequisitionId equals reqItem.RequisitionId
                                                             join inv in context.InventoryItem on reqItem.InventoryItemId equals inv.InventoryItemId
                                                             join uom in context.UnitOfMeasurement on inv.UnitOfMeasurementId equals uom.UnitOfMeasurementId
                                                             where reqItem.RequisitionId == requisition.RequisitionID && reqItem.PurchaseOrderId == requisition.PurchaseOrderID
                                                             select new RequisitionItem
                                                             {
                                                                 RequisitionItemID = reqItem.RequisitionItemId,
                                                                 InventoryItemID = inv.InventoryItemId,
                                                                 ItemName = inv.ItemName,
                                                                 UnitOfMeasurement = uom.Description,
                                                                 Quantity = reqItem.Quantity,
                                                                 Description = reqItem.Description,
                                                                 UnitPrice = reqItem.UnitPrice,
                                                                 PurchaseOrderId = reqItem.PurchaseOrderId,
                                                                 SupplierID = req.SupplierId,
                                                             }).ToList()
                                     }).ToList();

                    //requisitionItem = (from reqItems in requisitionItems
                    //                   join suppInfo in suppliers on reqItems.SupplierID equals suppInfo.SupplierID
                    //                   select new RequisitionItem
                    //                   {
                    //                       RequisitionItemID = reqItems.RequisitionItemID,
                    //                       InventoryItemID = reqItems.InventoryItemID,
                    //                       ItemName = reqItems.ItemName,
                    //                       UnitOfMeasurement = reqItems.UnitOfMeasurement,
                    //                       Quantity = reqItems.Quantity,
                    //                       Description = reqItems.Description,
                    //                       UnitPrice = reqItems.UnitPrice,
                    //                       PurchaseOrderId = reqItems.PurchaseOrderId,

                    //                       TinNumber = suppInfo.TinNumber,
                    //                       SupplierID = (int?)suppInfo.SupplierID,
                    //                       SupplierName = suppInfo.SupplierName,
                    //                       Address = suppInfo.Address,
                    //                       ContactPerson = suppInfo.ContactPerson,
                    //                       ContactNo = suppInfo.ContactNo,
                    //                       Email = suppInfo.Email
                    //                   }).ToList();


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

                return Json(purchaseOrder);//send data as Json
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        //VIEW REQUISITION ITEM
        public JsonResult PartialRequisitionItem(Requisition requisition)
        {
            try
            {
                List<RequisitionItem> requisitionItem = new List<RequisitionItem>();//Initialize Requisition model
                List<Supplier> supplier = new List<Supplier>();//Initialize Requisition model
                using (var context = new InventoryDbContext())
                {
                    //Query for getting requisitionItem
                    var requisitionItems = from req in context.Requisition
                                           join reqItem in context.RequisitionItem on req.RequisitionId equals reqItem.RequisitionId
                                           join inv in context.InventoryItem on reqItem.InventoryItemId equals inv.InventoryItemId
                                           join uom in context.UnitOfMeasurement on inv.UnitOfMeasurementId equals uom.UnitOfMeasurementId
                                           join pdItem in context.PartialDeliveryItem on reqItem.RequisitionItemId equals pdItem.RequisitionItemId
                                           where reqItem.RequisitionId == requisition.RequisitionID
                                           group new { reqItem, inv, pdItem, uom, req } by new { reqItem.RequisitionItemId, inv.InventoryItemId, reqItem.Quantity } into g
                                           select new
                                           {
                                               RequisitionItemID = g.Select(a => a.reqItem.RequisitionItemId).FirstOrDefault(),
                                               InventoryItemID = g.Select(a => a.inv.InventoryItemId).FirstOrDefault(),
                                               ItemName = g.Select(a => a.inv.ItemName).FirstOrDefault(),
                                               UnitDescription = g.Select(a => a.uom.Description).FirstOrDefault(),
                                               Quantity = g.Select(a => a.reqItem.Quantity).FirstOrDefault(),
                                               ItemDescription = g.Select(a => a.reqItem.Description).FirstOrDefault(),
                                               UnitPrice = g.Select(a => a.reqItem.UnitPrice).FirstOrDefault(),
                                               SupplierID = g.Select(a => a.req.SupplierId).FirstOrDefault(),
                                               BalanceQuantity = g.Select(a => a.reqItem.Quantity).FirstOrDefault() - g.Sum(a => a.pdItem.DeliveredQuantity)
                                           };
                    var supplierInfo = from supp in context.Supplier
                                       where supp.SupplierId == requisition.SupplierID
                                       select supp;

                    var reqItemAndSupp = from reqItems in requisitionItems
                                         join suppInfo in supplierInfo on reqItems.SupplierID equals suppInfo.SupplierId
                                         into g
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
                                             BalanceQuantity = reqItems.BalanceQuantity,

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
                        BalanceQuantity = ri.BalanceQuantity,

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

        //----------------------------------- ADD NEW UNIT OF MEASUREMENT ----------------------
        [HttpPost]
        public JsonResult AddNewUnitOfMeasurement(string unitDescription) //Add new Unit Of Measurement
        {
            try
            {
                using (var context = new InventoryDbContext())
                {
                    EUnitOfMeasurement tblUOM = new EUnitOfMeasurement()
                    {
                        Description = unitDescription
                    };
                    context.UnitOfMeasurement.Add(tblUOM);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
            return Json(string.Empty);
        }

        //----------------------------------- ADD NEW ITEM ---------------------------------------
        [HttpPost]
        public JsonResult AddNewItem(string newItemName, int unitOfMeasurementID, string newItemCode)//, int supplierID  //Add new item
        {
            try
            {

                using (var context = new InventoryDbContext())
                {
                    var tblInventoryItem = context.InventoryItem.FirstOrDefault(a => (a.ItemName.ToLower() == newItemName.ToLower() && a.ItemCode.ToLower() == newItemCode.ToLower() && a.UnitOfMeasurementId == unitOfMeasurementID));
                    if (tblInventoryItem == null)
                    {
                        //string name;
                        //if (newItemName == null)
                        //{
                        //    name = inventoryItem.ItemName;
                        //}
                        //else
                        //{
                        //    name = newItemName;
                        //}
                        EInventoryItem eInventoryItem = new EInventoryItem()
                        {
                            ItemName = newItemName,
                            UnitOfMeasurementId = unitOfMeasurementID,
                            ItemCode = newItemCode,
                        };
                        context.InventoryItem.Add(eInventoryItem);
                        context.SaveChanges();

                        tblInventoryItem = eInventoryItem;

                        return Json(string.Empty);
                    }
                    else
                    {
                        return Json("ItemExist");

                    }

                    //if (context.SupplierInventoryItem.Any(o => o.SupplierID == supplierID && o.InventoryID == eInventoryItem.InventoryItemID))
                    //{
                    //    return Json("ItemExist");
                    //}
                    //else
                    //{
                    //    //Insert new supplier item (if there is supplier selected)
                    //    TableSupplierInventoryItem tblSupplierItem = new TableSupplierInventoryItem()
                    //    {
                    //        SupplierID = supplierID,
                    //        InventoryID = tblInventoryItem.InventoryItemID
                    //    };
                    //    context.SupplierInventoryItem.Add(tblSupplierItem);
                    //    context.SaveChanges();//commit changes
                    //}
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        //----------------------------------- ADD REQUEST ---------------------------------------
        [HttpPost]
        public JsonResult AddRequisitionItem(Requisition requisition, int LocationID) // ElectronicMail electronicMail
        {
            try
            {
                //Use for selecting dbInventory
                using (var context = new InventoryDbContext())
                {
                   
                    //Insert into tblRequisition
                    ERequisition eRequisiiton = new ERequisition()
                    {
                        UserId = UserId,
                        RequiredDate = requisition.RequiredDate,
                        RequisitionType = requisition.RequisitionType,
                        SpecialInstruction = requisition.SpecialInstruction,
                        LocationId = LocationID,
                        Status = requisition.Status
                    };
                    context.Requisition.Add(eRequisiiton);//Add data
                    context.SaveChanges();//Execute changes

                    int currentRequisitionID = 0;//Initialize currentID
                    //Select current InventoryItemID
                    int requisitionID = (from req in context.Requisition
                                         orderby req.RequisitionId descending
                                         select req.RequisitionId).FirstOrDefault();
                    currentRequisitionID = requisitionID;//Put value to inventoryItemID

                    //Loop all items
                    foreach (var items in requisition.RequisitionItems)
                    {
                        //Insert into tblRequisitionItem
                        ERequisitionItem eRequisitionItem = new ERequisitionItem()
                        {
                            RequisitionId = currentRequisitionID,
                            InventoryItemId = items.InventoryItemID,
                            Quantity = items.Quantity,
                            Description = items.Description,
                            UnitPrice = items.UnitPrice
                        };
                        context.RequisitionItem.Add(eRequisitionItem);//Add data
                        context.SaveChanges();//Execute Changes
                    }
                }
                //electronicMail.Send();

            }
            catch (Exception ex)
            {
                return Json(ex);
            }
            return Json(string.Empty);
        }

        //----------------------------------- UPDATE REQUISITION ---------------------------------------
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
        //----------------------------------- DELIVERED REQUISITION ---------------------------------------
        [HttpPost]
        public JsonResult DeliverRequisition(int requisitionID, Requisition requisition, PartialDelivery partialDelivery)
        {
            try
            {
                List<RequisitionItem> requisitionItem = new List<RequisitionItem>();
                bool isFullyDelivered = false;
                using (var context = new InventoryDbContext())//Use DbInventory
                {
                    var eRequisition = context.Requisition.FirstOrDefault(x => x.RequisitionId == requisitionID);
                    eRequisition.Status = requisition.Status;
                    context.SaveChanges();//Save status

                    if (eRequisition != null)
                    {
                        EPartialDelivery ePartialDelivery = new EPartialDelivery()
                        {
                            RequisitionId = requisitionID,
                            SupplierInvoiceNo = partialDelivery.SupplierInvoiceNo,
                            DeliveryDate = partialDelivery.DeliveryDate,
                            DeliveryReceiptNo = partialDelivery.DeliveryReceiptNo,
                            SupplierId = requisition.SupplierID,
                        };
                        context.PartialDelivery.Add(ePartialDelivery);//Add data
                        context.SaveChanges();//Execute Changes

                        if (ePartialDelivery != null)
                        {
                            var quantity = 0;
                            foreach (var item in requisition.RequisitionItems)
                            {
                                if (item.isItemSelected)
                                    quantity = requisition.Status == "Delivered" ? item.Quantity : item.DeliveredQuantity;
                                else
                                    quantity = 0;
                                EPartialDeliveryItem ePartialDeliveryItem = new EPartialDeliveryItem()
                                {
                                    RequisitionItemId = item.RequisitionItemID,
                                    DeliveredQuantity = quantity,
                                    PartialDeliveryId = ePartialDelivery.PartialDeliveryId

                                };
                                context.PartialDeliveryItem.Add(ePartialDeliveryItem);//Add data
                                context.SaveChanges();//Execute Changes
                            }

                            requisitionItem = (from req in context.Requisition
                                               join reqItem in context.RequisitionItem on req.RequisitionId equals reqItem.RequisitionId
                                               join inv in context.InventoryItem on reqItem.InventoryItemId equals inv.InventoryItemId
                                               join pdItem in context.PartialDeliveryItem on reqItem.RequisitionItemId equals pdItem.RequisitionItemId
                                               where req.RequisitionId == requisitionID
                                               group new { reqItem, inv, pdItem } by new { reqItem.RequisitionItemId, inv.InventoryItemId, reqItem.Quantity } into g
                                               select new RequisitionItem
                                               {
                                                   RequisitionItemID = g.Select(a => a.reqItem.RequisitionItemId).FirstOrDefault(),
                                                   ItemName = g.Select(a => a.inv.ItemName).FirstOrDefault(),
                                                   BalanceQuantity = g.Select(a => a.reqItem.Quantity).FirstOrDefault() - g.Sum(a => a.pdItem.DeliveredQuantity)
                                               }).ToList();
                            foreach (var item in requisitionItem)
                            {
                                isFullyDelivered = item.BalanceQuantity <= 0;

                                if (!isFullyDelivered) break;
                            }
                            string status = "";

                            status = isFullyDelivered ? "Delivered" : "Partial Delivery";

                            var eRequisitionx = context.Requisition.FirstOrDefault(x => x.RequisitionId == requisitionID);
                            eRequisitionx.Status = status;
                            context.SaveChanges();//Save status
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
            return Json(string.Empty);
        }

        //DISPLAY PENDING REQUISITION
        //public JsonResult DisplayPendingRequisition()
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
        //                           where (req.Status != "Approved" && req.Status != "Declined")
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
        //                           RequisitionDateString = req.RequisitionDate.ToString(),
        //                           RequiredDateString = req.RequiredDate.ToString(),
        //                           RequisitionType = req.RequisitionType,
        //                           Status = req.Status,
        //                           SupplierID = req.SupplierID,
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
        //=====LOAD TOTAL PAGES======//
    }
}
