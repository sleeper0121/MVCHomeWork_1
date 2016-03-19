using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyMVCTest01.Models;
using System.Data.Entity.Validation;

namespace MyMVCTest01.Controllers
{
    public class 客戶聯絡人Controller : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();

        private 客戶聯絡人Repository repo = null;

        public 客戶聯絡人Repository Repo
        {
            get {
                if (repo == null)
                {
                    return repo = RepositoryHelper.Get客戶聯絡人Repository();
                }
                else{
                    return repo;
                }
            }                
        }

        private 客戶資料Repository repoC = null;

        public 客戶資料Repository RepoC
        {
            get
            {
                if (repoC == null)
                {
                    return repoC = RepositoryHelper.Get客戶資料Repository(Repo.UnitOfWork);
                }
                else {
                    return repoC;
                }
            }
        }




        // GET: 客戶聯絡人
        public ActionResult Index()
        {
  
            //var 客戶聯絡人 = db.客戶聯絡人.Include("客戶資料").Where(p => p.是否已刪除 == false).Where(q => q.客戶資料.是否已刪除 == false);


            return View(Repo.All().ToList());
        }

        [HttpPost]
        public ActionResult Index(string position)
        {
            if (!string.IsNullOrWhiteSpace(position))
            {
                var data = Repo.All().Where(p => p.職稱 == position);
                ViewBag.Msg = data.Count();
                return View(data);
            }
            else
            {
                ViewBag.Msg = "No data found";
                return View();
            }

        }


        public ActionResult GetContactsList(int? id)
        {
            if (id.HasValue)
            {
                var data = Repo.All().Where(p => p.客戶Id == id);
                return View(data);
            }
            return View();
        }

        [HttpPost]
        public ActionResult GetContactsList(IList<客戶聯絡人> data)
        {
            foreach (客戶聯絡人 item in data)
            {
                var contacter = Repo.Find(item.Id);
                contacter.職稱 = item.職稱;
                contacter.手機 = item.手機;
                contacter.電話 = item.電話;
            }
            Repo.UnitOfWork.Commit();
            //return RedirectToAction("Index","客戶資料");
            return View(data);
        }



        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            客戶聯絡人 客戶聯絡人 = Repo.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            //ViewBag.客戶Id = new SelectList(db.客戶資料.Where(p => p.是否已刪除 == false), "Id", "客戶名稱");

            ViewBag.客戶Id = new SelectList(RepoC.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                Repo.Add(客戶聯絡人);
                Repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
  
            ViewBag.客戶Id = new SelectList(RepoC.All() , "Id", "客戶名稱");
            return View();
        }

 

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            客戶聯絡人 客戶聯絡人 = Repo.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(RepoC.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                var db = (客戶資料Entities)Repo.UnitOfWork.Context;
                db.Entry(客戶聯絡人).State = EntityState.Modified;
                Repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(RepoC.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            客戶聯絡人 客戶聯絡人 = Repo.Find(id);
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
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            ////db.客戶聯絡人.Remove(客戶聯絡人);
            //客戶聯絡人.是否已刪除 = true;
            //db.SaveChanges();


            客戶聯絡人 客戶聯絡人 = Repo.Find(id);
            客戶聯絡人.是否已刪除 = true;
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
