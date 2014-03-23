using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL
{
    public class TypeDAO
    {
        public List<TypeDM> ReadType(string statement, SqlParameter[] parameters)
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
                    List<TypeDM> types = new List<TypeDM>();
                    while (data.Read())
                    {
                        TypeDM type = new TypeDM();
                        type.id = Convert.ToInt32(data["id"]);
                        type.type = data["type"].ToString();
                        types.Add(type);
                    }
                    return types;
                }
            }
        }
        public string GetType(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id",id)
                };
                return ReadType("GetTypeById", parameters).SingleOrDefault().type;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public int GetTypeId(string type)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Type",type)
                };
                return ReadType("GetTypeId",parameters).FirstOrDefault().id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public void CreateType(string type)
        {
            DAO dao = new DAO();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Type",type)
            };
            dao.Write("CreateType", parameters);
        }
        public bool DoesTypeExist(int id)
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