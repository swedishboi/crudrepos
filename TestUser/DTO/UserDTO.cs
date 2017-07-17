using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestUser.DTO
{
    public class UserDTO
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }
        public string mobile { get; set; }
        public string login { get; set; }
        /*
        public List<Position> position { get; set; }
        public List<Profile> profile { get; set; }
        */
    }//end
}