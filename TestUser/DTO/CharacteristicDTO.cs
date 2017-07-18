using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestUser.DTO
{
    public class CharacteristicDTO
    {
        public int charactersticId { get; set; }
        public int itemTypeId { get; set; }
        public string characteristicName { get; set; }
        public string notation { get; set; }
        public int dateTypeId { get; set; }
        public string characteristicValue { get; set; }
        public int unitId { get; set; }
    }
}