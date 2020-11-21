using Seller.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seller.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public LTWebEntities _db = new LTWebEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection Fields, Users u)
        {
                Seller.Models.GlobalVari.isNoti = true;
                var user = _db.Users.Find(Fields["Username"]);
                if (user is null)
                {

                }
                else
                {
                    if (String.Equals(user.Password, Fields["Password"]))
                    {
                    switch (user.Role) {
                        case "0":
                            Seller.Models.GlobalVari.isLog = true;
                            Seller.Models.GlobalVari.userLog = user.Username;
                            Seller.Models.GlobalVari.isLogSuccess = true;

                            return RedirectToAction("adminPage", "Login");
                        case "1":

                            //return View("~/Views/Home/Index.cshtml");
                            Seller.Models.GlobalVari.isLog = true;
                            Seller.Models.GlobalVari.userLog = user.Username;
                            Seller.Models.GlobalVari.isLogSuccess = true;

                            return RedirectToAction("managerPage", "Login");
                            break;
                        case "2":
                            Seller.Models.GlobalVari.isLog = true;
                            Seller.Models.GlobalVari.userLog = user.Username;
                            Seller.Models.GlobalVari.isLogSuccess = true;

                            return RedirectToAction("userPage", "Login");
                            break;
                    }

                    }
                }
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public ActionResult Signup(FormCollection Field)
        {
            if (Field["Password"] != Field["uRepass"])
            {
                return View();
            }
            Users user = new Users();
            user.Username = Field["Username"];
            user.Password = Field["Password"];
            user.nickname = Field["nickname"];
            user.email = Field["email"];
            if (Field["type"] == "User")
            {
                user.Role = "2";
            }
            else if (Field["type"] == "Seller")
            {
                user.Role = "1";
            }
            else return View();
            _db.Users.Add(user);
            _db.SaveChanges();

            return RedirectToAction("Login", "Login");
        }
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }
        public ActionResult userPage()
        {
            
            return View();
        }
        public ActionResult managerPage()
        {
            return View();
        }
        public ActionResult adminPage()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Seller.Models.GlobalVari.userLog = "";
            Seller.Models.GlobalVari.isLog = false;
            return RedirectToAction("Index", "Home");
        }

    }
}