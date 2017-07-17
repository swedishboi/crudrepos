using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TestUser.DTO;
using TestUser.DAL;

namespace TestUser.Models
{
    public class Profile
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Profile> GetAll()
        {
            List<ProfileDTO> profilesDTOList = new ProfileRepository().SelectAll();
            if (profilesDTOList == null)
                return null;
            List<Profile> profilesModelsList = new List<Profile>();
            foreach (var i in profilesDTOList)
            {
                profilesModelsList.Add(new Profile
                {
                    id = i.id,
                    name = i.name
                });
            }
            return profilesModelsList;
        }
    }//end
}