using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MVC3.DATA.EF;

namespace MVC3.UI.MVC.Models
{
    //Adding this ViewModel to combine domain-related info and other info (Qty) needed for shopping cart
    //Hence the name ViewModel
    public class ShoppingCartViewModel
    {
        [Range(1, int.MaxValue)]//Ensure values greater than zero up to a max value of int or other desired values
        public int qty { get; set; }

        public Book product { get; set; }

        //Fully qualified constructor to make it easy to store all the info
        public ShoppingCartViewModel(int qty, Book product)
        {
            this.qty = qty;
            this.product = product;
        }
    }
}