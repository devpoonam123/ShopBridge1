using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopBridge.Models
{
    public class Inventory
    {

        public int Id { get; set; }
       
        public string Name { get; set; }
       
        public string Description { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Image
        {
            get;set;
        }
    }
}