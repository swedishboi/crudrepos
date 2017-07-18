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
        public List<UserDTO> ListAllUsers()
        {
            List<UserDTO> result = null;

            using (SqlConnection conn = new SqlConnection((ConfigurationManager.ConnectionStrings["conStr"].ConnectionString)))
            {
                string cmdText = "SELECT dbo.T_Person.name, dbo.T_Person.surname, dbo.T_Person.patronymic, dbo.T_Person.email, dbo.T_Authentication.login, dbo.T_Authentication.password, dbo.T_Person.personId FROM dbo.T_Person INNER JOIN dbo.T_Authentication ON dbo.T_Person.personId = dbo.T_Authentication.personId ";

                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            result = new List<UserDTO>();

                            while (reader.Read())
                            {

                                result.Add(new UserDTO()
                                {
                                    personId = reader.GetGuid(6),
                                    login = reader.GetString(4),
                                    name = reader.GetString(0),
                                    surname = reader.GetString(1),
                                    patronymic = reader.GetString(2),
                                    password = reader.GetString(5),
                                    email = reader.GetString(3)
                                });
                            }
                        }
                    }
                }

                conn.Close();
            }

            return result;

        }

        public UserDTO LogIn(string login, string password)
        {
            UserDTO user = (from t in this.ListAllUsers()
                             where t.login == login
                            && t.password == password
                             select t).FirstOrDefault();

            return user;
        }

        public bool CreateUserTable(int positionId,Guid personId,int profileId)
        {
            bool success = false;

            try
            {

                using (SqlConnection conn = new SqlConnection((ConfigurationManager.ConnectionStrings["conStr"].ConnectionString)))
                {
                    string cmdText = "INSERT INTO [AccountingOfMaterialValues].[dbo].[T_User]([personId],[positionId],[profileId]) VALUES (@PersonId,@PositionId,@ProfileId)";

                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.Parameters.Add("@PersonId", SqlDbType.UniqueIdentifier).Value = personId;
                        cmd.Parameters.Add("@PositionId", SqlDbType.Int).Value = positionId;
                        cmd.Parameters.Add("@ProfileId", SqlDbType.Int).Value = profileId;


                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                success = true;
            }

            catch (Exception ex) { }

            return success;
        }

        public bool CreatePerson(UserDTO newuser)
        {
            bool success = false;

            try
            {

                using (SqlConnection conn = new SqlConnection((ConfigurationManager.ConnectionStrings["conStr"].ConnectionString)))
                {
                    string cmdText = " INSERT INTO [AccountingOfMaterialValues].[dbo].[T_Person]([personId],[name],[surname],[patronymic],[email]) VALUES (@ID,@Name,@Surname,@Patronymic,@Email)";

                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = newuser.personId;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = newuser.name;
                        cmd.Parameters.Add("@Surname", SqlDbType.VarChar).Value = newuser.surname;
                        cmd.Parameters.Add("@Patronymic", SqlDbType.VarChar).Value = newuser.patronymic;
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = newuser.email;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                success = true;
            }

            catch (Exception ex) { }

            return success;
        }

        public bool CreateAuthentication(UserDTO newuser)
        {
            bool success = false;

            try
            {

                using (SqlConnection conn = new SqlConnection((ConfigurationManager.ConnectionStrings["conStr"].ConnectionString)))
                {
                    string cmdText = "INSERT INTO [AccountingOfMaterialValues].[dbo].[T_Authentication] ([login],[password],[personId]) VALUES (@Login,@Password,@PersonId)";

                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.Parameters.Add("@PersonId", SqlDbType.UniqueIdentifier).Value = newuser.personId;
                        cmd.Parameters.Add("@Login", SqlDbType.VarChar).Value = newuser.login;
                        cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = newuser.password;
                        

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                success = true;
            }

            catch (Exception ex) { }

            return success;
        }


        public bool Register(string login, string password, string email, Guid id, string name, string patronymic,string surname,List<PositionDTO> positionList,List<ProfileDTO> profileList)
        {
            bool success = false;
            List<UserDTO> userList = this.ListAllUsers();

            UserDTO testUser = userList.Where(p => p.login == login).FirstOrDefault();

            if (testUser == null)
            {
                UserDTO user = new UserDTO()
                {
                    login = login,
                    password = password,
                    email = email,                   
                    personId=id,
                    name=name,
                    patronymic=patronymic,
                    surname=surname,
                    position=positionList,
                    profile=profileList
                };

                this.CreatePerson(user);
                this.CreateAuthentication(user);
                List<PositionDTO> listPosition = user.position;
                List<ProfileDTO> listProfile = user.profile;
                int count = listPosition.Count;
                for (int i = 0; i < count; i++)
                {
                    this.CreateUserTable(listPosition[i].positionId, user.personId, listProfile[i].profileId);
                }                
                success=true;
                return success;
            }

            return success;


        }
    }//end
}