using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCVApp.Models
{
    public class Products
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product Name  is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        public int FkCategoryId { get; set; }
        [Required(ErrorMessage = "Category name is required.")]
        public decimal Price { get; set; }
        public string Image { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [NotMapped]
        public SelectList CategoryName { get; set; }
        [NotMapped]
        public string SelectedCategory { get; set; }
    }
}