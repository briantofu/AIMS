
using AccountContext;
using AIMS.Helper;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Mvc.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string UserRole { get; set; }
        //DbManager dbManager = new DbManager();
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string currentUserlogged = WindowsUser.Username;
            bool authorized = false;
            try
            {
                string checkUsernameExist = "";
                int status = 1;
                using (var ctx = new AccountDbContext())
                {
                    checkUsernameExist = ctx.Users.Where(u => u.Username == currentUserlogged).Select(u => u.Username).FirstOrDefault();
                    status = ctx.Users.Where(u => u.Username == currentUserlogged).Select(u => u.Status).FirstOrDefault();
                    if (checkUsernameExist.Length > 0 && status == 0)
                    {
                        List<string> roleList = new List<string>();
                        var resultRoles = from ur in ctx.UserRoles
                                          join u in ctx.Users
                                            on ur.UserId equals u.UserId
                                          join r in ctx.Roles
                                            on ur.RoleId equals r.RoleId
                                          where u.Username == currentUserlogged
                                          select new
                                          {
                                              RoleName = r.RoleName
                                          };
                        roleList = resultRoles.Select(r => r.RoleName).ToList();
                        var isAuthorized = base.AuthorizeCore(httpContext);
                        authorized = !isAuthorized;
                        foreach (string role in roleList)
                        {
                            if (this.UserRole.Contains(role))
                            {
                                authorized = true;
                            }
                        }
                    }
                }
                //string queryString = "SELECT Username, Status FROM tbl_User WHERE Username = '" + currentUserlogged + "'";
                //string checkUsernameExist = "";
                //int status=1;

                //DataTable dtAccount = dbManager.SqlReader(queryString, "tblAccount");
                //foreach (DataRow dtAccountRow in dtAccount.Rows)
                //{
                //    checkUsernameExist = dtAccountRow["Username"].ToString();
                //    status = (int)dtAccountRow["Status"];
                //}

                //if (checkUsernameExist.Length > 0 && status==0)
                //{
                //    string sqlRole = "SELECT u.Username,r.Name FROM tbl_UserRole ur " +
                //             "INNER JOIN tbl_user u " +
                //                "ON ur.Userid = u.Userid " +
                //             "JOIN tbl_role r " +
                //                "ON ur.RoleId = r.RoleId " +
                //             "WHERE " +
                //                "u.username = '" + currentUserlogged + "'";
                //    List<string> roleList = new List<string>();
                //    DataTable dtRole = dbManager.SqlReader(sqlRole, "tblRoles");
                //    foreach (DataRow row in dtRole.Rows)
                //    {
                //        roleList.Add(row["Name"].ToString());
                //    }
                //    var isAuthorized = base.AuthorizeCore(httpContext);
                //    authorized = !isAuthorized;
                //    foreach (string role in roleList)
                //    {
                //        if (this.UserRole.Contains(role))
                //        {
                //            authorized = true;
                //        }
                //    }
                //}
                return authorized;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}