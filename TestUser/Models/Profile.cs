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
        public int profileId { get; set; }
        public string profileName { get; set; }
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
                    profileId = i.profileId,
                    profileName = i.profileName
                });
            }
            return profilesModelsList;
        }
        public bool Remove(int _id)
        {
            bool flag = new ProfileRepository().Delete(_id);
            return flag;
        }

        public bool Edit(int _id, string _name)
        {
            bool flag = new ProfileRepository().Update(_id, _name);
            return flag;
        }

        public bool Add(string _name)
        {
            bool flag = new ProfileRepository().Insert(_name);
            return flag;
        }

        public List<Profile> GetByUserId(Guid _id)
        {
            List<ProfileDTO> profilesDTOList = new ProfileRepository().SelectByUserId(_id);
            if (profilesDTOList == null)
                return null;
            List<Profile> profilesModelsList = new List<Profile>();
            foreach (var i in profilesDTOList)
            {
                profilesModelsList.Add(new Profile
                {
                    profileId = i.profileId,
                    profileName = i.profileName
                });
            }
            return profilesModelsList;
        }
    }//end
}