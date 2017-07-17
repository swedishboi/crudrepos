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
    public class UserRepository
    {
        string sqlExpSelect = "select T_Person.*, T_Authentication.login from T_Authentication inner join T_Person on T_Authentication.personId = T_Person.personId";
        string sqlExpInsert = "insert into T_User values (@id, @name, @surname, @patronymic, @mobile)";
        public List<UserDTO> SelectAll()
        {
            List<UserDTO> usersDTOList = null;
            using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlExpSelect, connect))
                {
                    connect.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            usersDTOList = new List<UserDTO>();
                            while (reader.Read())
                            {
                                usersDTOList.Add(new UserDTO
                                {
                                    id = reader.GetGuid(0),
                                    name =reader.GetString(1),
                                    surname =reader.GetString(2),
                                    patronymic = reader.GetString(3),
                                    mobile = reader.GetString(4),
                                    login = reader.GetString(5)
                                });
                            }
                        }
                    }
                }
                connect.Close();
            }
            return usersDTOList;
        }
        public UserDTO Create(Guid _id, string _name, string _surname, string _patronymic, string _mobile, string _login)
        {
            List<UserDTO> usersDTOList = SelectAll();
            if (usersDTOList == null)
                return null;
            UserDTO userDTO = usersDTOList.Where(p => p.login == _login).FirstOrDefault();
            if (userDTO == null)
            {
                userDTO = new UserDTO
                {
                    id = _id,
                    name = _name,
                    surname = _surname,
                    patronymic = _patronymic,
                    mobile = _mobile,
                    login = _login
                };
                this.Insert(userDTO);
                return userDTO;
            }
            return null;
        }
        public bool Insert (UserDTO userDTO)
        {
            bool flag = false;
            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlExpInsert, connect))
                    {
                        command.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = userDTO.id;
                        command.Parameters.Add("@name", SqlDbType.NVarChar).Value = userDTO.name;
                        command.Parameters.Add("@surname", SqlDbType.NVarChar).Value = userDTO.surname;
                        command.Parameters.Add("@patronymic", SqlDbType.NVarChar).Value = userDTO.patronymic;
                        command.Parameters.Add("@mobile", SqlDbType.NVarChar).Value = userDTO.mobile;
                        connect.Open();
                        command.ExecuteNonQuery();
                    }
                    connect.Close();
                    flag = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return flag;
        }
    }//end
}