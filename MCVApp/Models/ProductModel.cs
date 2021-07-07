using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCVApp.Models
{
    public class ProductModel
    {
        public int FkCategoryId { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public List<SelectListItem> Categories { get; set; }
        public IEnumerable<Products> Products { get; set; }

    }

}