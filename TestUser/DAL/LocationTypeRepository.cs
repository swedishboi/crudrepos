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
    public class LocationTypeRepository
    {
        string sqlExpSelect = "select * from T_LocationType";
        string sqlExpUpdate = "update T_LocationType set locationTypeName = @locationTypeName where locationTypeId = @locationTypeId";
        string sqlExpDelete = "delete from T_LocationType where locationTypeId = @locationTypeId";
        string sqlExpInsert = "insert into T_LocationType (locationTypeName) values (@locationTypeName)";

        public List<LocationTypeDTO> SelectAll()
        {
            List<LocationTypeDTO> locationTypesDTOList = null;
            using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlExpSelect, connect))
                {
                    connect.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            locationTypesDTOList = new List<LocationTypeDTO>();
                            while (reader.Read())
                            {
                                locationTypesDTOList.Add(new LocationTypeDTO
                                {
                                    locationTypeId = reader.GetInt32(0),
                                    locationTypeName = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
                connect.Close();
            }
            return locationTypesDTOList;
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
                        command.Parameters.Add("@locationTypeId", SqlDbType.Int).Value = _id;
                        command.Parameters.Add("@locationTypeName", SqlDbType.NVarChar).Value = _name;
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
                        command.Parameters.Add("@locationTypeId", SqlDbType.Int).Value = _id;
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
                        command.Parameters.Add("@locationTypeName", SqlDbType.NVarChar).Value = _name;
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
            List<LocationTypeDTO> list = SelectAll();
            LocationTypeDTO locationType = list.Where(p => p.locationTypeName == _name).FirstOrDefault();
            return locationType != null ? true : false;
        }
    }
}