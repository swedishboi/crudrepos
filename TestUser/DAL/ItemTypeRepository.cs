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
    public class ItemTypeRepository
    {
        string sqlExpSelect = "select * from T_ItemType";
        public List<ItemTypeDTO> SelectAll()
        {
            List<ItemTypeDTO> itemTypesDTOList = null;
            using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlExpSelect, connect))
                {
                    connect.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            itemTypesDTOList = new List<ItemTypeDTO>();
                            while (reader.Read())
                            {
                                itemTypesDTOList.Add(new ItemTypeDTO
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
            return itemTypesDTOList;
        }
    }//end
}