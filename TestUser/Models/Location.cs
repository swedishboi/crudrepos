using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TestUser.DTO;
using TestUser.DAL;

namespace TestUser.Models
{
    public class Location
    {
    	public int locationId { get; set; }
        public string locationName { get; set; }
        public int locationTypeId { get; set; }
        public int parentId { get; set; }

        public List<Location> GetAll()
        {
            List<LocationDTO> locationsDTOList = new LocationRepository().SelectAll();
            if (locationsDTOList == null)
                return null;
            List<Location> locationsModelsList = new List<Location>();
            foreach (var i in locationsDTOList)
            {
                locationsModelsList.Add(new Location
                {
                    locationId = i.locationId,
                    locationName = i.locationName,
                    locationTypeId = i.locationTypeId,
                    parentId = i.parentId
                });
            }
            return locationsModelsList;
        }
        public List<Location> GetByItemId(int _id)
        {
            List<LocationDTO> locationsDTOList = new LocationRepository().SelectByItemId(_id);
            if (locationsDTOList == null)
                return null;
            List<Location> locationsModelsList = new List<Location>();
            foreach (var i in locationsDTOList)
            {
                locationsModelsList.Add(new Location
                {
                    locationId = i.locationId,
                    locationName = i.locationName,
                    locationTypeId = i.locationTypeId,
                    parentId = i.parentId
                });
            }
            return locationsModelsList;
        }
    }
}