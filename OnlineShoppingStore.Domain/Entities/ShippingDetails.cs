﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Please enter a name")] 
        public string Name { get; set; }

        [Required(ErrorMessage ="Please enter teh address")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage =  "Please enter the city name")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter the State name")]
        public string State { get; set; }

        public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter the country name")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
