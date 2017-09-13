using AIMS.Models;
using AIMS.Models.Context;
using AIMS.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIMS.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client/ViewClient
        public ActionResult ViewClient()
        {
            return View();
        }
        public JsonResult DisplayAllClient()
        {
            try
            {
                List<Client> client = new List<Client>();
                using (var context = new InventoryDbContext())
                {
                    client = (from cl in context.TblClient
                              select new Client
                              {
                                  ClientBaseID = cl.ClientBaseID,
                                  ClientID = cl.ClientID,
                                  ClientName = cl.ClientName,
                                  Address = cl.Address,
                                  ContactNo = cl.ContactNo,
                                  TinNo = cl.TinNo
                              }).ToList();
                }
                return Json(client);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
        }
        // GET: Client/AddClient
        public JsonResult AddClient(Client client)
        {
            try
            {
                using (var context = new InventoryDbContext())
                {
                    TableClient tblClient = new TableClient()
                    {
                        ClientID = client.ClientID,
                        ClientName = client.ClientName,
                        Address = client.Address,
                        ContactNo = client.ContactNo,
                        TinNo = client.TinNo
                    };
                    context.TblClient.Add(tblClient);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
            return Json(string.Empty);
        }
    }
}