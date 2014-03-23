using BLL;
using MVCSports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSports.Controllers
{
    public class OrderController : Controller
    {
        static List<Order> orderList = new List<Order>();
        static List<Order> finalOrderList = new List<Order>();
        //
        // GET: /Order/
        public ActionResult Index()
        {
            List<Order> orderList = GetAllOrders();
            return View(orderList);
        }
        public List<Order> GetAllOrders()
        {
            List<Order> orderList = new List<Order>();
            List<OrderVM> newOrder = new List<OrderVM>();
            Logic logic = new Logic();
            Order order = new Order();
            newOrder = logic.GetAllOrders();
            foreach (OrderVM orderVm in newOrder)
            {
                order = ConvertOrderVmToOrder(orderVm);
                bool x = orderList.Contains(order);
                if (x == false)
                {
                    orderList.Add(order);
                }
            }
            return orderList;
        }
        public Order ConvertOrderVmToOrder(OrderVM orderVm)
        {
            Order order = new Order();
            if (orderVm != null)
            {
                order.id = orderVm.id;
                order.category = orderVm.category;
                order.type = orderVm.type;
                order.lastName = orderVm.lastName;
                order.firstName = orderVm.firstName;
                order.email = orderVm.email;
                order.dateOfBirth = orderVm.dateOfBirth;
                order.street = orderVm.street;
                order.city = orderVm.city;
                order.state = orderVm.state;
                order.zip = orderVm.zip;
                order.addressType = orderVm.addressType;
                order.number = orderVm.number;
                order.phoneType = orderVm.phoneType;
            }
            return order;
        }

        //
        // GET: /Order/Details/5
        public ActionResult Details(Order order)
        {
            return View(order);
        }

        //
        // GET: /Order/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Order/Create
        [HttpPost]
        public ActionResult Create(Order order)
        {
            Logic logic = new Logic();
            if (!ModelState.IsValid)
            {
                return View("Create", order);
            }
            logic.CreateOrder(order.category, order.type, order.lastName, order.firstName, order.email, order.dateOfBirth,
                order.street, order.city, order.state, order.zip, order.addressType, order.number, order.phoneType);
            orderList.Add(order);
            return RedirectToAction("Index");
        }

        //
        // GET: /Order/Edit/5
        public ActionResult Edit(Order order)
        {
            return View(order);
        }

        //
        // POST: /Order/Edit/5
        [HttpPost]
        public ActionResult Edit(Order order, FormCollection collection)
        {
            List<Order> orderList = GetAllOrders();
            Logic logic = new Logic();
            if (!ModelState.IsValid)
            {
                return View("Edit", order);
            }
            foreach (Order o in orderList)
            {
                if (o.id == order.id)
                {
                    o.id = order.id;
                    o.category = order.category ?? "None";
                    o.type = order.type ?? "None";
                    o.lastName = order.lastName ?? "None";
                    o.firstName = order.firstName ?? "None";
                    o.email = order.email ?? "None";
                    o.dateOfBirth = order.dateOfBirth;
                    o.street = order.street ?? "None";
                    o.city = order.city ?? "None";
                    o.state = order.state ?? "None";
                    o.zip = order.zip ?? "None";
                    o.addressType = order.addressType ?? "None";
                    o.number = order.number ?? "None";
                    o.phoneType = order.phoneType ?? "None";
                    logic.UpdateOrder(o.id, o.category, o.type, o.lastName, o.firstName, o.email, o.dateOfBirth,
                        o.street, o.city, o.state, o.zip, o.addressType, o.number, o.phoneType);
                }
            }
            return RedirectToAction("Index");
        }
        //
        // GET: /Order/Delete/5
        public ActionResult Delete(Order order)
        {
            return View(order);
        }

        //
        // POST: /Order/Delete/5
        [HttpPost]
        public ActionResult Delete(Order o, FormCollection collection)
        {
            List<OrderVM> newOrder = new List<OrderVM>();
            Logic logic = new Logic();
            newOrder = logic.GetAllOrders();
            foreach (OrderVM order in newOrder)
            {
                if (order.id == o.id)
                {
                    logic.DeleteOrder(order.id);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
