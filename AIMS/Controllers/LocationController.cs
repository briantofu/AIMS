using AccountsWebAuthentication.Helper;
using AIMS.Models;
using InventoryContext;
using InventoryEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AIMS.Controllers
{
    public class LocationController : Controller
    {
        // GET: Location
        [CustomAuthorize(AllowedRoles = new string[] { "PurchasingOfficer" })]
        public ActionResult ManageLocation()
        {
            return View();
        }

     

        //=====LOAD PAGE=====//
        public JsonResult LoadPages(Page page)
        {
            int totalpages = 0;
            var totalpositions = 0;

            using (var context = new InventoryDbContext())
            {
                totalpositions = context.Location.Count();
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
            List<Account> account = new List<Account>();//account = Account model
            List<Requisition> requisition = new List<Requisition>();//requisitions = Requisitions model
            try
            {

                List<Location> location = new List<Location>();//Initialize Location Model
                using (var context = new InventoryDbContext())//Use DbInventory
                {
                    //Get all existing location from database
                    location = (from loc in context.Location
                                select new Location
                                {
                                    LocationID = loc.LocationId,
                                    LocationName = loc.LocationName,
                                    LocationAddress = loc.LocationAddress
                                }).OrderBy(e => e.LocationID).Skip(beginning).Take(page.itemPerPage).ToList().ToList();
                    return Json(location);//return data as json
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        //ADD NEW LOCATION
        [HttpPost]
        public JsonResult AddNewLocation(Location location)
        {
            try
            {
                using (var context = new InventoryDbContext())//Use DbInventory
                {
                    if (context.Location.Any(l => l.LocationName.ToLower() == location.LocationName.ToLower()))
                    {
                        return Json("LocationExists");
                    }
                    else
                    {
                        //Add Location Name
                        ELocation eLocation = new ELocation()
                        {
                            LocationName = location.LocationName,
                            LocationAddress = location.LocationAddress
                        };
                        context.Location.Add(eLocation);//Insert new location
                        context.SaveChanges();//Save all changes

                        return Json(string.Empty);
                    }
                }
            }
            catch (Exception e)
            {
                return Json(e.ToString());
            }
        }

        //UPDATE LOCATION 
        [HttpPost]
        public JsonResult UpdateLocation(Location location)
        {
            try
            {
                using (var context = new InventoryDbContext())//use DbInventory
                {
                    if (context.Location.Any(l => l.LocationName.ToLower() == location.LocationName.ToLower()))
                    {
                        return Json("SameLocationName");
                    }
                    else
                    {
                        //Select specific location
                        var tblLocation = context.Location.FirstOrDefault(x => x.LocationId == location.LocationID);
                        tblLocation.LocationName = location.LocationName;//assign new location name
                        context.SaveChanges();//save all changes
                        return Json(string.Empty);
                    }
                }
            }
            catch (Exception e)
            {
                return Json(e.ToString());
            }
        }

        ////DISPLAY ALL LOCATION
        //public JsonResult DisplayAllLocation()
        //{
        //    try
        //    {
        //        List<Location> location = new List<Location>();//Initialize Location Model
        //        using (var context = new InventoryDbContext())//Use DbInventory
        //        {
        //            //Get all existing location from database
        //            var displayLocation = from loc in context.Location
        //                                  select loc;
        //            location = displayLocation.Select(
        //                loc => new Location
        //                {
        //                    LocationID = loc.LocationId,
        //                    LocationName = loc.LocationName
        //                }).ToList();

        //            return Json(location);//return data as json
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(e.ToString());
        //    }
        //}
    }
}