using AccountsContext;
using AccountsEntity;
using BaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AIMS.Controllers
{
    public class FilterActionsController : BaseController
    {
        // GET: FilterActions
        public ActionResult Index()
        {
            return View();
        }



        //public JsonResult SelectUserRoles()
        //{
        //    try
        //    {
        //        List<string> roleList = new List<string>();
        //        using (var context = new AccountDbContext())
        //        {
        //            var resultRoles = from ur in context.UserRoles
        //                              join u in context.Users
        //                                on ur.UserId equals u.UserId
        //                              join r in context.Roles
        //                                on ur.RoleId equals r.RoleId
        //                              where u.Username == Username
        //                              select new
        //                              {
        //                                  RoleName = r.RoleName
        //                              };
        //            roleList = resultRoles.Select(r => r.RoleName).ToList();
        //        }


        //        using (var context = new AccountDbContext())
        //        {
        //            DBase dbase = new DBase(context);
        //            var a = dbase.List<EUserRole>(b => true);
        //        }

        //        return Json(roleList);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex.ToString());
        //    }

        //}
    }
}