using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL
{
    public class PhoneDAO
    {
        public List<PhoneDM> ReadPhone(string statement, SqlParameter[] parameters)
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
                    List<PhoneDM> phones = new List<PhoneDM>();
                    while (data.Read())
                    {
                        PhoneDM phone = new PhoneDM();
                        phone.id = Convert.ToInt32(data["id"]);
                        phone.number = data["number"].ToString();
                        phones.Add(phone);
                    }
                    return phones;
                }
            }
        }
        public string GetPhone(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id",id)
                };
                return ReadPhone("GetPhoneById", parameters).SingleOrDefault().number;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public int GetPhoneId(string number)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Number",number)
                };
                return ReadPhone("GetPhoneId",
                    parameters).FirstOrDefault().id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public void CreatePhone(string number)
        {
            DAO dao = new DAO();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Number",number)
            };
            dao.Write("CreatePhone", parameters);
        }
        public bool DoesPhoneExist(int id)
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