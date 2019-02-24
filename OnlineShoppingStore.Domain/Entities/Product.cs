using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingStore.Domain.Entities
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage ="A name is needed.")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "A description is needed.")]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Enter a positive number.")]
        [Required(ErrorMessage = "A price is needed.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "A category is needed.")]
        public string Category { get; set; }
    }
}
