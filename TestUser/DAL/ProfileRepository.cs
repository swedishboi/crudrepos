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
                                    id = reader.GetInt32(0),
                                    name = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
                connect.Close();
            }
            return profilesDTOList;
        }
    }//end
}