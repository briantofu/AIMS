using AIMS.Helper;
using AIMS.Models;
using InventoryContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace AIMS.Controllers
{
    public class InventoryController : Controller
    {

        // GET: Inventory
        public ActionResult Stocks()
        {
            return View();
        }

        public ActionResult BeginningBalance()
        {
            return View();
        }

        //=====LOAD ALL PAGE=====//
        public JsonResult LoadPages(Page page)
        {
            int totalpages = 0;
            var totalpositions = 0;

            using (var context = new InventoryDbContext())
            {
                totalpositions = context.InventoryItem.Count();
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
            List<Stocks> stocks = new List<Stocks>();
            using (var context = new InventoryDbContext())
            {
                var computeRemainingQuantity = from invItem in context.InventoryItem
                                               join reqItem in context.RequisitionItem on invItem.InventoryItemId equals reqItem.InventoryItemId into joined
                                               from invItemj in joined.DefaultIfEmpty()
                                               join pdItem in context.PartialDeliveryItem on invItemj.RequisitionItemId equals pdItem.RequisitionItemId into joined2
                                               from reqItemj in joined2.DefaultIfEmpty()
                                               group new { invItem, reqItemj } by new { invItem.InventoryItemId } into g
                                               select new
                                               {
                                                   InventoryItemID = g.FirstOrDefault().invItem.InventoryItemId,
                                                   RemainingQuantity = g.Sum(a => (a.reqItemj == null ? 0 : a.reqItemj.DeliveredQuantity))
                                               };

                var computeRequestedQuantity = from invItem in context.InventoryItem
                                               join reqItem in context.RequestItem on invItem.InventoryItemId equals reqItem.InventoryItemId into joined
                                               from invItemj in joined.DefaultIfEmpty()
                                               join req in context.Request on invItemj.RequestId equals req.RequestId into joined2
                                               from reqj in joined2.DefaultIfEmpty()
                                               where reqj == null || reqj.Status == "Accepted"
                                               group new { invItem, invItemj, reqj } by invItem.InventoryItemId into grouped
                                               select new
                                               {
                                                   InventoryItemID = grouped.FirstOrDefault().invItem.InventoryItemId,
                                                   QuantityRequest = grouped.Sum(a => (a.invItemj == null ? 0 : a.invItemj.Quantity)),
                                                   Status = (grouped.FirstOrDefault().reqj == null) ? string.Empty : grouped.FirstOrDefault().reqj.Status,
                                                   LastRequestedDate = (grouped.FirstOrDefault().reqj == null) ? default(DateTime) : grouped.Max(a => a.reqj.RequestDate)
                                               };
                stocks = (from inv in context.InventoryItem
                          join uom in context.UnitOfMeasurement on inv.UnitOfMeasurementId equals uom.UnitOfMeasurementId
                          join remainingQty in computeRemainingQuantity on inv.InventoryItemId equals remainingQty.InventoryItemID into joined
                          from remainingQtyj in joined.DefaultIfEmpty()

                          join requestedQty in computeRequestedQuantity on inv.InventoryItemId equals requestedQty.InventoryItemID into joined2
                          from requestedQtyj in joined2.DefaultIfEmpty()

                          
                          select new Stocks
                          {
                              InventoryItemID = inv.InventoryItemId,
                              ItemName = inv.ItemName,
                              UnitOfDescription = uom.Description,
                              ItemCode = inv.ItemCode,
                              TotalStock = remainingQtyj == null ? 0 : remainingQtyj.RemainingQuantity,
                              RequestedQuantity = requestedQtyj == null ? 0 : requestedQtyj.QuantityRequest,
                              LastRequestedDate = requestedQtyj.LastRequestedDate,
                          }).OrderBy(e => e.InventoryItemID).Skip(beginning).Take(page.itemPerPage).ToList().ToList();
            }
            return Json(stocks);
        }


        //public JsonResult LoadData()
        //{
        //    List<> beginningbalance = new List<>();//AIMS.Model
        //    using (var ctx = new InventoryDbContext())
        //    {
        //        beginningbalance = ctx.beginningbalance.Select(a =>//sa loob ng context 
        //            new Student
        //            {
        //                //StudentID = a.StudentID,
        //                //StudentName = a.StudentName,
        //                //StudentAge = a.StudentAge
        //            }
        //        ).ToList();

        //    }
        //    return Json(students);
        //}
        //DISPLAY ALL REQUISITION
        //public JsonResult DisplayAllStocks()
        //{
        //    try
        //    {
        //        List<Stocks> stocks = new List<Stocks>();//requisitions = Requisitions model

        //        using (var context = new InventoryDbContext())
        //        {
        //            var computeRemainingQuantity = from invItem in context.InventoryItem
        //                                           join reqItem in context.RequisitionItem on invItem.InventoryItemId equals reqItem.InventoryItemId into joined
        //                                           from invItemj in joined.DefaultIfEmpty()
        //                                           join pdItem in context.PartialDeliveryItem on invItemj.RequisitionItemId equals pdItem.RequisitionItemId into joined2
        //                                           from reqItemj in joined2.DefaultIfEmpty()
        //                                           group new { invItem, reqItemj } by new { invItem.InventoryItemId } into g
        //                                           select new
        //                                           {
        //                                               InventoryItemID = g.FirstOrDefault().invItem.InventoryItemId,
        //                                               RemainingQuantity = g.Sum(a => (a.reqItemj == null ? 0 : a.reqItemj.DeliveredQuantity))
        //                                           };

        //            var computeRequestedQuantity = from invItem in context.InventoryItem
        //                                           join reqItem in context.RequestItem on invItem.InventoryItemId equals reqItem.InventoryItemId into joined
        //                                           from invItemj in joined.DefaultIfEmpty()
        //                                           join req in context.Request on invItemj.RequestId equals req.RequestId into joined2
        //                                           from reqj in joined2.DefaultIfEmpty()
        //                                           where reqj == null || reqj.Status == "Accepted"
        //                                           group new { invItem, invItemj, reqj } by invItem.InventoryItemId into grouped
        //                                           select new
        //                                           {
        //                                               InventoryItemID = grouped.FirstOrDefault().invItem.InventoryItemId,
        //                                               QuantityRequest = grouped.Sum(a => (a.invItemj == null ? 0 : a.invItemj.Quantity)),
        //                                               //Status = (grouped.FirstOrDefault().reqj == null) ? string.Empty : grouped.FirstOrDefault().reqj.Status
        //                                           };
        //            stocks = (from inv in context.InventoryItem
        //                      join remainingQty in computeRemainingQuantity on inv.InventoryItemId equals remainingQty.InventoryItemID into joined
        //                      from remainingQtyj in joined.DefaultIfEmpty()

        //                      join requestedQty in computeRequestedQuantity on inv.InventoryItemId equals requestedQty.InventoryItemID into joined2
        //                      from requestedQtyj in joined2.DefaultIfEmpty()

        //                      select new Stocks
        //                      {
        //                          InventoryItemID = inv.InventoryItemId,
        //                          ItemName = inv.ItemName,
        //                          RemainingQuantity = remainingQtyj == null ? 0 : remainingQtyj.RemainingQuantity,
        //                          RequestedQuantity = requestedQtyj == null ? 0 : requestedQtyj.QuantityRequest,
        //                      }).ToList();
        //        }
        //        return Json(stocks);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex);
        //    }
        //}
    }
}   