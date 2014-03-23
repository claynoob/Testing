using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL
{
    public class AddressDAO
    {
        public List<AddressDM> ReadAddress(string statement, SqlParameter[] parameters)
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
                    List<AddressDM> addresses = new List<AddressDM>();
                    while (data.Read())
                    {
                        AddressDM address = new AddressDM();
                        address.id = Convert.ToInt32(data["id"]);
                        address.street = data["street"].ToString();
                        address.city = data["city"].ToString();
                        address.state = data["state"].ToString();
                        address.zip = data["zip"].ToString();
                        addresses.Add(address);
                    }
                    return addresses;
                }
            }
        }
        public AddressDM GetAddress(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id",id)
                };
                return ReadAddress("GetAddressById", parameters).SingleOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public int GetAddressId(string street, string city, string state, string zip)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Street",street),
                    new SqlParameter("@City",city),
                    new SqlParameter("@State",state),
                    new SqlParameter("@Zip",zip)
                };
                return ReadAddress("GetAddressId",
                    parameters).SingleOrDefault().id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public void CreateAddress(string street, string city, string state, string zip)
        {
            DAO dao = new DAO();
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Street",street),
                    new SqlParameter("@City",city),
                    new SqlParameter("@State",state),
                    new SqlParameter("@Zip",zip)
                };
            dao.Write("CreateAddress", parameters);
        }
        public bool DoesAddressExist(int id)
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