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
                                    positionId = reader.GetInt32(0),
                                    positionName = reader.GetString(1)
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
            if (IsNull(_name)) return flag;
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

        public bool IsNull(string _name)
        {
            List<PositionDTO> list = SelectAll();
            PositionDTO pos = list.Where(p => p.positionName == _name).FirstOrDefault();
            return pos!=null ? true : false;
        }
    }//end
}