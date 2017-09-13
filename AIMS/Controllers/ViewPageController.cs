
using AIMS.Controllers;
using AIMS.Helper;
using AIMS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Helper;

namespace Accounts.Controllers
{
    public class ViewPageController : BaseController
    {
        public ViewPageController()
        {

        }
        DbManager dbManager = new DbManager();

        [System.Web.Mvc.Helper.CustomAuthorizeAttribute(UserRole = "Administrator")]
        [HttpGet]
        public ActionResult ForAdmin()
        {
            int userid = UserID;
            return View();
        }

        public JsonResult LoadRoles()
        {

            List<Role> roles = new List<Role>();
            DataTable dtRole = dbManager.SqlReader("SELECT * FROM DB_ACCOUNTS.dbo.tbl_Role", "tblAccount");
            foreach (DataRow row in dtRole.Rows)
            {
                roles.Add(
                new Role
                {
                    RoleID = (int)row["RoleId"],
                    RoleName = row["Name"].ToString()
                });
            }

            return Json(roles);
        }

        public JsonResult LoadRolesEdit(string roles)
        {
            string[] items = roles.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            bool checker = false;
            List<Role> role = new List<Role>();
            DataTable dtRole = dbManager.SqlReader("SELECT * FROM DB_ACCOUNTS.dbo.tbl_Role", "tblAccount");
            foreach (DataRow row in dtRole.Rows)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (row["Name"].ToString() == items[i])
                    {
                        checker = true;
                    }
                }
                role.Add(
                new Role
                {
                    RoleID = (int)row["RoleId"],
                    RoleName = row["Name"].ToString(),
                    RoleStatus = checker
                });
                checker = false;
            }
            return Json(role);
        }

        public JsonResult LoadData()
        {
            List<Account> accounts = new List<Account>();
            string queryString = "SELECT t2.UserID as UserId, t2.Username as Username, t2.Lastname as Lastname, t2.Firstname as Firstname, t2.Middlename as Middlename, t2.Department as Department, t2.ContactNo as Contact, t2.Email as Email, " +
                                        "Roles = STUFF( " +
                                        "(SELECT ', ' + convert(varchar(50), r.Name)" +
                                        "From DB_ACCOUNTS.dbo.tbl_UserRole t1 " +
                                        "INNER JOIN " +
                                            "DB_ACCOUNTS.dbo.tbl_user u " +
                                        "ON " +
                                            "t1.Userid = u.Userid " +
                                        "LEFT JOIN " +
                                            "DB_ACCOUNTS.dbo.tbl_role r " +
                                        "ON " +
                                           "t1.RoleId = r.RoleId " +
                                       "where t1.UserId = t2.UserId " +
                                        "FOR XML PATH('')) " +
                                        ", 1,1,'') " +
                                     "from DB_ACCOUNTS.dbo.tbl_User t2 " +
                                     "group by t2.UserID, t2.Username, t2.Lastname, t2.Firstname, t2.Middlename, t2.Department, t2.ContactNo, t2.Email; ";
            DataTable dtAccount = dbManager.SqlReader(queryString, "tblAccount");
            foreach (DataRow row in dtAccount.Rows)
            {
                accounts.Add(
                new Account
                {
                    UserID = (int)row["UserId"],
                    Username = row["Username"].ToString(),
                    Lastname = row["Lastname"].ToString(),
                    Firstname = row["Firstname"].ToString(),
                    Middlename = row["Middlename"].ToString(),
                    Department = row["Department"].ToString(),
                    Contact = row["Contact"].ToString(),
                    Email = row["Email"].ToString(),
                    Roles = row["Roles"].ToString(),
                });
            }
            return Json(accounts);
        }

        [HttpPost]
        public JsonResult AddUser(string username, string lastname, string firstname, string middlename, string department, string contact, string email, List<int> roles)
        {
            try
            {
                //=======INSERTING USER INFORMATION==========
                string queryString = "INSERT INTO DB_ACCOUNTS.dbo.tbl_User (Username, Lastname, Firstname, Middlename, Department, ContactNo, Email) values (@username, @lastname, @firstname, @middlename, @department, @contact, @email);";//Query
                List<Parameter> parameters = new List<Parameter>()
                {
                    new Parameter()
                    {
                        ParameterName = "@username",
                        ParameterValue = username
                    },
                    new Parameter
                    {
                        ParameterName = "@lastname",
                        ParameterValue = lastname,

                        },
                        new Parameter
                    {
                        ParameterName = "@firstname",
                        ParameterValue = firstname,

                    },
                    new Parameter
                    {
                        ParameterName = "@middlename",
                        ParameterValue = middlename,

                    },
                    new Parameter
                    {
                        ParameterName = "@department",
                        ParameterValue = department,

                    },
                    new Parameter
                    {
                        ParameterName = "@contact",
                        ParameterValue = contact,

                    },
                    new Parameter
                    {
                        ParameterName = "@email",
                        ParameterValue = email,

                    }
                };
                dbManager.SqlNonQuery(queryString, parameters);

                //=======GETTING THE MAX USER ID===========
                int userid = 0;
                DataTable dtLocation = dbManager.SqlReader("SELECT MAX(UserID) FROM DB_ACCOUNTS.dbo.tbl_User;", "tblAccount");
                foreach (DataRow row in dtLocation.Rows)
                {
                    userid = (int)row[0];
                }
                //=======INSERTING ROLES TO USER===========
                string query = "INSERT INTO DB_ACCOUNTS.dbo.tbl_UserRole (UserId, RoleId) values (@userid,@roleid);";
                foreach (int r in roles)
                {
                    List<Parameter> parameter = new List<Parameter>()
                    {
                        new Parameter
                        {
                            ParameterName = "@userid",
                            ParameterValue = userid.ToString()
                        },
                        new Parameter
                        {
                            ParameterName = "@roleid",
                            ParameterValue = r.ToString()
                        }
                    };
                    dbManager.SqlNonQuery(query, parameter);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
            return Json(string.Empty);
        }


        public JsonResult DeleteUser(int id)
        {
            try
            {
                string queryString = "DELETE FROM DB_ACCOUNTS.dbo.tbl_User WHERE UserID = @userid;";//Query
                List<Parameter> parameters = new List<Parameter>();
                parameters.Add(
                    new Parameter
                    {
                        ParameterName = "@userid",
                        ParameterValue = id.ToString()
                    }
                    );
                dbManager.SqlNonQuery(queryString, parameters);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
            return Json(string.Empty);
        }


        public JsonResult EditUser(Account account)
        {
            try
            {
                string queryString = "UPDATE DB_ACCOUNTS.dbo.tbl_User SET Username=@username, Lastname= @lastname, Firstname=@firstname, Middlename=@middlename, Department=@department, ContactNo=@contact, Email = @email WHERE UserID = @userid";
                //string query = "INSERT INTO tbl_User (Username, Lastname, Firstname, Middlename, Department, ContactNo, Email) values (@username, @lastname, @firstname, @middlename, @department, @contact, @email);";//Query
                List<Parameter> parameters = new List<Parameter>()
                {
                    new Parameter()
                    {
                        ParameterName = "@userid",
                        ParameterValue = account.UserID.ToString()
                    },
                    new Parameter()
                    {
                        ParameterName = "@username",
                        ParameterValue = account.Username
                    },
                    new Parameter
                    {
                        ParameterName = "@lastname",
                        ParameterValue = account.Lastname

                    },
                    new Parameter
                    {
                        ParameterName = "@firstname",
                        ParameterValue = account.Firstname

                    },
                    new Parameter
                    {
                        ParameterName = "@middlename",
                        ParameterValue = account.Middlename

                    },
                    new Parameter
                    {
                        ParameterName = "@department",
                        ParameterValue = account.Department

                    },
                    new Parameter
                    {
                        ParameterName = "@contact",
                        ParameterValue = account.Contact

                    },
                    new Parameter
                    {
                        ParameterName = "@email",
                        ParameterValue = account.Email

                    }
                };
                dbManager.SqlNonQuery(queryString, parameters);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
            return Json(string.Empty);
        }



        [System.Web.Mvc.Helper.CustomAuthorizeAttribute(UserRole = "User")]
        public ActionResult ForSimpleUser()
        {
            return View();
        }


        [System.Web.Mvc.Helper.CustomAuthorizeAttribute(UserRole = "Staff")]
        public ActionResult ForStaff()
        {
            return View();
        }

    }
}