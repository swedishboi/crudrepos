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
    public class DateTypeRepository
    {
    	string sqlExpSelect = "select * from T_DateTypeCharacteristic";
    	public List<DateTypeDTO> SelectAll()
        {
            List<DateTypeDTO> dateTypesDTOList = null;
            using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlExpSelect, connect))
                {
                    connect.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dateTypesDTOList = new List<DateTypeDTO>();
                            while (reader.Read())
                            {
                                dateTypesDTOList.Add(new DateTypeDTO()
                                {
                                    dateTypeId = reader.GetInt32(0),
                                    dateTypeName = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
                connect.Close();
            }
            return dateTypesDTOList;
        }
    }
}