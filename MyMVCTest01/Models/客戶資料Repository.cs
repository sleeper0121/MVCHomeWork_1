using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MyMVCTest01.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p => p.是否已刪除 == false);
        }
        public 客戶資料 Find(int? Id)
        {
            return base.All().Where(p => p.Id == Id.Value).FirstOrDefault();
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}