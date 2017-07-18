using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestUser.DTO
{
    public class UserDTO
    {
        public Guid personId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public List<PositionDTO> position { get; set; }
        public List<ProfileDTO> profile { get; set; }
    }//end
}