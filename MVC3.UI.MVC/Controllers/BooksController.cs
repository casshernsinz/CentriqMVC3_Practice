using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC3.DATA.EF;
using MVC3.UI.MVC.Models;

namespace MVC3.UI.MVC.Controllers
{
    public class BooksController : Controller
    {
        private BookStorePlusEntities db = new BookStorePlusEntities();

        #region Add To Cart - Shopping Cart Functionailty

        [HttpPost]
        public ActionResult AddToCart(int qty, int bookID)
        {
            //Creat an empty shopping cart (local)
            //A Dictionary<key, value> is a typed collection similar to a List<type>, this is going to store information in key value pairs
            Dictionary<int, ShoppingCartViewModel> shoppingCart = null;

            //Check the Cart in Session (global), if the Session has anything in it then assign its values to our (local) verion
            if(Session["cart"] != null)
            {
                //pull in the global version to our local version
                shoppingCart = (Dictionary<int, ShoppingCartViewModel>)Session["cart"];
            }
            else
            {
                //create an empty local version
                shoppingCart = new Dictionary<int, ShoppingCartViewModel>();
            }

            //Get the product getting added
            Book product = db.Books.Where(x => x.BookID == bookID).FirstOrDefault();
            //If not valid, return them to books index
            if(product == null)
            {
                return RedirectToAction("Index");
                //Custom Error page for invalid products could be added here <-- but that's irrelevant for our example
            }
            else
            {
                //Book is valid(product id was found and return a book)
                //Create a shoppingCartViewModel Object
                ShoppingCartViewModel item = new ShoppingCartViewModel(qty, product);

                //If valid and one already exists in the card, update the quantity in the loca
                if (shoppingCart.ContainsKey(product.BookID))
                {
                    shoppingCart[product.BookID].qty += qty;
                }
                else
                {
                    //If it is NOT in the cart we are going to add it locally
                    shoppingCart.Add(product.BookID, item);
                }
                Session["cart"] = shoppingCart;
            }
            //send to the shoppingCart to view products that have been added
            return RedirectToAction("Index", "ShoppingCart");
        }

        #endregion


        // GET: Books
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.BookRoyalty).Include(b => b.BookStatus).Include(b => b.Genre).Include(b => b.Publisher);
            return View(books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.BookID = new SelectList(db.BookRoyalties, "BookID", "BookID");
            ViewBag.BookStatusID = new SelectList(db.BookStatuses, "BookStatusID", "BookStatusName");
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName");
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "PublisherName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookID,ISBN,BookTitle,Description,GenreID,Price,UnitsSold,PublishDate,PublisherID,BookImage,IsSiteFeature,IsGenreFeature,BookStatusID")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookID = new SelectList(db.BookRoyalties, "BookID", "BookID", book.BookID);
            ViewBag.BookStatusID = new SelectList(db.BookStatuses, "BookStatusID", "BookStatusName", book.BookStatusID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", book.GenreID);
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "PublisherName", book.PublisherID);
            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookID = new SelectList(db.BookRoyalties, "BookID", "BookID", book.BookID);
            ViewBag.BookStatusID = new SelectList(db.BookStatuses, "BookStatusID", "BookStatusName", book.BookStatusID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", book.GenreID);
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "PublisherName", book.PublisherID);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookID,ISBN,BookTitle,Description,GenreID,Price,UnitsSold,PublishDate,PublisherID,BookImage,IsSiteFeature,IsGenreFeature,BookStatusID")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookID = new SelectList(db.BookRoyalties, "BookID", "BookID", book.BookID);
            ViewBag.BookStatusID = new SelectList(db.BookStatuses, "BookStatusID", "BookStatusName", book.BookStatusID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", book.GenreID);
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "PublisherName", book.PublisherID);
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
