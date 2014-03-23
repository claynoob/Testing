using BLL;
using MVCSports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSports.Controllers
{
    public class UserController : Controller
    {
        static List<User> users = new List<User>();
        static List<User> finalUsers = new List<User>();
        //
        // GET: /User/
        public ActionResult Index()
        {
            List<User> userIndexList = GetAllUsers();
            return View(userIndexList);
        }
        public List<User> GetAllUsers()
        {
            List<User> userList = new List<User>();
            List<UserVM> newUser = new List<UserVM>();
            Logic logic = new Logic();
            User user = new User();
            newUser = logic.GetAllUsers();
            foreach (UserVM userVm in newUser)
            {
                user = ConvertUserVmToUser(userVm);
                bool x = userList.Contains(user);
                if (x == false)
                {
                    userList.Add(user);
                }
            }
            return userList;
        }
        public User ConvertUserVmToUser(UserVM userVm)
        {
            User user = new User();
            if (userVm != null)
            {
                user.id = userVm.id;
                user.lastName = userVm.lastName;
                user.firstName = userVm.firstName;
                user.email = userVm.email;
                user.dateOfBirth = userVm.dateOfBirth;
                user.street = userVm.street;
                user.city = userVm.city;
                user.state = userVm.state;
                user.zip = userVm.zip;
                user.addressType = userVm.addressType;
                user.number = userVm.number;
                user.phoneType = userVm.phoneType;
            }
            return user;
        }

        //
        // GET: /User/Details/5
        public ActionResult Details(User user)
        {
            return View(user);
        }

        //
        // GET: /User/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            Logic logic = new Logic();
            if (!ModelState.IsValid)
            {
                return View("Create", user);
            }
            logic.CreateUserVM(user.id, user.lastName, user.firstName, user.email, user.dateOfBirth,
                user.street, user.city, user.state, user.zip, user.addressType,
                user.number, user.phoneType);
            users.Add(user);
            return RedirectToAction("Index");
        }

        //
        // GET: /User/Edit/5
        public ActionResult Edit(User user)
        {
            return View(user);
        }

        //
        // POST: /User/Edit/5
        [HttpPost]
        public ActionResult Edit(User user, FormCollection collection)
        {
            List<User> userList = GetAllUsers();
            Logic logic = new Logic();
            if (!ModelState.IsValid)
            {
                return View("Edit", user);
            }
            foreach (User us in userList)
            {
                if (us.id == user.id)
                {
                    us.id = user.id;
                    us.lastName = user.lastName ?? "None";
                    us.firstName = user.firstName ?? "None";
                    us.email = user.email ?? "None";
                    us.dateOfBirth = user.dateOfBirth;
                    us.street = user.street ?? "None";
                    us.city = user.city ?? "None";
                    us.state = user.state ?? "None";
                    us.zip = user.zip ?? "None";
                    us.addressType = user.addressType ?? "None";
                    us.number = user.number ?? "None";
                    us.phoneType = user.phoneType ?? "None";
                    logic.UpdateUser(us.id, us.lastName, us.firstName, us.email, us.dateOfBirth,
                        us.street, us.city, us.state, us.zip, us.addressType,
                        us.number, us.phoneType);
                }
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /User/Delete/5
        public ActionResult Delete(User user)
        {
            return View(user);
        }

        //
        // POST: /User/Delete/5
        [HttpPost]
        public ActionResult Delete(User user, FormCollection collection)
        {
            List<UserVM> newUser = new List<UserVM>();
            Logic logic = new Logic();
            newUser = logic.GetAllUsers();
            foreach (UserVM us in newUser)
            {
                if (us.id == user.id)
                {
                    logic.DeleteUser(us.id);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
