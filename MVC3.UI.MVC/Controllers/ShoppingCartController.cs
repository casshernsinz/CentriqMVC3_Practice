using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC3.UI.MVC.Models;

namespace MVC3.UI.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            //Create a local version of the shopping cart from the Sess(global) version
            //If the value is null of the counmtr is 0 create an empty instance and provide no cart items
            var shoppingCart = (Dictionary<int, ShoppingCartViewModel>)Session["cart"];

            if(shoppingCart == null || shoppingCart.Count == 0)
            {
                //new empty instance of the local shoppingCart to pass to the View
                //(strongly typed view MUST have an instance of our shopping cart in order to load)
                shoppingCart = new Dictionary<int, ShoppingCartViewModel>();
                ViewBag.Message = "There are no books in your cart.";
            }
            else
            {
                ViewBag.Message = null;
            }

            return View(shoppingCart);
        }

        public ActionResult UpdateCart(int bookID, int qty)
        {
            //retrieve the cart from session and assign it to our local dictionary
            Dictionary<int, ShoppingCartViewModel> shoppingCart = (Dictionary<int, ShoppingCartViewModel>)Session["cart"];

            //Update the quantity in the local storage
            shoppingCart[bookID].qty = qty;

            //Return the local cart to session
            Session["cart"] = shoppingCart;

            //Logic to display a message if they update to NO items in their cart
            if(shoppingCart.Count == 0)
            {
                ViewBag.Message = "There are no books in your cart.";
            }

            //return View("index" - the code in the index action WILL NOT run - the cart totals will not change
            //in fact, it may even cause an error because no shoppingCArt (dictionary) was sent to the view

            //RedirectAction to the index so that the code runs in that methods and reutns the view with the updated data
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int id)
        {
            //retrieve the cart from session and assign it to our local dictionary
            Dictionary<int, ShoppingCartViewModel> shoppingCart = (Dictionary<int, ShoppingCartViewModel>)Session["cart"];

            //call the remove() method from the dictionary class
            shoppingCart.Remove(id);

            //Setup the viewbag message for no results
            if(shoppingCart.Count == 0)
            {
                ViewBag.Message = "There are no books in your cart.";
                Session["cart"] = null;
            }

            return RedirectToAction("Index");
        }
    }
}