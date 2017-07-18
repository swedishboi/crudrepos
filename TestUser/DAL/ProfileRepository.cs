using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using TestUser.DTO;

namespace TestUser.DAL
{
    public class ProfileRepository
    {
        string sqlExpSelect = "select * from T_Profile";
        string sqlExpSelectByUserId = "select * from T_Profile inner join T_User on T_Profile.profileId = T_User.profileId where personId = @personId";
        string sqlExpUpdate = "update T_Profile set profileName = @profileName where profileId = @profileId";
        string sqlExpDelete = "delete from T_Profile where profileId = @profileId";
        string sqlExpInsert = "insert into T_Profile (profileName) values (@profileName)";

        public List<ProfileDTO> SelectAll()
        {
            List<ProfileDTO> profilesDTOList = null;
            using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlExpSelect, connect))
                {
                    connect.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            profilesDTOList = new List<ProfileDTO>();
                            while (reader.Read())
                            {
                                profilesDTOList.Add(new ProfileDTO()
                                {
                                    profileId = reader.GetInt32(0),
                                    profileName = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
                connect.Close();
            }
            return profilesDTOList;
        }
        public bool Update(int _id, string _name)
        {
            bool flag = false;
            if (IsNull(_name)) return flag;
            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlExpUpdate, connect))
                    {
                        command.Parameters.Add("@profileId", SqlDbType.Int).Value = _id;
                        command.Parameters.Add("@profileName", SqlDbType.NVarChar).Value = _name;
                        connect.Open();
                        command.ExecuteNonQuery();
                    }
                    connect.Close();
                }
                flag = true;
            }
            catch (Exception ex)
            {

            }
            return flag;
        }

        public bool Delete(int _id)
        {
            bool flag = false;
            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlExpDelete, connect))
                    {
                        command.Parameters.Add("@profileId", SqlDbType.Int).Value = _id;
                        connect.Open();
                        command.ExecuteNonQuery();
                    }
                    connect.Close();
                }
                flag = true;
            }
            catch (Exception ex)
            {

            }
            return flag;
        }

        public bool Insert(string _name)
        {
            bool flag = false;
            if (IsNull(_name)) return flag;
            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlExpInsert, connect))
                    {
                        command.Parameters.Add("@profileName", SqlDbType.NVarChar).Value = _name;
                        connect.Open();
                        command.ExecuteNonQuery();
                    }
                    connect.Close();
                }
                flag = true;
            }
            catch (Exception ex)
            {
            }
            return flag;
        }

        public bool IsNull(string _name)
        {
            List<ProfileDTO> list = SelectAll();
            ProfileDTO profile = list.Where(p => p.profileName == _name).FirstOrDefault();
            return profile != null ? true : false;
        }
        public List<ProfileDTO> SelectByUserId(Guid _id)
        {
            List<ProfileDTO> profileList = null;
            using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlExpSelectByUserId, connect))
                {
                    command.Parameters.Add("@personId", SqlDbType.UniqueIdentifier).Value = _id;
                    connect.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            profileList = new List<ProfileDTO>();
                            while (reader.Read())
                            {
                                profileList.Add(new ProfileDTO()
                                {
                                    profileId = reader.GetInt32(0),
                                    profileName = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
                connect.Close();
            }
            return profileList;
        }
    }//end
}