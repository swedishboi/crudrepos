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
    public class LocationRepository
    {
    	string sqlExpSelect = "select * from T_Location";
        string sqlExpSelectByItemId = 
            "select T_Location.* from T_Item inner join T_Location on T_Item.locationId = T_Location.locationId where T_Item.itemId = @itemId";
    	public List<LocationDTO> SelectAll()
        {
            List<LocationDTO> locationsDTOList = null;
            using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlExpSelect, connect))
                {
                    connect.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            locationsDTOList = new List<LocationDTO>();
                            while (reader.Read())
                            {
                                locationsDTOList.Add(new LocationDTO()
                                {
                                    locationId = reader.GetInt32(0),
                                    locationName = reader.GetString(1),
                                    locationTypeId = reader.GetInt32(2),
                                    parentId = reader.GetInt32(3)
                                });
                            }
                        }
                    }
                }
                connect.Close();
            }
            return locationsDTOList;
        }
        public List<LocationDTO> SelectByItemId(int _id)
        {
            List<LocationDTO> locationsDTOList = null;
            using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlExpSelectByItemId, connect))
                {
                    command.Parameters.Add("@itemId", SqlDbType.Int).Value = _id;
                    connect.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            locationsDTOList = new List<LocationDTO>();
                            while (reader.Read())
                            {
                                locationsDTOList.Add(new LocationDTO()
                                {
                                    locationId = reader.GetInt32(0),
                                    locationName = reader.GetString(1),
                                    locationTypeId = reader.GetInt32(2),
                                    parentId = reader.GetInt32(3)
                                });
                            }
                        }
                    }
                }
                connect.Close();
            }
            return locationsDTOList;
        }
    }
}