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
        public int itemTypeId { get; set; }
        public string itemTypeName { get; set; }

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
                    itemTypeId = i.itemTypeId,
                    itemTypeName = i.itemTypeName
                });
            }
            return itemTypesModelsList;
        }

        public bool Remove(int _id)
        {
            bool flag = new ItemTypeRepository().Delete(_id);
            return flag;
        }

        public bool Edit(int _id, string _name)
        {
            bool flag = new ItemTypeRepository().Update(_id, _name);
            return flag;
        }

        public bool Add(string _name)
        {
            bool flag = new ItemTypeRepository().Insert(_name);
            return flag;
        }
    }
}