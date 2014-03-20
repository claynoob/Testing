using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL
{
    public class PersonPhoneDAO
    {
        public List<PersonPhoneDM> ReadPersonPhone(string statement, SqlParameter[] parameters)
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
                    List<PersonPhoneDM> personPhones = new List<PersonPhoneDM>();
                    while (data.Read())
                    {
                        PersonPhoneDM personPhone = new PersonPhoneDM();
                        personPhone.id = Convert.ToInt32(data["id"]);
                        personPhone.personId = Convert.ToInt32(data["personId"]);
                        personPhone.phoneId = Convert.ToInt32(data["phoneId"]);
                        personPhone.typeId = Convert.ToInt32(data["typeId"]);
                        personPhones.Add(personPhone);
                    }
                    return personPhones;
                }
            }
        }
        public PersonPhoneDM GetPersonPhone(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id",id)
                };
                return ReadPersonPhone("GetPersonPhoneById", parameters).SingleOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public int GetPersonPhoneId(int personId, int phoneId, int typeId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@PersonId",personId),
                    new SqlParameter("@PhoneId",phoneId),
                    new SqlParameter("@TypeId",typeId)
                };
                return ReadPersonPhone("GetPersonPhoneId",
                    parameters).SingleOrDefault().id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public void CreatePersonPhone(int personId, int phoneId, int typeId)
        {
            DAO dao = new DAO();
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@PersonId",personId),
                    new SqlParameter("@PhoneId",phoneId),
                    new SqlParameter("@TypeId",typeId)
                };
            dao.Write("CreatePersonPhone", parameters);
        }
        public bool DoesPersonPhoneExist(int id)
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
        public void UpdatePersonPhoneDB(int id, string lastName, string firstName, string email, DateTime dateOfBirth,
            string number, string phoneType)
        {
            DAO dao = new DAO();
            PersonDAO pdao = new PersonDAO();
            PhoneDAO phdao = new PhoneDAO();
            TypeDAO tdao = new TypeDAO();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id",id),
                new SqlParameter("@PersonId",pdao.GetPersonId(lastName, firstName, email, dateOfBirth)),
                new SqlParameter("@PhoneId",phdao.GetPhoneId(number)),
                new SqlParameter("@TypeId",tdao.GetTypeId(phoneType))
            };
            dao.Write("UpdatePersonPhone", parameters);
        }
    }
}