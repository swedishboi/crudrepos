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
        public Guid personId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public List<Position> position { get; set; }
        public List<Profile> profile { get; set; }

        public List<User> GetAll()
        {
            List<UserDTO> usersDTOList = new UserRepository().ListAllUsers();
            if (usersDTOList == null)
                return null;
            List<User> usersModelsList = new List<User>();
            foreach (var i in usersDTOList)
            {
                usersModelsList.Add(new User
                {
                    password = i.password,
                    login = i.login,
                    personId = i.personId,
                    name = i.name,
                    surname = i.surname,
                    patronymic = i.patronymic,
                    email = i.email,
                    position = new Position().GetByUserId(i.personId),
                    profile = new Profile().GetByUserId(i.personId)
                });
            }
            return usersModelsList;
        }

        public User LogIn(string login, string password)
        {
            UserDTO userDTO = new UserRepository().LogIn(login, password);

            if (userDTO == null)
                return null;


            User userModel = new User()
            {
                login = userDTO.login,            
                personId = userDTO.personId,
                name = userDTO.name,
                surname = userDTO.surname,
                patronymic = userDTO.patronymic,
                email = userDTO.email,
                position = new Position().GetByUserId(userDTO.personId),
                profile = new Profile().GetByUserId(userDTO.personId)

            };

            return userModel;
        }

        public bool Register(string login, string password, string email, Guid id, string name, string patronymic, string surname, List<Position> positionList, List<Profile> profileList)
        {
            List<PositionDTO> positionDTOList = new List<PositionDTO>();
            List<ProfileDTO> profileDTOList = new List<ProfileDTO>();
            int count = positionList.Count;
            for (int i = 0; i < count; i++)
            {
                positionDTOList.Add(new PositionDTO 
                { 
                    positionId = positionList[i].positionId,
                    positionName = positionList[i].positionName 
                });

                profileDTOList.Add(new ProfileDTO
                {
                    profileId=profileList[i].profileId,
                    profileName=profileList[i].profileName

                });
            }

            bool success = new UserRepository().Register(login, password, email, id,name,patronymic,surname,positionDTOList,profileDTOList);           
            return success;
           
        }
    }//end
}