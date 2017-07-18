using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestUser.Models
{
    public class Item
    {
        public int itemId { get; set; }
        public string barcode { get; set; }
        public int purchaseCost { get; set; }
        public DateTime purchaseDate { get; set; }
        public Guid responsiblePerson { get; set; }
        //тип предмета (id / name)
        //Список локации
        //Список характеристик (значений характеристик)
    }
}