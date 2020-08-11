using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC3.UI.MVC.Models
{
    public class CategoryViewModel
    {
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        [UIHint("MultilineText")]
        public string Notes { get; set; }
    }

    public class HyperlinkViewModel
    {
        public int HyperlinkID { get; set; }

        public string URL { get; set; }

        public string LinkText { get; set; }

        public int CategoryID { get; set; }

        [DisplayFormat(DataFormatString ="{0:d}")]
        public System.DateTime DateCreated { get; set; }

        [UIHint("MultilineText")]
        public string Notes { get; set; }

        public virtual CategoryViewModel Category { get; set; }
    }
}