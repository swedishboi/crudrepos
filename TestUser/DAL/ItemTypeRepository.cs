﻿using System;
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
        string sqlExpUpdate = "update T_ItemType set itemTypeName = @itemTypeName where itemTypeId = @itemTypeId";
        string sqlExpDelete = "delete from T_ItemType where itemTypeId = @itemTypeId";
        string sqlExpInsert = "insert into T_ItemType (itemTypeName) values (@itemTypeName)";

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
                                    itemTypeId = reader.GetInt32(0),
                                    itemTypeName = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
                connect.Close();
            }
            return itemTypesDTOList;
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
                        command.Parameters.Add("@itemTypeId", SqlDbType.Int).Value = _id;
                        command.Parameters.Add("@itemTypeName", SqlDbType.NVarChar).Value = _name;
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
                        command.Parameters.Add("@itemTypeId", SqlDbType.Int).Value = _id;
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
                        command.Parameters.Add("@itemTypeName", SqlDbType.NVarChar).Value = _name;
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
            List<ItemTypeDTO> list = SelectAll();
            ItemTypeDTO itemType = list.Where(p => p.itemTypeName == _name).FirstOrDefault();
            return itemType != null ? true : false;
        }
    }//end
}