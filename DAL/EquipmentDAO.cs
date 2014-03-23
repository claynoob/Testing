using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL
{
    public class EquipmentDAO
    {
        public List<EquipmentDM> ReadEquipment(string statement, SqlParameter[] parameters)
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
                    List<EquipmentDM> equipmentList = new List<EquipmentDM>();
                    while (data.Read())
                    {
                        EquipmentDM equipment = new EquipmentDM();
                        equipment.id = Convert.ToInt32(data["equipmentId"]);
                        equipment.categoryId = Convert.ToInt32(data["categoryId"]);
                        equipment.typeId = Convert.ToInt32(data["typeId"]);
                        equipmentList.Add(equipment);
                    }
                    return equipmentList;
                }
            }
        }
        public EquipmentDM GetEquipment(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id",id)
                };
                return ReadEquipment("GetEquipmentById", parameters).SingleOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<EquipmentDM> GetAllEquipment()
        {
            List<EquipmentDM> allEquipment = ReadEquipment("GetAllEquipment", null);
            if (allEquipment != null)
            {
                return allEquipment;
            }
            else
            {
                allEquipment.Take(0).ToList();
                return allEquipment;
            }
        }
        public int GetEquipmentId(int categoryId, int typeId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@CategoryId",categoryId),
                    new SqlParameter("@TypeId",typeId)
                };
                return ReadEquipment("GetEquipmentId",parameters).SingleOrDefault().id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public void CreateEquipment(int categoryId, int typeId)
        {
            DAO dao = new DAO();
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@CategoryId",categoryId),
                    new SqlParameter("@TypeId",typeId)
                };
            dao.Write("CreateEquipment", parameters);
        }
        public bool DoesEquipmentExist(int id)
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
        public void DeleteEquipment(int id)
        {
            DAO dao = new DAO();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id",id)
            };
            dao.Write("DeleteEquipmentById", parameters);
        }
        public void UpdateEquipmentDB(int id, int categoryId, int typeId)
        {
            DAO dao = new DAO();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id",id),
                new SqlParameter("@CategoryId", categoryId),
                new SqlParameter("@TypeId", typeId)
            };
            dao.Write("UpdateEquipment", parameters);
        }
        public EquipmentDM GetEquipmentById(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id",id)
            };
            return ReadEquipment("GetEquipmentById", parameters).SingleOrDefault();
        }
        public EquipmentDM GetEquipment(int categoryId, int typeId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CategoryId",categoryId),
                new SqlParameter("@TypeId",typeId)
            };
            return ReadEquipment("GetEquipment", parameters).SingleOrDefault();
        }
        public EquipmentDM GetEquipment(string category, string type)
        {
            int categoryId = GetEquipmentCategoryId(category);
            int typeId = GetEquipmentTypeId(type);
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@CategoryId",categoryId),
                    new SqlParameter("@TypeId",typeId)
                };
            EquipmentDM equip = new EquipmentDM();
            List<EquipmentDM> equ = new List<EquipmentDM>();
            equ = ReadEquipment("GetEquipment", parameters);
            foreach (EquipmentDM equipmentDm in equ)
            {
                equip = equipmentDm;
            }
            return equip;
        }
        public static int GetEquipmentCategoryId(string category)
        {
            CategoryDAO dao = new CategoryDAO();
            int categoryId = dao.GetCategoryId(category);
            if (categoryId == 0)
            {
                dao.CreateCategory(category);
                categoryId = dao.GetCategoryId(category);
            }
            return categoryId;
        }
        public static int GetEquipmentTypeId(string type)
        {
            TypeDAO dao = new TypeDAO();
            int typeId = dao.GetTypeId(type);
            if (typeId == 0)
            {
                dao.CreateType(type);
                typeId = dao.GetTypeId(type);
            }
            return typeId;
        }
    }
}