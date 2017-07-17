using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TestUser.DTO;
using TestUser.DAL;

namespace TestUser.Models
{
    public class ItemType
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<ItemType> GetAll()
        {
            List<ItemTypeDTO> itemTypesDTOList = new ItemTypeRepository().SelectAll();
            if (itemTypesDTOList == null)
                return null;
            List<ItemType> itemTypesModelsList = new List<ItemType>();
            foreach (var i in itemTypesDTOList)
            {
                itemTypesModelsList.Add(new ItemType
                {
                    id = i.id,
                    name = i.name
                });
            }
            return itemTypesModelsList;
        }
    }
}