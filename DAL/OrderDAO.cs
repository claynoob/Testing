using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL
{
    public class OrderDAO
    {
        public List<OrderDM> ReadOrder(string statement, SqlParameter[] parameters)
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
                    List<OrderDM> orderList = new List<OrderDM>();
                    while (data.Read())
                    {
                        OrderDM order = new OrderDM();
                        order.id = Convert.ToInt32(data["id"]);
                        order.equipmentId = Convert.ToInt32(data["equipmentId"]);
                        order.userId = Convert.ToInt32(data["userId"]);
                        orderList.Add(order);
                    }
                    return orderList;
                }
            }
        }
        public List<OrderDM> GetAllOrders()
        {
            List<OrderDM> allOrders = ReadOrder("GetAllOrders", null);
            if (allOrders != null)
            {
                return allOrders;
            }
            else
            {
                allOrders.Take(0).ToList();
                return allOrders;
            }
        }
        public OrderDM GetOrder(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id",id)
                };
                return ReadOrder("GetOrderById", parameters).SingleOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public int GetOrderId(int equipmentId, int userId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EquipmentId",equipmentId),
                    new SqlParameter("@UserId",userId)
                };
                return ReadOrder("GetOrderId",
                    parameters).SingleOrDefault().id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public void CreateOrder(OrderDM order)
        {
            DAO dao = new DAO();
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EquipmentId",order.equipmentId),
                    new SqlParameter("@UserId",order.userId)
                };
            dao.Write("CreateOrder", parameters);
        }
        public void DeleteOrder(int id)
        {
            DAO dao = new DAO();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id",id)
            };
            dao.Write("DeleteOrder", parameters);
        }
        public bool DoesOrderExist(int id)
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
        public void UpdateOrderDB(int id, int equipmentId, int userId)
        {
            DAO dao = new DAO();
            EquipmentDAO edao = new EquipmentDAO();
            UserDAO udao = new UserDAO();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id",id),
                new SqlParameter("@EquipmentId", equipmentId),
                new SqlParameter("@UserId", userId)
            };
            dao.Write("UpdateOrder", parameters);
        }
    }
}