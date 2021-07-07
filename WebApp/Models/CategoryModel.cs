using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class CategoryModel
    {
        [Display(Name = "Id")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "CategoryCode is required.")]
        public string CategoryCode { get; set; }

    }
}
