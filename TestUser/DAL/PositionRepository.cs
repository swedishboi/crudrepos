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
    public class PositionRepository
    {
        string sqlExpSelect = "select * from T_Position";
        string sqlExpUpdate = "update T_Position set positionName = @posName where positionId = @posId";
        string sqlExpDelete = "delete from T_Position where positionId = @posId";
        string sqlExpInsert = "insert into T_Position (positionName) values (@posName)";
        public List<PositionDTO> SelectAll()
        {
            List<PositionDTO> positionsDTOList = null;
            using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlExpSelect, connect))
                {
                    connect.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            positionsDTOList = new List<PositionDTO>();
                            while (reader.Read())
                            {
                                positionsDTOList.Add(new PositionDTO
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
            return positionsDTOList;
        }
        public bool Update(int _id, string _name)
        {
            bool flag = false;
            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlExpUpdate, connect))
                    {
                        command.Parameters.Add("@posId", SqlDbType.Int).Value = _id;
                        command.Parameters.Add("@posName", SqlDbType.NVarChar).Value = _name;
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
                        command.Parameters.Add("@posId", SqlDbType.Int).Value = _id;
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
        public PositionDTO Create(string _name)
        {
            List<PositionDTO> positionsDTOList = SelectAll();
            PositionDTO positionDTO = positionsDTOList.Where(p => p.name == _name).FirstOrDefault();
            if (positionDTO == null)
            {
                PositionDTO position = new PositionDTO()
                {
                    name = _name
                };
                this.Insert(position);
                return position;
            }
            return null;
        }
        public bool Insert(PositionDTO positionDTO)
        {
            bool flag = false;
            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlExpInsert, connect))
                    {
                        command.Parameters.Add("@posName", SqlDbType.NVarChar).Value = positionDTO.name;
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
    }//end
}