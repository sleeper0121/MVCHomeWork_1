using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyMVCTest01.Models;

namespace MyMVCTest01.Controllers
{
    public class 客戶資料Controller : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();

  

        private 客戶資料Repository repo = null;

        public 客戶資料Repository Repo
        {
            get
            {
                if (repo == null)
                {
                    return repo = RepositoryHelper.Get客戶資料Repository();
                }
                else {
                    return repo;
                }
            }
        }

        // GET: 客戶資料
        public ActionResult Index()
        {
            return View(Repo.All());
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料 客戶資料 = Repo.Find(id);
            //ViewBag.Contacts = 客戶資料.客戶聯絡人.id;
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
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                Repo.Add(客戶資料);
                Repo.UnitOfWork.Commit();
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
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料 客戶資料 = Repo.Find(id);

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
        public ActionResult Edit(int id,FormCollection form)
        {
            var customer = Repo.Find(id);

            if(TryUpdateModel<客戶資料>(customer , new string[] { "Id","客戶名稱","統一編號","電話","傳真","地址","Email", "客戶列表" }))
            { 
                Repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            return View(customer);
        }

        [HandleError(ExceptionType = typeof(ArgumentException), View = "Error")]
        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            throw new ArgumentException("YOLO");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = Repo.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        public ActionResult 客戶列表()
        {
            var repo_listC = RepositoryHelper.Get客戶列表Repository(Repo.UnitOfWork);

            //List<SelectListItem> listItem = new List<SelectListItem>();
            //listItem.Add(new SelectListItem() { }) 

            ViewData["List"] = new SelectList(Repo.All().Select(p=>p.客戶分類).Distinct());
            
            //if (!String.IsNullOrEmpty(keyword))
            //{
            //    var data = repo_listC.All().Where(p => p.客戶名稱 == keyword);
            //    return View(data);
            //}
            //else
            //{
                var data = repo_listC.All();
                return View(data);
            //}
        }
        [HttpPost]
        public ActionResult 客戶列表(string List)
        {
            var repo_listC = RepositoryHelper.Get客戶列表Repository(Repo.UnitOfWork);

            //List<SelectListItem> listItem = new List<SelectListItem>();
            //listItem.Add(new SelectListItem() { }) 

            ViewData["List"] = new SelectList(Repo.All().Select(p => p.客戶分類).Distinct());

            if (!String.IsNullOrEmpty(List))
            {
                var data = repo_listC.All().Where(p => p.客戶分類 == List);
                return View(data);
            }
            else
            {
                var data = repo_listC.All();
                return View(data);
            }
        }


        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = Repo.Find(id);
            客戶資料.是否已刪除 = true;
            Repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var db = (客戶資料Entities)Repo.UnitOfWork.Context;
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
