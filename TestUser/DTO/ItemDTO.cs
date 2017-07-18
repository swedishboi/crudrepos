using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestUser.DTO
{
    public class ItemDTO
    {
        public int itemId { get; set; }
        public int itemTypeId { get; set; }
        public string barcode { get; set; }
        public int purchaseCost { get; set; }
        public DateTime purchaseDate { get; set; }
        public Guid responsiblePerson { get; set; }
        public List<LocationDTO> location { get; set; }
        public List<CharacteristicDTO> characterstic { get; set; }
    }
}