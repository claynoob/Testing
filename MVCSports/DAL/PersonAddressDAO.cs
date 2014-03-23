using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL
{
    public class PersonAddressDAO
    {
        public List<PersonAddressDM> ReadPersonAddress(string statement,
            SqlParameter[] parameters)
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
                    List<PersonAddressDM> personAddresses = new List<PersonAddressDM>();
                    while (data.Read())
                    {
                        PersonAddressDM personAddress = new PersonAddressDM();
                        personAddress.id = Convert.ToInt32(data["id"]);
                        personAddress.personId = Convert.ToInt32(data["personId"]);
                        personAddress.addressId = Convert.ToInt32(data["addressId"]);
                        personAddress.typeId = Convert.ToInt32(data["typeId"]);
                        personAddresses.Add(personAddress);
                    }
                    return personAddresses;
                }
            }
        }
        public PersonAddressDM GetPersonAddress(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id",id)
                };
                return ReadPersonAddress("GetPersonAddressById", parameters).SingleOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public int GetPersonAddressId(int personId, int addressId, int typeId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@PersonId",personId),
                    new SqlParameter("@AddressId",addressId),
                    new SqlParameter("@TypeId",typeId)
                };
                return ReadPersonAddress("GetPersonAddressId",
                    parameters).SingleOrDefault().id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public void CreatePersonAddress(int personId, int addressId, int typeId)
        {
            DAO dao = new DAO();
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@PersonId",personId),
                    new SqlParameter("@AddressId",addressId),
                    new SqlParameter("@TypeId",typeId)
                };
            dao.Write("CreatePersonAddress", parameters);
        }
        public bool DoesPersonAddressExist(int id)
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
        public void UpdatePersonAddressDB(int id, string lastName, string firstName, string email, DateTime dateOfBirth,
            string street, string city, string state, string zip, string addressType)
        {
            DAO dao = new DAO();
            PersonDAO pdao = new PersonDAO();
            AddressDAO adao = new AddressDAO();
            TypeDAO tdao = new TypeDAO();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id",id),
                new SqlParameter("@PersonId", pdao.GetPersonId(lastName, firstName, email, dateOfBirth)),
                new SqlParameter("@AddressId", adao.GetAddressId(street, city, state, zip)),
                new SqlParameter("@TypeId", tdao.GetTypeId(addressType))
            };
            dao.Write("UpdatePersonAddress", parameters);
        }
    }
}