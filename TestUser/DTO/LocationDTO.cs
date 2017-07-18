using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestUser.DTO
{
    public class LocationDTO
    {
    	public int locationId { get; set; }
        public string locationName { get; set; }
        public int locationTypeId { get; set; }
        public int parentId { get; set; }
    }
}