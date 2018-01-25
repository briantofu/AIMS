using AccountsContext;
using AccountsFunction;
using AccountsWebAuthentication.Helper;
using AIMS.Models;
using InventoryContext;
using InventoryEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace AIMS.Controllers
{
    public class RequestController : BaseController
    {
        private IFUser _iFUser;
        public RequestController()
        {
            _iFUser = new FUser();
        }
        // GET: Request

        [CustomAuthorize(AllowedRoles = new string[] { "DepartmentHead" })]
        public ActionResult ViewRequest()
        {
            return View();
        }

        //GET : Request/AddRequest
        [CustomAuthorize(AllowedRoles = new string[] { "Employee" })]
        public ActionResult AddRequest()
        {
            return View();
        }

        //GET : Request/AddRequest
        [CustomAuthorize(AllowedRoles = new string[] { "Employee" })]
        public ActionResult CurrentRequestStatus()
        {
            return View();
        }

        //GET ALL PENDING REQUEST
        public JsonResult AllPendingRequest()
        {
            try
            {
                //List<Account> account = new List<Account>();//account = Account model
                List<Request> requests = new List<Request>();//requests = Requests model

                using (var context = new InventoryDbContext())//Use DbInventory
                {
                    //SELECT ALL REQUEST FROM DbInventory
                    requests = (from req in context.Request
                                join loc in context.Location
                                  on req.LocationId equals loc.LocationId
                                select new Request
                                {
                                    RequestID = req.RequestId,
                                    SpecialInstruction = req.SpecialInstruction,
                                    RequisitionDate = req.RequestDate,
                                    RequiredDate = req.RequiredDate,
                                    RequisitionType = req.RequisitionType,
                                    Status = req.Status,
                                    LocationID = loc.LocationId,
                                    LocationName = loc.LocationName,
                                    UserID = req.UserId
                                }).ToList();

                }
                var users = _iFUser.Read();

                //using (var context = new AccountDbContext())//Use dbAccount
                //{
                //    var userIDs = requests.Select(b => b.UserID);

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

                //JOIN TABLE USER AND TABLE REQUEST
                requests = (from req in requests
                            join acc in users
                              on req.UserID equals acc.UserId
                            where req.Status == "Pending"
                            select new Request
                            {
                                RequestID = req.RequestID,

                                Firstname = acc.Username,
                                //Middlename = acc.Middlename,
                                //Lastname = acc.Lastname,
                                //Department = acc.Department,
                                //Contact = acc.Contact,
                                //Email = acc.Email,

                                SpecialInstruction = req.SpecialInstruction,
                                RequisitionDateString = req.RequisitionDate.ToString(),
                                RequiredDateString = req.RequiredDate.ToString(),
                                RequisitionType = req.RequisitionType,
                                Status = req.Status,

                                LocationID = req.LocationID,
                                LocationName = req.LocationName
                            }).ToList();
                return Json(requests);//Return as json
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
        }

        public JsonResult AllAcceptDecline()
        {
            try
            {
                //List<Account> account = new List<Account>();//account = Account model
                List<Request> requests = new List<Request>();//requests = Requests model

                using (var context = new InventoryDbContext())//Use DbInventory
                {
                    //SELECT ALL REQUEST FROM DbInventory
                    requests = (from req in context.Request
                                join loc in context.Location
                                  on req.LocationId equals loc.LocationId
                                select new Request
                                {
                                    RequestID = req.RequestId,
                                    SpecialInstruction = req.SpecialInstruction,
                                    RequisitionDate = req.RequestDate,
                                    RequiredDate = req.RequiredDate,
                                    RequisitionType = req.RequisitionType,
                                    Status = req.Status,
                                    LocationID = loc.LocationId,
                                    LocationName = loc.LocationName,
                                    UserID = req.UserId
                                }).ToList();

                }
                var users = _iFUser.Read();

                //using (var context = new AccountDbContext())//Use dbAccount
                //{
                //    var userIDs = requests.Select(b => b.UserID);

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

                //JOIN TABLE USER AND TABLE REQUEST
                requests = (from req in requests
                            join acc in users
                              on req.UserID equals acc.UserId
                            where req.Status != "Pending"
                            select new Request
                            {
                                RequestID = req.RequestID,

                                Firstname = acc.Username,
                                //Middlename = acc.Middlename,
                                //Lastname = acc.Lastname,
                                //Department = acc.Department,
                                //Contact = acc.Contact,
                                //Email = acc.Email,

                                SpecialInstruction = req.SpecialInstruction,
                                RequisitionDateString = req.RequisitionDate.ToString(),
                                RequiredDateString = req.RequiredDate.ToString(),
                                RequisitionType = req.RequisitionType,
                                Status = req.Status,

                                LocationID = req.LocationID,
                                LocationName = req.LocationName
                            }).ToList();
                return Json(requests);//Return as json
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
        }
        //GET ALL REQUEST
        public JsonResult AllRequest()
        {
            try
            {
                List<Account> account = new List<Account>();//account = Account model
                List<Request> requests = new List<Request>();//requests = Requests model

                using (var context = new InventoryDbContext())//Use DbInventory
                {
                    //SELECT ALL REQUEST FROM DbInventory
                    requests = (from req in context.Request
                                join loc in context.Location
                                  on req.LocationId equals loc.LocationId
                                select new Request
                                {
                                    RequestID = req.RequestId,
                                    SpecialInstruction = req.SpecialInstruction,
                                    RequisitionDate = req.RequestDate,
                                    RequiredDate = req.RequiredDate,
                                    RequisitionType = req.RequisitionType,
                                    Status = req.Status,
                                    ReasonForDeclined = req.ReasonForDeclined,
                                    LocationID = loc.LocationId,
                                    LocationName = loc.LocationName,
                                    UserID = req.UserId
                                }).ToList();

                }
                var users = _iFUser.Read();

                //using (var context = new AccountDbContext())//Use dbAccount
                //{
                //    var userIDs = requests.Select(b => b.UserID);

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

                //JOIN TABLE USER AND TABLE REQUEST
                requests = (from req in requests
                            join acc in account
                              on req.UserID equals acc.UserID
                            select new Request
                            {
                                RequestID = req.RequestID,

                                Firstname = acc.Firstname,
                                Middlename = acc.Middlename,
                                Lastname = acc.Lastname,
                                Department = acc.Department,
                                Contact = acc.Contact,
                                Email = acc.Email,

                                SpecialInstruction = req.SpecialInstruction,
                                RequisitionDateString = req.RequisitionDate.ToString(),
                                RequiredDateString = req.RequiredDate.ToString(),
                                RequisitionType = req.RequisitionType,
                                ReasonForDeclined = req.ReasonForDeclined,
                                Status = req.Status,

                                LocationID = req.LocationID,
                                LocationName = req.LocationName
                            }).ToList();
                return Json(requests);//Return as json
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
        }

        //GET ALL ITEM
        public JsonResult AllItem()
        {
            try
            {
                List<InventoryItem> item = new List<InventoryItem>();//requests = Requests model
                using (var context = new InventoryDbContext())
                {
                    var allItem = from invItem in context.InventoryItem
                                  join uom in context.UnitOfMeasurement
                                    on invItem.UnitOfMeasurementId equals uom.UnitOfMeasurementId
                                  select new
                                  {
                                      InventoryItemID = invItem.InventoryItemId,
                                      ItemName = invItem.ItemName,
                                      UnitOfMeasurementID = uom.UnitOfMeasurementId,
                                      UnitDescription = uom.Description
                                  };
                    item = allItem.Select(
                        ai => new InventoryItem
                        {
                            InventoryItemID = ai.InventoryItemID,
                            ItemName = ai.ItemName,
                            UnitOfMeasurement = new UnitOfMeasurement
                            {
                                UnitOfMeasurementID = ai.UnitOfMeasurementID,
                                Description = ai.UnitDescription
                            }
                        }).ToList();
                }
                return Json(item);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
        }

        //VIEW REQUEST ITEM
        public JsonResult RequestItem(int requestID)
        {
            try
            {
                List<RequestItem> requestItem = new List<RequestItem>();//Exam = Exam model
                using (var context = new InventoryDbContext())
                {
                    var displayItem = from req in context.Request
                                      join reqItem in context.RequestItem
                                        on req.RequestId equals reqItem.RequestId
                                      join inv in context.InventoryItem
                                        on reqItem.InventoryItemId equals inv.InventoryItemId
                                      join uom in context.UnitOfMeasurement
                                        on inv.UnitOfMeasurementId equals uom.UnitOfMeasurementId
                                      where reqItem.RequestId == requestID
                                      select new
                                      {
                                          InventoryItemId = inv.InventoryItemId,
                                          ItemName = inv.ItemName,
                                          UnitDescription = uom.Description,
                                          Quantity = reqItem.Quantity,
                                          ItemDescription = reqItem.Description,
                                          SpecialInstruction = req.SpecialInstruction
                                      };
                    requestItem = displayItem.Select(
                        ri => new RequestItem
                        {
                            InventoryItemID = ri.InventoryItemId,
                            ItemName = ri.ItemName,
                            UnitOfMeasurement = ri.UnitDescription,
                            Quantity = ri.Quantity,
                            Description = ri.ItemDescription
                        }).ToList();
                }

                //DataTable dtRequestItem = dbManager.SqlReader(
                //           "SELECT " +
                //                "i.InventoryItemId,i.Name,uom.Description as uomDesc,ri.Quantity,ri.Description,r.SpecialInstruction " +
                //            "FROM " +
                //                "DB_INVENTORY.dbo.tbl_Request r " +
                //            "JOIN " +
                //                "DB_ACCOUNTS.dbo.tbl_User u " +
                //            "ON " +
                //                "r.UserId = u.UserId " +
                //            "JOIN " +
                //                "DB_INVENTORY.dbo.tbl_RequestItem ri " +
                //            "ON " +
                //                "r.RequestId = ri.RequestId " +
                //            "JOIN " +
                //                "DB_INVENTORY.dbo.tbl_InventoryItem i " +
                //            "ON " +
                //                "i.InventoryItemId = ri.InventoryItemId " +
                //            "JOIN " +
                //                "tbl_UnitOfMeasurement uom " +
                //            "ON " +
                //                "i.UnitOfMeasurementId = uom.UnitOfMeasurementId " +
                //            "WHERE " +
                //                 "ri.RequestId = @requestid"
                //            , "tblRequestItem", parameters);
                return Json(requestItem);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
        }

        // ADD REQUEST
        [HttpPost]
        public JsonResult AddRequestItem(Request request, int LocationID)
        {
            try
            {
                using (var context = new InventoryDbContext())//Use DbInventory
                {
                  
                    ERequest eRequest = new ERequest()
                    {
                        UserId = UserId,
                        RequiredDate = request.RequiredDate,
                        //RequisitionType = request.RequisitionType,
                        SpecialInstruction = request.SpecialInstruction,
                        LocationId = LocationID,
                        Status = "Pending"
                    };
                    context.Request.Add(eRequest);//Add Data
                    context.SaveChanges();//Execute insert or changes

                    int currentRequestID = 0;//initialize currentID
                    //Select current requestID
                    int reqID = (from req in context.Request
                                 orderby req.RequestId descending
                                 select req.RequestId).FirstOrDefault();
                    currentRequestID = reqID;//Put value to inventoryItemID
                    foreach (var items in request.RequestItems)
                    {
                        ERequestItem eRequestItem = new ERequestItem()
                        {
                            RequestId = currentRequestID,
                            InventoryItemId = items.InventoryItemID,
                            Quantity = items.Quantity,
                            Description = items.Description
                        };
                        context.RequestItem.Add(eRequestItem);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
            return Json(string.Empty);
        }

        //ACCEPT REQUEST
        [HttpPost]
        public JsonResult AcceptRequest(int requestID, Request request)
        {
            try
            {
                var updateStatus = new ERequest()
                {
                    RequestId = requestID,
                    Status = "Accepted"

                };
                using (var context = new InventoryDbContext())
                {
                    context.Request.Attach(updateStatus);
                    context.Entry(updateStatus).Property(x => x.Status).IsModified = true;
                    context.SaveChanges();
                }



                return Json(string.Empty);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
        }

        //REJECT REQUEST
        [HttpPost]
        public JsonResult DeclineRequest(int requestID, string status, string reasonForDeclining)
        {
            try
            {
             
                using (var context = new InventoryDbContext())
                {
                    var eRequest = context.Request.FirstOrDefault(x => x.RequestId == requestID);
                    eRequest.Status = status;
                    eRequest.ReasonForDeclined = reasonForDeclining;
                    context.SaveChanges();//Save all changes
                    
                }
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
            return Json(string.Empty);
        }
        
        ////DISPLAY ALL EXISTING UNIT OF MEASUREMENT
        //public JsonResult AllUnitOfMeasurement()
        //{
        //    return Json(requestbll.AllUnitOfMeasurement());
        //}

        ////ADD NEW UNIT OF MEASUREMENT
        //[HttpPost]
        //public JsonResult AddNewUnitOfMeasurement(string unitOfMeasurement) //Add new Unit Of Measurement
        //{
        //    requestbll.AddNewUnitOfMeasurement(unitOfMeasurement);
        //    return Json(string.Empty);
        //}

        ////ADD NEW ITEM
        //[HttpPost]
        //public JsonResult AddNewItem(string newItemName, string unitOfMeasurement) //Add new item
        //{
        //    requestbll.AddNewItem(newItemName, unitOfMeasurement);
        //    return Json(string.Empty);
        //}
    }
}