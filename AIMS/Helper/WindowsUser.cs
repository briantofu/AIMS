using AccountContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace AIMS.Helper
{
    public static class WindowsUser
    {
        static DbManager dbManager = new DbManager();
        public static string Username
        {
            get
            {
                WindowsIdentity clientId = (WindowsIdentity)HttpContext.Current.User.Identity;
                return clientId.Name;
            }
        }

        public static string UserID
        {
            get
            {
                string UserID = null;
                DataTable dtLocation = dbManager.SqlReader("SELECT UserID FROM DB_ACCOUNTS.dbo.tbl_User WHERE Username = '" + Username + "'", "tblAccount");
                foreach (DataRow row in dtLocation.Rows)
                {
                    UserID =row["UserID"].ToString();
                }
                return UserID;
            }
        }

        public static List<string> UserRoles
        {
            get
            {
                List<string> roleList = new List<string>();
                using (var context = new AccountDbContext())
                {
                    var resultRoles = from ur in context.UserRoles
                                      join u in context.Users
                                        on ur.UserId equals u.UserId
                                      join r in context.Roles
                                        on ur.RoleId equals r.RoleId
                                      where u.Username == Username
                                      select new
                                      {
                                          RoleName = r.RoleName
                                      };
                    roleList = resultRoles.Select(r => r.RoleName).ToList();
                }

                return roleList;
            }

        }

    }
}