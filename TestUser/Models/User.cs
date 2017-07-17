using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TestUser.DTO;
using TestUser.DAL;

namespace TestUser.Models
{
    public class User
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

        public User Register(Guid _id, string _name, string _surname, string _patronymic, string _mobile, string _login)
        {
            UserDTO userDTO = new UserRepository().Create(_id, _name, _surname, _patronymic, mobile, _login);
            if (userDTO == null)
                return null;
            User userModel = new User
            {
                id = _id,
                name = _name,
                surname = _surname,
                patronymic = _patronymic,
                mobile = _mobile,
                login = _login
            };
            return userModel;
        }
    }//end
}