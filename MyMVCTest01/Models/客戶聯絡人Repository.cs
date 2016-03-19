using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MyMVCTest01.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(p=>p.是否已刪除==false);
        }
        public 客戶聯絡人 Find(int? Id)
        {
            return base.All().Where(p => p.Id == Id.Value).FirstOrDefault();
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}