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
using ClosedXML.Excel;

namespace MVC5WebWork.Controllers
{
    public class 客戶聯絡人Controller : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();
        客戶聯絡人Repository 客戶聯絡人repo = RepositoryHelper.Get客戶聯絡人Repository();
        客戶資料Repository 客戶資料repo = RepositoryHelper.Get客戶資料Repository();


        // GET: 客戶聯絡人
        public ActionResult Index(string SortBy,string search, int PageNo = 1)
        {
            var 客戶聯絡人 = 客戶聯絡人repo.Get篩選後客戶聯絡人(SortBy, search);

            return View(客戶聯絡人.ToPagedList(PageNo, 10));
        }

        public ActionResult SaveToExcel(string FileName, string SortBy, string search, int PageNo = 1)
        {
            var 客戶聯絡人 = 客戶聯絡人repo.Get篩選後客戶聯絡人(SortBy, search);

            XLWorkbook xl = new XLWorkbook();
            var 標籤名稱 = xl.Worksheets.Add(FileName + "清單");

            string[] str = new string[] { "職稱", "姓名", "Email", "手機", "電話", "客戶名稱" };
            int colIdx = 1;

            for (int i = 0; i < str.Length; i++)
            {
                標籤名稱.Cell(1, colIdx).Value = str[i];
                colIdx++;
            }

            int rowIdx = 2;
            foreach (var item in 客戶聯絡人.ToPagedList(PageNo, 10))
            {
                標籤名稱.Cell(rowIdx, 1).Value = item.職稱;
                標籤名稱.Cell(rowIdx, 2).Value = item.姓名;
                標籤名稱.Cell(rowIdx, 3).Value = item.Email;
                標籤名稱.Cell(rowIdx, 4).Value = item.手機;
                標籤名稱.Cell(rowIdx, 5).Value = item.電話;
                標籤名稱.Cell(rowIdx, 6).Value = item.客戶資料.客戶名稱;
                標籤名稱.Column(rowIdx).AdjustToContents();
                rowIdx++;
            }

            xl.SaveAs(Server.MapPath("~/Content/" + FileName + "清單.xlsx"));

            return File(Server.MapPath("~/Content/" + FileName + "清單.xlsx"), "application /vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName + ".xlsx");
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = 客戶聯絡人repo.Find(id.Value);
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(客戶資料repo.All(), "Id", "客戶名稱");
            //ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,IsDelete")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                客戶聯絡人repo.Add(客戶聯絡人);
                客戶聯絡人repo.UnitOfWork.Commit();
                //db.客戶聯絡人.Add(客戶聯絡人);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(客戶資料repo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            //ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = 客戶聯絡人repo.Find(id.Value);
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(客戶資料repo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            //ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,IsDelete")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                var data = 客戶聯絡人repo.UnitOfWork.Context;
                data.Entry(客戶聯絡人).State = EntityState.Modified;
                客戶聯絡人repo.UnitOfWork.Commit();
                //db.Entry(客戶聯絡人).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(客戶資料repo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            //ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = 客戶聯絡人repo.Find(id.Value);
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = 客戶聯絡人repo.Find(id);
            客戶聯絡人repo.Delete(客戶聯絡人);
            客戶聯絡人repo.UnitOfWork.Commit();
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            //db.客戶聯絡人.Remove(客戶聯絡人);
            //db.SaveChanges();
            return RedirectToAction("Index");
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
