using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TestUser.DTO;
using TestUser.DAL;

namespace TestUser.Models
{
    public class LocationType
    {
        public int locationTypeId { get; set; }
        public string locationTypeName { get; set; }

        public List<LocationType> GetAll()
        {
            List<LocationTypeDTO> locationTypesDTOList = new LocationTypeRepository().SelectAll();
            if (locationTypesDTOList == null)
                return null;
            List<LocationType> locationTypesModelsList = new List<LocationType>();
            foreach (var i in locationTypesDTOList)
            {
                locationTypesModelsList.Add(new LocationType
                {
                    locationTypeId = i.locationTypeId,
                    locationTypeName = i.locationTypeName
                });
            }
            return locationTypesModelsList;
        }

        public bool Remove(int _id)
        {
            bool flag = new LocationTypeRepository().Delete(_id);
            return flag;
        }

        public bool Edit(int _id, string _name)
        {
            bool flag = new LocationTypeRepository().Update(_id, _name);
            return flag;
        }

        public bool Add(string _name)
        {
            bool flag = new LocationTypeRepository().Insert(_name);
            return flag;
        }
    }
}