using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL
{
    public class CategoryDAO
    {
        public List<CategoryDM> ReadType(string statement, SqlParameter[] parameters)
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
                    List<CategoryDM> categories = new List<CategoryDM>();
                    while (data.Read())
                    {
                        CategoryDM category = new CategoryDM();
                        category.id = Convert.ToInt32(data["id"]);
                        category.category = data["category"].ToString();
                        categories.Add(category);
                    }
                    return categories;
                }
            }
        }
        public string GetCategory(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id",id)
                };
                return ReadType("GetCategoryById", parameters).SingleOrDefault().category;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public int GetCategoryId(string category)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Category",category)
                };
                return ReadType("GetCategoryIdByCategoryName",
                    parameters).FirstOrDefault().id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public void CreateCategory(string category)
        {
            DAO dao = new DAO();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Category",category)
            };
            dao.Write("CreateCategory", parameters);
        }
        public bool DoesCategoryExist(int id)
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