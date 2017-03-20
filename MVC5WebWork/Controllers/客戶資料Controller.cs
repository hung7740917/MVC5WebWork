using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5WebWork.Models;
using PagedList;
using System.Web.Security;
using ClosedXML.Excel;

namespace MVC5WebWork.Controllers
{
    public class 客戶資料Controller : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();
        客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();

        // GET: 客戶資料
        public ActionResult Index(string SortBy, string search, int PageNo = 1)
        {
            var data = repo.Get篩選後客戶資料(SortBy,search);

            ViewBag.search = search;
            ViewBag.SortBy = SortBy;
            ViewBag.PageNo = PageNo;

            return View(data.ToPagedList(PageNo, 10));
        }

        public ActionResult SaveToExcel(string FileName, string SortBy, string search, int PageNo = 1)
        {
            var data = repo.Get篩選後客戶資料(SortBy, search);

            XLWorkbook xl = new XLWorkbook();
            var 標籤名稱 = xl.Worksheets.Add(FileName + "清單");

            string[] str = new string[] { "客戶名稱", "統一編號", "電話", "傳真", "地址", "Email" };
            int colIdx = 1;

            for (int i = 0; i < str.Length; i++)
            {
                標籤名稱.Cell(1, colIdx).Value = str[i];
                colIdx++;
            }

            int rowIdx = 2;
            foreach (var item in data.ToPagedList(PageNo, 10))
            {
                標籤名稱.Cell(rowIdx, 1).Value = item.客戶名稱;
                標籤名稱.Cell(rowIdx, 2).Value = item.統一編號;
                標籤名稱.Cell(rowIdx, 3).Value = item.電話;
                標籤名稱.Cell(rowIdx, 4).Value = item.傳真;
                標籤名稱.Cell(rowIdx, 5).Value = item.地址;
                標籤名稱.Cell(rowIdx, 6).Value = item.Email;
                標籤名稱.Column(rowIdx).AdjustToContents();
                rowIdx++;
            }

            xl.SaveAs(Server.MapPath("~/Content/closedXml.xlsx"));

            return File(Server.MapPath("~/Content/" + FileName + "清單.xlsx"), "application /vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName + ".xlsx");
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id.Value);
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                //客戶資料.密碼 = FormsAuthentication.HashPasswordForStoringInConfigFile(客戶資料.密碼, "SHA1");
                repo.Add(客戶資料);
                repo.UnitOfWork.Commit();
                //db.客戶資料.Add(客戶資料);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            客戶資料 客戶資料 = repo.Find(id.Value);
            //客戶資料 客戶資料 = db.客戶資料.Find(id);

            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            var data = repo.Find(id);
            if (TryUpdateModel(data, new string[] { "密碼", "電話", "傳真", "地址", "Email" }))
            {
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(data);
            //if (ModelState.IsValid)
            //{
            //    var data = repo.UnitOfWork.Context;
            //    data.Entry(客戶資料).State = EntityState.Modified;
            //    repo.UnitOfWork.Commit();
            //    //db.Entry(客戶資料).State = EntityState.Modified;
            //    //db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id.Value);
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = repo.Find(id);
            repo.Delete(客戶資料);
            repo.UnitOfWork.Commit();
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            //db.客戶資料.Remove(客戶資料);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult 客戶清單(string search)
        {
            客戶清單檢視表Repository 客戶清單檢視表repo = RepositoryHelper.Get客戶清單檢視表Repository();

            var data = 客戶清單檢視表repo.All().AsQueryable();

            if (!String.IsNullOrEmpty(search))
            {
                data = data.Where(p => p.客戶名稱.Contains(search));
            }
            return View(data.ToList());
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                FormsAuthentication.RedirectFromLoginPage(loginVM.帳號, false);
                return RedirectToAction("Index");
            }

            return View(loginVM);
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
