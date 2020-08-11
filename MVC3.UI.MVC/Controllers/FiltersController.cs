using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC3.DATA.EF;//This is added for our EF domain models
using System.Data.Entity;//This is added for EF
using PagedList;//Added after NuGet PagedList
using PagedList.Mvc;//Added after NuGet PagedList.MVC

namespace MVC3.UI.MVC.Controllers
{
    public class FiltersController : Controller
    {
        private BookStorePlusEntities db = new BookStorePlusEntities();

        // GET: Filters
        public ActionResult Index()
        {

            return View();
        }

        //Servor Side Filter Action
        public ActionResult AuthorsQS(string searchFilter)
        {
            if (String.IsNullOrEmpty(searchFilter))
            {
                //If optional search isn't used, return all records

                return View(db.Authors.ToList());
            }
            else
            {
                //If optional search is used, compare it to the first and last name. Use Linq
                //This is a Method example, below this is a Keyword Syntax example
                string searchUpCase = searchFilter.ToUpper();
                List<Author> searchResults = db.Authors
                                             .Where(a => a.FirstName.ToUpper().Contains(searchUpCase)
                                             || a.LastName.ToUpper().Contains(searchUpCase))
                                             .OrderBy(a => a.FirstName)
                                             .ThenBy(a => a.LastName)
                                             .ToList();

                //Method syntax example above Or Query syntax example below
                List<Author> searchResult2 = (from a in db.Authors
                                              where a.FirstName.ToUpper().Contains(searchUpCase) ||
                                              a.LastName.ToUpper().Contains(searchUpCase)
                                              orderby a.FirstName, a.LastName
                                              select a).ToList();
                return View(searchResults);
            }
        }

        public ActionResult LabMagazinesQS(string searchFilter)
        {
            if (String.IsNullOrEmpty(searchFilter))
            {
                //If optional search isn't used, return all records

                return View(db.Magazines.ToList());
            }
            else
            {
                //If optional search is used, compare it to the first and last name. Use Linq
                //This is a Method example, below this is a Keyword Syntax example

                string searchUpCase = searchFilter.ToUpper();
                List<Magazine> searchResults = db.Magazines
                                             .Where(m => m.MagazineTitle.ToUpper().Contains(searchUpCase)
                                             || m.Category.ToUpper().Contains(searchUpCase))
                                             .OrderBy(m => m.MagazineTitle)
                                             .ThenBy(m => m.Category)
                                             .ToList();

                //Method syntax example above Or Query syntax example below
                //List<Author> searchResult2 = (from m in db.Magazines
                //                              where m.MagazineTitle.ToUpper().Contains(searchUpCase) ||
                //                              m.PublishRate.ToUpper().Contains(searchUpCase)
                //                              orderby m.MagazineTitle, m.PublishRate
                //                              select m).ToList();
                return View(searchResults);
            }
        }

        public ActionResult BooksMVCPaging(string searchString, string currentFilter, int page = 1)
        {
            int pageSize = 5;
            var books = db.Books.OrderBy(b => b.BookTitle).ToList();

            #region Search With Paging
            //We are tracking it's a new search(Go To Page 1 with results)
            //or if it's a previous search(track with current filter and use paging based on the last request)
            /*
             * In the Action-
             * SearchString only gets assigned new searches
             * If searchString has a value, then it is a search - update the page to 1(first page of results)
             * Else if searchString is null, assign searchString to use the currentFilter value ( either null/empty OR previously tracked old search)
             * 
             */

            #endregion

            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            //Check if the searchString is not null or empty.
            //If it is NOT null use the filter to grab the new data set

            if (!String.IsNullOrEmpty(searchString))
            {
                books = (from b in books
                        where b.BookTitle.ToLower().Contains(searchString.ToLower())
                        orderby b.BookTitle
                         select b).ToList();
            }

            //Set up a ViewBag variable for passing currentFilter based on whatever searchString is now
            ViewBag.CurrentFilter = searchString;

            return View(books.ToPagedList(page, pageSize));
        }

        public ActionResult LabMagazinesMVCPaging(string searchString, string currentFilter, int page = 1)
        {
            int pageSize = 5;
            var magazines = db.Magazines.OrderBy(c => c.Category).ToList();

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                magazines = (from c in magazines
                             where c.Category.ToLower().Contains(searchString.ToLower())
                         orderby c.Category
                         select c).ToList();
            }

            ViewBag.CurrentFilter = searchString;

            return View(magazines.ToPagedList(page, pageSize));
        }
    }
}