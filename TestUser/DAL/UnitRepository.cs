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
    public class UnitRepository
    {
        string sqlExpSelect = "select * from T_ItemTypeCharacteristicsUnit";

        public List<UnitDTO> SelectAll()
        {
            List<UnitDTO> unitsDTOList = null;
            using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlExpSelect, connect))
                {
                    connect.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            unitsDTOList = new List<UnitDTO>();
                            while (reader.Read())
                            {
                                unitsDTOList.Add(new UnitDTO
                                {
                                    unitId = reader.GetInt32(0),
                                    characteristicId = reader.GetInt32(1),
                                    unitName = reader.GetString(2)
                                });
                            }
                        }
                    }
                }
                connect.Close();
            }
            return unitsDTOList;
        }
    }
}