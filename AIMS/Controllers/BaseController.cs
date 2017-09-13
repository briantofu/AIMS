
using AccountContext;
using AccountsWebAuthentication.Helper;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace AIMS.Controllers
{
    public class BaseController : Controller
    {
        //DbManager dbManager = new DbManager();

        protected string Username  
        {
            get
            {
                return WindowsUser.Username; //GET Username and UserID
            }
        }

      
        protected int UserID
        {
            get
            {
                int UserID = 0;
                using (var ctx = new AccountDbContext())
                {
                    UserID = ctx.Users.Where(u => u.Username == Username).Select(u => u.UserId).FirstOrDefault();
                }
                return UserID;
            }
        }

    }
}