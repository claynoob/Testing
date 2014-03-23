using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL
{
    public class UserDAO
    {
        public List<UserDM> ReadUser(string statement, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(
                @"Data Source=.\SQLEXPRESS;Initial Catalog=SportsEquipment;Integrated Security=SSPI;"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    SqlDataReader data = command.ExecuteReader();
                    List<UserDM> users = new List<UserDM>();
                    while (data.Read())
                    {
                        UserDM user = new UserDM();
                        user.id = Convert.ToInt32(data["userId"]);
                        user.personAddressId = Convert.ToInt32(data["personAddressId"]);
                        user.personPhoneId = Convert.ToInt32(data["personPhoneId"]);
                        users.Add(user);
                    }
                    return users;
                }
            }
        }
        public void CreateUser(int personAddressId, int personPhoneId)
        {
            DAO dao = new DAO();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@PersonAddressId",personAddressId),
                new SqlParameter("@PersonPhoneId",personPhoneId)
            };
            dao.Write("CreateUser", parameters);
        }
        public void DeleteUser(int id)
        {
            DAO dao = new DAO();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id",id)
            };
            dao.Write("DeleteUserById", parameters);
        }
        public List<UserDM> GetAllUsers()
        {
            List<UserDM> allUsers = ReadUser("GetAllUsers", null);
            if (allUsers != null)
            {
                return allUsers;
            }
            else
            {
                allUsers.Take(0).ToList();
                return allUsers;
            }
        }
        public UserDM GetUser(int personAddressId, int personPhoneId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@PersonAddressId",personAddressId),
                new SqlParameter("@PersonPhoneId",personPhoneId)
            };
            UserDM us = new UserDM();
            List<UserDM> users = new List<UserDM>();
            users = ReadUser("GetUser", parameters);
            foreach (UserDM userDm in users)
            {
                us = userDm;
            }
            return us;
        }
        public UserDM GetUserById(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id",id)
            };
            return ReadUser("GetUserById", parameters).SingleOrDefault();
        }
        public void UpdateUserDB(int id, int personAddressId, int personPhoneId)
        {
            DAO dao = new DAO();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@PersonAddressId", personAddressId),
                new SqlParameter("@PersonPhoneId", personPhoneId)
            };
            dao.Write("UpdateUser", parameters);
        }
        public bool DoesUserExist(int id)
        {
            if (id != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}