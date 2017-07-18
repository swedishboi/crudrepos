using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TestUser.DTO;
using TestUser.DAL;

namespace TestUser.Models
{
    public class Item
    {
        public int itemId { get; set; }
        public int itemTypeId { get; set; }
        public string barcode { get; set; }
        public int purchaseCost { get; set; }
        public DateTime purchaseDate { get; set; }
        public Guid responsiblePerson { get; set; }
        public List<Location> location { get; set; }
        public List<Characteristic> characterstic { get; set; }
        public List<Item> GetAll()
        {
            List<ItemDTO> itemsDTOList = new ItemRepository().SelectAll();
            if (itemsDTOList == null)
                return null;
            List<Item> itemsModelsList = new List<Item>();
            foreach (var i in itemsDTOList)
            {
                itemsModelsList.Add(new Item
                {
                    itemId = i.itemId,
                    itemTypeId = i.itemTypeId,
                    barcode = i.barcode,
                    purchaseCost = i.purchaseCost,
                    purchaseDate = i.purchaseDate,
                    responsiblePerson = i.responsiblePerson,
                    location = new Location().GetByItemId(i.itemId),
                    characterstic = new Characteristic().GetByItemId(i.itemId)
                    
                });
            }
            return itemsModelsList;
        }
    }
}