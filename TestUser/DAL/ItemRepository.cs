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
    public class ItemRepository
    {
        string sqlExpSelect = "select purchaseDate, itemTypeId, purchaseCost, itemId, barcode from T_Item";
        public List<ItemDTO> SelectAll()
        {
            List<ItemDTO> itemsDTOList = null;
            using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlExpSelect, connect))
                {
                    connect.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            itemsDTOList = new List<ItemDTO>();
                            while (reader.Read())
                            {
                                itemsDTOList.Add(new ItemDTO
                                {
                                    itemId = reader.GetInt32(0),
                                    barcode = reader.GetString(1),
                                    itemTypeId = reader.GetInt32(2),
                                    purchaseCost = reader.GetInt32(3),
                                    purchaseDate = reader.GetDateTime(4),
                                    responsiblePerson = reader.GetGuid(5)
                                });
                            }
                        }
                    }
                }
                connect.Close();
            }
            return itemsDTOList;
        }
    }
}