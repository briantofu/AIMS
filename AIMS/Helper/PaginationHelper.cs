using AIMS.Helper;
using AIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIMS.Helper
{

    public class PaginationHelper
    {
        private static int itemPerPage = 10;
        DbManager dbManager = new DbManager();

        public List<Page> PaginationLoadPages(Page page, string query)
        {
            int totalpages = 0;
            int totalusers = Convert.ToInt32(dbManager.SqlReader(query));
            if (totalusers % itemPerPage != 0)
            {
                totalpages = (totalusers / itemPerPage) + 1;
            }
            else
            {
                totalpages = (totalusers / itemPerPage);
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
            return pages;
        }

        public List<Page> PaginationLoadInitPages(string query)
        {
            int totalpages = 0;
            int totalusers = Convert.ToInt32(dbManager.SqlReader(query));
            if (totalusers % itemPerPage != 0)
            {
                totalpages = (totalusers / itemPerPage) + 1;
            }
            else
            {
                totalpages = (totalusers / itemPerPage);
            }
            List<Page> pages = new List<Page>();
            for (int x = 1; x <= totalpages; x++)
            {
                if (x == 1)
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
            return pages;
        }

        public List<Page> PaginationLoadLastPage(string query)
        {
            int totalpages = 0;
            int totalusers = Convert.ToInt32(dbManager.SqlReader(query));
            if (totalusers % itemPerPage != 0)
            {
                totalpages = (totalusers / itemPerPage) + 1;
            }
            else
            {
                totalpages = (totalusers / itemPerPage);
            }
            List<Page> pages = new List<Page>();
            for (int x = 1; x <= totalpages; x++)
            {
                if (x == totalpages)
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
            return pages;
        }

        public int PaginationLastPage(string query)
        {
            int totalpages = 0;
            int totalusers = Convert.ToInt32(dbManager.SqlReader(query));
            if (totalusers % itemPerPage != 0)
            {
                totalpages = (totalusers / itemPerPage) + 1;
            }
            else
            {
                totalpages = (totalusers / itemPerPage);
            }
            return totalpages;
        }
    }
}