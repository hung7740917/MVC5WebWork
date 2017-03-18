using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5WebWork.Models;
using System.Web.Security;

namespace MVC5WebWork.Controllers
{
    public class 會員資料Controller : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();
        會員資料Repository repo = RepositoryHelper.Get會員資料Repository();

        // GET: 會員資料/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: 會員資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Account,Password,姓名")] 會員資料 會員資料)
        {
            if (ModelState.IsValid)
            {
                repo.Add(會員資料);
                repo.UnitOfWork.Commit();
                //db.會員資料.Add(會員資料);
                //db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(會員資料);
        }

        // GET: 會員資料/Edit/5
        public ActionResult Edit(string account)
        {
            if (account == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            會員資料 會員資料 = repo.Find(account);
            //會員資料 會員資料 = db.會員資料.Find(id);
            if (會員資料 == null)
            {
                return HttpNotFound();
            }
            return View(會員資料);
        }

        // POST: 會員資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Account,Password,姓名")] 會員資料 會員資料)
        {
            if (ModelState.IsValid)
            {
                var data = repo.UnitOfWork.Context;
                data.Entry(會員資料).State = EntityState.Modified;
                repo.UnitOfWork.Commit();
                //db.Entry(會員資料).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(會員資料);
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "客戶資料");
            }
            return View();

        }
        [HttpPost]
        public ActionResult Login(LoginVM loginVM)
        {

            if (ModelState.IsValid)
            {
                string strAccount = loginVM.Account;
                //string userData = "user";

                FormsAuthentication.RedirectFromLoginPage(strAccount, false);
                //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                //    strAccount,
                //    DateTime.Now,
                //    DateTime.Now.AddMinutes(30),
                //    false,
                //    userData,
                //    FormsAuthentication.FormsCookiePath);
                //string enTicket = FormsAuthentication.Encrypt(ticket);
                //Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, enTicket));
                return RedirectToAction("Index", "客戶資料");
            }
            return View(loginVM);
        }

        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "會員資料");
        }
    }
}
